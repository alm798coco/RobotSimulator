using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;  // for DllImport
using UnityEngine;
using UnityEngine.UI;

public class clsRt64Controller : MonoBehaviour
{
    public static int hPlm = 0;
    public static short Commen = 0;
    public const short TaskSel = 0;
    public const int PAXMAX = 16;                               // 物理軸最大数
    public const int LAXMAX = 9;                                // 論理軸最大数
    public const int TASKMAX = 8;                               // タスク最大数
    static Rt64ecdata.ASSAXISTBL axtbl = Rt64ecdata.ASSAXISTBL.Init();        // 軸割り当て情報(物理軸 → 論理軸)
    public static sbyte[,] l2p = new sbyte[TASKMAX, LAXMAX];    // 軸割り当て情報(論理軸 → 物理軸)
    public static long p_axis_en;                               // 有効物理軸フラグ(全タスク)
    public static long[] p_axis_ent = new long[TASKMAX];        // 有効物理軸フラグ(タスク毎)
    public static int[] l_axis_en = new int[TASKMAX];           // 有効論理軸フラグ
    private GameObject[] CrPosAry = new GameObject[9];
    public GameObject m_Robot;
    public GameObject m_Kata;

    // Start is called before the first frame update
    void Start()
    {
        m_Robot = GameObject.Find("Robot");
        m_Kata = clsSetParent.SearchTransform("AxisV6S048", m_Robot.transform).gameObject;

        Rt64eccom64api.SXDEF sdf = Rt64eccom64api.SXDEF.Init();
        int sts = 0;
        int[] AxMltplr = new int[9];
        short dot_exp;
        int dotn;
        int cnt;

        Commen = 0;
        sdf.nSize = Marshal.SizeOf(sdf);
        sdf.fShare = 1;                             // 共有フラグ
        sdf.fLogging = 0;                           // 通信ロギングフラグ
        sdf.pLogFile = "";							// 通信ロギングファイル名

        // イーサネット通信 の場合
        sdf.fComType = Rt64eccom64api.COMETHERNET;// 通信種別フラグ
        sdf.nIpAddr[0] = 192;                   // IPアドレス
        sdf.nIpAddr[1] = 168;
        sdf.nIpAddr[2] = 10;
        sdf.nIpAddr[3] = 10;

        sts = Rt64eccom64api.InitCommProc(ref sdf, ref hPlm);
        if (sts == Syncdef.E_OK)
        {
            Commen = 1;
        }
        dot_exp = 3;
        dotn = (int)Math.Pow(10, dot_exp);
        for (cnt = AxMltplr.GetLowerBound(0); cnt <= AxMltplr.GetUpperBound(0); cnt++)
        {
            AxMltplr[cnt] = dotn;
        }

        // テクノコード変換ライブラリ初期化
        Rt64ectcd64api.TcdCircleSet(10);
        Rt64ectcd64api.TcdFeedUnitSet(1, Rt64ectcd64api.SEC_UNIT);
        Rt64ectcd64api.TcdSTSelect(4);
        Rt64ectcd64api.TcdPaccTimeSet(500);
        Rt64ectcd64api.TcdCircleMode(1);
        Rt64ectcd64api.TcdDiametralAxis(0);
        Rt64ectcd64api.TcdSetMultiplier(AxMltplr);
        Rt64ectcd64api.TcdSetZXPlane(0);

        // Ｇコード変換ライブラリ初期化
        Rt64ecgcd64api.GcdCircleSet(10);
        Rt64ecgcd64api.GcdFeedUnitSet(1, Rt64ecgcd64api.SEC_UNIT);
        Rt64ecgcd64api.GcdSTSelect(4);
        Rt64ecgcd64api.GcdPaccTimeSet(500);
        Rt64ecgcd64api.GcdCircleMode(1);
        Rt64ecgcd64api.GcdDiametralAxis(0);
        Rt64ecgcd64api.GcdSetMultiplier(AxMltplr);
        Rt64ecgcd64api.GcdSetZXPlane(0);

        for (int i = 0; i < CrPosAry.Length; i++)
        {
            CrPosAry[i] = GameObject.Find("Text" + i);
        }

        if (Commen == 1)
        {
            sts = ReceiveAssAxisTbl();
            if (Syncdef.E_OK != sts)
            {
                Commen = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        int sts;
        int sz = 0;
        //Rt64ecdata.STATUS stat = Rt64ecdata.STATUS.Init();
        Rt64ecdata.RBTPOSINFO rpos = Rt64ecdata.RBTPOSINFO.Init();

        if (1 == Commen)
        {
            //sts = Rt64eccom64api.ReceiveData(hPlm, Syncdef.DAT_STATUS, 0, 0, ref sz, ref stat);
            sts = Rt64eccom64api.ReceiveData(hPlm, Syncdef.DAT_RBTPOSINF, 0, 0, ref sz, ref rpos);
            if (sts != Syncdef.E_OK)
            {
                Commen = 0;
            }
            else
            {
                int cnt;
                for (cnt = CrPosAry.GetLowerBound(0); cnt <= CrPosAry.GetUpperBound(0); cnt++)
                {
                    if (0 != (l_axis_en[TaskSel] & (1 << cnt)))
                    {
                        int axln;
                        axln = l2p[TaskSel, cnt];
                        //CrPosAry[cnt].GetComponent<Text>().text = stat.ax[axln].ComReg.ToString();
                        CrPosAry[cnt].GetComponent<Text>().text = rpos.rbtcr[axln].ToString();

                        if (axln == 2)
                        {
                            m_Kata.transform.eulerAngles = new Vector3(0.0f, 0.0f, rpos.rbtcr[axln] / 1000.0f);
                        }
                    }
                    else
                        CrPosAry[cnt].GetComponent<Text>().text = "0";
                }                
            }
        }
    }

    public int ReceiveAssAxisTbl()
    {
        int sts = Syncdef.E_ERR;
        int cnt;
        // 各種データ初期化
        p_axis_en = 0;
        for (cnt = l_axis_en.GetLowerBound(0); cnt <= l_axis_en.GetUpperBound(0); cnt++)
        {
            int cn2;
            l_axis_en[cnt] = 0;
            p_axis_ent[cnt] = 0;

            for (cn2 = l2p.GetLowerBound(0); cn2 <= l2p.GetUpperBound(0); cn2++)
                l2p[cnt, cn2] = -1;
        }

        // 軸割り当て情報取得 → 内部データに反映
        int size = 0;
        Rt64ecdata.ASSAXISTBL tbl = Rt64ecdata.ASSAXISTBL.Init();
        sts = Rt64eccom64api.ReceiveData(hPlm, Syncdef.DAT_ASSAXISTBL, 0, 0, ref size, ref tbl);
        if (Syncdef.E_OK != sts)
        {
            // CommErrMessageDisp (sts)
        }
        else
        {
            long axpb;
            axpb = 1;
            p_axis_en = tbl.axis_en;     // 有効物理軸フラグ設定
            for (cnt = tbl.ass_axis.GetLowerBound(0); cnt <= tbl.ass_axis.GetUpperBound(0); cnt++)
            {
                byte axpn;
                axpn = (byte)cnt;
                if (0 != (p_axis_en & axpb))
                {
                    int task;
                    int axln;
                    int axlb;
                    task = tbl.ass_axis[axpn].tsk;
                    axln = tbl.ass_axis[axpn].axn;
                    axlb = 1 << (axln);
                    axtbl.ass_axis[axpn].tsk = tbl.ass_axis[axpn].tsk;
                    axtbl.ass_axis[axpn].axn = tbl.ass_axis[axpn].axn;
                    l2p[task, axln] = (sbyte)axpn;
                    l_axis_en[task] = l_axis_en[task] + axlb;
                    p_axis_ent[task] = p_axis_ent[task] + axpb;
                }
                else
                {
                    axtbl.ass_axis[cnt].tsk = -1;
                    axtbl.ass_axis[cnt].axn = -1;
                }
                axpb = axpb << 1;
            }
        }
        return (sts);
    }
}
