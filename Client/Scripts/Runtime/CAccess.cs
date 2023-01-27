using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System.IO;
using System.Linq;
using System.Diagnostics;

/** 기본 접근자 */
public static partial class CAccess {
	#region 클래스 프로퍼티
	public static bool IsSupportMSAA => SystemInfo.supportsMultisampledTextures > KCDefine.B_VAL_0_INT;
	public static string MidnightDeltaTimeStr => new System.DateTime(CAccess.MidnightDeltaTime.Ticks).ToString(KCDefine.B_DATE_T_FMT_HH_MM_SS);

	public static System.DateTime MidnightTime => System.DateTime.Today.AddDays(KCDefine.B_VAL_1_REAL);
	public static System.TimeSpan MidnightDeltaTime => CAccess.MidnightTime - System.DateTime.Now;
	#endregion // 클래스 프로퍼티

	#region 클래스 함수
	/** 조건을 검사한다 */
	[Conditional("DEBUG"), Conditional("DEVELOPMENT_BUILD")]
	public static void Assert(bool a_bIsTrue) {
		UnityEngine.Assertions.Assert.IsTrue(a_bIsTrue);
	}

	/** 유저 문자열을 반환한다 */
	public static string GetUserStr(EUserType a_eUserType) {
		// 유저 타입이 유효하지 않을 경우
		if(!a_eUserType.ExIsValid()) {
			return KCDefine.B_TOKEN_USER_UNKNOWN;
		}

		return (a_eUserType == EUserType.A) ? KCDefine.B_TOKEN_USER_A : KCDefine.B_TOKEN_USER_B;
	}

	/** 버전 문자열을 반환한다 */
	public static string GetVerStr(string a_oVer, EUserType a_eUserType) {
		string oUserStr = CAccess.GetUserStr(a_eUserType);
		return string.Format(KCDefine.B_TEXT_FMT_VER, a_oVer, oUserStr);
	}

	/** 읽기용 스트림을 반환한다 */
	public static FileStream GetReadStream(string a_oFilePath) {
		CAccess.Assert(a_oFilePath.ExIsValid());
		return File.Exists(a_oFilePath) ? File.Open(a_oFilePath, FileMode.Open, FileAccess.Read) : null;
	}

	/** 쓰기용 스트림을 반환한다 */
	public static FileStream GetWriteStream(string a_oFilePath) {
		CAccess.Assert(a_oFilePath.ExIsValid());
		string oDirPath = Path.GetDirectoryName(a_oFilePath).Replace(KCDefine.B_TOKEN_R_SLASH, KCDefine.B_TOKEN_SLASH);

		// 디렉토리가 없을 경우
		if(oDirPath.ExIsValid() && !Directory.Exists(oDirPath)) {
			Directory.CreateDirectory(oDirPath);
		}

		return File.Open(a_oFilePath, FileMode.Create, FileAccess.Write);
	}

	/** 랜덤 확률을 반환한다 */
	public static (int, float) GetRandPercent(List<float> a_oPercentList) {
		CAccess.Assert(a_oPercentList.ExIsValid());

		float fPercent = Random.Range(KCDefine.B_VAL_0_REAL, a_oPercentList.Sum((a_fPercent) => a_fPercent));
		float fComparePercent = KCDefine.B_VAL_0_REAL;

		for(int i = 0; i < a_oPercentList.Count - KCDefine.B_VAL_1_INT; ++i) {
			fComparePercent += Mathf.Abs(a_oPercentList[i]);

			// 확률을 만족 할 경우
			if(fPercent.ExIsLessEquals(fComparePercent) && !Mathf.Abs(a_oPercentList[i]).ExIsEquals(KCDefine.B_VAL_0_REAL)) {
				return (i, a_oPercentList[i]);
			}
		}

		return (a_oPercentList.Count - KCDefine.B_VAL_1_INT, a_oPercentList.LastOrDefault());
	}

	/** 디렉토리를 순회한다 */
	public static void EnumerateDirectories(string a_oDirPath, System.Func<List<string>, List<string>, bool> a_oCallback, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oCallback != null && a_oDirPath.ExIsValid()));
		bool bIsValid = a_oCallback != null && a_oDirPath.ExIsValid();

		// 디렉토리가 존재 할 경우
		if(bIsValid && Directory.Exists(a_oDirPath)) {
			var oDirPaths = Directory.GetDirectories(a_oDirPath);

			// 디렉토리 순회가 가능 할 경우
			if(a_oCallback(oDirPaths.ToList(), Directory.GetFiles(a_oDirPath).ToList())) {
				for(int i = 0; i < oDirPaths.Length; ++i) {
					CAccess.EnumerateDirectories(oDirPaths[i], a_oCallback);
				}
			}
		}
	}
	#endregion // 클래스 함수

	#region 제네릭 클래스 함수
	/** 열거형 값을 반환한다 */
	public static List<T> GetEnumVals<T>() {
		return (System.Enum.GetValues(typeof(T)) as T[]).ToList();
	}

	/** 열거형 문자열을 반환한다 */
	public static List<string> GetEnumStrs<T>() {
		var oEnumStrList = new List<string>();
		var oEnumValList = CAccess.GetEnumVals<T>();

		for(int i = 0; i < oEnumValList.Count; ++i) {
			oEnumStrList.Add(oEnumValList[i].ToString());
		}

		return oEnumStrList;
	}
	#endregion // 제네릭 클래스 함수
}

/** 기본 접근자 - 추가 */
public static partial class CAccess {
	#region 클래스 함수 (CExtension)
	/** 값을 비교한다 */
	public static int ExCompare(this float a_fSender, float a_fRhs) {
		// 값이 동일 할 경우
		if(a_fSender.ExIsEquals(a_fRhs)) {
			return KCDefine.B_COMPARE_EQUALS;
		}

		return a_fSender.ExIsLess(a_fRhs) ? KCDefine.B_COMPARE_LESS : KCDefine.B_COMPARE_GREATE;
	}

	/** 값을 비교한다 */
	public static int ExCompare(this double a_dblSender, double a_dblRhs) {
		// 값이 동일 할 경우
		if(a_dblSender.ExIsEquals(a_dblRhs)) {
			return KCDefine.B_COMPARE_EQUALS;
		}

		return a_dblSender.ExIsLess(a_dblRhs) ? KCDefine.B_COMPARE_LESS : KCDefine.B_COMPARE_GREATE;
	}

	/** 시간을 비교한다 */
	public static int ExCompare(this System.DateTime a_stSender, System.DateTime a_stRhs) {
		return a_stSender.ExGetDeltaTime(a_stRhs).ExCompare(KCDefine.B_VAL_0_REAL);
	}

	/** DPI 픽셀 => 픽셀로 변환한다 */
	public static float ExDPIPixelsToPixels(this int a_nSender) {
		return a_nSender * (CAccess.ScreenDPI / KCDefine.B_DEF_SCREEN_DPI);
	}

	/** DPI 픽셀 => 픽셀로 변환한다 */
	public static float ExDPIPixelsToPixels(this float a_fSender) {
		return a_fSender * (CAccess.ScreenDPI / KCDefine.B_DEF_SCREEN_DPI);
	}
	#endregion // 클래스 함수 (CExtension)

	#region 제네릭 클래스 함수 (CExtension)
	/** 값을 교환한다 */
	public static void ExSwap<T>(this T[] a_oSender, int a_nIdx01, int a_nIdx02, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender.ExIsValid() && a_oSender.ExIsValidIdx(a_nIdx01) && a_oSender.ExIsValidIdx(a_nIdx02)));

		// 인덱스가 유효 할 경우
		if(a_oSender.ExIsValid() && (a_oSender.ExIsValidIdx(a_nIdx01) && a_oSender.ExIsValidIdx(a_nIdx02))) {
			T tTemp = a_oSender[a_nIdx01];
			a_oSender[a_nIdx01] = a_oSender[a_nIdx02];
			a_oSender[a_nIdx02] = tTemp;
		}
	}

	/** 값을 교환한다 */
	public static void ExSwap<T>(this T[,] a_oSender, Vector3Int a_stIdx01, Vector3Int a_stIdx02, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender.ExIsValid() && a_oSender.ExIsValidIdx(a_stIdx01) && a_oSender.ExIsValidIdx(a_stIdx02)));

		// 인덱스가 유효 할 경우
		if(a_oSender.ExIsValid() && (a_oSender.ExIsValidIdx(a_stIdx01) && a_oSender.ExIsValidIdx(a_stIdx02))) {
			T tTemp = a_oSender[a_stIdx01.y, a_stIdx01.x];
			a_oSender[a_stIdx01.y, a_stIdx01.x] = a_oSender[a_stIdx02.y, a_stIdx02.x];
			a_oSender[a_stIdx02.y, a_stIdx02.x] = tTemp;
		}
	}

	/** 값을 교환한다 */
	public static void ExSwap<T>(this List<T> a_oSender, int a_nIdx01, int a_nIdx02, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender.ExIsValid() && a_oSender.ExIsValidIdx(a_nIdx01) && a_oSender.ExIsValidIdx(a_nIdx02)));

		// 인덱스가 유효 할 경우
		if(a_oSender.ExIsValid() && (a_oSender.ExIsValidIdx(a_nIdx01) && a_oSender.ExIsValidIdx(a_nIdx02))) {
			T tTemp = a_oSender[a_nIdx01];
			a_oSender[a_nIdx01] = a_oSender[a_nIdx02];
			a_oSender[a_nIdx02] = tTemp;
		}
	}

	/** 값을 교환한다 */
	public static void ExSwap<K, V>(this Dictionary<K, V> a_oSender, K a_tKey01, K a_tKey02, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender.ExIsValid() && a_oSender.ContainsKey(a_tKey01) && a_oSender.ContainsKey(a_tKey02)));

		// 키가 유효 할 경우
		if(a_oSender.ExIsValid() && (a_oSender.ContainsKey(a_tKey01) && a_oSender.ContainsKey(a_tKey02))) {
			V tTemp = a_oSender[a_tKey01];
			a_oSender[a_tKey01] = a_oSender[a_tKey02];
			a_oSender[a_tKey02] = tTemp;
		}
	}

	/** 값을 재배치한다 */
	public static void ExShuffle<T>(this T[] a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 재베치가 가능 할 경우
		if(a_oSender != null) {
			for(int i = 0; i < a_oSender.Length; ++i) {
				a_oSender.ExSwap(i, Random.Range(KCDefine.B_VAL_0_INT, a_oSender.Length), a_bIsEnableAssert);
			}
		}
	}

	/** 값을 재배치한다 */
	public static void ExShuffle<T>(this List<T> a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 재배치가 가능 할 경우
		if(a_oSender != null) {
			for(int i = 0; i < a_oSender.Count; ++i) {
				a_oSender.ExSwap(i, Random.Range(KCDefine.B_VAL_0_INT, a_oSender.Count), a_bIsEnableAssert);
			}
		}
	}

	/** 값을 재배치한다 */
	public static void ExShuffle<K, V>(this Dictionary<K, V> a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 재배치가 가능 할 경우
		if(a_oSender != null) {
			var oKeyList = a_oSender.Keys.ToList();

			for(int i = 0; i < oKeyList.Count; ++i) {
				a_oSender.ExSwap(oKeyList[i], oKeyList[Random.Range(KCDefine.B_VAL_0_INT, oKeyList.Count)], a_bIsEnableAssert);
			}
		}
	}
	#endregion // 제네릭 클래스 함수 (CExtension)
}
