using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clsChangeCamera : MonoBehaviour
{
    public GameObject m_cameraObj1;
    public GameObject m_cameraObj2;
    public GameObject m_cameraObj3;
    public GameObject m_cameraObj4;

    private Camera m_camera1;
    private Camera m_camera2;
    private Camera m_camera3;
    private Camera m_camera4;
    private Rect m_camera1InitRect;
    private Rect m_camera2InitRect;
    private Rect m_camera3InitRect;
    private Rect m_camera4InitRect;

    // Start is called before the first frame update
    void Start()
    {
        m_camera1 = m_cameraObj1.GetComponent<Camera>();
        m_camera2 = m_cameraObj2.GetComponent<Camera>();
        m_camera3 = m_cameraObj3.GetComponent<Camera>();
        m_camera4 = m_cameraObj4.GetComponent<Camera>();

        m_camera1InitRect = m_camera1.rect;
        m_camera2InitRect = m_camera2.rect;
        m_camera3InitRect = m_camera3.rect;
        m_camera4InitRect = m_camera4.rect;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetCamera(m_camera1, true);
            SetCamera(m_camera2, false);
            SetCamera(m_camera3, false);
            SetCamera(m_camera4, false);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetCamera(m_camera1, false);
            SetCamera(m_camera2, true);
            SetCamera(m_camera3, false);
            SetCamera(m_camera4, false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetCamera(m_camera1, false);
            SetCamera(m_camera2, false);
            SetCamera(m_camera3, true);
            SetCamera(m_camera4, false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetCamera(m_camera1, false);
            SetCamera(m_camera2, false);
            SetCamera(m_camera3, false);
            SetCamera(m_camera4, true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetCamera(m_camera1, false);
            SetCamera(m_camera2, false);
            SetCamera(m_camera3, false);
            SetCamera(m_camera4, false);

            m_camera1.rect = new Rect(new Vector2(0, 0), new Vector2(0.5f, 1.0f));
            m_camera2.rect = new Rect(new Vector2(0.5f, 0), new Vector2(0.5f, 1.0f));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SetCamera(m_camera1, false);
            SetCamera(m_camera2, false);
            SetCamera(m_camera3, false);
            SetCamera(m_camera4, false);

            m_camera1.rect = new Rect(new Vector2(0, 0.5f), new Vector2(0.5f, 0.5f));
            m_camera2.rect = new Rect(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
            m_camera3.rect = new Rect(new Vector2(0, 0), new Vector2(1.0f, 0.5f));
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            InitCamera();
        }
    }

    private void SetCamera(Camera camera, bool setOn)
    {
        if (setOn)
        {
            camera.rect = new Rect(new Vector2(0, 0), new Vector2(1.0f, 1.0f));
        }
        else
        {
            camera.rect = new Rect(new Vector2(0, 0), new Vector2(0, 0));
        }
    }    

    private void InitCamera()
    {
        m_camera1.rect = m_camera1InitRect;
        m_camera2.rect = m_camera2InitRect;
        m_camera3.rect = m_camera3InitRect;
        m_camera4.rect = m_camera4InitRect;
    }
}
