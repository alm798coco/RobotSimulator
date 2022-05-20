using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clsCurrentCameraUIMnager : MonoBehaviour
{
    private Text m_text;

    private List<string> m_keyCodeList = new List<string> { "1", "2", "3", "4" };

    void Start()
    {
        m_text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (m_keyCodeList.Contains(Input.inputString))
            {
                m_text.text = Input.inputString;
            }
        }
    }
}
