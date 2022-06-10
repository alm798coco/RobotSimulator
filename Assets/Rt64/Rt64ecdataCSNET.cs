using System;
using System.Runtime.InteropServices;

class Rt64ecdata
{
	// ------------------------------------------------------------------------
	//		サーボパラメータデータ構造体（９０８０ﾊﾞｲﾄ固定長）
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
	public struct AXIS_PARAMETER	// 各軸独立パラメータ構造体（１００ﾊﾞｲﾄ固定長）
	{
		public int InPos;					// ＩＮＰＯＳ量 [pls]
		public int ErMax;					// 偏差上限値 [pls]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
		public sbyte[]	Reserved0;
		public int Ka;						// 補間時定数 [msec]
		public int SKa;						// Ｓ字補間時定数 [msec]
		public int Dx;						// ＰＴＰ時定数 [msec]
		public int PtpFeed;					// ＰＴＰ速度 [pls/sec]
		public int JogFeed;					// ＪＯＧ送り速度[pls/sec]
		public int SoftLimP;				// ソフトリミット＋側 [pls]
		public int SoftLimM;				// ソフトリミット－側 [pls]
		public int OrgDir;					// 原点復帰方向
		public int OrgOfs;					// 原点距離 [pls]
		public int OrgPos;					// 原点復帰逃げ位置 (未使用) [pls]
		public int OrgFeed;					// 原点復帰早送り速度 [pls/sec]
		public int AprFeed;					// 原点復帰アプローチ速度 [pls/sec]
		public int SrchFeed;				// 原点復帰最終サーチ速度 [pls/sec]
		public int OrgPri;					// 原点復帰順位
		public int Homepos;					// ﾎｰﾑﾎﾟｼﾞｼｮﾝ位置 [pls]
		public int Homepri;					// ﾎｰﾑﾎﾟｼﾞｼｮﾝ順位
		public int BackL;					// バックラッシュ補正量 [pls]
		public int Revise;					// 形状補正係数
		public int OrgCsetOfs;				// 原点復帰時論理座標
		public int handle_max;				// ｼﾞｮｲｽﾃｨｯｸ/ﾊﾝﾄﾞﾙ最大送り速度
		public int handle_ka;				// ｼﾞｮｲｽﾃｨｯｸ/ﾊﾝﾄﾞﾙ加減速時定数
		public int HomingMethod;			// Homing Method
		public int TangFeedMax;				// 接線制御軸上限速度
		public int TangPosOfs;				// 接線制御軸基準位置オフセット
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]
		public sbyte[]	Reserved1;
		public static AXIS_PARAMETER Init(){
			AXIS_PARAMETER tmp = new AXIS_PARAMETER();
			tmp.Reserved0 = new sbyte[4];
			tmp.Reserved1 = new sbyte[32];
			return tmp;
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
	public struct	ADD_PARAMETER			// 特殊パラメータ共用体（１２０バイト）
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=120)]
		public sbyte[] Reserved;
		public static ADD_PARAMETER Init(){
			ADD_PARAMETER tmp = new ADD_PARAMETER();
			tmp.Reserved = new sbyte[120];
			return tmp;
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
	public struct	PARAMETER_DATA	// （９０８０バイト）
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=64)]
		public AXIS_PARAMETER[]	AxisParam;
		public ADD_PARAMETER AddParam;		// 特殊パラメータ未定義
		public static PARAMETER_DATA Init(){
			PARAMETER_DATA tmp = new PARAMETER_DATA();
			tmp.AxisParam = new AXIS_PARAMETER[64];
			for(int cnt = tmp.AxisParam.GetLowerBound(0); cnt <= tmp.AxisParam.GetUpperBound(0); cnt++)
				tmp.AxisParam[cnt] = Rt64ecdata.AXIS_PARAMETER.Init();
			tmp.AddParam = Rt64ecdata.ADD_PARAMETER.Init();
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	// オプションパラメータデータ構造体（５１２ﾊﾞｲﾄ固定長）
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
	public struct	OPTIONPRM_DATA
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=512)]
		public sbyte[] Reserved;			// 未使用
		public static OPTIONPRM_DATA Init(){
			OPTIONPRM_DATA tmp = new OPTIONPRM_DATA();
			tmp.Reserved = new sbyte[512];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	// アンサーステータス情報構造体（２７６８ﾊﾞｲﾄ）
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
	public struct	MCSTATUS				// 全体情報構造体
	{
		public int Status;					// 全体ステータス
		public int Alarm;					// 全体アラーム
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public sbyte[] Reserved; // 未使用
		public static MCSTATUS Init(){
			MCSTATUS tmp = new MCSTATUS();
			tmp.Reserved = new sbyte[8];
			return tmp;
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
	public struct	AXSTATUS	// 各軸情報構造体
	{
		public int AxStatus;				// 軸ステータス
		public int AxAlarm;					// 軸アラーム
		public int ComReg;					// 指令位置
		public int PosReg;					// 機械位置
		public int ErrReg;					// 偏差量
		public int BlockSeg;				// 最新ブロック払い出し量
		public int AbsReg;					// 絶対位置
		public int Trq;						// トルク
		public int ModReg;					// 残移動量
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=12)]
		public sbyte[] Reserved;			// 未使用
		public static AXSTATUS Init(){
			AXSTATUS tmp = new AXSTATUS();
			tmp.Reserved = new sbyte[12];
			return tmp;
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
	public struct	TASKSTATUS				// タスク情報構造体
	{
		public int TaskStatus;				// タスクステータス
		public int TaskAlarm;				// タスクアラーム
		public short Override;				// 送りオーバーライド設定
		public short COverride;				// 補間オーバーライド設定
		public short SOverride;				// 主軸オーバーライド設定
		public short ProgramNo;				// 選択・実行プログラム番号
		public int StepNo;					// 実行ステップ番号
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=12)]
		public sbyte[] Reserved;			// 未使用
		public static TASKSTATUS Init(){
			TASKSTATUS tmp = new TASKSTATUS();
			tmp.Reserved = new sbyte[12];
			return tmp;
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	STATUS	// アンサーステータス情報構造体
	{
		public MCSTATUS mc;					// 全体情報
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=64)]
		public AXSTATUS[] ax;				// 軸情報
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
		public TASKSTATUS[] task;			// タスク情報
		public static STATUS Init(){
			STATUS tmp = new STATUS();
			tmp.mc = Rt64ecdata.MCSTATUS.Init();
			tmp.ax = new AXSTATUS[64];
			for(int cnt = tmp.ax.GetLowerBound(0); cnt <= tmp.ax.GetUpperBound(0); cnt++)
				tmp.ax[cnt] = Rt64ecdata.AXSTATUS.Init();
			tmp.task = new TASKSTATUS[8];
			for(int cnt = tmp.task.GetLowerBound(0); cnt <= tmp.task.GetUpperBound(0); cnt++)
				tmp.task[cnt] = Rt64ecdata.TASKSTATUS.Init();
			return tmp;
		}
	}

	// for STATUS.mc.Status
	public const uint S_MCS_ALM				= 0x1;			// アラーム発生中
	public const uint S_MCS_SETTING			= 0x2;			// 全タスクセッティングモード
	public const uint S_MCS_SENSE			= 0x10;			// 高速センサーラッチ完
	//public const uint						= 0x20;			//
	public const uint S_MCS_PRGCHG			= 0x40;			// 動作プログラムデータ変更
	public const uint S_MCS_RTCWARN			= 0x80;			// 制御周期負荷ワーニング(87.5%)
	public const uint S_MCS_RTCFAIL			= 0x100;		// 制御周期負荷過大
	public const uint S_MCS_FDTWARN			= 0x200;		// FDT読込ワーニング

	// for STATUS.mc.Alarm
//	public const uint S_MCA_EMS				= 0x1;			// 非常停止
	public const uint S_MCA_BACKUP			= 0x2;			// バックアップエラー
	public const uint S_MCA_PARAMETER		= 0x4;			// パラメータ未設定エラー
	public const uint S_MCA_ROMSW_BKUP		= 0x8;			// ROMSWﾊﾞｯｸｱｯﾌﾟｴﾗｰ
	public const uint S_MCA_ECT_INIT		= 0x10;			// EtherCAT初期化エラー
	public const uint S_MCA_ECT_WCOM		= 0x20;			// EtherCAT全体通信エラー
	// public const uint					= 0x40;			//
	public const uint S_MCA_FDTFAIL			= 0x80;			// FDT読込エラー
	// public const uint						= 0x100;		//
	public const uint S_MCA_ECT_IO_COMM		= 0x200;		// EtherCAT IO通信エラー
	public const uint S_MCA_SYSTEM			= 0x8000;		// システムエラー

	// for STATUS.ax[n].AxStatus
	public const uint S_AXS_INPOS			= 0x1;			// インポジション
	public const uint S_AXS_ACC_NZE			= 0x2;			// 加減速たまり有り
	public const uint S_AXS_SVON			= 0x4;			// サーボＯＮ
	public const uint S_AXS_ZRN				= 0x8;			// 原点復帰完了
	public const uint S_AXS_AXMV			= 0x10;			// 独立位置決め中
	public const uint S_AXS_AXMVSTP			= 0x20;			// 独立位置決め停止中
	public const uint S_AXS_SPIN			= 0x40;			// SPIN動作中
	public const uint S_AXS_SPINSTP			= 0x80;			// SPIN停止中
	public const uint S_AXS_TRQCTRL			= 0x100;		// トルク制御中
	public const uint S_AXS_ORGFIX			= 0x200;		// 原点確定済み
	public const uint S_AXS_VELCTRL			= 0x400;		// 速度制御中

	// for STATUS.ax[n].AxAlarm
	public const uint S_AXA_ERALM_P			= 0x1;			// ＋方向偏差過大
	public const uint S_AXA_ERALM_M			= 0x2;			// －方向偏差過大
	public const uint S_AXA_SALM			= 0x4;			// サーボアンプアラーム
	public const uint S_AXA_SLIMIT_P		= 0x8;			// ＋方向ソフトリミット
	public const uint S_AXA_SLIMIT_M		= 0x10;			// －方向ソフトリミット
	public const uint S_AXA_HLIMIT_P		= 0x20;			// ＋方向ハードリミット
	public const uint S_AXA_HLIMIT_M		= 0x40;			// －方向ハードリミット
	public const uint S_AXA_COMLIMIT_P		= 0x80;			// ＋方向パルス発生過大
	public const uint S_AXA_COMLIMIT_M		= 0x100;		// －方向パルス発生過大
	public const uint S_AXA_SPWOFF			= 0x200;		// サーボ主電源ＯＦＦ
	public const uint S_AXA_ECT_AXCOM		= 0x400;		// EtherCAT各軸通信エラー
	public const uint S_AXA_ECT_MLTCMD		= 0x800;		// EtherCAT多重コマンド
	public const uint S_AXA_ECT_USCMD		= 0x1000;		// EtherCAT未対応コマンドエラー

	// for STATUS.task[n].TaskStatus
	public const uint S_TKS_ALM				= 0x1;			// アラーム発生中
	public const uint S_TKS_FG_END			= 0x2;			// ＦＧ完了
	public const uint S_TKS_FG_STOP			= 0x4;			// ＦＧ中断中
	public const uint S_TKS_FG_BUN			= 0x8;			// ＦＧ分配中
	public const uint S_TKS_EXEC			= 0x10;			// プログラム運転中
	public const uint S_TKS_STOP			= 0x20;			// プログラム停止中
	public const uint S_TKS_BLKS			= 0x40;			// ブロック途中停止
	public const uint S_TKS_SEQ_END			= 0x80;			// 各種シーケンス完了
	public const uint S_TKS_SINGLE			= 0x100;		// シングルステップモード
	public const uint S_TKS_TEACH			= 0x200;		// ティーチングモード
	public const uint S_TKS_CYCLE			= 0x400;		// サイクル運転モード
	public const uint S_TKS_MLK_STS			= 0x800;		// マシンロック（未対応）
	public const uint S_TKS_MODE			= 0xF000;		// モード情報エリア

	public const uint S_TKS_INPOS			= 0x10000;		// 割り当て軸 インポジション
	public const uint S_TKS_ACC_NZE			= 0x20000;		// 割り当て軸 加減速たまり有り
	public const uint S_TKS_SVON			= 0x40000;		// 割り当て軸 サーボＯＮ
	public const uint S_TKS_ZRN				= 0x80000;		// 割り当て軸 原点復帰完了
	public const uint S_TKS_AXMV			= 0x100000;		// 割り当て軸 独立位置決め中
	public const uint S_TKS_AXMVSTP			= 0x200000;		// 割り当て軸 独立位置決め停止中
	public const uint S_TKS_RBTMOVING		= 0x400000;		// ロボット軸移動中

	public const uint S_TKS_SENSE			= 0x1000000;	// 高速センサーラッチ完
	public const uint S_TKS_TANG			= 0x2000000;	// 接線制御ＯＮ
	public const uint S_TKS_REEL_END		= 0x4000000;	// 最終層巻数異常警告(巻線)
	public const uint S_TKS_PNTTBLCHG		= 0x8000000;	// モータポイントテーブル変更フラグ
	public const uint S_TKS_RPNTTBLCHG		= 0x10000000;	// ロボットポイントテーブル変更フラグ

	// for STATUS.task[n].TaskAlarm
	public const uint S_TKA_PRGERR			= 0x1;			// プログラム実行エラー
	public const uint S_TKA_MOUTERR			= 0x2;			// Ｍコード実行エラー
	public const uint S_TKA_AXIS			= 0x4;			// 割り当て軸エラー
	public const uint S_TKA_FGERR			= 0x8;			// ＦＧ内部演算エラー
	public const uint S_TKA_POWEROFF		= 0x10;			// サーボＯＦＦエラー
	public const uint S_TKA_EXTALMA			= 0x20;			// 外部アラームＡエラー
	public const uint S_TKA_EXTALMB			= 0x40;			// 外部アラームＢエラー
	public const uint S_TKA_EXTALMC			= 0x80;			// 外部アラームＣエラー
	public const uint S_TKA_EMS				= 0x100;		// 非常停止
	public const uint S_TKA_RBTERR			= 0x200;		// ロボット系エラー

	// ------------------------------------------------------------------------
	//	EtherCATステータス情報構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	ECT_WHOLESTATUS
	{
		public short mst_init_errc;		// EtherCATマスター初期化エラーコード
		public short mst_ESM;			// EtherCAT ESM(EtherCAT State Machine)
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
		public byte[] Alignment;		//
		public int mst_NotifiCode;		// EtherCATマスター通知コード
		public int mst_whcom_errc;		// EtherCATマスター全体エラーコード
		public long mst_axcom_err;		// EtherCATマスターエラー発生軸フラグ
		public long no_rcv_err;			// データ受信エラー発生軸フラグ
		public long time_out_err;		// タイムアウトエラー発生軸フラグ
		public long watchdog_err;		// WDTエラー発生軸フラグ
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
		public byte[] Reserved;			// 予約
		public static ECT_WHOLESTATUS Init(){
			ECT_WHOLESTATUS tmp = new ECT_WHOLESTATUS();
			tmp.Alignment = new byte[4];
			tmp.Reserved = new byte[16];
			return tmp;
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	ECT_AXSTATUS
	{
		public int mst_axNotifiCode;	// EtherCATマスター通知コード
		public int mst_axcom_errc;		// EtherCATマスター通知エラーコード
		public short ControlWord;		// ControlWord(6040h)
		public short StatusWord;		// StatusWord(6041h)
		public byte ModesOfOpe;			// ModesOfOperation(6060h)
		public byte ModesOfOpeDisp;		// ModesOfOperationDisplay(6061h)
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
		public byte[] Alignment;		//
		public short TouchProbeFunc;	// TouchProbeFunction(60B8h)
		public short TouchProbeStat;	// TouchProbeStatus(60B9h)
		public int DigitalInputs;		// Digital inputs(60FDh)
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
		public byte[] Reserved;			// 予約
		public static ECT_AXSTATUS Init(){
			ECT_AXSTATUS tmp = new ECT_AXSTATUS();
			tmp.Alignment = new byte[2];
			tmp.Reserved = new byte[8];
			return tmp;
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	ECT_INFOAX
	{
		public ECT_WHOLESTATUS WholeStat;
		public ECT_AXSTATUS	AxStat;
		public static ECT_INFOAX Init(){
			ECT_INFOAX tmp = new ECT_INFOAX();
			tmp.WholeStat = Rt64ecdata.ECT_WHOLESTATUS.Init();
			tmp.AxStat = Rt64ecdata.ECT_AXSTATUS.Init();
			return tmp;
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	ECT_INFOALL
	{
		public ECT_WHOLESTATUS WholeStat;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
		public ECT_AXSTATUS[] AxStat;
		public static ECT_INFOALL Init(){
			ECT_INFOALL tmp = new ECT_INFOALL();
			tmp.WholeStat = Rt64ecdata.ECT_WHOLESTATUS.Init();
			tmp.AxStat = new ECT_AXSTATUS[9];
			for(int cnt = tmp.AxStat.GetLowerBound(0); cnt <= tmp.AxStat.GetUpperBound(0); cnt++)
				tmp.AxStat[cnt] = Rt64ecdata.ECT_AXSTATUS.Init();
			return tmp;
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	ECT_INFO64ALL
	{
		public ECT_WHOLESTATUS WholeStat;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		public ECT_AXSTATUS[] AxStat;
		public static ECT_INFO64ALL Init(){
			ECT_INFO64ALL tmp = new ECT_INFO64ALL();
			tmp.WholeStat = Rt64ecdata.ECT_WHOLESTATUS.Init();
			tmp.AxStat = new ECT_AXSTATUS[64];
			for(int cnt = tmp.AxStat.GetLowerBound(0); cnt <= tmp.AxStat.GetUpperBound(0); cnt++)
				tmp.AxStat[cnt] = Rt64ecdata.ECT_AXSTATUS.Init();
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	EtherCAT受信データ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	ECT_MON
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=64)]
		public byte[] dat;
		public static ECT_MON Init(){
			ECT_MON tmp = new ECT_MON();
			tmp.dat = new byte[64];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	EtherCAT送受信データPDOマッピング構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	ECT_PDO_OBJ
	{
		public ushort Index;				//Index:XXXXh
		public byte SubIndex;				//SubIndex:xxh
		public byte Reserved0;
		public short Length;				//データ長[Bit]
		public short Offset;				//データオフセット[Bit]
		public static ECT_PDO_OBJ Init(){
			return(new ECT_PDO_OBJ());
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	ECT_PDO_MAP
	{
		public short OutRecCnt;
		public short InRecCnt;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
		public byte[] Reserved0;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=512)]
		public ECT_PDO_OBJ[] OutRec;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=512)]
		public ECT_PDO_OBJ[] InRec;

		public static ECT_PDO_MAP Init(){
			ECT_PDO_MAP tmp = new ECT_PDO_MAP();
			tmp.Reserved0 = new byte[4];
			tmp.OutRec = new ECT_PDO_OBJ[512];
			for(int cnt = tmp.OutRec.GetLowerBound(0); cnt <= tmp.OutRec.GetUpperBound(0); cnt++)
				tmp.OutRec[cnt] = Rt64ecdata.ECT_PDO_OBJ.Init();
			tmp.InRec = new ECT_PDO_OBJ[512];
			for(int cnt = tmp.InRec.GetLowerBound(0); cnt <= tmp.InRec.GetUpperBound(0); cnt++)
				tmp.InRec[cnt] = Rt64ecdata.ECT_PDO_OBJ.Init();
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	汎用入出力情報構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	IODATA
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=116)]
		public ushort[] InputData;			// 汎用入力
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=64)]
		public ushort[] OutputData;			// 汎用出力
		public static IODATA Init(){
			IODATA tmp = new IODATA();
			tmp.InputData = new ushort[116];
			tmp.OutputData = new ushort[64];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	ＤＮＣバッファ情報構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	DNCBUFI
	{
		public int size;					// バッファ使用容量（バイト）
		public int Free;					// バッファ空き容量（バイト）
		public static DNCBUFI Init(){
			return(new DNCBUFI());
		}
	}

	// ------------------------------------------------------------------------
	//	センサーラッチ位置情報構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	SENSEPOS
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] SenPos;				// ｾﾝｻｰﾗｯﾁﾎﾟｼﾞｼｮﾝ（論理座標系）
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] SenPosA;				// ｾﾝｻｰﾗｯﾁﾎﾟｼﾞｼｮﾝ（ｱﾌﾞｿ座標系）
		public static SENSEPOS Init(){
			SENSEPOS tmp = new SENSEPOS();
			tmp.SenPos = new int[9];
			tmp.SenPosA = new int[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	プログラムボリュームラベル構造体（１０４ﾊﾞｲﾄ固定長）
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	BINPRG_LABEL
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
		public byte[] Reserved1;			// 予約
		public short BlockNumber;			// 有効ブロック長
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=22)]
		public byte[] Reserved2;			// 予約
		public short ProgramType;			// ﾌﾟﾛｸﾞﾗﾑｺｰﾄﾞﾀｲﾌﾟ(0:T､ 1:G)
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=76)]
		public byte[] Reserved3;			// 予約
		public static BINPRG_LABEL Init(){
			BINPRG_LABEL tmp = new BINPRG_LABEL();
			tmp.Reserved1 = new byte[2];
			tmp.Reserved2 = new byte[22];
			tmp.Reserved3 = new byte[76];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	プログラム１ブロックデータ構造体（１０４ﾊﾞｲﾄ固定長）
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	BINPRG_BLOCK
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=101)]
		public byte[] Reserved;				// 予約
		public byte PrgType;				// ﾌﾟﾛｸﾞﾗﾑｺｰﾄﾞﾀｲﾌﾟ(0:T､ 1:G)
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
		public byte[] Reserved2;			// 未使用
		public static BINPRG_BLOCK Init(){
			BINPRG_BLOCK tmp = new BINPRG_BLOCK();
			tmp.Reserved = new byte[101];
			tmp.Reserved2 = new byte[2];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	Ａ／Ｄ＆ＰＯＳ情報構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	AD_POS
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
		public int[] Ad;					// Ａ／Ｄ値
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] Pos;					// 論理座標系機械位置
		public static AD_POS Init(){
			AD_POS tmp = new AD_POS();
			tmp.Ad	= new int[4];
			tmp.Pos = new int[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	ＴＰＣロギング情報構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	TPCINFO
	{
		public short Log;					// ＴＰＣﾃﾞｰﾀﾛｷﾞﾝｸﾞﾌﾗｸﾞ
		public short Num;					// ＴＰＣﾃﾞｰﾀﾛｷﾞﾝｸﾞﾎﾟｲﾝﾄ数
		public static TPCINFO Init(){
			return(new TPCINFO());
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	TPCINFOEX
	{
		public short Log;					// ロギング中フラグﾞ
		public short Reserved;				// 予約
		public int	Size;					// バッファ使用容量（バイト）
		public int	Free;					// バッファ空き容量（バイト）
		public static TPCINFOEX Init(){
			return(new TPCINFOEX());
		}
	}

	// ------------------------------------------------------------------------
	//	ＴＰＣデータ構造体
	// ------------------------------------------------------------------------
	//----------
	// TPCH_LOG_POS用
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	TPCDAT_POS
	{
		public int	pr1;					// 第１軸機械位置
		public ushort hi1;					// 汎用入力１状態
		public ushort ho1;					// 汎用出力１状態
		public int	pr2;					// 第２軸機械位置
		public ushort hi2;					// 汎用入力２状態
		public ushort ho2;					// 汎用出力２状態
		public static TPCDAT_POS Init(){
			return(new TPCDAT_POS());
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	TPC	// TPCDAT_POSと同じ型
	{
		public int	pr1;					// 第１軸機械位置
		public short hi1;					// 汎用入力１状態
		public short ho1;					// 汎用出力１状態
		public int	pr2;					// 第２軸機械位置
		public short hi2;					// 汎用入力２状態
		public short ho2;					// 汎用出力２状態
		public static TPC Init(){
			return(new TPC());
		}
	}

	//----------
	// TPCH_LOG_ECT用
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	TPCDAT_ECT_16BYTE
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
		public byte[] dt;
		public static TPCDAT_ECT_16BYTE Init(){
			TPCDAT_ECT_16BYTE tmp = new TPCDAT_ECT_16BYTE();
			tmp.dt = new byte[16];
			return tmp;
		}
	}
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	TPCDAT_ECT_32BYTE
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]
		public byte[] dt;
		public static TPCDAT_ECT_32BYTE Init(){
			TPCDAT_ECT_32BYTE tmp = new TPCDAT_ECT_32BYTE();
			tmp.dt = new byte[32];
			return tmp;
		}
	}
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	TPCDAT_ECT_48BYTE
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=48)]
		public byte[] dt;
		public static TPCDAT_ECT_48BYTE Init(){
			TPCDAT_ECT_48BYTE tmp = new TPCDAT_ECT_48BYTE();
			tmp.dt = new byte[48];
			return tmp;
		}
	}
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	TPCDAT_ECT_64BYTE
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=64)]
		public byte[] dt;
		public static TPCDAT_ECT_64BYTE Init(){
			TPCDAT_ECT_64BYTE tmp = new TPCDAT_ECT_64BYTE();
			tmp.dt = new byte[64];
			return tmp;
		}
	}


	// ------------------------------------------------------------------------
	//	ＶＥＲ／ＰＥＲ情報構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	VPER
	{
		public short ver; //
		public short vmp; //
		public short vmm; //
		public short per; //
		public short pmp; //
		public short pmm; //
		public static VPER Init(){
			return(new VPER());
		}
	}

	// ------------------------------------------------------------------------
	//	ピッチエラー補正用パラメータ情報構造体（３４５６０バイト固定長）
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	REV_AX	// 各軸補正用パラメータ
	{
		public int RevMagnify;	// 補正倍率
		public int RevSpace;	// 補正間隔
		public int RevTopNo;	// 補正データ先頭番号
		public int RevMCnt;		// －側補正区間数
		public int RevPCnt;		// ＋側補正区間数
		public static REV_AX Init(){
			return(new REV_AX());
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	PITCH_ERR_REV
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=64)]
		public REV_AX[] RevAx;	// 各軸補正用パラメータ
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=33280)]
		public byte[] RevDt;	// 補正データ
		public static PITCH_ERR_REV Init(){
			PITCH_ERR_REV tmp = new PITCH_ERR_REV();
			tmp.RevAx = new REV_AX[64];
			for(int cnt = tmp.RevAx.GetLowerBound(0); cnt <= tmp.RevAx.GetUpperBound(0); cnt++)
				tmp.RevAx[cnt] = Rt64ecdata.REV_AX.Init();
			tmp.RevDt = new byte[33280];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	ツール長補正用パラメータ情報構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	TOOL_H
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=20)]
		public int[] h;
		public static TOOL_H Init(){
			TOOL_H tmp = new TOOL_H();
			tmp.h = new int[20];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	マクロ変数データ
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	VARIABLE
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=200)]
		public short[] Var;
		public static VARIABLE Init(){
			VARIABLE tmp = new VARIABLE();
			tmp.Var = new short[200];
			return tmp;
		}
	}

	// グローバルマクロ変数情報								：#1000～#1199(WORDアクセス領域)
	public const short	VARW_GLB_STR	= 1000;				// 先頭アドレス
	public const short	VARW_GLB_NUM	=  200;				// データ数
	// ラダー共有マクロ変数(RTMC64 -> PLC)情報				：#1200～#1299(WORDアクセス領域)
	public const short	VARW_LDW_STR	= 1200;				// 先頭アドレス
	public const short	VARW_LDW_NUM	=  100;				// データ数
	// ラダー共有マクロ変数(RTMC64 <- PLC)情報				：#1300～#1399(WORDアクセス領域)
	public const short	VARW_LDR_STR	= 1300;				// 先頭アドレス
	public const short	VARW_LDR_NUM	=  100;				// データ数
	// ローカルマクロ変数情報								：#1400～#1499(WORDアクセス領域)
	public const short	VARW_LOC_STR	= 1400;				// 先頭アドレス
	public const short	VARW_LOC_NUM	=  100;				// データ数

	// グローバルマクロ変数情報								：#5500～#5699(LONGアクセス領域)
	public const short	VARL_GLB_STR	= 5500;				// 先頭アドレス
	public const short	VARL_GLB_NUM	= (VARW_GLB_NUM/2);	// データ数
	// ラダー共有マクロ変数(RTMC64 -> PLC)情報				：#5700～#5799(LONGアクセス領域)
	public const short	VARL_LDW_STR	= 5700;				// 先頭アドレス
	public const short	VARL_LDW_NUM	= (VARW_LDW_NUM/2);	// データ数
	// ラダー共有マクロ変数(RTMC64 <- PLC)情報				：#5800～#5899(LONGアクセス領域)
	public const short	VARL_LDR_STR	= 5800;				// 先頭アドレス
	public const short	VARL_LDR_NUM	= (VARW_LDR_NUM/2);	// データ数
	// ローカルマクロ変数情報								：#5900～#5999(LONGアクセス領域)
	public const short	VARL_LOC_STR	= 5900;				// 先頭アドレス
	public const short	VARL_LOC_NUM	= (VARW_LOC_NUM/2);	// データ数

	// ------------------------------------------------------------------------
	//	補間前加減速パラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	ACO_PRM
	{
		public int aco_acot;				// 補間前加減速時定指数
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=7)]
		public int[] aco_accdat;			// 補間前加減速最小ｵｰﾊﾞﾗｲﾄﾞ切換加速度
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=7)]
		public short[] aco_ovrdat;			// 補間前加減速最小ｵｰﾊﾞﾗｲﾄﾞﾃﾞｰﾀ
		public short	Reserved;			// 未使用
		public static ACO_PRM Init(){
			ACO_PRM tmp = new ACO_PRM();
			tmp.aco_accdat = new int[7];
			tmp.aco_ovrdat = new short[7];
			return tmp;
		}
	}


	// for ACO_PRM.aco_acot
	public const int ACO_TMMIN = 0;	 	// 補間前加減速時定数最小値
	public const int ACO_TMMAX = 16383;		// 補間前加減速時定数最大値

	// for ACO_PRM.aco_accdat[]
	public const int ACO_ACCMIN = 0;	 	// 補間前加減速切換加速度最小値
//	public const int ACO_ACCMAX = 8000000;	// 補間前加減速切換加速度最大値
	public const int ACO_ACCMAX = (1000000000 * 2);	// 補間前加減速切換加速度最大値

	// for ACO_PRM.aco_ovrdat[]
	public const short ACO_OVRMIN = 1;	 	// 補間前加減速ｵｰﾊﾞﾗｲﾄﾞ最小値
	public const short ACO_OVRMAX = 100;	// 補間前加減速ｵｰﾊﾞﾗｲﾄﾞ最大値

	// ------------------------------------------------------------------------
	//	ティーチング情報用パラメータ情報構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	TEACHSTS
	{
		public int Status;					// ティーチングステータス
		public int StepNo;					// 実行ステップ番号
		public int TchStepNo;				// ティーチングステップ番号
		public int Reserved;				// 未使用
		public static TEACHSTS Init(){
			return(new TEACHSTS());
		}
	}

	// for TEACHSTS.Status
	public const int T_TEACH		= 0x1;	// ティーチングモード
	public const int T_TEACHSTP		= 0x2;	// ティーチング開始ステップ
	public const int T_TEACHEN		= 0x4;	// ティーチング可能ステップ
	public const int T_TEACHSTPPRV	= 0x8;	// ﾃｨｰﾁﾝｸﾞ開始ｽﾃｯﾌﾟの前ｽﾃｯﾌﾟ

	// ------------------------------------------------------------------------
	//	手パ操作情報構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	HANDLESTS
	{
		public short handle_mode;			// 手パ有効フラグ
		public short robot_mode;			// ロボット手パ有効フラグ
		public short kp;					// 手パ設定倍率
		public short ax1;					// 手パ第１軸
		public short ax2;					// 手パ第２軸
		public static HANDLESTS Init(){
			return(new HANDLESTS());
		}
	}

	// for HANDLESTS.handle_mode
	public const short HDL_MD_HANDLE	= 1;		// 手パモード
	public const short HDL_MD_JOYSTICK = 2;			// ジョイスティックモード
	public const short HDL_MD_ENABLE	= 0x1000;	// 手パ/ジョイスティック有効

	// ------------------------------------------------------------------------
	//		ＲＯＭソフトバージョンデータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	ROMVERSION
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
		public byte[] Version;				// バージョン文字列
		public short EvenSum;				// SUM:even (rom)
		public short OddSum;				// SUM:odd	(rom)
		public short FlashSum;				// SUM:ＳＨ内部ＦＬＡＳＨ
		public short FlashFlg;				// ＳＨ内部ＦＬＡＳＨ使用フラグ
		public short KindID;				// 機種ＩＤ
		public int SerialID;				// シリアルＩＤ
		public int ProductID;				// プロダクトＩＤ
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=28)]
		public byte[] Reserved;
		public static ROMVERSION Init(){
			ROMVERSION tmp = new ROMVERSION();
			tmp.Version	= new byte[16];
			tmp.Reserved = new byte[28];
			return tmp;
		}
	}

	// for ROMVERSION.KindID
	public const short	SID_BKIND		= 30;		// シリアルナンバー（機種ＩＤ）
	public const string SID_BKIND_STR	= "RT64EC";	// シリアルナンバー（機種ＩＤ）

	// ------------------------------------------------------------------------
	//	ＲＴＣ処理時間格納構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	RTCTIME
	{
		public int RtcMax;					// 最大処理時間[us]
		public int RtcNow;					// 現在処理時間[us]
		public static RTCTIME Init(){
			return(new RTCTIME());
		}
	}

	// ------------------------------------------------------------------------
	//	通信周期設定値構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	CYCLETIME
	{
		public int CycleTime;				// 制御周期[us]
		public static CYCLETIME Init(){
			return(new CYCLETIME());
		}
	}

	// ------------------------------------------------------------------------
	//	フォアグランド処理時間格納構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	FORTIME
	{
		public int ForMax;					// 最大処理時間[us]
		public int ForNow;					// 現在処理時間[us]
		public static FORTIME Init(){
			return(new FORTIME());
		}
	}

	// ------------------------------------------------------------------------
	//	ツール径補正用パラメータ情報構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	TOOL_D	// （８０バイト）
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=20)]
		public int[] d;						// ツール径補正データ
		public static TOOL_D Init(){
			TOOL_D tmp = new TOOL_D();
			tmp.d = new int[20];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	プログラムブロック情報構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	PRGBLK_INF_SUB
	{
		public short PrgNo;					// プログラム番号
		public short TaskNo;				// タスク番号
		public static PRGBLK_INF_SUB Init(){
			return(new PRGBLK_INF_SUB());
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	PRGBLK_INF	// （２５６バイト）
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=64)]
		public PRGBLK_INF_SUB[] inf;
		public static PRGBLK_INF Init(){
			PRGBLK_INF tmp = new PRGBLK_INF();
			tmp.inf = new PRGBLK_INF_SUB[64];
			for(int cnt = tmp.inf.GetLowerBound(0); cnt <= tmp.inf.GetUpperBound(0); cnt++)
				tmp.inf[cnt] = Rt64ecdata.PRGBLK_INF_SUB.Init();
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	マクロ変数データ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	MCRREG
	{
		public int Val;						// マクロ変数値
		public static MCRREG Init(){
			return(new MCRREG());
		}
	}

	// ------------------------------------------------------------------------
	//	工具長補正情報構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	TOOLHSTS
	{
		public short toolh_en;				// 補正有効フラグ
		public short toolh_no;				// 選択中補正No
		public static TOOLHSTS Init(){
			return(new TOOLHSTS());
		}
	}

	// ------------------------------------------------------------------------
	//	工具径補正情報構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	TOOLDSTS
	{
		public short toold_en;				// 補正有効フラグ
		public short toold_no;				// 選択中補正No
		public static TOOLDSTS Init(){
			return(new TOOLDSTS());
		}
	}

	// ------------------------------------------------------------------------
	//	工具径補正エラー情報構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	TOOLDERR
	{
		public int StepNo;					// エラー発生ステップ番号
		public int ErrCode;					// エラーコード
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
		public byte[] Reserved;
		public static TOOLDERR Init(){
			TOOLDERR tmp = new TOOLDERR();
			tmp.Reserved = new byte[8];
			return tmp;
		}
	}

	// for TOOLDERR.ErrCode
	public const int TDERR_STATE_ANOMALY	= (1 <<	0);		// 正体不明エラー（内部状態異常）
	public const int TDERR_DCHG_CIR			= (1 <<	1);		// 円弧指令での径補正モード変更エラー
	public const int TDERR_INHB_COMMAND		= (1 <<	2);		// 径補正中の禁止命令指定エラー
	public const int TDERR_MACRO			= (1 <<	3);		// マクロ変数エラー
	public const int TDERR_D_RANGE			= (1 <<	4);		// 径指定範囲外エラー
	public const int TDERR_LIN_NOMOVE		= (1 <<	5);		// 移動量なしエラー
	public const int TDERR_LIN_SE_SAME		= (1 <<	6);		// 始点/終点一致エラー
	public const int TDERR_CIR_CENT_IL		= (1 <<	7);		// 円弧中心点演算エラー
	public const int TDERR_LINLIN_NOCP		= (1 <<	8);		// 交点なしエラー (LIN → LIN)
	public const int TDERR_LINCIR_NOCP		= (1 <<	9);		// 交点なしエラー (LIN → CIR、CIR → LIN)
	public const int TDERR_CIRCIR_NOCP		= (1 << 10);	// 交点なしエラー (CIR → CIR)
	public const int TDERR_LIN_REVERSE		= (1 << 11);	// 移動方向反転エラー（LIN）
	public const int TDERR_CIR_REVERSE		= (1 << 12);	// 移動方向反転エラー（CIR）


	// ------------------------------------------------------------------------
	//	位置決めポイントテーブルデータ構造体（３６ﾊﾞｲﾄ固定長）
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	AXIS_POINT	// ポイントデータ構造体（３６ﾊﾞｲﾄ固定長）
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] AxisPos;				// 各軸アブソ位置
		public static AXIS_POINT Init(){
			AXIS_POINT tmp = new AXIS_POINT();
			tmp.AxisPos = new int[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	ロボット位置決めポイントテーブルデータ構造体（４０ﾊﾞｲﾄ固定長）
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	RAXIS_POINT	// ポイントデータ構造体（３６ﾊﾞｲﾄ固定長）
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] AxisPos;				// 各軸アブソ位置
		public int   rbtfig;				// ロボット姿勢情報
		public static RAXIS_POINT Init(){
			RAXIS_POINT tmp = new RAXIS_POINT();
			tmp.AxisPos = new int[9];
			return tmp;
		}
	}

	public const short POINTTBLMAX = 400;	// 位置決めポイントテーブル最大数
	public const short RBT_FIGURE_IDX = 9;	// ロボットポイントテーブル
											// ロボット姿勢格納インデックス
	public const short RBT_FIGURE_BIT = (1 << RBT_FIGURE_IDX);

	// ------------------------------------------------------------------------
	//	絶対値エンコーダオフセット構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	ABSENCOFS
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=64)]
		public int[] ofs;
		public static ABSENCOFS Init(){
			ABSENCOFS tmp = new ABSENCOFS();
			tmp.ofs = new int[64];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	EtherCATオブジェクト選択情報
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	ECTOBJSEL
	{
		public short Index;					// オブジェクトIndex
		public byte Subindex;				// オブジェクトSubindex
		public byte Size;					// オブジェクトサイズ
		public static ECTOBJSEL Init(){
			return(new ECTOBJSEL());
		}
	}

	// ------------------------------------------------------------------------
	//	EtherCATオブジェクト値
	// ------------------------------------------------------------------------
	// BYTE(1byte)アクセス
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	ECTOBJVAL_BT
	{
		public byte bt;						// BYTE(1byte)アクセス
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=255)]
		public byte[] alignment;			// アライメント
		public static ECTOBJVAL_BT Init(){
			ECTOBJVAL_BT tmp = new ECTOBJVAL_BT();
			tmp.alignment = new byte[255];
			return tmp;
		}
	}
	// WORD(2byte)アクセス
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	ECTOBJVAL_WD
	{
		public short wd;					// WORD(2byte)アクセス
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=254)]
		public byte[] alignment;			// アライメント
		public static ECTOBJVAL_WD Init(){
			ECTOBJVAL_WD tmp = new ECTOBJVAL_WD();
			tmp.alignment = new byte[254];
			return tmp;
		}
	}
	// DWORD(4byte)アクセス
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	ECTOBJVAL_DW
	{
		public int dw;						// DWORD(4byte)アクセス
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=252)]
		public byte[] alignment;			// アライメント
		public static ECTOBJVAL_DW Init(){
			ECTOBJVAL_DW tmp = new ECTOBJVAL_DW();
			tmp.alignment = new byte[252];
			return tmp;
		}
	}
	// 文字列(最大256byte)アクセス
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	ECTOBJVAL_STR
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=256)]
		public byte[] str;					// 文字列(最大256byte)アクセス
		public static ECTOBJVAL_STR Init(){
			ECTOBJVAL_STR tmp = new ECTOBJVAL_STR();
			tmp.str = new byte[256];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	軸割り当て情報構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	ASS_AXIS
	{
		public sbyte tsk;
		public sbyte axn;
		public static ASS_AXIS Init(){
			return(new ASS_AXIS());
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	ASSAXISTBL
	{
		public long axis_en;				// 有効軸フラグ	※同期従軸は含まない
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=64)]
		public ASS_AXIS[] ass_axis;
		public static ASSAXISTBL Init(){
			ASSAXISTBL tmp = new ASSAXISTBL();
			tmp.ass_axis = new ASS_AXIS[64];
			for(int cnt = tmp.ass_axis.GetLowerBound(0); cnt <= tmp.ass_axis.GetUpperBound(0); cnt++)
				tmp.ass_axis[cnt] = Rt64ecdata.ASS_AXIS.Init();
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	ロボットパラメータ情報構造体					
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	RBTPRM
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] RbtLimM;					// ロボット座標-側リミット(X～T)
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] RbtLimP;					// ロボット座標+側リミット(X～T)
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] MotLimM;					// ロボット動作時モータ座標-側リミット(X～T)
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] MotLimP;					// ロボット動作時モータ座標+側リミット(X～T)
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] RbtInchFeed;				// ロボットインチング速度[pps]	
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] RbtJogFeed;				// ロボットJOG速度[pps]	
		public int RbtKa;						// ロボット座標補間時定数 [msec]
		public int RbtSKa;						// ロボット座標Ｓ字補間時定数 [msec]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] MotDir;					// モータ回転方向(1:正,-1:負)
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] MotOfs;					// モータ回転オフセット量[deg]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] RbtHandleMax;				// ロボット手パ/ジョイスティック最高速度[pps]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] RbtInpos;				// ロボットINPOS(X~T)
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=144)]
		public byte[] Reserved;					// 予約
		public static RBTPRM Init(){
			RBTPRM tmp = new RBTPRM();
			tmp.RbtLimM = new int[9];
			tmp.RbtLimP = new int[9];
			tmp.MotLimM = new int[9];
			tmp.MotLimP = new int[9];
			tmp.RbtInchFeed = new int[9];
			tmp.RbtJogFeed = new int[9];
			tmp.MotDir = new int[9];
			tmp.MotOfs = new int[9];
			tmp.RbtHandleMax = new int[9];
			tmp.RbtInpos = new int[9];
			tmp.Reserved = new byte[144];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	デルタロボット機構用パラメータ情報構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	RBTDELTAPRM
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
		public int[] LinkLen;				// リンク長(L1,L2)
		public int BaseOfs;					// ベースプレート側リンク取付半径
		public int BottomOfs;				// ボトムプレート側リンク取付半径
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=3)]
		public int[] WorkOfs;				// 作業オフセット
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=3)]
		public int[] AjstOfs;				// 調整用オフセット
		public int BasePlateLen;			// 原点姿勢時のプレート距離
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=84)]
		public byte[] Reserved;
		public static RBTDELTAPRM Init(){
			RBTDELTAPRM tmp = new RBTDELTAPRM();
			tmp.LinkLen = new int[2];
			tmp.WorkOfs = new int[3];
			tmp.AjstOfs = new int[3];
			tmp.Reserved = new byte[84];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	スカラロボット機構パラメータ情報構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	RBTSCARAPRM
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
		public int[] LinkPrm;				// リンク長(L1,L2)
		public int 	 BanAreaR;				// ロボット系移動禁止エリア半径
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=116)]
		public byte[] Reserved;
		public static RBTSCARAPRM Init(){
			RBTSCARAPRM tmp = new RBTSCARAPRM();
			tmp.LinkPrm = new int[2];
			tmp.Reserved = new byte[116];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	6軸垂直多関節機構パラメータ情報構造体					
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	RBT6AXISPRM
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
		public int[] LinkPrm;					//  Link(L1,L1d,L2d,L2,L2dd,L3,L4,L5)
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=96)]
		public byte[] Reserved;					// 予約
		public static RBT6AXISPRM Init(){
			RBT6AXISPRM tmp = new RBT6AXISPRM();
			tmp.LinkPrm = new int[8];
			tmp.Reserved = new byte[96];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	ロボット位置情報構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	RBTPOSINFO
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] rbtvcr;				// ワーク座標系仮想指令位置
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] rbtvpos;				// ワーク座標系仮想ポジション
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] rbtcr;					// ワーク座標系仮想ポジション
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] rbtpr;					// ワーク座標系機械(FB)位置
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] rbtinc;				// ワーク座標系最新ブロック払い出し量
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] rbtmod;				// ワーク座標系残移動量
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] rbtbasevcr;			// ベース座標系仮想指令位置
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] rbtbasecr;				// ベース座標系指令位置
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] rbtbasepr;				// ベース座標系機械(FB)位置
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] mcr;					// モータ系指令位置
		public int rbtfig;					// ロボット姿勢情報
		public short rbttool;				// ツール座標系番号
		public short rbtwork;				// ワーク座標系番号
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=144)]
		public byte[] Reserved;
		public static RBTPOSINFO Init(){
			RBTPOSINFO tmp = new RBTPOSINFO();
			tmp.rbtvcr = new int[9];
			tmp.rbtvpos = new int[9];
			tmp.rbtcr = new int[9];
			tmp.rbtpr = new int[9];
			tmp.rbtinc = new int[9];
			tmp.rbtmod = new int[9];
			tmp.rbtbasevcr = new int[9];
			tmp.rbtbasecr = new int[9];
			tmp.rbtbasepr = new int[9];
			tmp.mcr = new int[9];
			tmp.Reserved = new byte[144];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	ロボット系エラー情報構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	RBTERRINF
	{
		public int rbt_mlim_err;			// エラー：－方向ロボットリミットエラー
		public int rbt_plim_err;			// エラー：＋方向ロボットリミットエラー
		public int mot_mlim_err;			// エラー：－方向モータリミットエラー
		public int mot_plim_err;			// エラー：＋方向モータリミットエラー
		public int rbt_spdovr_err;			// エラー：速度過大エラー
		public int rbt_fig_err;				// エラー：姿勢エラー
											//	D0:該当姿勢無し
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=40)]
		public byte[] Reserved;
		public static RBTERRINF Init(){
			RBTERRINF tmp = new RBTERRINF();
			tmp.Reserved = new byte[40];
			return tmp;
		}
	}

	// for RBTERRINF.rbt_fig_err
	public const int ERR_RFIG_NONFIGURE = 0xFFFF;	// D00-D15:該当姿勢無し
													// 機構により細分化する
    public const int ERR_RFIG_DIFF_FIG = 0x10000;	// 姿勢不一致

	// ------------------------------------------------------------------------
	//	ロボット姿勢情報構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	RBTFIGINFO
	{
		public int RbtFigInfo;				// ロボット姿勢情報
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
		public byte[] Reserved;
		public static RBTFIGINFO Init(){
			RBTFIGINFO tmp = new RBTFIGINFO();
			tmp.Reserved = new byte[4];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	モータポジション情報構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct MOTPOSINF
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] MotPos;				// モータ指令座標(エンコーダ座標[指令値])
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] MotAPos;				// モータFB座標(エンコーダ座標[FB値])
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] AbsOfsDat;				// アブソオフセット値(機械座標系原点モータ座標)
		public static MOTPOSINF Init(){
			MOTPOSINF tmp = new MOTPOSINF();
			tmp.MotPos = new int[9];
			tmp.MotAPos = new int[9];
			tmp.AbsOfsDat = new int[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	動作モードデータ変更コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	MODECHG
	{
		public short mode;					// 動作モード
		public static MODECHG Init(){
			return(new MODECHG());
		}
	}

	// ------------------------------------------------------------------------
	//	ＪＯＧ移動開始コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	JOGSTART
	{
		public short AxisFlag;				// 移動軸選択フラグ
		public short JogVect;				// 軸移動方向フラグ
		public static JOGSTART Init(){
			return(new JOGSTART());
		}
	}

	// ------------------------------------------------------------------------
	//	原点復帰移動開始コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	ZRNSTART
	{
		public short AxisFlag;				// 移動軸選択フラグ
		public static ZRNSTART Init(){
			return(new ZRNSTART());
		}
	}

	// ------------------------------------------------------------------------
	//	バックアップメモリ初期化コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	GENERATION
	{
		public short InitCmnd;				// 初期化データ選択フラグ
		public static GENERATION Init(){
			return(new GENERATION());
		}
	}

	// for GENERATION.InitCmnd
	public const short GEN_PARAM	= 0x1;	// パラメータ初期化指定
	public const short GEN_PROGRAM	= 0x2;	// 動作プログラム初期化指定
	public const short GEN_POSITION	= 0x4;	// アブソ座標初期化指定
	public const short GEN_VARIABLE	= 0x8;	// マクロ変数初期化指定
	public const short GEN_PNTTBL	= 0x10;	// モータポイントテーブル初期化指定
	public const short GEN_RBTPNTTBL= 0x20;	// ロボットポイントテーブル初期化指定

	// ------------------------------------------------------------------------
	//	ＰＴＰ移動開始コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	PTPSTART
	{
		public int AxisFlag;				// 移動軸選択フラグ
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] IncAxis;				// 各軸ｲﾝｸﾘﾒﾝﾄ移動量
		public static PTPSTART Init(){
			PTPSTART tmp = new PTPSTART();
			tmp.IncAxis = new int[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	ＰＴＰ移動開始コマンドパラメータ構造体（ＡＢＳＯ）
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	PTPASTART
	{
		public int AxisFlag;				// 移動軸選択フラグ
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] PosAxis;				// 各軸位置決めﾎﾟｼﾞｼｮﾝ
		public static PTPASTART Init(){
			PTPASTART tmp = new PTPASTART();
			tmp.PosAxis = new int[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	補間移動開始コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	LINSTART
	{
		public int AxisFlag;				// 移動軸選択フラグ
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] IncAxis;				// 各軸ｲﾝｸﾘﾒﾝﾄ移動量
		public int Feed;					// 補間送り速度
		public static LINSTART Init(){
			LINSTART tmp = new LINSTART();
			tmp.IncAxis = new int[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	高速センサーラッチ補間移動開始コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	SLINSTART
	{
		public int AxisFlag;				// 移動軸選択フラグ
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] IncAxis;				// 各軸ｲﾝｸﾘﾒﾝﾄ移動量
		public int Feed;					// 補間送り速度
		public short NoSkipF;				// スキップ抑制フラグ
		public short Reserve;				// ﾃﾞｰﾀをﾊﾟｯｸするためのﾀﾞﾐｰ
		public static SLINSTART Init(){
			SLINSTART tmp = new SLINSTART();
			tmp.IncAxis = new int[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	補間移動開始コマンドパラメータ構造体（ＡＢＳＯ）
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	LINASTART
	{
		public int AxisFlag;				// 移動軸選択フラグ
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] PosAxis;				// 各軸位置決めﾎﾟｼﾞｼｮﾝ
		public int Feed;					// 補間送り速度
		public static LINASTART Init(){
			LINASTART tmp = new LINASTART();
			tmp.PosAxis = new int[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	高速センサーラッチ補間移動開始コマンドパラメータ構造体（ＡＢＳＯ）
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	SLINASTART
	{
		public int AxisFlag;				// 移動軸選択フラグ
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] PosAxis;				// 各軸位置決めﾎﾟｼﾞｼｮﾝ
		public int Feed;					// 補間送り速度
		public short NoSkipF;				// スキップ抑制フラグ
		public int Reserve;					// ﾃﾞｰﾀをﾊﾟｯｸするためのﾀﾞﾐｰ
		public static SLINASTART Init(){
			SLINASTART tmp = new SLINASTART();
			tmp.PosAxis = new int[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	汎用出力直接制御コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	OUTPUTPAT
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=12)]
		public ushort[] OutputPat;			// 汎用出力1～3
		public static OUTPUTPAT Init(){
			OUTPUTPAT tmp = new OUTPUTPAT();
			tmp.OutputPat = new ushort[12];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	プログラム選択コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	PROGSEL
	{
		public short ProgSel;				// 選択プログラム番号
		public static PROGSEL Init(){
			return(new PROGSEL());
		}
	}

	// ------------------------------------------------------------------------
	//	軸速度オーバーライド変更コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	OVERCHG
	{
		public short Override;				// オーバーライド設定値
		public static OVERCHG Init(){
			return(new OVERCHG());
		}
	}

	// ------------------------------------------------------------------------
	//	主軸ＯＮ／ＯＦＦコマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	SPCMND
	{
		public short SpOut;					// 主軸指令フラグ
		public static SPCMND Init(){
			return(new SPCMND());
		}
	}

	// ------------------------------------------------------------------------
	//	主軸回転数変更コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	SPREVSET
	{
		public int SpRevo;					// 主軸回転数
		public static SPREVSET Init(){
			return(new SPREVSET());
		}
	}

	// ------------------------------------------------------------------------
	//	主軸回転数情報構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	SPREVDAT
	{
		public int ComSpRevo;				// 主軸指令回転数
		public int ActSpRevo;				// 主軸実回転数
		public static SPREVDAT Init(){
			return(new SPREVDAT());
		}
	}

	// ------------------------------------------------------------------------
	//	回転軸回転動作コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	SPINAX
	{
		public short OverFlag;				// オーバーライドフラグ
		public short AxisFlag;				// 移動軸選択フラグ
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] RevAx;					// 各軸回転数
		public static SPINAX Init(){
			SPINAX tmp = new SPINAX();
			tmp.RevAx = new int[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	汎用入力／出力強制制御コマンドパラメータサブ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	IO_CMD
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
		public byte[] IoBit;				// 汎用入出力D0~15ﾋﾞｯﾄ変更ｺﾏﾝﾄﾞ
		public static IO_CMD Init(){
			IO_CMD tmp = new IO_CMD();
			tmp.IoBit = new byte[16];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	汎用入力強制制御コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	COMPINPUT
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=116)]
		public IO_CMD[] InCmd;
		public static COMPINPUT Init(){
			COMPINPUT tmp = new COMPINPUT();
			tmp.InCmd = new IO_CMD[116];
			for(int cnt = tmp.InCmd.GetLowerBound(0); cnt <= tmp.InCmd.GetUpperBound(0); cnt++)
				tmp.InCmd[cnt] = Rt64ecdata.IO_CMD.Init();
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	汎用出力強制制御コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	COMPOUTPUT
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=64)]
		public IO_CMD[] OutCmd;
		public static COMPOUTPUT Init(){
			COMPOUTPUT tmp = new COMPOUTPUT();
			tmp.OutCmd = new IO_CMD[64];
			for(int cnt = tmp.OutCmd.GetLowerBound(0); cnt <= tmp.OutCmd.GetUpperBound(0); cnt++)
				tmp.OutCmd[cnt] = Rt64ecdata.IO_CMD.Init();
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	汎用入出力強制制御コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	COMPIOBIT
	{
		public short Pno;					// 入出力ポート番号
		public ushort Bno;					// 制御ビット番号
		public short flg;					// 制御フラグ
		public static COMPIOBIT Init(){
			return(new COMPIOBIT());
		}
	}

	// for COMPIOBIT.Pno
	public const short INPORT	= 0x0;			// 入力ポート指定
	public const short OUTPORT = -1 * 0x8000;	// 出力ポート指定

	// for COMPIOBIT.Flg
	public const short IONOTCARE = 0;			// 状態変更無し
	public const short IORELEASE = 1;			// 強制ＯＮ／ＯＦＦ終了
	public const short IOSET	 = 2;			// 強制ＯＮ
	public const short IORESET	= 3;			// 強制ＯＦＦ

	// ------------------------------------------------------------------------
	//		接線制御ON/OFFコマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	TLINESW
	{
		public short tlinesw;					// 接線制御機能ｽｲｯﾁ
		public static TLINESW Init(){
			return(new TLINESW());
		}
	}

	// ------------------------------------------------------------------------
	//		ティーチングステップ変更コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	STEPCHG
	{
		public int	StepNo;
		public static STEPCHG Init(){
			return(new STEPCHG());
		}
	}

	// ------------------------------------------------------------------------
	//		ＡＤサンプリングデータロギングコマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	ADSC
	{
		public short sc;
		public static ADSC Init(){
			return(new ADSC());
		}
	}

	// ------------------------------------------------------------------------
	//		ＴＰＣデータ選択コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	TPCSEL
	{
		public short ax1;
		public short hi1;
		public short ho1;
		public short ax2;
		public short hi2;
		public short ho2;
		public static TPCSEL Init(){
			return(new TPCSEL());
		}
	}

	// ------------------------------------------------------------------------
	//		ＴＰＣデータ選択コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	TPCSEL_HEADER	// ヘッダ情報
	{
		public short LogSel;				// ロギング種別選択
		public short TrigSel;				// トリガ選択
		public short Interval;				// ロギング周期[msec]
		public short Reserved;				// 予約
		public static TPCSEL_HEADER Init(){
			return(new TPCSEL_HEADER());
		}
	}

	// for TPCSEL_HEADER.LogSel (ロギング種別)
	public const short TPCH_LOG_POS	= 0;		// ポジション・入出力
	public const short TPCH_LOG_ECT	= 1;		// EtherCAT PDO
	public const short TPCH_LOG_64CH = 2;		// 64CH


	//----------
	// ポジション・入出力ロギング要求（旧ＴＰＣデータ）
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	TPCSEL_POS	// ポジション・入出力ロギング要求（旧ＴＰＣデータ）
	{
		public TPCSEL_HEADER h;
		public TPCSEL sel;
		public static TPCSEL_POS Init(){
			TPCSEL_POS tmp = new TPCSEL_POS();
			tmp.h = Rt64ecdata.TPCSEL_HEADER.Init();
			tmp.sel = Rt64ecdata.TPCSEL.Init();
			return tmp;
		}
	}


	//----------
	// EtherCAT通信ロギング要求
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct TPCSEL_ECT		// ヘッダ情報
	{
		public TPCSEL_HEADER	h;			// ヘッダ情報
		public short			ax_sel;		// ロギング軸選択(論理軸)
		public short			dt_sel;		// ロギングデータサイズ選択
		public short			sr_sel;		// ロギング通信方向選択
		public static TPCSEL_ECT Init(){
			TPCSEL_ECT tmp = new TPCSEL_ECT();
			tmp.h = Rt64ecdata.TPCSEL_HEADER.Init();
			return tmp;
		}
	}

	// for TPCSEL_ECT.byte_sel（通信データサイズ選択） ※ EtherCAT通信設定に合わせる必要はありません。
	public const short TPCS_ECT_DT_16BYTE	= 0;		// 16byteデータロギング
	public const short TPCS_ECT_DT_32BYTE	= 1;		// 32byteデータロギング
	public const short TPCS_ECT_DT_48BYTE	= 2;		// 48byteデータロギング
	public const short TPCS_ECT_DT_64BYTE	= 3;		// 64byteデータロギング

	// for TPCSEL_ECT.sr_sel	（送受信選択）
	public const short TPCS_ECT_RS_ALL		= 0;		// 送受信
	public const short TPCS_ECT_RS_SEND		= 1;		// 送信のみ
	public const short TPCS_ECT_RS_RECEIVE	= 2;		// 受信のみ

	//----------
	// 64CHロギング要求
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	TPCSEL_ATTR
	{
		public int 				item;					// 属性項目
		public int 				val;					// 属性値
		public static TPCSEL_ATTR Init(){
			return(new TPCSEL_ATTR());
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	TPCSEL_CH
	{
		public short			log_item;				// ロギング項目
		public byte				csv_out_type;	// CSV出力形式(セッティングPC用)
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=5)]
		public byte[]			Reserved;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
		public TPCSEL_ATTR[]	attribute;				// ロギング項目属性
		public static TPCSEL_CH Init(){
			TPCSEL_CH tmp = new TPCSEL_CH();
			tmp.Reserved = new byte[5];
			tmp.attribute = new TPCSEL_ATTR[4];
			for(int cnt = tmp.attribute.GetLowerBound(0); cnt <= tmp.attribute.GetUpperBound(0); cnt++)
				tmp.attribute[cnt] = Rt64ecdata.TPCSEL_ATTR.Init();
			return tmp;
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct TPCSEL_64CH
	{
		public TPCSEL_HEADER	h;						// ヘッダ情報
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=64)]
		public TPCSEL_CH[]		ch;						// ロギング軸選択(論理軸)
		public static TPCSEL_64CH Init(){
			TPCSEL_64CH tmp = new TPCSEL_64CH();
			tmp.h = Rt64ecdata.TPCSEL_HEADER.Init();
			tmp.ch = new TPCSEL_CH[64];
			for(int cnt = tmp.ch.GetLowerBound(0); cnt <= tmp.ch.GetUpperBound(0); cnt++)
				tmp.ch[cnt] = Rt64ecdata.TPCSEL_CH.Init();
			return tmp;
		}
	}

	// for TPCSEL_CH.log_item	（ロギング項目選択）
	//								-1			指定無し
	public const short TPCS_64_I_COMREG		= 0;		// 指令位置
	public const short TPCS_64_I_POSREG		= 1;		// 機械位置
	public const short TPCS_64_I_ERRREG		= 2;		// 偏差量
	public const short TPCS_64_I_BLOCKSEG	= 3;		// 最新ブロック払い出し量
	public const short TPCS_64_I_ABSREG		= 4;		// 絶対位置
	public const short TPCS_64_I_TRQ		= 5;		// トルク
	public const short TPCS_64_I_MCSTATUS	= 6;		// 全体ステータス
	public const short TPCS_64_I_MCALARM	= 7;		// 全体アラーム
	public const short TPCS_64_I_AXSTATUS	= 8;		// 軸ステータス
	public const short TPCS_64_I_AXALARM	= 9;		// 軸アラーム
	public const short TPCS_64_I_TASKSTATUS	= 10;		// タスクステータス
	public const short TPCS_64_I_TASKALARM	= 11;		// タスクアラーム
	public const short TPCS_64_I_MACRO		= 12;		// マクロ変数
	public const short TPCS_64_I_DI			= 13;		// 入力信号
	public const short TPCS_64_I_DO			= 14;		// 出力信号
	public const short TPCS_64_I_PDO_OBJ	= 15;		// PDOデータ(オブジェクト指定)
	public const short TPCS_64_I_RXPDO_DAT	= 16;		// RxPDOデータ(オフセット指定)
	public const short TPCS_64_I_TXPDO_DAT	= 17;		// TxPDOデータ(オフセット指定)
	public const short TPCS_64_I_TIME		= 18;		// 時計
	public const short TPCS_64_I_VCOMREG	= 19;		// 仮想指令位置
	public const short TPCS_64_I_RCOMREG	= 20;		// ロボット系指令位置
	public const short TPCS_64_I_RPOSREG	= 21;		// ロボット系機械位置
	public const short TPCS_64_I_RVCOMREG	= 22;		// ロボット系仮想指令位置

	// for TPCSEL_ATTR.item	（属性項目選択）
	//								-1			指定無し
	public const int TPCS_64_A_TASK			= 0;		// タスク番号
	public const int TPCS_64_A_AXIS			= 1;		// 軸番号
	public const int TPCS_64_A_MACRO		= 2;		// マクロ変数番号
	public const int TPCS_64_A_IO			= 3;		// IO信号番号
	public const int TPCS_64_A_PDO_IDX		= 4;		// PDOオブジェクトIndex
	public const int TPCS_64_A_PDO_SUBIDX	= 5;		// PDOオブジェクトSubindex
	public const int TPCS_64_A_PDO_DAT_OFS	= 6;		// PDOデータオフセット
	public const int TPCS_64_A_PDO_DAT_SZ	= 7;		// PDOデータサイズ(1-4[byte])

	// ------------------------------------------------------------------------
	//		座標系設定コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	COORDSET
	{
		public int AxisFlag;				// 軸選択フラグ
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] PosAxis;				// 各軸論理座標値
		public static COORDSET Init(){
			COORDSET tmp = new COORDSET();
			tmp.PosAxis = new int[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//		軸インタロック設定コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	AXINTLK
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public short[] IntlkSw;				// 軸インタロック指定スイッチ
		public static AXINTLK Init(){
			AXINTLK tmp = new AXINTLK();
			tmp.IntlkSw = new short[9];
			return tmp;
		}
	}


	// ------------------------------------------------------------------------
	//		軸ネグレクト設定コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	AXNGLCT
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public short[] NeglectSw;			// 軸ネグレクト指定スイッチ
		public static AXNGLCT Init(){
			AXNGLCT tmp = new AXNGLCT();
			tmp.NeglectSw = new short[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//		各軸サーボＯＮ／ＯＦＦコマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	SVONOFFCHG
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public short[] SvOnSw;				// ｻｰﾎﾞON/OFF指定スイッチ
		public static SVONOFFCHG Init(){
			SVONOFFCHG tmp = new SVONOFFCHG();
			tmp.SvOnSw = new short[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//		トルク制限モード変更コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	TRQLIMCHG
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public short[] TrqLimSw;			// トルク制限モード指定スイッチ
		public static TRQLIMCHG Init(){
			TRQLIMCHG tmp = new TRQLIMCHG();
			tmp.TrqLimSw = new short[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//		各軸制御モード変更コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	AXCTRLCHG
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public short[] CtrlSw;				// 制御モード指定スイッチ
		public static AXCTRLCHG Init(){
			AXCTRLCHG tmp = new AXCTRLCHG();
			tmp.CtrlSw = new short[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//		Ｍコード出力コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	OUTMCD
	{
		public short mcd;					// 出力Ｍコード
		public static OUTMCD Init(){
			return(new OUTMCD());
		}
	}

	// ------------------------------------------------------------------------
	//		手パモード構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	HANDLEMODE
	{
		public short mode; // 手パモード
		public static HANDLEMODE Init(){
			return(new HANDLEMODE());
		}
	}

	// ------------------------------------------------------------------------
	//		ロボット手パモード構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	RBTHANDLEMODE
	{
		public short mode; // 手パモード
		public static RBTHANDLEMODE Init(){
			return(new RBTHANDLEMODE());
		}
	}

	// ------------------------------------------------------------------------
	//		手パ倍率パラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	HANDLEKP
	{
		public int kp; // 手パ倍率
		public static HANDLEKP Init(){
			return(new HANDLEKP());
		}
	}

	// ------------------------------------------------------------------------
	//		手パ軸パラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	HANDLEAXIS
	{
		public int Axis; // 手パ有効軸
		public static HANDLEAXIS Init(){
			return(new HANDLEAXIS());
		}
	}

	// ------------------------------------------------------------------------
	//		サイクル運転モード変更コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	CYCLECHG
	{
		public short cycle;					// サイクル運転有効フラグ
		public static CYCLECHG Init(){
			return(new CYCLECHG());
		}
	}

	// ------------------------------------------------------------------------
	//	マクロ変数書込コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	MCRVALSET
	{
		public int RegNo;					// マクロ変数番号
		public int Val;						// マクロ変数値
		public static MCRVALSET Init(){
			return(new MCRVALSET());
		}
	}

	// ------------------------------------------------------------------------
	//	独立位置決めコマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	AXMV
	{
		public int AxFlg;					// 軸フラグ
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] PosAxis;				// 移動量/目標位置
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] Feed;					// 移動速度
		public static AXMV Init(){
			AXMV tmp = new AXMV();
			tmp.PosAxis = new int[9];
			tmp.Feed	= new int[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	トルク指令コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	TRQCMD
	{
		public int AxisFlag;				// 軸フラグ
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] Torque;				// 指令トルク [%]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] VClamp;				// 速度制限 [rpm] (-1:位置制御モード)
		public static TRQCMD Init(){
			TRQCMD tmp	= new TRQCMD();
			tmp.Torque	= new int[9];
			tmp.VClamp	= new int[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	速度制御指令コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	VELCMD
	{
		public int AxisFlag;				// 軸フラグ
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] Velocity;				// 速度指令 [指令単位/sec]
		public static VELCMD Init(){
			VELCMD tmp	= new VELCMD();
			tmp.Velocity	= new int[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	LIMA指令コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	LIMASTART
	{
		public int	PntNo;						// ポイント番号
		public int	AxisFlag;					// 軸フラグ
		public int	Feed;						// 補間送り速度 [pls/sec]
		public static LIMASTART Init(){
			return(new LIMASTART());
		}
	}
	// ------------------------------------------------------------------------
	//	プログラムブロック 移動コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	BLKMVDATA
	{
		public short SrcPno;				// 移動対象P番号(1-32767)
		public short DstBlk;				// 移動先先頭BLOCK番号(0～63)
		public static BLKMVDATA Init(){
			return(new BLKMVDATA());
		}
	}

	// ------------------------------------------------------------------------
	//	プログラムブロック コピーコマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	BLKCPYDATA
	{
		public short SrcPno;				// コピー元P番号(1-32767)
		public short DstBlk;				// コピー先先頭BLOCK番号(0～63)
		public short DstPno;				// コピー後P番号(1-32767)
		public short DstTask;				// コピー後タスク番号(0-7)
		public static BLKCPYDATA Init(){
			return(new BLKCPYDATA());
		}
	}

	// ------------------------------------------------------------------------
	//	プログラムブロック 削除コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	BLKDLDATA
	{
		public short Pno;					// 削除対象のP番号(1-32767)
		public static BLKDLDATA Init(){
			return(new BLKDLDATA());
		}
	}

	// ------------------------------------------------------------------------
	//	EtherCATパラメータ保存構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	ECTPRMSAVE
	{
		public int StationNo;				// 局番号
		public static ECTPRMSAVE Init(){
			return(new ECTPRMSAVE());
		}
	}

	// ------------------------------------------------------------------------
	//	ロボット座標系補間移動開始コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
	public struct RLINSTART
	{
		public int AxisFlag;				// 移動軸選択フラグ
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
		public int[] IncAxis;				// ｲﾝｸﾘﾒﾝﾄ移動量
		public int Feed;					// 補間送り速度
		public static RLINSTART Init()
		{
			RLINSTART tmp = new RLINSTART();
			tmp.IncAxis = new int[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	ロボット座標系補間移動開始コマンドパラメータ構造体（ＡＢＳＯ）
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	RLINASTART
	{
		public int AxisFlag;				// 移動軸選択フラグ
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] PosAxis;				// 位置決めﾎﾟｼﾞｼｮﾝ
		public int Feed;					// 補間送り速度
		public static RLINASTART Init(){
			RLINASTART tmp	= new RLINASTART();
			tmp.PosAxis	= new int[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	ツール系補間移動開始コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	RTLINSTART
	{
		public int AxisFlag;				// 移動軸選択フラグ
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] IncAxis;				// 軸移動量
		public int Feed;					// 補間送り速度
		public static RTLINSTART Init(){
			RTLINSTART tmp	= new RTLINSTART();
			tmp.IncAxis	= new int[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	ツール系補間移動開始コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	RJMOVPSTART
	{
		public int PntNo;					// ポイント番号
		public int AxisFlag;				// 移動軸選択フラグ
		public static RJMOVPSTART Init(){
			return(new RJMOVPSTART());
		}
	}

	// ------------------------------------------------------------------------
	//	ロボット関節移動開始コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	RJMOVSTART
	{
		public int AxisFlag;				// 移動軸選択フラグ
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] IncAxis;				// 軸移動量
		public static RJMOVSTART Init(){
			RJMOVSTART tmp	= new RJMOVSTART();
			tmp.IncAxis	= new int[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	ロボットアブソ関節移動開始コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	RJMOVASTART
	{
		public int AxisFlag;				// 移動軸選択フラグ
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] PosAxis;				// 軸移動量
		public static RJMOVASTART Init(){
			RJMOVASTART tmp	= new RJMOVASTART();
			tmp.PosAxis	= new int[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	ロボット姿勢切り替え開始コマンドパラメータ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	RBTFIGCHG
	{
		public int RbtFigFlag;				// ロボット姿勢選択フラグ
											//   6軸垂直多関節   ：0-7
											//   スカラ：0:左腕、1:右腕
		public int Feed;					// 送り速度
		public static RBTFIGCHG Init(){
			return(new RBTFIGCHG());
		}
	}

	// ------------------------------------------------------------------------
	//	ワーク座標定義データ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	RBTWORKPRM
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=64*6)]
		public int[] WorkOfs;				// X方向、Y方向、Z方向、X軸周り、Y軸周り、Z軸周り
		public static RBTWORKPRM Init(){
			RBTWORKPRM tmp	= new RBTWORKPRM();
			tmp.WorkOfs	= new int[24];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	ワーク座標情報データ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	RBTWORKSTS
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=6)]
		public int[] WorkOfs;				// X方向、Y方向、Z方向、X軸周り、Y軸周り、Z軸周り
		public short Work;					// ワーク座標系番号

		[MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
		public byte[] Reserved;
		public static RBTWORKSTS Init(){
			RBTWORKSTS tmp	= new RBTWORKSTS();
			tmp.WorkOfs	= new int[6];
			tmp.Reserved = new byte[2];
			return tmp;
		}
	}

	// -----------------------------------------------------------------
	//	ツール座標定義データ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	RBTTOOLPRM
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=64*6)]
		public int[] ToolOfs;				// X方向、Y方向、Z方向、X軸周り、Y軸周り、Z軸周り
		public static RBTTOOLPRM Init(){
			RBTTOOLPRM tmp	= new RBTTOOLPRM();
			tmp.ToolOfs	= new int[24];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	ツール座標情報データ構造体
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	RBTTOOLSTS
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=6)]
		public int[] ToolOfs;				// X方向、Y方向、Z方向、X軸周り、Y軸周り、Z軸周り
		public short Tool;					// ツール座標系番号

		[MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
		public byte[] Reserved;
		public static RBTTOOLSTS Init(){
			RBTTOOLSTS tmp	= new RBTTOOLSTS();
			tmp.ToolOfs	= new int[6];
			tmp.Reserved = new byte[2];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//	ツール座標系切り替えデータ構造体										
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	RBTTOOLCHG
	{
		public short Tool;				// ツール座標系番号
		public static RBTTOOLCHG Init(){
			return(new RBTTOOLCHG());
		}
	}

	// ------------------------------------------------------------------------
	//	ワーク座標系切り替えデータ構造体									
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	RBTWORKCHG
	{
		public short Work;				// ワーク座標系番号
		public static RBTWORKCHG Init(){
			return(new RBTWORKCHG());
		}
	}


	// ------------------------------------------------------------------------
	//	タスク番号指定定数
	// ------------------------------------------------------------------------
	public const short TASKMAX = 8;							// タスク最大数(0～7)

	public const short TOOLMAX		= 	64;							/* 各タスクツール設定最大数	*/
	public const short WORKMAX		= 	64;							/* 各タスクワーク座標設定最大数	*/
	public const short TOOLPARAMNUM	= 	6;							/* 各タスク座標設定項目数	*/
	public const short WORKPARAMNUM	= 	6;							/* 各タスク座標設定項目数	*/

}
