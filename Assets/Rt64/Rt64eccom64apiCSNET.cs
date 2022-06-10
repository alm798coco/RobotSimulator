using System;
using System.Runtime.InteropServices;

class Rt64eccom64api
{
	// ------------------------------------------------------------------------
	//	通信初期化構造体定義
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct	SXDEF
	{
		public int nSize;				// 通信初期化構造体サイズ
		public short fComType;			// 通信種別フラグ
		public short fShare;			// 共有フラグ
		public int fLogging;			// 通信ロギングフラグ
		public int alignment0;
		[MarshalAs(UnmanagedType.LPStr)]
		public string pLogFile;			// 通信ロギングファイル名
		[MarshalAs(UnmanagedType.LPStr)]
		public string pNodeName;		// INtimeノード名
		public int LogFileMaxSz;		// ロギングファイル最大サイズ
		public int alignment1;
		public short LogFileMaxNum;		// ロギングファイル最大数
		public short Reserved;		// 予約
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
		public byte[] nIpAddr;			// IPアドレス
		public static SXDEF Init(){
			SXDEF tmp = new SXDEF();
			tmp.nIpAddr = new byte[4];
			return tmp;
		}
	}

	// ----------------------------------------------
	// 通信種別コード (for SXDEF.fComType)
	// ----------------------------------------------
	public const short COMSHAREMEM	= 0;			// 共有メモリ通信
	public const short COMETHERNET	= 1;			// イーサネット通信

	// ----------------------------------------------
	// 通信ロギングフラグ (for SXDEF.fLogging)
	// ----------------------------------------------
	public const int COML_DISABLE	= 0x1;			// ロギング無効
	public const int COML_ALL		= 0x2;			// 常時ロギング
	public const int COML_ALL_INIT	= 0x4;			// 常時ロギング(初期化/終了のみ)
	public const int COML_ALL_SND	= 0x8;			// 常時ロギング(データ送信のみ)
	public const int COML_ALL_RCV	= 0x10;			// 常時ロギング(データ受信のみ)
	public const int COML_ALL_REQ	= 0x20;			// 常時ロギング(コマンド要求のみ)
	public const int COML_ERR		= 0x40;			// エラーロギング
	public const int COML_HERR		= 0x80;			// エラーロギング（伝送エラーのみ）
	public const int COML_RETRY		= 0x100;		// リトライロギング

	// ------------------------------------------------------------------------
	//   共通通信関数プロトタイプ
	// ------------------------------------------------------------------------
	[DllImport("rt64eccomnt64.dll")]	public extern static int InitCommProc(ref SXDEF psxdef, ref int	phCom);
	[DllImport("rt64eccomnt64.dll")]	public extern static int QuitCommProc(int hCom);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, IntPtr data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, [In] byte[] data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.PARAMETER_DATA data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.PITCH_ERR_REV data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.TOOL_H data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.VARIABLE data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.MCRREG data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.TOOL_D data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.ACO_PRM data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.RBTPRM data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.RBTDELTAPRM data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.RBTSCARAPRM data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.RBT6AXISPRM data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.RBTWORKPRM data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.RBTTOOLPRM data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.AXIS_POINT data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.RAXIS_POINT data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.ABSENCOFS data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.ECTOBJSEL data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.ECTOBJVAL_BT data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.ECTOBJVAL_WD data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.ECTOBJVAL_DW data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.ECTOBJVAL_STR data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.BINPRG_BLOCK data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref short data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref int data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, IntPtr	data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, [Out()] byte[] data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.PARAMETER_DATA data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.STATUS data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.IODATA data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.DNCBUFI data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.PITCH_ERR_REV data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.SENSEPOS data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.TOOL_H data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.TEACHSTS data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.VARIABLE data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.TOOL_D data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.RTCTIME data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.FORTIME data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.CYCLETIME data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.TPCINFO data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.TPCINFOEX data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.TPCDAT_POS data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.ROMVERSION data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.BINPRG_BLOCK data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.MCRREG data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.ECT_INFOAX data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.ECT_INFOALL data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.ECT_MON data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.ECT_PDO_MAP data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.HANDLESTS data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.ACO_PRM data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.RBTPRM data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.RBTDELTAPRM data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.RBTSCARAPRM data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.RBT6AXISPRM data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.RBTWORKPRM data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.RBTTOOLPRM data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.RBTWORKSTS data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.RBTTOOLSTS data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.TOOLHSTS data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.TOOLDSTS data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.TOOLDERR data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.AXIS_POINT data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.RAXIS_POINT data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.ABSENCOFS data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.ECTOBJSEL data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.ECTOBJVAL_BT data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.ECTOBJVAL_WD data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.ECTOBJVAL_DW data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.ECTOBJVAL_STR data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.ASSAXISTBL data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.PRGBLK_INF data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.SPREVDAT data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.RBTPOSINFO data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.RBTERRINF data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.RBTFIGINFO data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.MOTPOSINF data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref short data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref int data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, IntPtr data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.MODECHG data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.JOGSTART data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.ZRNSTART data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.GENERATION data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.PTPSTART data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.PTPASTART data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.LINSTART data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.LINASTART data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.OUTPUTPAT data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.PROGSEL data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.SPCMND data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.SPREVSET data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.SLINSTART data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.SLINASTART data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.COMPINPUT data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.COMPOUTPUT data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.COMPIOBIT data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.OVERCHG data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.TPCSEL data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.TPCSEL_POS data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.TPCSEL_ECT data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.TPCSEL_64CH data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.TPCSEL_HEADER data);		// REQ_TPCSEL_EX
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.COORDSET data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.AXINTLK data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.AXNGLCT data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.SVONOFFCHG data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.TRQLIMCHG data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.AXCTRLCHG data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.OUTMCD data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.SPINAX data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.TLINESW data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.STEPCHG data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.HANDLEMODE data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.HANDLEKP data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.HANDLEAXIS data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.CYCLECHG data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.MCRVALSET data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.AXMV data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.TRQCMD data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.VELCMD data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.LIMASTART data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.BLKMVDATA data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.BLKCPYDATA data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.BLKDLDATA data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.ECTPRMSAVE data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.RLINSTART data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.RLINASTART data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.RTLINSTART data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.RJMOVPSTART data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.RJMOVSTART data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.RJMOVASTART data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.RBTFIGCHG data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.RBTTOOLCHG data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.RBTWORKCHG data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.RBTHANDLEMODE data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref short data);
	[DllImport("rt64eccomnt64.dll")]	public extern static int SendCommand(int hCom, short cmnd, short task, ref int data);

	// ------------------------------------------------------------------------
	//	タイマー関数プロトタイプ
	// ------------------------------------------------------------------------
	[DllImport("rt64eccomnt64.dll")]	public extern static void SetTimeOut(ref int origin);
	[DllImport("rt64eccomnt64.dll")]	public extern static bool CheckTimeOut(int origin, int limit);

}