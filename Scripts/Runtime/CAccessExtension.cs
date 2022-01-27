using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

/** 기본 접근 확장 클래스 */
public static partial class CAccessExtension {
	#region 클래스 함수
	/** 유효 여부를 검사한다 */
	public static bool ExIsValid(this string a_oSender) {
		return a_oSender != null && a_oSender.Length > KCDefine.B_VAL_0_INT;
	}

	/** 유효 여부를 검사한다 */
	public static bool ExIsValid(this System.DateTime a_stSender) {
		return a_stSender.Ticks >= KCDefine.B_VAL_0_LONG;
	}

	/** 유효 여부를 검사한다 */
	public static bool ExIsValid(this System.TimeSpan a_stSender) {
		return a_stSender.Ticks >= KCDefine.B_VAL_0_LONG;
	}

	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx(this int a_nSender) {
		return a_nSender > KCDefine.B_IDX_INVALID;
	}

	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx(this Vector2Int a_stSender) {
		return a_stSender.x > KCDefine.B_IDX_INVALID && a_stSender.y > KCDefine.B_IDX_INVALID;
	}

	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx(this Vector3Int a_stSender) {
		return CAccessExtension.ExIsValidIdx((Vector2Int)a_stSender) && a_stSender.z > KCDefine.B_IDX_INVALID;
	}

	/** 빌드 번호 유효 여부를 검사한다 */
	public static bool ExIsValidBuildNum(this int a_nSender) {
		return a_nSender >= KCDefine.B_MIN_BUILD_NUM;
	}

	/** 빌드 버전 유효 여부를 검사한다 */
	public static bool ExIsValidBuildVer(this string a_oSender) {
		return System.Version.TryParse(a_oSender, out System.Version oVer);
	}

	/** 언어 유효 여부를 검사한다 */
	public static bool ExIsValidLanguage(this string a_oSender) {
		return a_oSender.ExIsValid() && !a_oSender.Equals(KCDefine.B_TEXT_UNKNOWN);
	}

	/** 국가 코드 유효 여부를 검사한다 */
	public static bool ExIsValidCountryCode(this string a_oSender) {
		return a_oSender.ExIsValid() && !a_oSender.ToUpper().Equals(KCDefine.B_TEXT_UNKNOWN);
	}

	/** 동일 여부를 검사한다 */
	public static bool ExIsEquals(this float a_fSender, float a_fRhs) {
		return Mathf.Approximately(a_fSender, a_fRhs);
	}

	/** 동일 여부를 검사한다 */
	public static bool ExIsEquals(this double a_dblSender, double a_dblRhs) {
		double dblDeltaTime = System.Math.Abs(a_dblSender) - System.Math.Abs(a_dblRhs);
		return dblDeltaTime >= -double.Epsilon && dblDeltaTime <= double.Epsilon;
	}

	/** 작음 여부를 검사한다 */
	public static bool ExIsLess(this float a_fSender, float a_fRhs) {
		return a_fSender < a_fRhs - float.Epsilon;
	}

	/** 작거나 같음 여부를 검사한다 */
	public static bool ExIsLessEquals(this float a_fSender, float a_fRhs) {
		return a_fSender.ExIsLess(a_fRhs) || a_fSender.ExIsEquals(a_fRhs);
	}

	/** 큰 여부를 검사한다 */
	public static bool ExIsGreate(this float a_fSender, float a_fRhs) {
		return a_fSender > a_fRhs + float.Epsilon;
	}

	/** 크거나 같음 여부를 검사한다 */
	public static bool ExIsGreateEquals(this float a_fSender, float a_fRhs) {
		return a_fSender.ExIsGreate(a_fRhs) || a_fSender.ExIsEquals(a_fRhs);
	}

	/** 작음 여부를 검사한다 */
	public static bool ExIsLess(this double a_dblSender, double a_dblRhs) {
		return a_dblSender < a_dblRhs - double.Epsilon;
	}

	/** 작거나 같음 여부를 검사한다 */
	public static bool ExIsLessEquals(this double a_dblSender, double a_dblRhs) {
		return a_dblSender.ExIsLess(a_dblRhs) || a_dblSender.ExIsEquals(a_dblRhs);
	}

	/** 큰 여부를 검사한다 */
	public static bool ExIsGreate(this double a_dblSender, double a_dblRhs) {
		return a_dblSender > a_dblRhs + double.Epsilon;
	}

	/** 크거나 같음 여부를 검사한다 */
	public static bool ExIsGreateEquals(this double a_dblSender, double a_dblRhs) {
		return a_dblSender.ExIsGreate(a_dblRhs) || a_dblSender.ExIsEquals(a_dblRhs);
	}

	/** 완료 여부를 검사한다 */
	public static bool ExIsComplete(this Task a_oSender) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.IsCompleted && !a_oSender.IsFaulted && !a_oSender.IsCanceled;
	}

	/** 유럽 연합 여부를 검사한다 */
	public static bool ExIsEU(this string a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());
		return KCDefine.B_EU_COUNTRY_CODE_LIST.Contains(a_oSender.ToUpper());
	}

	/** 시간 간격을 반환한다 */
	public static double ExGetDeltaTime(this System.DateTime a_stSender, System.DateTime a_stRhs) {
		CAccess.Assert(a_stSender.ExIsValid() && a_stRhs.ExIsValid());
		return (a_stSender - a_stRhs).TotalSeconds;
	}

	/** 시간 간격을 반환한다 */
	public static double ExGetDeltaTimePerMinutes(this System.DateTime a_stSender, System.DateTime a_stRhs) {
		CAccess.Assert(a_stSender.ExIsValid() && a_stRhs.ExIsValid());
		return (a_stSender - a_stRhs).TotalMinutes;
	}

	/** 시간 간격을 반환한다 */
	public static double ExGetDeltaTimePerHours(this System.DateTime a_stSender, System.DateTime a_stRhs) {
		CAccess.Assert(a_stSender.ExIsValid() && a_stRhs.ExIsValid());
		return (a_stSender - a_stRhs).TotalHours;
	}

	/** 시간 간격을 반환한다 */
	public static double ExGetDeltaTimePerDays(this System.DateTime a_stSender, System.DateTime a_stRhs) {
		CAccess.Assert(a_stSender.ExIsValid() && a_stRhs.ExIsValid());
		return (a_stSender - a_stRhs).TotalDays;
	}

	/** 시간 간격을 반환한다 */
	public static long ExGetDeltaTimePerTicks(this System.DateTime a_stSender, System.DateTime a_stRhs) {
		CAccess.Assert(a_stSender.ExIsValid() && a_stRhs.ExIsValid());
		return (a_stSender - a_stRhs).Ticks;
	}

	/** 이전 인덱스를 반환한다 */
	public static Vector2Int ExGetPrevIdx(this Vector2Int a_stSender, EDirection a_eDirection) {
		CAccess.Assert(!a_stSender.Equals(KCDefine.B_IDX_INVALID_2D));
		CAccess.Assert(a_eDirection >= EDirection.UP && a_eDirection <= EDirection.RIGHT_DOWN);
		
		return a_stSender + KCDefine.B_IDX_OFFSET_INFO_LIST_2D[(int)a_eDirection].Item1;
	}
	
	/** 다음 인덱스를 반환한다 */
	public static Vector2Int ExGetNextIdx(this Vector2Int a_stSender, EDirection a_eDirection) {
		CAccess.Assert(!a_stSender.Equals(KCDefine.B_IDX_INVALID_2D));
		CAccess.Assert(a_eDirection >= EDirection.UP && a_eDirection <= EDirection.RIGHT_DOWN);
		
		return a_stSender + KCDefine.B_IDX_OFFSET_INFO_LIST_2D[(int)a_eDirection].Item2;
	}

	/** 변경 된 문자열을 반환한다 */
	public static string ExGetReplaceStr(this string a_oSender, string a_oTarget, string a_oReplace, int a_nReplaceTimes = KCDefine.B_VAL_1_INT) {
		CAccess.Assert(a_oSender != null && a_oTarget.ExIsValid());

		for(int i = 0; i < a_nReplaceTimes && a_oSender.Contains(a_oTarget); ++i) {
			a_oSender = a_oSender.Replace(a_oTarget, a_oReplace);
		}

		return a_oSender;
	}

	/** 파일 이름이 변경 된 경로를 반환한다 */
	public static string ExGetReplaceFileNamePath(this string a_oSender, string a_oFileName, bool a_bIsResetExtension = false) {
		CAccess.Assert(a_oSender.ExIsValid() && a_oFileName.ExIsValid());
		return a_oSender.ExGetReplaceStr(a_bIsResetExtension ? Path.GetFileName(a_oSender) : Path.GetFileNameWithoutExtension(a_oSender), a_oFileName);
	}

	/** 값 => 왼쪽 쉬프트 비트로 변환한다 */
	public static int ExToLShiftBits(this int a_nSender, int a_nOffset) {
		CAccess.Assert(a_nOffset >= KCDefine.B_VAL_0_INT);
		return a_nSender << a_nOffset;
	}

	/** 값 => 오른쪽 쉬프트 비트로 변환한다 */
	public static int ExToRShiftBits(this int a_nSender, int a_nOffset) {
		CAccess.Assert(a_nOffset >= KCDefine.B_VAL_0_INT);
		return a_nSender >> a_nOffset;
	}

	/** 비트 => 리스트로 변환한다 */
	public static List<int> ExToBits(this int a_nSender) {
		var oBitList = new List<int>();

		for(int i = 0; i < sizeof(int) * KCDefine.B_UNIT_BITS_PER_BYTE; ++i) {
			oBitList.Add(((a_nSender & KCDefine.B_VAL_1_INT.ExToLShiftBits(i)) != KCDefine.B_VAL_0_INT) ? KCDefine.B_VAL_1_INT : KCDefine.B_VAL_0_INT);
		}

		return oBitList;
	}

	/** 리스트 => 비트로 변환한다 */
	public static int ExToBits(this List<int> a_oSender) {
		CAccess.Assert(a_oSender != null);
		int nVal = KCDefine.B_VAL_0_INT;

		for(int i = 0; i < a_oSender.Count; ++i) {
			nVal |= KCDefine.B_VAL_1_INT.ExToLShiftBits(a_oSender[i]);
		}

		return nVal;
	}

	/** 로컬 => 월드로 변환한다 */
	public static Vector3 ExToWorld(this Vector3 a_stSender, GameObject a_oObj, bool a_bIsCoord = true) {
		return a_bIsCoord ? a_oObj.transform.TransformPoint(a_stSender) : a_oObj.transform.TransformDirection(a_stSender);
	}

	/** 월드 => 로컬로 변환한다 */
	public static Vector3 ExToLocal(this Vector3 a_stSender, GameObject a_oObj, bool a_bIsCoord = true) {
		return a_bIsCoord ? a_oObj.transform.InverseTransformPoint(a_stSender) : a_oObj.transform.InverseTransformDirection(a_stSender);
	}
	#endregion			// 클래스 함수

	#region 제네릭 클래스 함수
	/** 유효 여부를 검사한다 */
	public static bool ExIsValid<T>(this T[] a_oSender) {
		return a_oSender != null && a_oSender.Length > KCDefine.B_VAL_0_INT;
	}

	/** 유효 여부를 검사한다 */
	public static bool ExIsValid<T>(this T[,] a_oSender) {
		return a_oSender != null && a_oSender.GetLength(KCDefine.B_VAL_0_INT) > KCDefine.B_VAL_0_INT && a_oSender.GetLength(KCDefine.B_VAL_1_INT) > KCDefine.B_VAL_0_INT;
	}

	/** 유효 여부를 검사한다 */
	public static bool ExIsValid<T>(this List<T> a_oSender) {
		return a_oSender != null && a_oSender.Count > KCDefine.B_VAL_0_INT;
	}
	
	/** 유효 여부를 검사한다 */
	public static bool ExIsValid<K, V>(this Dictionary<K, V> a_oSender) {
		return a_oSender != null && a_oSender.Count > KCDefine.B_VAL_0_INT;
	}

	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx<T>(this T[] a_oSender, int a_nIdx) {
		CAccess.Assert(a_oSender != null);
		return a_nIdx > KCDefine.B_IDX_INVALID && a_nIdx < a_oSender.Length;
	}

	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx<T>(this T[,] a_oSender, Vector2Int a_stIdx) {
		CAccess.Assert(a_oSender != null);
		return (a_stIdx.y > KCDefine.B_IDX_INVALID && a_stIdx.y < a_oSender.GetLength(KCDefine.B_VAL_0_INT)) && (a_stIdx.x > KCDefine.B_IDX_INVALID && a_stIdx.x < a_oSender.GetLength(KCDefine.B_VAL_1_INT));
	}

	/** 인덱스 유효 여부룰 검사한다 */
	public static bool ExIsValidIdx<T>(this List<T> a_oSender, int a_nIdx) {
		CAccess.Assert(a_oSender != null);
		return a_nIdx > KCDefine.B_IDX_INVALID && a_nIdx < a_oSender.Count;
	}

	/** 포함 여부를 검사한다 */
	public static bool ExIsContains<T>(this T[] a_oSender, T a_tVal) {
		CAccess.Assert(a_oSender != null);
		return System.Array.FindIndex(a_oSender, (a_tCompareVal) => a_tVal.Equals(a_tCompareVal)) > KCDefine.B_IDX_INVALID;
	}

	/** 포함 여부를 검사한다 */
	public static bool ExIsContains<T>(this T[] a_oSender, List<T> a_oValList) {
		CAccess.Assert(a_oSender != null && a_oValList != null);

		for(int i = 0; i < a_oValList.Count; ++i) {
			// 값이 없을 경우
			if(!a_oSender.ExIsContains(a_oValList[i])) {
				return false;
			}
		}

		return true;
	}

	/** 포함 여부를 검사한다 */
	public static bool ExIsContains<T>(this T[,] a_oSender, T a_tVal) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExToSingleArray().ExIsContains(a_tVal);
	}

	/** 포함 여부를 검사한다 */
	public static bool ExIsContains<T>(this T[,] a_oSender, List<T> a_oValList) {
		CAccess.Assert(a_oSender != null && a_oValList != null);
		return a_oSender.ExToSingleArray().ExIsContains(a_oValList);
	}

	/** 포함 여부를 검사한다 */
	public static bool ExIsContains<T>(this List<T> a_oSender, List<T> a_oValList) {
		CAccess.Assert(a_oSender != null && a_oValList != null);

		for(int i = 0; i < a_oValList.Count; ++i) {
			// 값이 없을 경우
			if(!a_oSender.Contains(a_oValList[i])) {
				return false;
			}
		}

		return true;
	}

	/** 포함 여부를 검사한다 */
	public static bool ExIsContains<K, V>(this Dictionary<K, V> a_oSender, List<K> a_oKeyList) {
		CAccess.Assert(a_oSender != null && a_oKeyList != null);

		for(int i = 0; i < a_oKeyList.Count; ++i) {
			// 값이 없을 경우
			if(!a_oSender.ContainsKey(a_oKeyList[i])) {
				return false;
			}
		}

		return true;
	}

	/** 완료 여부를 검사한다 */
	public static bool ExIsComplete<T>(this Task<T> a_oSender) {
		CAccess.Assert(a_oSender != null);
		return (a_oSender as Task).ExIsComplete() && a_oSender.Result != null;
	}

	/** 값을 반환한다 */
	public static T ExGetVal<T>(this T[] a_oSender, int a_nIdx, T a_tDefVal) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_nIdx) ? a_oSender[a_nIdx] : a_tDefVal;
	}

	/** 값을 반환한다 */
	public static T ExGetVal<T>(this T[,] a_oSender, Vector2Int a_stIdx, T a_tDefVal) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_stIdx) ? a_oSender[a_stIdx.y, a_stIdx.x] : a_tDefVal;
	}

	/** 값을 반환한다 */
	public static T ExGetVal<T>(this List<T> a_oSender, int a_nIdx, T a_tDefVal) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_nIdx) ? a_oSender[a_nIdx] : a_tDefVal;
	}
	
	/** 필드 값을 반환한다 */
	public static object ExGetFieldVal<T>(this object a_oSender, string a_oName, BindingFlags a_eBindingFlags) {
		CAccess.Assert(a_oName.ExIsValid());
		return typeof(T).GetField(a_oName, a_eBindingFlags).GetValue(a_oSender);
	}

	/** 런타임 필드 값을 반환한다 */
	public static object ExGetRuntimeFieldVal<T>(this object a_oSender, string a_oName) {
		CAccess.Assert(a_oName.ExIsValid());
		var oFieldInfos = typeof(T).GetRuntimeFields();

		foreach(var oFieldInfo in oFieldInfos) {
			// 필드 이름이 동일 할 경우
			if(oFieldInfo.Name.Equals(a_oName)) {
				return oFieldInfo.GetValue(a_oSender);
			}
		}

		return null;
	}

	/** 프로퍼티 값을 반환한다 */
	public static object ExGetPropertyVal<T>(this object a_oSender, string a_oName, BindingFlags a_eBindingFlags) {
		CAccess.Assert(a_oName.ExIsValid());
		return typeof(T).GetProperty(a_oName, a_eBindingFlags).GetValue(a_oSender);
	}

	/** 런타임 프로퍼티 값을 반환한다 */
	public static object ExGetRuntimePropertyVal<T>(this object a_oSender, string a_oName) {
		CAccess.Assert(a_oName.ExIsValid());
		var oPropertyInfos = typeof(T).GetRuntimeProperties();
		
		foreach(var oPropertyInfo in oPropertyInfos) {
			// 프로퍼티 이름과 동일 할 경우
			if(oPropertyInfo.Name.Equals(a_oName)) {
				return oPropertyInfo.GetValue(a_oSender);
			}
		}

		return null;
	}

	/** 값을 변경한다 */
	public static void ExSetVal<T>(this T[] a_oSender, int a_nIdx, T a_tVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 인덱스가 유효 할 경우
		if(a_oSender != null && a_oSender.ExIsValidIdx(a_nIdx)) {
			a_oSender[a_nIdx] = a_tVal;
		}
	}

	/** 값을 변경한다 */
	public static void ExSetVal<T>(this T[,] a_oSender, Vector2Int a_stIdx, T a_tVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 인덱스가 유효 할 경우
		if(a_oSender != null && a_oSender.ExIsValidIdx(a_stIdx)) {
			a_oSender[a_stIdx.y, a_stIdx.x] = a_tVal;
		}
	}

	/** 값을 변경한다 */
	public static void ExSetVal<T>(this List<T> a_oSender, int a_nIdx, T a_tVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 인덱스가 유효 할 경우
		if(a_oSender != null && a_oSender.ExIsValidIdx(a_nIdx)) {
			a_oSender[a_nIdx] = a_tVal;
		}
	}

	/** 값을 변경한다 */
	public static void ExSetVal<K, V>(this Dictionary<K, V> a_oSender, K a_tKey, V a_tVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 키가 유효 할 경우
		if(a_oSender != null && a_oSender.ContainsKey(a_tKey)) {
			a_oSender[a_tKey] = a_tVal;
		}
	}

	/** 값을 변경한다 */
	public static void ExSetVals<T>(this T[] a_oSender, List<(int, T)> a_oValInfoList, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oValInfoList != null));

		// 값이 유효 할 경우
		if(a_oSender != null && a_oValInfoList != null) {
			for(int i = 0; i < a_oValInfoList.Count; ++i) {
				a_oSender.ExSetVal(a_oValInfoList[i].Item1, a_oValInfoList[i].Item2, a_bIsEnableAssert);
			}
		}
	}

	/** 값을 변경한다 */
	public static void ExSetVals<T>(this List<T> a_oSender, List<(int, T)> a_oValInfoList, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oValInfoList != null));

		// 값이 유효 할 경우
		if(a_oSender != null && a_oValInfoList != null) {
			for(int i = 0; i < a_oValInfoList.Count; ++i) {
				a_oSender.ExSetVal(a_oValInfoList[i].Item1, a_oValInfoList[i].Item2, a_bIsEnableAssert);
			}
		}
	}

	/** 값을 변경한다 */
	public static void ExSetVals<K, V>(this Dictionary<K, V> a_oSender, Dictionary<K, V> a_oValDict, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oValDict != null));

		// 값이 존재 할 경우
		if(a_oSender != null && a_oValDict != null) {
			foreach(var stKeyVal in a_oValDict) {
				a_oSender.ExSetVal(stKeyVal.Key, stKeyVal.Value, a_bIsEnableAssert);
			}
		}
	}

	/** 필드 값을 변경한다 */
	public static void ExSetFieldVal<T>(this object a_oSender, string a_oName, BindingFlags a_eBindingFlags, object a_oVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oName.ExIsValid());

		// 이름이 유효 할 경우
		if(a_oName.ExIsValid()) {
			typeof(T).GetField(a_oName, a_eBindingFlags).SetValue(a_oSender, a_oVal);
		}
	}

	/** 런타임 필드 값을 변경한다 */
	public static void ExSetRuntimeFieldVal<T>(this object a_oSender, string a_oName, object a_oVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oName.ExIsValid());

		// 이름이 유효 할 경우
		if(a_oName.ExIsValid()) {
			var oFieldInfos = typeof(T).GetRuntimeFields();

			foreach(var oFieldInfo in oFieldInfos) {
				// 필드 이름이 동일 할 경우
				if(oFieldInfo.Name.Equals(a_oName)) {
					oFieldInfo.SetValue(a_oSender, a_oVal);
				}
			}
		}
	}

	/** 프로퍼티 값을 변경한다 */
	public static void ExSetPropertyVal<T>(this object a_oSender, string a_oName, BindingFlags a_eBindingFlags, object a_oVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oName.ExIsValid());

		// 이름이 유효 할 경우
		if(a_oName.ExIsValid()) {
			typeof(T).GetProperty(a_oName, a_eBindingFlags).SetValue(a_oSender, a_oVal);
		}
	}

	/** 런타임 프로퍼티 값을 변경한다 */
	public static void ExSetRuntimePropertyVal<T>(this object a_oSender, string a_oName, object a_oVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oName.ExIsValid());

		// 이름이 유효 할 경우
		if(a_oName.ExIsValid()) {
			var oPropertyInfos = typeof(T).GetRuntimeProperties();

			foreach(var oPropertyInfo in oPropertyInfos) {
				// 프로퍼티 이름이 동일 할 경우
				if(oPropertyInfo.Name.Equals(a_oName)) {
					oPropertyInfo.SetValue(a_oSender, a_oVal);
				}
			}
		}
	}

	/** 2 차원 배열 => 1 차원 배열로 복사한다 */
	private static void ExCopyToSingleArray<T>(this T[,] a_oSender, T[] a_oDestVals, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oDestVals != null));

		// 복사가 가능 할 경우
		if(a_oSender != null && a_oDestVals != null) {
			for(int i = 0; i < a_oSender.GetLength(KCDefine.B_VAL_0_INT); ++i) {
				for(int j = 0; j < a_oSender.GetLength(KCDefine.B_VAL_1_INT); ++j) {
					a_oDestVals.ExSetVal((i * a_oSender.GetLength(KCDefine.B_VAL_1_INT)) + j, a_oSender[i, j], a_bIsEnableAssert);
				}
			}
		}
	}

	/** 2 차원 배열 => 1 차원 배열로 변환한다 */
	private static T[] ExToSingleArray<T>(this T[,] a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());

		var oVals = new T[a_oSender.Length];
		a_oSender.ExCopyToSingleArray(oVals);

		return oVals;
	}
	#endregion			// 제네릭 클래스 함수
}
