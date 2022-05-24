using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class clsToolManager : MonoBehaviour
{
    public string m_watchDirectoryPath = @"\\192.168.10.46\work\project\★テクノ_20220422_ロボットシミュ\ファイル受け渡し\小島\テスト\Tool";
    public string m_watchFileFilter = "*.csv";
    private BlockingCollection<clsToolData> m_controleCollection { get; set; } = new BlockingCollection<clsToolData>();

    // Start is called before the first frame update
    void Start()
    {
        clsDirectoryWatch _directoryWatch = new clsDirectoryWatch(m_watchDirectoryPath, m_watchFileFilter, WatcherCreated, clsConst.DirectoryWathMode.Created);
        _directoryWatch.StartWatch();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_controleCollection.TryTake(out clsToolData _controleData))
        {
            switch (_controleData.Mode)
            {
                case clsConst.ToolControleMode.Create:
                    Create(_controleData.ToolFileName, _controleData.ToolName, _controleData.Transrate);
                    break;
                case clsConst.ToolControleMode.Edit:
                    Edit(_controleData.ToolName, _controleData.Transrate, _controleData.Rotate);
                    break;
                case clsConst.ToolControleMode.Link:
                    Link(_controleData.ToolName, _controleData.LinkRobotName, _controleData.LinkPartuName);
                    break;
                default:
                    break;
            }
        }
    }

    private void Create(string fileName, string toolName, Vector3 position)
    {
        GameObject _obj = Resources.Load<GameObject>(fileName);
        if (_obj == null)
        {
            return;
        }

        GameObject _inst = Instantiate<GameObject>(_obj, position, Quaternion.identity);
        _inst.name = toolName;
    }

    private void Edit(string toolName, Vector3 transrate, Vector3 rotate)
    {
        GameObject gameObject = GameObject.Find(toolName);
        gameObject.transform.Translate(transrate);
        gameObject.transform.Rotate(rotate);
    }

    private void Link(string toolName, string robotName, string partsName)
    {
        GameObject _gameObject = GameObject.Find(robotName);
        Transform _transform = clsSetParent.SearchTransform(partsName, _gameObject.transform);

        GameObject _toolObject = GameObject.Find(toolName);
        _toolObject.transform.parent = _transform;
    }

    private void WatcherCreated(object sender, FileSystemEventArgs e)
    {
        string _judgeStr = Path.GetFileNameWithoutExtension(e.FullPath);
        int _startInd = _judgeStr.IndexOf('_');
        if (_startInd == -1)
        {
            return;
        }

        clsConst.ToolControleMode _mode;
        _judgeStr = _judgeStr.Remove(_startInd);
        switch (_judgeStr)
        {
            case "Create":
                _mode = clsConst.ToolControleMode.Create;
                break;
            case "Edit":
                _mode = clsConst.ToolControleMode.Edit;
                break;
            case "Link":
                _mode = clsConst.ToolControleMode.Link;
                break;
            default:
                return;
        }

        List<string> _strList = clsCsvController.ReadCsv(e.FullPath);

        clsToolData _toolData = new clsToolData()
        {
            Mode = _mode,
            ToolFileName = _strList[0],
            ToolName = _strList[1],
            Transrate = new Vector3(float.Parse(_strList[2]), float.Parse(_strList[3]), float.Parse(_strList[4])),
            Rotate = new Vector3(float.Parse(_strList[5]), float.Parse(_strList[6]), float.Parse(_strList[7])),
            LinkRobotName = _strList[8],
            LinkPartuName = _strList[9]
        };

        m_controleCollection.Add(_toolData);

        File.Delete(e.FullPath);
    }
}
