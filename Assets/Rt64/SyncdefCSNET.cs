using System;
using System.Runtime.InteropServices;

class Syncdef
{
	// ========================================================================
	//	データ種別コード
	// ========================================================================
	public const short	DAT_PARAMETER		= 0x0;	// サーボパラメータ
	public const short	DAT_PROGRAM			= 0x1;	// 運転プログラム
	public const short	DAT_STATUS			= 0x2;	// サーボステータス
	public const short	DAT_IODATA			= 0x3;	// I/O信号状態
	public const short	DAT_OPTIONPRM		= 0x9;	// オプションパラメータ
	public const short	DAT_DNCDATA			= 0xE;	// ＤＮＣデータ
	public const short	DAT_DNCBUFI			= 0xF;	// ＤＮＣバッファ情報
	public const short	DAT_PITCHERR		= 0x10;	// ピッチエラー補正用パラメータ
	public const short	DAT_SENSEPOS		= 0x11;	// センサーラッチ位置情報
	public const short	DAT_TOOLHOFS		= 0x12;	// 工具長補正データ
	public const short	DAT_FORCEIO			= 0x13;	// 強制入出力ビットデータ
	public const short	DAT_TEACHSTS		= 0x18;	// ティーチング情報
	public const short	DAT_VARIABLE		= 0x19;	// マクロ変数データ
	public const short	DAT_TOOLDIA			= 0x1a;	// 工具径補正データ
	public const short	DAT_PRGBLKINFO		= 0x1b;	// プログラムブロック情報
	public const short	DAT_ABSENCOFS		= 0x1c;	// 絶対値エンコーダオフセット
	public const short	DAT_RTC				= 0x21;	// 制御周期処理時間情報
	public const short	DAT_ADDATA			= 0x22;	// Ａ／Ｄ＆ＰＯＳ情報
	public const short	DAT_ADLOG			= 0x23;	// Ａ／Ｄ＆ＰＯＳロギング情報
	public const short	DAT_TPCINFO			= 0x24;	// ＴＰＣロギング情報
	public const short	DAT_TPCDATA			= 0x25;	// ＴＰＣロギングデータ
	//public const short					= 0x26;	//
	public const short	DAT_VERSION			= 0x27;	// ＲＯＭバージョンデータ
	public const short	DAT_AXNEGLECT		= 0x28;	// 軸無効設定情報
	public const short	DAT_AXINTLOCK		= 0x29;	// 軸インタロック設定情報
	public const short	DAT_TPCINFO_EX		= 0x2a;	// ＴＰＣロギング情報
	public const short	DAT_ONEBLOCK		= 0x30;	// プログラム１ブロックデータ
	public const short	DAT_AXSVONEN		= 0x31;	// 軸サーボＯＮ有効設定情報
	public const short	DAT_HANDLESTS		= 0x32;	// 手パモード設定情報
	public const short	DAT_MCRREG			= 0x33;	// マクロ変数読出/書込
	public const short	DAT_ECT_INFO		= 0x34;	// EtherCAT情報読み出し
	public const short	DAT_ECT_MON			= 0x35;	// EtherCAT送受信データ読み出し
	public const short	DAT_ECT_PDO_MAP		= 0x36;	// EtherCATPDOマッピング読出し
	public const short	DAT_ACOPARAM		= 0x37;	// 補間前加減速ﾊﾟﾗﾒｰﾀ読出/書込
	public const short	DAT_TOOLHSTS		= 0x39;	// 工具長補正設定情報
	public const short	DAT_TOOLDSTS		= 0x3a;	// 工具径補正設定情報
	public const short	DAT_POINTTABLE		= 0x3b;	// 位置決めポイントテーブル読出/書込
	public const short	DAT_ASSAXISTBL		= 0x3c;	// 軸割り当て情報読み出し
	public const short	DAT_TOOLDERR		= 0x3d;	// 工具径補正エラー情報
	public const short	DAT_VERPER			= 0x40;	// ＶＥＲ／ＰＥＲ値
	public const short	DAT_SPREV			= 0x41;	// 主軸回転数読出し
	public const short	DAT_ROMSWITCH		= 0x60;	// ＲＯＭスイッチデータ
	public const short	DAT_OPTSWITCH		= 0x61;	// オプションスイッチデータ
	public const short	DAT_RBTPOSINF		= 0x70;	/* ロボット位置情報				*/
	public const short	DAT_RBTERRINF		= 0x71;	/* ロボットエラー情報読出			*/
	public const short	DAT_RBTFIGINF		= 0x72;	/* ロボット姿勢情報読出				*/
	public const short	DAT_MOTPOSINF		= 0x73;	/* モータポジション情報読出			*/
	public const short	DAT_RBTPOINTTABLE	= 0x80;	/* ロボットポイントテーブル読出/書込	*/
	public const short	DAT_RBTTOOLPRM		= 0x81;	/* ロボットツール座標系定義読出/書込	*/
	public const short	DAT_RBTWORKPRM		= 0x82;	/* ロボットワーク座標系定義情報		*/
	public const short	DAT_RBTPRM			= 0x83;	/* ロボットパラメータ読出/書込	*/
	public const short	DAT_RBTDELTAPRM		= 0x84;	/* デルタロボット機構パラメータ読出/書込	*/
	public const short	DAT_RBTSCARAPRM		= 0x85;	/* スカラロボット機構パラメータ読出/書込	*/
	public const short	DAT_RBT6AXISPRM		= 0x86;	/* 6軸垂直多関節機構パラメータ読出/書込	*/
	public const short	DAT_RBTTOOLSTS		= 0x8a;	/* ロボットツール座標系情報読出	*/
	public const short	DAT_RBTWORKSTS		= 0x8b;	/* ロボットワーク座標系情報読出	*/
	public const short	DAT_FORGROUND		= 0x90;	// フォアグランド処理時間情報
	public const short	DAT_CYCLETIME		= 0x91;	// 制御周期情報
	public const short	DAT_ECOBJSEL		= 0xA0;	// EtherCATｵﾌﾞｼﾞｪｸﾄ選択情報書込/読出
	public const short	DAT_ECOBJVAL		= 0xA1;	// EtherCATｵﾌﾞｼﾞｪｸﾄ値書込/読出

	public const short	DAT_FLASHROMINIT	= 0xfd;	// システム予約（ﾌﾗｯｼｭｼｽﾃﾑﾃﾞｰﾀ）
	public const short	DAT_FLASHROM		= 0xfe;	// システム予約（ﾌﾗｯｼｭｼｽﾃﾑﾃﾞｰﾀ）
	public const short	DAT_RESERVED		= 0xFF;	// システム予約


	// --------------------------------------------------------------------------
	//	コマンドコード
	// --------------------------------------------------------------------------
	public const short	REQ_LOOPBACK_TEST	= 0x00;	// ＤＰ通信 ループバックテスト
	public const short	REQ_DATA_SEND		= 0x01;	// ＤＰ通信 データ送信要求
	public const short	REQ_DATA_RECEIVE	= 0x02;	// ＤＰ通信 データ受信要求
	public const short	REQ_MODECHG			= 0x10;	// ﾓｰﾄﾞ変更
	public const short	REQ_JOGSTART		= 0x11;	// ＪＯＧ移動開始
	public const short	REQ_ZRNSTART		= 0x12;	// 原点復帰開始
	public const short	REQ_STOP			= 0x13;	// 移動停止
	public const short	REQ_GENE			= 0x14;	// ｼﾞｪﾈﾚｰｼｮﾝ
	public const short	REQ_PTPSTART		= 0x15;	// ＰＴＰ移動開始
	public const short	REQ_PTPASTART		= 0x16;	// ＰＴＰ移動開始（ABSO）
	public const short	REQ_LINSTART		= 0x17;	// 補間移動開始
	public const short	REQ_LINASTART		= 0x18;	// 補間移動開始（ABSO）
	public const short	REQ_ORGSET			= 0x19;	// 原点設定
	public const short	REQ_RESET			= 0x1A;	// ﾘｾｯﾄ
	public const short	REQ_RESTART			= 0x1B;	// 順行再開
	public const short	REQ_PRGCLEAR		= 0x1C;	// 内蔵ﾌﾟﾛｸﾞﾗﾑｸﾘｱ
	public const short	REQ_OUTPUT			= 0x1D;	// 汎用出力直接制御
	public const short	REQ_SERVOON			= 0x1E;	// ｻｰﾎﾞ電源ON
	public const short	REQ_SERVOOFF		= 0x1F;	// ｻｰﾎﾞ電源OFF
	public const short	REQ_PROGSTRT		= 0x20;	// ﾌﾟﾛｸﾞﾗﾑ実行開始
	public const short	REQ_PROGSTOP		= 0x21;	// ﾌﾟﾛｸﾞﾗﾑ実行停止
	public const short	REQ_PROGSLCT		= 0x22;	// 実行ﾌﾟﾛｸﾞﾗﾑ選択
	public const short	REQ_ALLZRN			= 0x24;	// 全軸原点復帰開始
	public const short	REQ_ERCLEAR			= 0x25;	// ＥＲクリア for SVM
	public const short	REQ_SMPLSTRT		= 0x26;	// サンプリング開始 for SVM
	public const short	REQ_SMPLSTOP		= 0x27;	// サンプリング停止 for SVM
	public const short	REQ_SLINSTART		= 0x28;	// 高速ｾﾝｻｰﾗｯﾁ補間移動開始
	public const short	REQ_SLINASTART		= 0x29;	// 高速ｾﾝｻｰﾗｯﾁ補間移動開始(ABSO)
	public const short	REQ_COMPINPUT		= 0x2A;	// 汎用入力強制制御（一括）
	public const short	REQ_COMPOUTPUT		= 0x2B;	// 汎用出力強制制御（一括）
	public const short	REQ_COMPIOBIT		= 0x2C;	// 汎用入出力強制制御（ビット）
	public const short	REQ_OVRDCHGP		= 0x2D;	// 軸速度ｵｰﾊﾞｰﾗｲﾄﾞ％値変更
	public const short	REQ_ADLOG			= 0x2E;	// Ａ／Ｄロギング要求
	public const short	REQ_ADLOGCLR		= 0x2F;	// Ａ／Ｄロギングバッファクリア要求
	public const short	REQ_ZRNSHIFT		= 0x30;	// 原点シフト要求
	public const short	REQ_TPCSEL			= 0x31;	// ＴＰＣデータ選択
	public const short	REQ_TPCLOG			= 0x32;	// ＴＰＣデータロギングＯＮ／ＯＦＦ
	public const short	REQ_AXAUTOZRN		= 0x33;	// 各軸自動原点復帰コマンド
	public const short	REQ_PTPBSTART		= 0x34;	// ＰＴＰ移動開始(機械座標系ABSO)
	public const short	REQ_LINBSTART		= 0x35;	// 補間移動開始(機械座標系ABSO)
	public const short	REQ_CYCLE			= 0x36;	// サイクル運転モード変更コマンド
	public const short	REQ_COORDSET		= 0x37;	// 座標系設定
	public const short	REQ_AXISINTLK		= 0x38;	// 軸インタロックコマンド
	public const short	REQ_AXISNEG			= 0x39;	// 軸ネグレクトコマンド
	public const short	REQ_SVONOFF			= 0x3A;	// 各軸サーボＯＮ／ＯＦＦ
	public const short	REQ_TRQLIMCHG		= 0x3B;	// トルク制限モード変更
	public const short	REQ_AXCTRLCHG		= 0x3C;	// 各軸制御モード変更
	public const short	REQ_MCDOUT			= 0x3D;	// Ｍコード出力
	public const short	REQ_TPCSEL_EX		= 0x3E;	// ＴＰＣデータ選択
	public const short	REQ_PANEL			= 0x40;	// システム予約
	public const short	REQ_ROMSWGEN		= 0x41;	// システム予約
	public const short	REQ_TIMERRESET		= 0x42;	// システム予約
	public const short	REQ_OUTPUTBIT		= 0x44;	// 汎用出力直接制御（ビット）
	public const short	REQ_TRQCMD			= 0x46;	// トルク指令
	public const short	REQ_LIMASTART		= 0x47;	// ポイント移動指令
	public const short	REQ_SYNEGPTPSTART	= 0x49;	// 同期軸無視ＰＴＰ移動開始
	public const short	REQ_SYNEGPTPASTART	= 0x4a;	// 同期軸無視ＰＴＰ移動開始（ABSO）
	public const short	REQ_SYNEGPTPBSTART	= 0x4b;	// 同期軸無視ＰＴＰ移動開始（機械座標系ABSO）
	public const short	REQ_VCTRLCMD		= 0x4c;	// 速度制御指令
	public const short	REQ_SPCMND			= 0x50;	// 主軸Ｄ／Ａ出力指令
	public const short	REQ_SPREVSET		= 0x51;	// 主軸回転数設定
	public const short	REQ_SPINAX			= 0x52;	// 回転軸回転動作指令
	public const short	REQ_TLINE			= 0x54;	// 接線制御ＯＮ／ＯＦＦ
	public const short	REQ_HANDLE			= 0x5c;	// 手パモードON/OFF設定
	public const short	REQ_HANDLEKP		= 0x5d;	// 手パモード倍率設定
	public const short	REQ_HANDLEAXIS1		= 0x5e;	// 手パ有効軸１
	public const short	REQ_HANDLEAXIS2		= 0x5f;	// 手パ有効軸２
	public const short	REQ_SINGLE			= 0x60;	// シングルステップモード
	public const short	REQ_TEACH			= 0x61;	// ティーチング
	public const short	REQ_PRGINS			= 0x62;	// プログラム挿入
	public const short	REQ_PRGALT			= 0x63;	// プログラム置換
	public const short	REQ_PRGDEL			= 0x64;	// プログラム削除
	public const short	REQ_PRGREV			= 0x65;	// プログラム逆行運転
	public const short	REQ_STEPCHG			= 0x66;	// プログラムステップ変更
	public const short	REQ_STEPSKIP		= 0x67;	// プログラムステップスキップ
	public const short	REQ_AXMV			= 0x68;	// 独立位置決めインクレ指令
	public const short	REQ_AXMVA			= 0x69;	// 独立位置決め論理座標系アブソ指令
	public const short	REQ_AXMVB			= 0x6a;	// 独立位置決め機械座標系アブソ指令
	public const short	REQ_PRGBLKMV		= 0x6B;	// プログラムブロック移動
	public const short	REQ_PRGBLKCPY		= 0x6C;	// プログラムブロックコピー
	public const short	REQ_PRGBLKDEL		= 0x6D;	// プログラムブロック削除
	public const short	REQ_REFMEM			= 0x6E;	// Flashﾒﾓﾘへ運転ﾌﾟﾛｸﾞﾗﾑ反映指令
	public const short	REQ_REFPOINT		= 0x6F;	// Flashﾒﾓﾘへポイントデータ反映指令
	public const short	REQ_TASKSTART		= 0x80;	// ﾏﾙﾁﾀｽｸﾌﾟﾛｸﾞﾗﾑ開始
	public const short	REQ_TASKSTOP		= 0x81;	// ﾏﾙﾁﾀｽｸﾌﾟﾛｸﾞﾗﾑ停止
	public const short	REQ_TASKRESET		= 0x82;	// ﾏﾙﾁﾀｽｸﾌﾟﾛｸﾞﾗﾑﾘｾｯﾄ
	public const short	REQ_HOME			= 0x83;	// homepos移動
	public const short	REQ_MCRREG			= 0x84;	// マクロ変数書き込みコマンド
	public const short	REQ_COVRDCHGP		= 0x85;	// 補間オーバーライド％値変更
	public const short	REQ_SOVRDCHGP		= 0x86;	// 主軸オーバーライド％値変更
	public const short	REQ_MABSCOORDSET	= 0x90;	// アブソ座標設定コマンド
	public const short	REQ_ECPRMSAVE		= 0x91;	// EtherCATパラメータ保存
	public const short	REQ_RBTJOGSTART		= 0xA0;	// ロボットＪＯＧ移動開始
	public const short	REQ_RBTLINSTART		= 0xA1;	// ロボット直線補間移動開始
	public const short	REQ_RBTLINASTART	= 0xA2;	// ロボットアブソ直線補間移動開始
	public const short	REQ_RBTLINPSTART	= 0xA3;	// ロボットポイント移動開始
	public const short	REQ_RBTTJOGSTART	= 0xA4;	// ロボットツール座標系ＪＯＧ移動開始
	public const short	REQ_RBTTLINSTART	= 0xA5;	// ロボットツール座標系直線補間移動開始
	public const short	REQ_RBTJMOVSTART	= 0xA6;	// ロボット関節移動開始
	public const short	REQ_RBTJMOVASTART	= 0xA7;	// ロボットアブソ関節移動開始
	public const short	REQ_RBTJMOVPSTART	= 0xA8;	// ポイント指定ロボット関節移動開始
	public const short	REQ_RBTFIGCHG		= 0xB0;	// ロボット姿勢切り替え
	public const short	REQ_RBTTOOLCHG		= 0xB1;	// ロボットツール座標系切り替え
	public const short	REQ_RBTWORKCHG		= 0xB2;	// ロボットワーク座標系切り替え
	public const short	REQ_RBTHANDLE		= 0xB3;	// ロボット手パモードON/OFF設定

	// ========================================================================
	//	エラーコード
	// ========================================================================
	public const int	E_OK			= 0;		// エラーなし
	public const int	E_DEVNRDY		= 1;		// デバイス未初期化
	public const int	E_PARAM			= 2;		// パラメータエラー
	public const int	E_TIME			= 3;		// 通信タイムアウト
	public const int	E_RTRY			= 4;		// リトライオーバー
	public const int	E_MLTRTRY		= 5;		// 多重リトライ
	public const int	E_HARDER		= 6;		// 通信エラー
	public const int	E_NEXIST		= 7;		// 要求データが存在しない
	public const int	E_PROTECT		= 8;		// データ受信不可状態
	public const int	E_SEQ			= 9;		// 通信シーケンスエラー
	public const int	E_PRGTERM		= 10;		// プログラム実行中断
	public const int	E_PRGBUFF		= 11;		// プログラムバッファフル
	public const int	E_CMDNOT		= 12;		// コマンド実行不可
	public const int	E_EMPTYHANDLE	= 13;		// 空き通信ハンドル無し
	public const int	E_NOHANDLE		= 14;		// 無効ハンドル
	public const int	E_BUSY			= 15;		// 通信ビジー
	public const int	E_PRMWRITE		= 16;		// パラメータ書込エラー

	public const int	E_USERDEF		= 256;		// ユーザー定義エラーコード領域開始

	public const int	E_ERR			= -1;		// 不明エラー

}
