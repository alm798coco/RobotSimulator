using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class clsLoadResourcesList : MonoBehaviour
{
    public string m_writeCsvPath = @"\\192.168.10.46\work\project\★テクノ_20220422_ロボットシミュ\ファイル受け渡し\小島\テスト\EnableRobot\EnableRobot.csv";

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] _robotArray = Resources.LoadAll<GameObject>("");

        List<string> _optionList = new List<string>();

        using (StreamWriter sw = new StreamWriter(m_writeCsvPath, false))
        {
            foreach (GameObject _robo in _robotArray)
            {
                sw.WriteLine(_robo.name);
                _optionList.Add(_robo.name);
            }       
        }

        Dropdown _dd = GameObject.Find("Dropdown").GetComponent<Dropdown>();
        _dd.ClearOptions();
        _dd.AddOptions(_optionList);
    }
}
