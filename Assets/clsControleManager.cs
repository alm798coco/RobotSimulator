using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class clsControleManager : MonoBehaviour
{
    public string m_watchDirectoryPath = @"\\192.168.10.46\work\project\★テクノ_20220422_ロボットシミュ\ファイル受け渡し\小島\テスト\Controle";
    public string m_watchFileFilter = "*.csv";
    private string m_gameObjeName;

    private BlockingCollection<clsControleData> m_controleCollection { get; set; } = new BlockingCollection<clsControleData>();

    // Start is called before the first frame update
    void Start()
    {
        clsDirectoryWatch _directoryWatch = new clsDirectoryWatch(m_watchDirectoryPath, m_watchFileFilter, WatcherCreated, clsConst.DirectoryWathMode.Created);
        _directoryWatch.StartWatch();

        m_gameObjeName = this.gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_controleCollection.TryTake(out clsControleData _controleData))
        {
            Controle(_controleData.RobotName, _controleData.PartsName, _controleData.MoveAmount, _controleData.RotateAmount);
        }
    }

    private void Controle(string robotName, string partsName, Vector3 moveAmount, Quaternion rotateAmount)
    {
        float speed = 0.5f;
        clsControleRobotParts.Move(robotName, partsName, moveAmount);
        clsControleRobotParts.RoatateOri(robotName, partsName, rotateAmount);
    }

    private void WatcherCreated(object sender, FileSystemEventArgs e)
    {
        string _judgeStr = Path.GetFileNameWithoutExtension(e.FullPath);
        int _startInd = _judgeStr.LastIndexOf('_');
        if (_startInd == -1)
        {
            return;
        }
        _judgeStr = _judgeStr.Remove(_startInd);
        if (m_gameObjeName != _judgeStr)
        {
            return;
        }

        List<string> _strList = clsCsvController.ReadCsv(e.FullPath);

        foreach (string _str in _strList)
        {
            string[] _array = _str.Split(',');
            clsControleData _robotData = new clsControleData()
            {
                RobotName = _array[0],
                PartsName = _array[1],
                MoveAmount = new Vector3(float.Parse(_array[2]), float.Parse(_array[3]), float.Parse(_array[4])),
                RotateAmount = Quaternion.Euler(float.Parse(_array[5]), float.Parse(_array[6]), float.Parse(_array[7]))
            };
            m_controleCollection.TryAdd(_robotData);
        }

        File.Delete(e.FullPath);
    }
}
