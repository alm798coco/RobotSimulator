using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clsFollowCamera : MonoBehaviour
{
    private GameObject m_camera;
    private List<string> m_keyCodeList = new List<string> { "1", "2", "3", "4" };
    private Vector3 _addPosFront;
    private Vector3 _addPosTop;
    private Vector3 _addPosRight;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (m_keyCodeList.Contains(Input.inputString))
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                switch (Input.inputString)
                {
                    case "1":
                        m_camera = GameObject.Find("Camera1");
                        break;
                    case "2":
                        m_camera = GameObject.Find("Camera2");
                        break;
                    case "3":
                        m_camera = GameObject.Find("Camera3");
                        break;
                    case "4":
                        m_camera = GameObject.Find("Camera4");
                        break;
                    default:
                        break;
                }

                //_addPos = m_camera.transform.position;
            }
        }

        if (m_camera != null)
        {

            transform.position = m_camera.transform.forward;
            transform.rotation = m_camera.transform.rotation;
        }
    }
}
