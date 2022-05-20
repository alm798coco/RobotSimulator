using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class clsDirectoryWatch
{
    string m_dicPath;
    string m_filetr;
    FileSystemEventHandler m_eventHandle;
    clsConst.DirectoryWathMode m_watchMode;

    public clsDirectoryWatch(string directoryPath, string filter, FileSystemEventHandler eventHandle, clsConst.DirectoryWathMode watchMode)
    {
        m_dicPath = directoryPath;
        m_filetr = filter;
        m_eventHandle = eventHandle;
        m_watchMode = watchMode;
    }

    public void StartWatch()
    {
        FileSystemWatcher _watcher = new FileSystemWatcher(m_dicPath, m_filetr);

        _watcher.NotifyFilter =
                (NotifyFilters.Attributes
                | NotifyFilters.LastAccess
                | NotifyFilters.LastWrite
                | NotifyFilters.FileName
                | NotifyFilters.DirectoryName);

        _watcher.IncludeSubdirectories = false;

        switch (m_watchMode)
        {
            case clsConst.DirectoryWathMode.Created:
                _watcher.Created += new FileSystemEventHandler(m_eventHandle);
                break;
            case clsConst.DirectoryWathMode.Changed:
                _watcher.Changed += new FileSystemEventHandler(m_eventHandle);
                break;
            case clsConst.DirectoryWathMode.Deleted:
                _watcher.Deleted += new FileSystemEventHandler(m_eventHandle);
                break;
            default:
                break;
        }
        

        _watcher.EnableRaisingEvents = true;
    }
}
