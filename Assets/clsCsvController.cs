using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class clsCsvController
{
    public static List<string> ReadCsv(string csvFilePath)
    {
        List<string> _strList = new List<string>();
        using (StreamReader _sr = new StreamReader(csvFilePath))
        {
            while (!_sr.EndOfStream)
            {
                _strList.Add(_sr.ReadLine());
            }                
        }

        return _strList;
    }

    public static string ReadCsvLine(string csvFilePath)
    {
        string _str = string.Empty;
        using (StreamReader _sr = new StreamReader(csvFilePath))
        {
            _str = _sr.ReadLine();
        }

        return _str;
    }

    public static List<string> ReadCsvSplitComma(string csvFilePath)
    {
        List<string> _strList = new List<string>();
        using (StreamReader _sr = new StreamReader(csvFilePath))
        {
            string _line = _sr.ReadLine();
            string[] _array = _line.Split(',');

            _strList.AddRange(_array);
        }

        return _strList;
    }

    public static string[] ReadCsvLineSplitComma(string csvFilePath)
    {
        string[] _array = null;
        using (StreamReader _sr = new StreamReader(csvFilePath))
        {
            string _line = _sr.ReadLine();
            _array = _line.Split(',');
        }

        return _array;
    }    

    public static void WriteCsv(string csvFilePath, List<string> writeList, bool append)
    {
        using (StreamWriter sw = new StreamWriter(csvFilePath, append))
        {
            foreach (string _str in writeList)
            {
                sw.WriteLine(_str);
            }
        }
    }
}
