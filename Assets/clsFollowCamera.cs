using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clsFollowCamera : MonoBehaviour
{
    public GameObject m_camera;
    public string m_key;
    private bool m_moveFlg = false;
    private List<string> m_keyCodeList = new List<string> { "1", "2", "3", "4" };
    private float m_forward = 30.0f;
    private float m_up = 5.0f;
    private float m_Right = 11.0f;
    private Vector3 m_addPosFront;
    private Vector3 m_addPosUp;
    private Vector3 m_addPosRight;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        m_keyCodeList.Remove(m_key);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (m_key == Input.inputString)
            {
                m_moveFlg = true;
            }
            else if (m_keyCodeList.Contains(Input.inputString))
            {
                m_moveFlg = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (m_moveFlg)
        {            
            m_addPosFront = new Vector3(m_camera.transform.forward.x * m_forward
                    , m_camera.transform.forward.y * m_forward
                    , m_camera.transform.forward.z * m_forward);
            m_addPosUp = new Vector3(m_camera.transform.up.x * m_up
                    , m_camera.transform.up.y * m_up
                    , m_camera.transform.up.z * m_up);
            m_addPosRight = new Vector3(m_camera.transform.right.x * m_Right
                    , m_camera.transform.right.y * m_Right
                    , m_camera.transform.right.z * m_Right);

            transform.position = m_camera.transform.position + m_addPosFront + m_addPosUp + m_addPosRight;

            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else
        {
            transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }
}
