using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra.Single;


public class clsGyakuUndoTest : MonoBehaviour
{
    public Transform axis1_tf, axis2_tf, axis3_tf;      //各軸のTransform(逆運動学の結果を出力してロボットを動かす)
    public Transform hand_transform;                    //アーム先端のTransform(逆運動学の結果確認用)
    public Vector3 pw4_target;                          //アーム先端の目標位置（Inspectorビューから入力）

    //---リンクのパラメータ---//
    private Vector3 pw1, pw2, pw3, pw4;    //ワールド座標系での位置
    private Quaternion Rw1, Rw2, Rw3;      //ワールド座標系での姿勢
    private Vector3 p1, p2, p3, p4;        //ローカル座標系での位置（親リンク相対）
    private Quaternion R1, R2, R3;         //ローカル座標系での姿勢（親リンク相対）
    private float angle1, angle2, angle3;  //関節角度
    private Vector3 a1, a2, a3;            //関節軸ベクトル（親リンク相対）

    private float lambda = 0.1f;    //逆運動学の収束を調整する係数（0～1で設定）

    //UI
    //public UnityEngine.UI.Text transform_label;

    void Start()
    {
        //固定値のパラメータを設定
        //p1 = new Vector3(0f, 1f, 0f);   //第1軸のローカル座標（親リンク相対）
        //p2 = new Vector3(0f, 1f, 0f);   //第2軸のローカル座標（親リンク相対）
        //p3 = new Vector3(0f, 3f, 0f);   //第3軸のローカル座標（親リンク相対）
        //p4 = new Vector3(0f, 3f, 0f);   //アーム先端のローカル座標（親リンク相対）
        p1 = axis1_tf.localPosition; //第1軸のローカル座標（親リンク相対）
        p2 = axis2_tf.localPosition;   //第2軸のローカル座標（親リンク相対）
        p3 = axis3_tf.localPosition;   //第3軸のローカル座標（親リンク相対）
        p4 = hand_transform.localPosition;   //アーム先端のローカル座標（親リンク相対）
        a1 = Vector3.up;                //第1軸の関節軸ベクトル（親リンク相対）
        a2 = Vector3.forward;             //第2軸の関節軸ベクトル（親リンク相対）
        a3 = Vector3.forward;             //第3軸の関節軸ベクトル（親リンク相対）

        //関節角度の初期値を設定
        //※0度だとヤコビアンの逆行列が存在しないため少し角度を付けておく
        angle1 = 1f;                   //第1軸の回転角度
        angle2 = 1f;                   //第2軸の回転角度
        angle3 = 1f;                   //第3軸の回転角度
    }

    void FixedUpdate()
    {

        for (int i = 0; i < 50; i++)
        {
            //// 順運動学 ////
            pw1 = p1;
            Rw1 = Quaternion.AngleAxis(angle1, a1);

            pw2 = Rw1 * p2 + pw1;
            R2 = Quaternion.AngleAxis(angle2, a2);
            Rw2 = Rw1 * R2;

            pw3 = Rw2 * p3 + pw2;
            R3 = Quaternion.AngleAxis(angle3, a3);
            Rw3 = Rw2 * R3;

            pw4 = Rw3 * p4 + pw3;


            //// ヤコビアンを作成 ////
            Vector3 j1 = Vector3.Cross(Rw1 * a1, pw4 - pw1);
            Vector3 j2 = Vector3.Cross(Rw2 * a2, pw4 - pw2);
            Vector3 j3 = Vector3.Cross(Rw3 * a3, pw4 - pw3);

            var J = DenseMatrix.OfArray(new float[,]     // ※Math.Netを使用
            {
                { j1.x, j2.x, j3.x },
                { j1.y, j2.y, j3.y },
                { j1.z, j2.z, j3.z }
            });

            //// 逆運動学 ////

            //アーム先端の目標位置からの誤差を計算
            Vector3 err = pw4_target - pw4;
            float err_norm = err.sqrMagnitude;

            //err_norm < 1E-5なら計算終了
            if (err_norm < 1E-5) break;

            // ※Jの逆行列と掛け算するためにerrをMath.Netの行列に入れなおす
            var err2 = DenseMatrix.OfArray(new float[,]
            {
                { err.x },
                { err.y },
                { err.z }
            });

            var d_angle = lambda * J.Inverse() * err2;

            //逆運動学で得た角度修正量を各軸に反映する(d_angleはラジアンなので度に変換)
            angle1 += d_angle[0, 0] * 180f / 3.14159f;
            angle2 += d_angle[1, 0] * 180f / 3.14159f;
            angle3 += d_angle[2, 0] * 180f / 3.14159f;
        }

        //逆運動学で計算した関節角度をロボットに反映
        Quaternion axis1_rot = Quaternion.AngleAxis(angle1, Vector3.up);
        Quaternion axis2_rot = Quaternion.AngleAxis(angle2, Vector3.forward);
        Quaternion axis3_rot = Quaternion.AngleAxis(angle3, Vector3.forward);

        axis1_tf.localRotation = axis1_rot;
        axis2_tf.localRotation = axis2_rot;
        axis3_tf.localRotation = axis3_rot;
    }
}
