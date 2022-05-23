using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;

public class clsControleManager : MonoBehaviour
{
    public string m_watchDirectoryPath = @"\\192.168.10.46\work\project\★テクノ_20220422_ロボットシミュ\ファイル受け渡し\小島\テスト\Controle";
    public string m_watchFileFilter = "*.csv";
    private string m_gameObjeName;
    private List<clsMoveData> m_moveDataList { get; set; } = new List<clsMoveData>();
    private List<clsRotateData> m_rotateDataList { get; set; } = new List<clsRotateData>();

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
            Transform _partsTran = clsSetParent.SearchTransform(_controleData.PartsName, transform);
            Vector3 _moveDest = _partsTran.localPosition + _controleData.MoveAmount;
            Vector3 _rotateDest = _partsTran.localEulerAngles + _controleData.RotateAmount;

            if (m_moveDataList.Any(x => x.PartsName == _controleData.PartsName))
            {
                m_moveDataList.FirstOrDefault(x => x.PartsName == _controleData.PartsName).RobotName = _controleData.RobotName;
                m_moveDataList.FirstOrDefault(x => x.PartsName == _controleData.PartsName).PartsName = _controleData.PartsName;
                m_moveDataList.FirstOrDefault(x => x.PartsName == _controleData.PartsName).MoveDestination = _moveDest;
                m_moveDataList.FirstOrDefault(x => x.PartsName == _controleData.PartsName).Time = _controleData.Time;
            }
            else
            {
                clsMoveData _moveData = new clsMoveData()
                {
                    RobotName = _controleData.RobotName,
                    PartsName = _controleData.PartsName,
                    MoveDestination = _moveDest,
                    Time = _controleData.Time
                };
                m_moveDataList.Add(_moveData);
            }

            if (m_rotateDataList.Any(x => x.PartsName == _controleData.PartsName))
            {
                m_rotateDataList.FirstOrDefault(x => x.PartsName == _controleData.PartsName).RobotName = _controleData.RobotName;
                m_rotateDataList.FirstOrDefault(x => x.PartsName == _controleData.PartsName).PartsName = _controleData.PartsName;
                m_rotateDataList.FirstOrDefault(x => x.PartsName == _controleData.PartsName).RotateDestination = _rotateDest;
                m_rotateDataList.FirstOrDefault(x => x.PartsName == _controleData.PartsName).Time = _controleData.Time;
            }
            else
            {
                clsRotateData _rotateData = new clsRotateData()
                {
                    RobotName = _controleData.RobotName,
                    PartsName = _controleData.PartsName,
                    RotateDestination = _rotateDest,
                    Time = _controleData.Time
                };
                m_rotateDataList.Add(_rotateData);
            }
        }

        for (int i = m_moveDataList.Count - 1; i >= 0; i--)
        {
            Transform _partsTran = clsSetParent.SearchTransform(m_moveDataList[i].PartsName, transform);

            Vector3 _moveVelocity = m_moveDataList[i].MoveVelocity;
            _partsTran.localPosition = Vector3.SmoothDamp(_partsTran.localPosition, m_moveDataList[i].MoveDestination, ref _moveVelocity, m_moveDataList[i].Time);
            m_moveDataList[i].MoveVelocity = _moveVelocity;

            if (m_moveDataList[i].MoveVelocity.normalized == Vector3.zero)
            {
                m_moveDataList.RemoveAt(i);
            }
        }

        for (int i = m_rotateDataList.Count - 1; i >= 0; i--)
        {
            Transform _partsTran = clsSetParent.SearchTransform(m_rotateDataList[i].PartsName, transform);

            Vector3 _rotateVelocity = m_rotateDataList[i].RotateVelocity;
            float xAngle = Mathf.SmoothDampAngle(_partsTran.localEulerAngles.x, m_rotateDataList[i].RotateDestination.x, ref _rotateVelocity.x, m_rotateDataList[i].Time);
            float yAngle = Mathf.SmoothDampAngle(_partsTran.localEulerAngles.y, m_rotateDataList[i].RotateDestination.y, ref _rotateVelocity.y, m_rotateDataList[i].Time);
            float zAngle = Mathf.SmoothDampAngle(_partsTran.localEulerAngles.z, m_rotateDataList[i].RotateDestination.z, ref _rotateVelocity.z, m_rotateDataList[i].Time);
            Vector3 _quate = new Vector3(xAngle, yAngle, zAngle);
            _partsTran.localEulerAngles = _quate;
            m_rotateDataList[i].RotateVelocity = _rotateVelocity;

            if (m_rotateDataList[i].RotateVelocity.normalized == Vector3.zero)
            {
                m_rotateDataList.RemoveAt(i);
            }
        }
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
                RotateAmount = new Vector3(float.Parse(_array[5]), float.Parse(_array[6]), float.Parse(_array[7])),
                Time = float.Parse(_array[8])
            };
            m_controleCollection.TryAdd(_robotData);
        }

        File.Delete(e.FullPath);
    }
}
