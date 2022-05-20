using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class clsRobotCreateManager : MonoBehaviour
{
    public string m_watchDirectoryPath = @"\\192.168.10.46\work\project\★テクノ_20220422_ロボットシミュ\ファイル受け渡し\小島\テスト\CSV出力";
    public string m_watchFileFilter = "*.csv";
    public string m_fileBackupDirectoryPath = @"\\192.168.10.46\work\project\★テクノ_20220422_ロボットシミュ\ファイル受け渡し\小島\テスト\CSV退避\";

    public string m_writeCsvFilePath = @"\\192.168.10.46\work\project\★テクノ_20220422_ロボットシミュ\ファイル受け渡し\小島\テスト\CreatedRobot\test.csv";

    private BlockingCollection<clsCreateRobotData> m_createInstCollection { get; set; } = new BlockingCollection<clsCreateRobotData>();

    // Start is called before the first frame update
    void Start()
    {
        clsDirectoryWatch _directoryWatch = new clsDirectoryWatch(m_watchDirectoryPath, m_watchFileFilter, WatcherCreated, clsConst.DirectoryWathMode.Created);
        _directoryWatch.StartWatch();

        if (File.Exists(m_writeCsvFilePath))
        {
            File.Delete(m_writeCsvFilePath);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_createInstCollection.TryTake(out clsCreateRobotData _robotData))
        {
            Create(_robotData.RobotName, _robotData.UnityName, _robotData.PointX, _robotData.PointY, _robotData.PointZ);
        }
    }

    private void Create(string robotName, string unityName, float pointX, float pointY, float pointZ)
    {
        try
        {
            GameObject _obj = Resources.Load<GameObject>(robotName);
            if (_obj == null)
            {
                return;
            }

            GameObject _inst = Instantiate<GameObject>(_obj, new Vector3(pointX, pointY, pointZ), Quaternion.identity);
            _inst.name = unityName;

            // 親子関係作成スクリプトアタッチ
            _inst.AddComponent<clsSetParentsManager>();
            // 関節作成スクリプトアタッチ
            _inst.AddComponent<clsCreateJointManager>();
            // 動作スクリプトアタッチ
            _inst.AddComponent<clsControleManager>();

            List<string> _writeList = new List<string>() { $"{unityName},{robotName}" };
            clsCsvController.WriteCsv(m_writeCsvFilePath, _writeList, true);
        }
        catch (System.Exception)
        {
        }
    }

    private void WatcherCreated(object sender, FileSystemEventArgs e)
    {
        string[] _array = clsCsvController.ReadCsvLineSplitComma(e.FullPath);

        clsCreateRobotData _robotData = new clsCreateRobotData()
        {
            RobotName = _array[0],
            UnityName = _array[1],
            PointX = float.Parse(_array[2]),
            PointY = float.Parse(_array[3]),
            PointZ = float.Parse(_array[4])
        };
        m_createInstCollection.TryAdd(_robotData);
        File.Delete(e.FullPath);
    }
}
