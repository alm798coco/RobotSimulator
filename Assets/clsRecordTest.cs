using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UTJ.FrameCapturer;

public class clsRecordTest : MonoBehaviour
{
    public GameObject m_camera1;

    // Start is called before the first frame update
    void Start()
    {
       m_camera1.GetComponent<MovieRecorder>().BeginRecording();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            m_camera1.GetComponent<MovieRecorder>().EndRecording();
        }
    }
}
