using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[RequireComponent(typeof(Camera))]
public class clsCameraController : MonoBehaviour
{
    public string m_key;

    [SerializeField, Range(0.1f, 10f)]
    private float m_wheelSpeed = 2f;

    [SerializeField, Range(0.1f, 10f)]
    private float m_moveSpeed = 0.3f;

    [SerializeField, Range(0.1f, 10f)]
    private float m_rotateSpeed = 0.3f;

    private Vector3 m_preMousePos;

    private bool _moveFlg = false;

    private List<string> m_keyCodeList = new List<string> { "1", "2", "3", "4" };

    private Camera m_camera;

    private void Start()
    {
        m_keyCodeList.Remove(m_key);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (m_key == Input.inputString)
            {
                _moveFlg = true;
                GetComponent<Camera>().cullingMask = -1;
            }
            else if (m_keyCodeList.Contains(Input.inputString))
            {
                _moveFlg = false;

                GetComponent<Camera>().cullingMask = ~(1 << 8);
            }            
        }

        if (!_moveFlg)
        {
            return;
        }

        MouseUpdate();
    }

    private void MouseUpdate()
    {
        if (Input.GetMouseButtonDown(0) ||
           Input.GetMouseButtonDown(1) ||
           Input.GetMouseButtonDown(2))
        {
            m_preMousePos = Input.mousePosition;
        }

        MouseDrag(Input.mousePosition);

        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel != 0.0f)
        {
            MouseWheel(scrollWheel);
        }
    }

    private void MouseWheel(float delta)
    {
        transform.position += transform.forward * delta * m_wheelSpeed;
    }

    private void MouseDrag(Vector3 mousePos)
    {
        Vector3 diff = mousePos - m_preMousePos;

        if (diff.magnitude < Vector3.kEpsilon)
        {
            //return;
        }

        if (Input.GetMouseButton(0))
        {
            transform.Translate(-diff * Time.deltaTime * m_moveSpeed);
        }
        else if (Input.GetMouseButton(1))
        {
            CameraRotate(new Vector2(-diff.y, diff.x) * m_rotateSpeed);
        }

        m_preMousePos = mousePos;
    }

    public void CameraRotate(Vector2 angle)
    {
        transform.RotateAround(transform.position, transform.right, angle.x);
        transform.RotateAround(transform.position, Vector3.up, angle.y);
    }
}