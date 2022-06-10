using System;
using System.Runtime.InteropServices;
using System.Text;

class Rt64ectcd64api
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
	//	テクノコードプログラム変換関数プロトタイプ
	// ------------------------------------------------------------------------
	[DllImport("rt64ectcnv64.dll")] public extern static short MemProgramConv(string text, ref int BinCnvSize, [Out()] byte[] bin, ref short pno);
	[DllImport("rt64ectcnv64.dll")] public extern static short FileProgramConv(string fname, ref int BinCnvSize, [Out()] byte[] bin, ref short pno);
	[DllImport("rt64ectcnv64.dll")] public extern static short MemTaskProgramConv(string text, ref int BinCnvSize, [Out()] byte[] bin, short Task, ref short pno);
	[DllImport("rt64ectcnv64.dll")] public extern static short FileTaskProgramConv(string fname, ref int BinCnvSize, [Out()] byte[] bin, short Task, ref short pno);
	[DllImport("rt64ectcnv64.dll")] public extern static short MemProgramRConv([In()] byte[] bin, ref int TextCnvSize, StringBuilder text);
	[DllImport("rt64ectcnv64.dll")] public extern static short FileProgramRConv(string fname, ref int TextCnvSize, StringBuilder text);
	[DllImport("rt64ectcnv64.dll")] public extern static short DncProgramConv(string text, ref int BinCnvSize, [Out()] byte[] bin, short cont);

	[DllImport("rt64ectcnv64.dll")] public extern static short TcdCircleSet(short CirSize);
	[DllImport("rt64ectcnv64.dll")] public extern static short TcdSetProgMemSize(short mflg);
	[DllImport("rt64ectcnv64.dll")] public extern static short TcdFeedUnitSet(int pulse, short timef);
	[DllImport("rt64ectcnv64.dll")] public extern static short TcdPositionUnitSet(short flag);
	[DllImport("rt64ectcnv64.dll")] public extern static short TcdPaccTimeSet(short flag);
	[DllImport("rt64ectcnv64.dll")] public extern static short TcdSTSelect(short SlxType);
	[DllImport("rt64ectcnv64.dll")] public extern static short TcdCircleMode(short mode);
	[DllImport("rt64ectcnv64.dll")] public extern static short TcdDiametralAxis(short ax);
	[DllImport("rt64ectcnv64.dll")] public extern static short TcdSetMultiplier(int[] tbl);
	[DllImport("rt64ectcnv64.dll")] public extern static int   TcdGetErrLine();
	[DllImport("rt64ectcnv64.dll")] public extern static short TcdSetZXPlane(short zx_flg);

	// ｾｰﾌﾃｨ版変換API
	[DllImport("rt64ectcnv64.dll")] public extern static short MemProgramConvS(string text, int BinBufSize, ref int BinCnvSize, [Out()] byte[] bin, ref short pno);
	[DllImport("rt64ectcnv64.dll")] public extern static short FileProgramConvS(string fname, int BinBufSize, ref int BinCnvSize, [Out()] byte[] bin, ref short pno);
	[DllImport("rt64ectcnv64.dll")] public extern static short MemTaskProgramConvS(string text, int BinBufSize, ref int BinCnvSize, [Out()] byte[] bin, short task, ref short pno);
	[DllImport("rt64ectcnv64.dll")] public extern static short FileTaskProgramConvS(string fname, int BinBufSize, ref int BinCnvSize, [Out()] byte[] bin, short task, ref short pno);
	[DllImport("rt64ectcnv64.dll")] public extern static short MemProgramRConvS([In()] byte[] bin, int TextBufSize, ref int TextCnvSize, StringBuilder text);
	[DllImport("rt64ectcnv64.dll")] public extern static short FileProgramRConvS(string fname, int TextBufSize, ref int TextCnvSize, StringBuilder text);
	[DllImport("rt64ectcnv64.dll")] public extern static short DncProgramConvS(string text, int BinBufSize, ref int BinCnvSize, [Out()] byte[] bin, short cont);

	//マルチスレッド拡張定義
	[DllImport("rt64ectcnv64.dll")] public extern static short InitTcdProcEx(ref SXCNVDEF psxcnvdef, ref int phTcd);
	[DllImport("rt64ectcnv64.dll")] public extern static short QuitTcdProcEx(int hTcd);
	[DllImport("rt64ectcnv64.dll")] public extern static short MemProgramConvEx(int hTcd, string text, int BinBufSize, ref int BinCnvSize, [Out()] byte[] bin, ref short pno);
	[DllImport("rt64ectcnv64.dll")] public extern static short FileProgramConvEx(int hTcd, string fname, int BinBufSize, ref int BinCnvSize, [Out()] byte[] bin, ref short pno);
	[DllImport("rt64ectcnv64.dll")] public extern static short MemTaskProgramConvEx(int hTcd, string text, int BinBufSize, ref int BinCnvSize, [Out()] byte[] bin, short Task, ref short pno);
	[DllImport("rt64ectcnv64.dll")] public extern static short FileTaskProgramConvEx(int hTcd, string fname, int BinBufSize, ref int BinCnvSize, [Out()] byte[] bin, short Task, ref short pno);
	[DllImport("rt64ectcnv64.dll")] public extern static short MemProgramRConvEx(int hTcd, [In()] byte[] bin, int TextBufSize, ref int TextCnvSize, StringBuilder text);
	[DllImport("rt64ectcnv64.dll")] public extern static short FileProgramRConvEx(int hTcd, string fname, int TextBufSize, ref int TextCnvSize, StringBuilder text);
	[DllImport("rt64ectcnv64.dll")] public extern static short DncProgramConvEx(int hTcd, string text, int BinBufSize, ref int BinCnvSize, [Out()] byte[] bin, short cont);

	[DllImport("rt64ectcnv64.dll")] public extern static short TcdCircleSetEx(int hTcd, short CirSize);
	[DllImport("rt64ectcnv64.dll")] public extern static short TcdSetProgMemSizeEx(int hTcd, short mflg);
	[DllImport("rt64ectcnv64.dll")] public extern static short TcdFeedUnitSetEx(int hTcd, int pulse, short timef);
	[DllImport("rt64ectcnv64.dll")] public extern static short TcdPositionUnitSetEx(int hTcd, short flag);
	[DllImport("rt64ectcnv64.dll")] public extern static short TcdPaccTimeSetEx(int hTcd, short flag);
	[DllImport("rt64ectcnv64.dll")] public extern static short TcdSTSelectEx(int hTcd, short SlxType);
	[DllImport("rt64ectcnv64.dll")] public extern static short TcdCircleModeEx(int hTcd, short mode);
	[DllImport("rt64ectcnv64.dll")] public extern static short TcdDiametralAxisEx(int hTcd, short ax);
	[DllImport("rt64ectcnv64.dll")] public extern static short TcdSetMultiplierEx(int hTcd, int[] tbl);
	[DllImport("rt64ectcnv64.dll")] public extern static int   TcdGetErrLineEx(int hTcd);
	[DllImport("rt64ectcnv64.dll")] public extern static short TcdSetZXPlaneEx(int hTcd, short zx_flg);
}
