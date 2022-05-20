using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class clsSetParentsManager : MonoBehaviour
{
    public string m_watchDirectoryPath = @"\\192.168.10.46\work\project\★テクノ_20220422_ロボットシミュ\ファイル受け渡し\小島\テスト\Parent";
    public string m_watchFileFilter = "*.csv";
    private string m_gameObjeName;

    private BlockingCollection<clsSetParentData> m_setParentsCollection { get; set; } = new BlockingCollection<clsSetParentData>();

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
        if (m_setParentsCollection.TryTake(out clsSetParentData _parentData))
        {
            bool _result = clsSetParent.Set(_parentData.SourceObjName, _parentData.ParentName, _parentData.ChildName);
        }
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
            clsSetParentData _robotData = new clsSetParentData()
            {
                SourceObjName = m_gameObjeName,
                ParentName = _array[0],
                ChildName = _array[1]
            };
            m_setParentsCollection.TryAdd(_robotData);
        }       
    }
}
