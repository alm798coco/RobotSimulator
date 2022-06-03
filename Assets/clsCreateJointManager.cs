using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class clsCreateJointManager : MonoBehaviour
{
    public string m_watchDirectoryPath = @"\\192.168.10.46\work\project\★テクノ_20220422_ロボットシミュ\ファイル受け渡し\小島\テスト\CSV出力_関節";
    public string m_watchFileFilter = "*.csv";
    public string m_writeCsvFilePath = $@"\\192.168.10.46\work\project\★テクノ_20220422_ロボットシミュ\ファイル受け渡し\小島\テスト\Joint";
    private string m_gameObjeName;

    private BlockingCollection<clsCreateJointData> m_createJointCollection { get; set; } = new BlockingCollection<clsCreateJointData>();

    // Start is called before the first frame update
    void Start()
    {
        clsDirectoryWatch _directoryWatch = new clsDirectoryWatch(m_watchDirectoryPath, m_watchFileFilter, WatcherCreated, clsConst.DirectoryWathMode.Created);
        _directoryWatch.StartWatch();

        m_gameObjeName = this.gameObject.name;

        if (File.Exists($@"{m_writeCsvFilePath}\{m_gameObjeName}.csv"))
        {
            File.Delete($@"{m_writeCsvFilePath}\{m_gameObjeName}.csv");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_createJointCollection.TryTake(out clsCreateJointData _jointData))
        {
            CreateJoint(_jointData.JointPartsName, _jointData.NewName, _jointData.PointX, _jointData.PointY, _jointData.PointZ, _jointData.EnablePoint);
        }
    }

    private void CreateJoint(string jointPartsName, string newPartsName,float pointX, float pointY, float pointZ, bool enablePoint)
    {        
        Transform _joitPart = clsSetParent.SearchTransform(jointPartsName, this.gameObject.transform);
        Rigidbody _joinRigid = _joitPart.GetComponent<Rigidbody>();

        Vector3 _vector3;
        if (enablePoint)
        {
             _vector3 = new Vector3(pointX, pointY, pointZ);
        }
        else
        {
            //_vector3 = _joitPart.position;
            _vector3 = _joinRigid.worldCenterOfMass;
        }

        Quaternion _quaternion = _joitPart.rotation;

        clsCreateEmptyObj.Create(newPartsName, _vector3, _quaternion, true, m_gameObjeName);
        clsSetParent.Set(m_gameObjeName, _joitPart.parent.gameObject.name, newPartsName);
        clsSetParent.Set(m_gameObjeName, newPartsName, jointPartsName);

        Transform _partsTran = clsSetParent.SearchTransform(newPartsName, this.gameObject.transform);
        _partsTran.gameObject.AddComponent<clsStepControle>();

        List<string> _writeList = new List<string>() { $"{newPartsName},{jointPartsName}" };
        clsCsvController.WriteCsv($@"{m_writeCsvFilePath}\{m_gameObjeName}.csv", _writeList, true);
    }

    private void WatcherCreated(object sender, FileSystemEventArgs e)
    {
        if (m_gameObjeName != Path.GetFileNameWithoutExtension(e.FullPath))
        {
            return;
        }

        List<string> _strList = clsCsvController.ReadCsv(e.FullPath);

        foreach (string _str in _strList)
        {
            string[] _array = _str.Split(',');
            clsCreateJointData _robotData = new clsCreateJointData()
            {
                JointPartsName = _array[0],
                NewName = _array[1],
                PointX = float.Parse(_array[2]),
                PointY = float.Parse(_array[3]),
                PointZ = float.Parse(_array[4]),
                EnablePoint = bool.Parse(_array[5])
            };
            m_createJointCollection.TryAdd(_robotData);
        }

        File.Delete(e.FullPath);
    }
}
