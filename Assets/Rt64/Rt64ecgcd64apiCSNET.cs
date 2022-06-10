using System;
using System.Runtime.InteropServices;
using System.Text;

class Rt64ecgcd64api
{
	// ------------------------------------------------------------------------
	//	T/Gコード変換初期化構造体定義
	// ------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
	public struct  SXCNVDEF
	{
		public int nSize;				// 変換初期化構造体サイズ
		public int FeedUnitPulse;		// 補間移動時Ｆ単位時間あたりの移動量[pulse](1-200000)
		public short FeedUnitTimeF;		// 補間移動時Ｆ単位時間フラグ ( 0:sec / 1:min )
		public short CircleAccuracy;	// 円弧補間計算精度[pulse](1-1000)
		public short SamplingTime;		// RTMC64-EC制御周期[μsec](25~4000us)
		public short PaccTime;			// プリ解加減速時間[msec]
		public short PositionUnit;		// 座標/移動量/円弧半径小数点単位指定(0:1, 1:10, 2:100, 3:1000, 4:10000, 5:100000)
		public short CircleMode;		// 円弧補間解析モード（0:ﾌﾟﾘ解､1:SLX内部解析）
		public short DiametralAxis;		// 直径指令軸フラグ
		public short ZXPlane;			// ZX平面指定フラグ（0:ＸＺ平面、1:ＺＸ平面）
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=9)]
		public int[] Multiplier;		// 各軸小数点位置(1-100000 ※必ず10^nで指定すること)
		public int   alignment0;
		public static SXCNVDEF Init(){
			SXCNVDEF tmp = new SXCNVDEF();
			tmp.Multiplier = new int[9];
			return tmp;
		}
	}

	// ------------------------------------------------------------------------
	//		ラベル定義
	// ------------------------------------------------------------------------
	public const short SEC_UNIT = 0;				// 秒単位指定
	public const short MIN_UNIT = 1;				// 分単位指定

	// ------------------------------------------------------------------------
	//		エラーコード
	// ------------------------------------------------------------------------
	public const short E_PRG_OK				= (0);		// エラーなし
	// 変換エラー
	public const short E_PRG_FILE			= (0x1000);	// ﾌﾟﾛｸﾞﾗﾑﾌｧｲﾙｴﾗｰ
	public const short E_PRG_BUFFOVRFLW		= (0x2000);	// ﾌﾟﾛｸﾞﾗﾑﾊﾞｯﾌｧｵｰﾊﾞｰﾌﾛｰ
	public const short E_PRG_FORMAT			= (0x3000);	// ﾌﾟﾛｸﾞﾗﾑﾌｫｰﾏｯﾄｴﾗｰ
	public const short E_PRG_CNVCALC		= (0x4000);	// ﾌﾟﾛｸﾞﾗﾑ変換計算ｴﾗｰ
	public const short E_PRG_WRKMEMOVRFLW	= (0x5000);	// 作業ﾒﾓﾘｵｰﾊﾞｰﾌﾛｰ
	public const short E_PRG_STPNOMASK		= (0x0fff);	// ｽﾃｯﾌﾟ番号ﾏｽｸﾊﾟﾀｰﾝ
	// 変換ハンドルエラー
	public const short E_PRG_DEVNRDY		= (-8);		// 変換ﾗｲﾌﾞﾗﾘ未初期化ｴﾗｰ
	public const short E_PRG_PARAM			= (-9);		// 変換ﾗｲﾌﾞﾗﾘ初期化ﾊﾟﾗﾒｰﾀｴﾗｰ
	public const short E_PRG_MEMACQ			= (-10);	// 内部処理用ﾒﾓﾘ取得ｴﾗｰ
	public const short E_PRG_MUTXAQC		= (-11);	// 同期ｵﾌﾞｼﾞｪｸﾄ取得ｴﾗｰ
	public const short E_PRG_ABANDONED		= (-12);	// 同期ｵﾌﾞｼﾞｪｸﾄ破棄ｴﾗｰ
	public const short E_PRG_EMPTYHANDLE	= (-13);	// 空きﾌﾟﾛｸﾞﾗﾑ変換ﾊﾝﾄﾞﾙ無し
	public const short E_PRG_NOHANDLE		= (-14);	// 無効ﾌﾟﾛｸﾞﾗﾑ変換ﾊﾝﾄﾞﾙ
	public const short E_PRG_BUSY			= (-15);	// ﾌﾟﾛｸﾞﾗﾑ変換ﾋﾞｼﾞｰ

	// ------------------------------------------------------------------------
	//	Ｇコードプログラム変換関数プロトタイプ
	// ------------------------------------------------------------------------
	[DllImport("rt64ecgcnv64.dll")] public extern static short MemGcodeConv(string text, ref int BinCnvSize, [Out()] byte[] bin, ref short pno);
	[DllImport("rt64ecgcnv64.dll")] public extern static short FileGcodeConv(string fname, ref int BinCnvSize, [Out()] byte[] bin, ref short pno);
	[DllImport("rt64ecgcnv64.dll")] public extern static short MemTaskGcodeConv(string text, ref int BinCnvSize, [Out()] byte[] bin, short Task, ref short pno);
	[DllImport("rt64ecgcnv64.dll")] public extern static short FileTaskGcodeConv(string fname, ref int BinCnvSize, [Out()] byte[] bin, short Task, ref short pno);
	[DllImport("rt64ecgcnv64.dll")] public extern static short MemGcodeRConv([In()] byte[] bin, ref int TextCnvSize, StringBuilder text);
	[DllImport("rt64ecgcnv64.dll")] public extern static short FileGcodeRConv(string fname, ref int TextCnvSize, StringBuilder text);
	[DllImport("rt64ecgcnv64.dll")] public extern static short DncGcodeConv(string text, ref int BinCnvSize, [Out()] byte[] bin, short cont);

	[DllImport("rt64ecgcnv64.dll")] public extern static short GcdCircleSet(short CirSize);
	[DllImport("rt64ecgcnv64.dll")] public extern static short GcdSetProgMemSize(short mflg);
	[DllImport("rt64ecgcnv64.dll")] public extern static short GcdFeedUnitSet(int pulse, short timef);
	[DllImport("rt64ecgcnv64.dll")] public extern static short GcdPositionUnitSet(short flag);
	[DllImport("rt64ecgcnv64.dll")] public extern static short GcdPaccTimeSet(short flag);
	[DllImport("rt64ecgcnv64.dll")] public extern static short GcdSTSelect(short SlxType);
	[DllImport("rt64ecgcnv64.dll")] public extern static short GcdCircleMode(short mode);
	[DllImport("rt64ecgcnv64.dll")] public extern static short GcdDiametralAxis(short ax);
	[DllImport("rt64ecgcnv64.dll")] public extern static short GcdSetMultiplier(int[] tbl);
	[DllImport("rt64ecgcnv64.dll")] public extern static int   GcdGetErrLine();
	[DllImport("rt64ecgcnv64.dll")] public extern static short GcdSetZXPlane(short zx_flg);

	// ｾｰﾌﾃｨ版変換API
	[DllImport("rt64ecgcnv64.dll")] public extern static short MemGcodeConvS(string text, int BinBufSize, ref int BinCnvSize, [Out()] byte[] bin, ref short pno);
	[DllImport("rt64ecgcnv64.dll")] public extern static short FileGcodeConvS(string fname, int BinBufSize, ref int BinCnvSize, [Out()] byte[] bin, ref short pno);
	[DllImport("rt64ecgcnv64.dll")] public extern static short MemTaskGcodeConvS(string text, int BinBufSize, ref int BinCnvSize, [Out()] byte[] bin, short task, ref short pno);
	[DllImport("rt64ecgcnv64.dll")] public extern static short FileTaskGcodeConvS(string fname, int BinBufSize, ref int BinCnvSize, [Out()] byte[] bin, short task, ref short pno);
	[DllImport("rt64ecgcnv64.dll")] public extern static short MemGcodeRConvS([In()] byte[] bin, int TextBufSize, ref int TextCnvSize, StringBuilder text);
	[DllImport("rt64ecgcnv64.dll")] public extern static short FileGcodeRConvS(string fname, int TextBufSize, ref int TextCnvSize, StringBuilder text);
	[DllImport("rt64ecgcnv64.dll")] public extern static short DncGcodeConvS(string text, int BinBufSize, ref int BinCnvSize, [Out()] byte[] bin, short cont);

	//マルチスレッド拡張定義
	[DllImport("rt64ecgcnv64.dll")] public extern static short InitGcdProcEx(ref SXCNVDEF psxcnvdef, ref int phGcd);
	[DllImport("rt64ecgcnv64.dll")] public extern static short QuitGcdProcEx(int hGcd);
	[DllImport("rt64ecgcnv64.dll")] public extern static short MemGcodeConvEx(int hGcd, string text, int BinBufSize, ref int BinCnvSize, [Out()] byte[] bin, ref short pno);
	[DllImport("rt64ecgcnv64.dll")] public extern static short FileGcodeConvEx(int hGcd, string fname, int BinBufSize, ref int BinCnvSize, [Out()] byte[] bin, ref short pno);
	[DllImport("rt64ecgcnv64.dll")] public extern static short MemTaskGcodeConvEx(int hGcd, string text, int BinBufSize, ref int BinCnvSize, [Out()] byte[] bin, short Task, ref short pno);
	[DllImport("rt64ecgcnv64.dll")] public extern static short FileTaskGcodeConvEx(int hGcd, string fname, int BinBufSize, ref int BinCnvSize, [Out()] byte[] bin, short Task, ref short pno);
	[DllImport("rt64ecgcnv64.dll")] public extern static short MemGcodeRConvEx(int hGcd, [In()] byte[] bin, int TextBufSize, ref int TextCnvSize, StringBuilder text);
	[DllImport("rt64ecgcnv64.dll")] public extern static short FileGcodeRConvEx(int hGcd, string fname, int TextBufSize, ref int TextCnvSize, StringBuilder text);
	[DllImport("rt64ecgcnv64.dll")] public extern static short DncGcodeConvEx(int hGcd, string text, int BinBufSize, ref int BinCnvSize, [Out()] byte[] bin, short cont);

	[DllImport("rt64ecgcnv64.dll")] public extern static short GcdCircleSetEx(int hGcd, short CirSize);
	[DllImport("rt64ecgcnv64.dll")] public extern static short GcdFeedUnitSetEx(int hGcd, int pulse, short timef);
	[DllImport("rt64ecgcnv64.dll")] public extern static short GcdPositionUnitSetEx(int hGcd, short flag);
	[DllImport("rt64ecgcnv64.dll")] public extern static short GcdPaccTimeSetEx(int hGcd, short flag);
	[DllImport("rt64ecgcnv64.dll")] public extern static short GcdSTSelectEx(int hGcd, short SlxType);
	[DllImport("rt64ecgcnv64.dll")] public extern static short GcdCircleModeEx(int hGcd, short mode);
	[DllImport("rt64ecgcnv64.dll")] public extern static short GcdDiametralAxisEx(int hGcd, short ax);
	[DllImport("rt64ecgcnv64.dll")] public extern static short GcdSetMultiplierEx(int hGcd, int[] tbl);
	[DllImport("rt64ecgcnv64.dll")] public extern static int   GcdGetErrLineEx(int hGcd);
	[DllImport("rt64ecgcnv64.dll")] public extern static short GcdSetZXPlaneEx(int hGcd, short zx_flg);

}
