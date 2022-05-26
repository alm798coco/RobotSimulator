using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[RequireComponent(typeof(Camera))]
public class clsCameraController : MonoBehaviour
{
    public string m_key = "1";

    [SerializeField, Range(0.1f, 10f)]
    private float m_wheelSpeed = 2f; // �z�C�[�� �g�k���x

    [SerializeField, Range(0.1f, 10f)]
    private float m_moveSpeed = 0.3f; // ���N���b�N�h���b�O �ړ����x

    [SerializeField, Range(0.1f, 10f)]
    private float m_rotateSpeed = 0.3f; // �E�N���b�N�h���b�O ��]���x

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
        // ���炩���N���b�N�������_�ł̃}�E�X�J�n�ʒu
        if (Input.GetMouseButtonDown(0) ||
           Input.GetMouseButtonDown(1) ||
           Input.GetMouseButtonDown(2))
        {
            m_preMousePos = Input.mousePosition;
        }

        // �����_�ł̃}�E�X�ʒu������
        MouseDrag(Input.mousePosition);

        // �z�C�[�����쎞�̊g�k
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel != 0.0f)
        {
            MouseWheel(scrollWheel);
        }
    }

    private void MouseWheel(float delta)
    {
        // �g�k Z���W�Ƀ}�E�X�z�C�[���Ɗ��x�𔽉f
        transform.position += transform.forward * delta * m_wheelSpeed;
    }

    private void MouseDrag(Vector3 mousePos)
    {
        // �}�E�X�J�n�ʒu�ƌ����_�ł̈ʒu�̍�
        Vector3 diff = mousePos - m_preMousePos;

        // �����L���Ȓl�łȂ��ꍇ
        // ���̃x�N�g����Vector3�̌���Ȃ�0�ɋ߂����̒l��菬�����ꍇ
        if (diff.magnitude < Vector3.kEpsilon)
        {
            //return;
        }

        if (Input.GetMouseButton(0))
        {
            // ���N���b�N�h���b�O�ňړ�

            // ��(���F���])�ɒ��O�ƍ��̃t���[���Ԃ̌o�ߎ��ԂƊ��x�𔽉f
            // Translate�͌��݂̈ʒu���瑊�ΓI�Ȉʒu�ֈړ�����(���݂̈ʒu����������ړ�����)
            transform.Translate(-diff * Time.deltaTime * m_moveSpeed);
        }
        else if (Input.GetMouseButton(1))
        {
            // �E�N���b�N�h���b�O�ŉ�]

            // X��Y���ŉ�]���̈ʒu��XY�t�ɂȂ邽�ߓ���ւ���
            CameraRotate(new Vector2(-diff.y, diff.x) * m_rotateSpeed);
        }

        // �}�E�X�J�n�ʒu���X�V
        m_preMousePos = mousePos;
    }

    public void CameraRotate(Vector2 angle)
    {
        // �J�����㉺��]
        // �J�������g�𒆐S��X����x�x��]
        transform.RotateAround(transform.position, transform.right, angle.x);

        // �J�������E��]
        // �J�������g�𒆐S��Y����y�x��]
        transform.RotateAround(transform.position, Vector3.up, angle.y);
    }
}