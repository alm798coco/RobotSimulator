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
    private float m_wheelSpeed = 2f; // ホイール 拡縮感度

    [SerializeField, Range(0.1f, 10f)]
    private float m_moveSpeed = 0.3f; // 左クリックドラッグ 移動感度

    [SerializeField, Range(0.1f, 10f)]
    private float m_rotateSpeed = 0.3f; // 右クリックドラッグ 回転感度

    private Vector3 m_preMousePos;

    private bool _moveFlg = false;

    private List<string> m_keyCodeList = new List<string> { "1", "2", "3", "4" };

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
            }
            else if (m_keyCodeList.Contains(Input.inputString))
            {
                _moveFlg = false;
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
        // 何らかをクリックした時点でのマウス開始位置
        if (Input.GetMouseButtonDown(0) ||
           Input.GetMouseButtonDown(1) ||
           Input.GetMouseButtonDown(2))
        {
            m_preMousePos = Input.mousePosition;
        }

        // 現時点でのマウス位置を引数
        MouseDrag(Input.mousePosition);

        // ホイール操作時の拡縮
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel != 0.0f)
        {
            MouseWheel(scrollWheel);
        }
    }

    private void MouseWheel(float delta)
    {
        // 拡縮 Z座標にマウスホイールと感度を反映
        transform.position += transform.forward * delta * m_wheelSpeed;
    }

    private void MouseDrag(Vector3 mousePos)
    {
        // マウス開始位置と現時点での位置の差
        Vector3 diff = mousePos - m_preMousePos;

        // 差が有効な値でない場合
        // 差のベクトルがVector3の限りなく0に近い正の値より小さい場合
        if (diff.magnitude < Vector3.kEpsilon)
        {
            //return;
        }

        if (Input.GetMouseButton(0))
        {
            // 左クリックドラッグで移動

            // 差(負：反転)に直前と今のフレーム間の経過時間と感度を反映
            // Translateは現在の位置から相対的な位置へ移動する(現在の位置から引数分移動する)
            transform.Translate(-diff * Time.deltaTime * m_moveSpeed);
        }
        else if (Input.GetMouseButton(1))
        {
            // 右クリックドラッグで回転

            // X軸Y軸で回転時の位置がXY逆になるため入れ替える
            CameraRotate(new Vector2(-diff.y, diff.x) * m_rotateSpeed);
        }

        // マウス開始位置を更新
        m_preMousePos = mousePos;
    }

    public void CameraRotate(Vector2 angle)
    {
        // カメラ上下回転
        // カメラ自身を中心にX軸でx度回転
        transform.RotateAround(transform.position, transform.right, angle.x);

        // カメラ左右回転
        // カメラ自身を中心にY軸でy度回転
        transform.RotateAround(transform.position, Vector3.up, angle.y);
    }
}