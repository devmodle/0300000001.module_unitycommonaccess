using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

/** 기본 접근자 확장 클래스 */
public static partial class CAccessExtension {
	#region 클래스 함수
	/** 정수 여부를 검사한다 */
	public static bool ExIsInt(this decimal a_dmSender) {
		return a_dmSender == decimal.Truncate(a_dmSender);
	}

	/** 실수 여부를 검사한다 */
	public static bool ExIsReal(this decimal a_dmSender) {
		return a_dmSender != decimal.Truncate(a_dmSender);
	}

	/** 유효 여부를 검사한다 */
	public static bool ExIsValid(this float a_fSender) {
		return !float.IsNaN(a_fSender) && !float.IsInfinity(a_fSender);
	}

	/** 유효 여부를 검사한다 */
	public static bool ExIsValid(this double a_dblSender) {
		return !double.IsNaN(a_dblSender) && !double.IsInfinity(a_dblSender);
	}

	/** 유효 여부를 검사한다 */
	public static bool ExIsValid(this string a_oSender) {
		return !string.IsNullOrEmpty(a_oSender);
	}

	/** 유효 여부를 검사한다 */
	public static bool ExIsValid(this Vector3 a_stSender) {
		return a_stSender.x.ExIsValid() && a_stSender.y.ExIsValid() && a_stSender.z.ExIsValid();
	}

	/** 유효 여부를 검사한다 */
	public static bool ExIsValid(this System.DateTime a_stSender) {
		return a_stSender.Ticks >= KCDefine.B_VAL_0_INT;
	}

	/** 유효 여부를 검사한다 */
	public static bool ExIsValid(this System.TimeSpan a_stSender) {
		return a_stSender.Ticks >= KCDefine.B_VAL_0_INT;
	}

	/** 유효 여부를 검사한다 */
	public static bool ExIsValid(this SimpleJSON.JSONNode a_oSender) {
		return a_oSender != null && a_oSender.Value != null && !a_oSender.Value.Equals(KCDefine.B_TEXT_NULL);
	}

	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx(this int a_nSender) {
		return a_nSender > KCDefine.B_IDX_INVALID;
	}

	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx(this Vector2Int a_stSender, int a_nZ = KCDefine.B_VAL_0_INT) {
		return a_stSender.ExTo3D(a_nZ).ExIsValidIdx();
	}

	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx(this Vector3Int a_stSender) {
		return a_stSender.x > KCDefine.B_IDX_INVALID && a_stSender.y > KCDefine.B_IDX_INVALID && a_stSender.z > KCDefine.B_IDX_INVALID;
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
		double dblDelta = System.Math.Abs(a_dblSender) - System.Math.Abs(a_dblRhs);
		return dblDelta >= -double.Epsilon && dblDelta <= double.Epsilon;
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

	/** 성공 완료 여부를 검사한다 */
	public static bool ExIsCompleteSuccess(this Task a_oSender) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsComplete() && a_oSender.IsCompletedSuccessfully;
	}

	/** 유럽 연합 여부를 검사한다 */
	public static bool ExIsEU(this string a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());
		return KCDefine.B_EU_COUNTRY_CODE_LIST.Contains(a_oSender.ToUpper());
	}

	/** 범위 포함 여부를 검사한다 */
	public static bool ExIsInRange(this int a_nSender, int a_nMinVal, int a_nMaxVal) {
		CAccessExtension.LessCorrectSwap(ref a_nMinVal, ref a_nMaxVal);
		return a_nSender >= a_nMinVal && a_nSender <= a_nMaxVal;
	}

	/** 범위 포함 여부를 검사한다 */
	public static bool ExIsInRange(this long a_nSender, long a_nMinVal, long a_nMaxVal) {
		CAccessExtension.LessCorrectSwap(ref a_nMinVal, ref a_nMaxVal);
		return a_nSender >= a_nMinVal && a_nSender <= a_nMaxVal;
	}

	/** 범위 포함 여부를 검사한다 */
	public static bool ExIsInRange(this float a_fSender, float a_fMinVal, float a_fMaxVal) {
		CAccessExtension.LessCorrectSwap(ref a_fMinVal, ref a_fMaxVal);
		return a_fSender.ExIsGreateEquals(a_fMinVal) && a_fSender.ExIsLessEquals(a_fMaxVal);
	}

	/** 범위 포함 여부를 검사한다 */
	public static bool ExIsInRange(this double a_dblSender, double a_dblMinVal, double a_dblMaxVal) {
		CAccessExtension.LessCorrectSwap(ref a_dblMinVal, ref a_dblMaxVal);
		return a_dblSender.ExIsGreateEquals(a_dblMinVal) && a_dblSender.ExIsLessEquals(a_dblMaxVal);
	}

	/** 최소 값을 반환한다 */
	public static float ExGetMinVal(this float a_fSender, float a_fRhs) {
		return a_fSender.ExIsLess(a_fRhs) ? a_fSender : a_fRhs;
	}

	/** 최소 값을 반환한다 */
	public static double ExGetMinVal(this double a_dblSender, double a_dblRhs) {
		return a_dblSender.ExIsLess(a_dblRhs) ? a_dblSender : a_dblRhs;
	}

	/** 최대 값을 반환한다 */
	public static float ExGetMaxVal(this float a_fSender, float a_fRhs) {
		return a_fSender.ExIsGreate(a_fRhs) ? a_fSender : a_fRhs;
	}

	/** 최대 값을 반환한다 */
	public static double ExGetMaxVal(this double a_dblSender, double a_dblRhs) {
		return a_dblSender.ExIsGreate(a_dblRhs) ? a_dblSender : a_dblRhs;
	}

	/** 보정 값을 반환한다 */
	public static float ExGetClampVal(this float a_fSender, float a_fMinVal, float a_fMaxVal) {
		return a_fSender.ExGetMaxVal(a_fMinVal).ExGetMinVal(a_fMaxVal);
	}

	/** 보정 값을 반환한다 */
	public static double ExGetClampVal(this double a_dblSender, double a_dblMinVal, double a_dblMaxVal) {
		return a_dblSender.ExGetMaxVal(a_dblMinVal).ExGetMinVal(a_dblMaxVal);
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
	public static Vector3Int ExGetPrevIdx(this Vector2Int a_stSender, EDirection a_eDirection, int a_nZ = KCDefine.B_VAL_0_INT) {
		return a_stSender.ExTo3D(a_nZ).ExGetPrevIdx(a_eDirection);
	}

	/** 이전 인덱스를 반환한다 */
	public static Vector3Int ExGetPrevIdx(this Vector3Int a_stSender, EDirection a_eDirection) {
		CAccess.Assert(!a_stSender.Equals(KCDefine.B_IDX_INVALID_3D));
		CAccess.Assert(a_eDirection >= EDirection.UP && a_eDirection <= EDirection.RIGHT_DOWN);

		return new Vector3Int(a_stSender.x + KCDefine.B_IDX_OFFSET_INFO_LIST_2D[(int)a_eDirection].Item1.x, a_stSender.y + KCDefine.B_IDX_OFFSET_INFO_LIST_2D[(int)a_eDirection].Item1.y, a_stSender.z);
	}

	/** 다음 인덱스를 반환한다 */
	public static Vector3Int ExGetNextIdx(this Vector2Int a_stSender, EDirection a_eDirection, int a_nZ = KCDefine.B_VAL_0_INT) {
		return a_stSender.ExTo3D(a_nZ).ExGetNextIdx(a_eDirection);
	}

	/** 다음 인덱스를 반환한다 */
	public static Vector3Int ExGetNextIdx(this Vector3Int a_stSender, EDirection a_eDirection) {
		CAccess.Assert(!a_stSender.Equals(KCDefine.B_IDX_INVALID_3D));
		CAccess.Assert(a_eDirection >= EDirection.UP && a_eDirection <= EDirection.RIGHT_DOWN);

		return new Vector3Int(a_stSender.x + KCDefine.B_IDX_OFFSET_INFO_LIST_2D[(int)a_eDirection].Item2.x, a_stSender.y + KCDefine.B_IDX_OFFSET_INFO_LIST_2D[(int)a_eDirection].Item2.y, a_stSender.z);
	}

	/** 파일 이름을 반환한다 */
	public static string ExGetFileName(this string a_oSender, bool a_bIsIncludeExtension = true) {
		CAccess.Assert(a_oSender != null);
		return a_bIsIncludeExtension ? Path.GetFileName(a_oSender) : Path.GetFileNameWithoutExtension(a_oSender);
	}

	/** 파일 이름이 변경 된 경로를 반환한다 */
	public static string ExGetReplaceFileNamePath(this string a_oSender, string a_oFileName, bool a_bIsResetExtension = false) {
		CAccess.Assert(a_oSender != null && a_oFileName.ExIsValid());
		return a_oSender.Replace(a_oSender.ExGetFileName(a_bIsResetExtension), a_oFileName);
	}

	/** 값을 탐색한다 */
	public static int ExFindVal(this SimpleJSON.JSONArray a_oSender, System.Predicate<SimpleJSON.JSONNode> a_oCompare) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);

		for(int i = 0; i < a_oSender.Count; ++i) {
			// 값이 존재 할 경우
			if(a_oCompare(a_oSender[i])) {
				return i;
			}
		}

		return KCDefine.B_IDX_INVALID;
	}
	#endregion // 클래스 함수

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
	public static bool ExIsValid<T>(this Stack<T> a_oSender) {
		return a_oSender != null && a_oSender.Count > KCDefine.B_VAL_0_INT;
	}

	/** 유효 여부를 검사한다 */
	public static bool ExIsValid<T>(this Queue<T> a_oSender) {
		return a_oSender != null && a_oSender.Count > KCDefine.B_VAL_0_INT;
	}

	/** 유효 여부를 검사한다 */
	public static bool ExIsValid<T>(this HashSet<T> a_oSender) {
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
	public static bool ExIsValidIdx<T>(this T[,] a_oSender, Vector3Int a_stIdx) {
		CAccess.Assert(a_oSender != null);
		return (a_stIdx.y > KCDefine.B_IDX_INVALID && a_stIdx.y < a_oSender.GetLength(KCDefine.B_VAL_0_INT)) && (a_stIdx.x > KCDefine.B_IDX_INVALID && a_stIdx.x < a_oSender.GetLength(KCDefine.B_VAL_1_INT));
	}
	
	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx<T>(this List<T> a_oSender, int a_nIdx) {
		CAccess.Assert(a_oSender != null);
		return a_nIdx > KCDefine.B_IDX_INVALID && a_nIdx < a_oSender.Count;
	}

	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx<T>(this List<T[]> a_oSender, Vector3Int a_stIdx) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_stIdx.y) && a_oSender[a_stIdx.y].ExIsValidIdx(a_stIdx.x);
	}

	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx<T>(this List<T[,]> a_oSender, Vector3Int a_stIdx) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_stIdx.z) && a_oSender[a_stIdx.z].ExIsValidIdx(a_stIdx);
	}

	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx<T>(this List<List<T>> a_oSender, Vector3Int a_stIdx) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_stIdx.y) && a_oSender[a_stIdx.y].ExIsValidIdx(a_stIdx.x);
	}

	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx<V>(this Dictionary<int, V> a_oSender, int a_nIdx) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ContainsKey(a_nIdx);
	}

	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx<V>(this Dictionary<int, Dictionary<int, V>> a_oSender, Vector3Int a_stIdx) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.TryGetValue(a_stIdx.y, out Dictionary<int, V> oValDict) && oValDict.ExIsValidIdx(a_stIdx.x);
	}

	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx<V>(this Dictionary<int, Dictionary<int, Dictionary<int, V>>> a_oSender, Vector3Int a_stIdx) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.TryGetValue(a_stIdx.z, out Dictionary<int, Dictionary<int, V>> oValDictContainer) && oValDictContainer.ExIsValidIdx(a_stIdx);
	}

	/** 포함 여부를 검사한다 */
	public static bool ExIsContains<T>(this T[] a_oSender, T a_tVal) {
		CAccess.Assert(a_oSender != null);
		return System.Array.FindIndex(a_oSender, (a_tCompareVal) => a_tVal.Equals(a_tCompareVal)) > KCDefine.B_IDX_INVALID;
	}

	/** 포함 여부를 검사한다 */
	public static bool ExIsContains<T>(this T[] a_oSender, List<T> a_oValList) {
		CAccess.Assert(a_oSender != null && a_oValList != null);
		return a_oValList.All((a_tVal) => a_oSender.ExIsContains(a_tVal));
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
		return a_oValList.All((a_tVal) => a_oSender.Contains(a_tVal));
	}

	/** 포함 여부를 검사한다 */
	public static bool ExIsContains<K, V>(this Dictionary<K, V> a_oSender, List<K> a_oKeyList) {
		CAccess.Assert(a_oSender != null && a_oKeyList != null);
		return a_oKeyList.All((a_tKey) => a_oSender.ContainsKey(a_tKey));
	}

	/** 완료 여부를 검사한다 */
	public static bool ExIsComplete<T>(this Task<T> a_oSender) {
		CAccess.Assert(a_oSender != null);
		return (a_oSender as Task).ExIsComplete() && a_oSender.Result != null;
	}

	/** 성공 완료 여부를 검사한다 */
	public static bool ExIsCompleteSuccess<T>(this Task<T> a_oSender) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsComplete() && a_oSender.IsCompletedSuccessfully;
	}

	/** 값을 반환한다 */
	public static T ExGetVal<T>(this T[] a_oSender, int a_nIdx, T a_tDefVal) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_nIdx) ? a_oSender[a_nIdx] : a_tDefVal;
	}

	/** 값을 반환한다 */
	public static T ExGetVal<T>(this T[] a_oSender, System.Predicate<T> a_oCompare, T a_tDefVal) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);

		for(int i = 0; i < a_oSender.Length; ++i) {
			// 값이 존재 할 경우
			if(a_oCompare(a_oSender[i])) {
				return a_oSender[i];
			}
		}

		return a_tDefVal;
	}

	/** 값을 반환한다 */
	public static T ExGetVal<T>(this T[,] a_oSender, Vector3Int a_stIdx, T a_tDefVal) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_stIdx) ? a_oSender[a_stIdx.y, a_stIdx.x] : a_tDefVal;
	}

	/** 값을 반환한다 */
	public static T ExGetVal<T>(this T[,] a_oSender, System.Predicate<T> a_oCompare, T a_tDefVal) {
		CAccess.Assert(a_oSender != null);

		for(int i = 0; i < a_oSender.GetLength(KCDefine.B_VAL_0_INT); ++i) {
			for(int j = 0; j < a_oSender.GetLength(KCDefine.B_VAL_1_INT); ++j) {
				// 값이 존재 할 경우
				if(a_oCompare(a_oSender[i, j])) {
					return a_oSender[i, j];
				}
			}
		}

		return a_tDefVal;
	}

	/** 값을 반환한다 */
	public static T ExGetVal<T>(this List<T> a_oSender, int a_nIdx, T a_tDefVal) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_nIdx) ? a_oSender[a_nIdx] : a_tDefVal;
	}

	/** 값을 반환한다 */
	public static T ExGetVal<T>(this List<T> a_oSender, System.Predicate<T> a_oCompare, T a_tDefVal) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);
		return a_oSender.ExGetVal(a_oSender.FindIndex(a_oCompare), a_tDefVal);
	}

	/** 값을 반환한다 */
	public static T ExGetVal<T>(this List<T[]> a_oSender, Vector3Int a_stIdx, T a_tDefVal) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_stIdx) ? a_oSender[a_stIdx.y][a_stIdx.x] : a_tDefVal;
	}

	/** 값을 반환한다 */
	public static T ExGetVal<T>(this List<T[,]> a_oSender, Vector3Int a_stIdx, T a_tDefVal) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_stIdx) ? a_oSender[a_stIdx.z][a_stIdx.y, a_stIdx.x] : a_tDefVal;
	}

	/** 값을 반환한다 */
	public static T ExGetVal<T>(this List<List<T>> a_oSender, Vector3Int a_stIdx, T a_tDefVal) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_stIdx) ? a_oSender[a_stIdx.y][a_stIdx.x] : a_tDefVal;
	}

	/** 값을 반환한다 */
	public static V ExGetVal<K, V>(this Dictionary<K, V> a_oSender, System.Predicate<KeyValuePair<K, V>> a_oCompare, V a_tDefVal) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);
		var stResult = a_oSender.ExFindVal(a_oCompare);

		return stResult.Item1 ? a_oSender[stResult.Item2] : a_tDefVal;
	}

	/** 값을 반환한다 */
	public static V ExGetVal<V>(this Dictionary<int, Dictionary<int, V>> a_oSender, Vector3Int a_stIdx, V a_tDefVal) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_stIdx) ? a_oSender[a_stIdx.y][a_stIdx.x] : a_tDefVal;
	}

	/** 값을 반환한다 */
	public static V ExGetVal<V>(this Dictionary<int, Dictionary<int, Dictionary<int, V>>> a_oSender, Vector3Int a_stIdx, V a_tDefVal) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_stIdx) ? a_oSender[a_stIdx.z][a_stIdx.y][a_stIdx.x] : a_tDefVal;
	}

	/** 값을 반환한다 */
	public static List<T> ExGetVals<T>(this List<T> a_oSender, System.Predicate<T> a_oCompare, List<T> a_oOutValList) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);
		a_oOutValList = a_oOutValList ?? new List<T>();

		for(int i = 0; i < a_oSender.Count; ++i) {
			// 값이 존재 할 경우
			if(a_oCompare(a_oSender[i])) {
				a_oOutValList.Add(a_oSender[i]);
			}
		}

		return a_oOutValList;
	}

	/** 값을 반환한다 */
	public static List<V> ExGetVals<K, V>(this Dictionary<K, V> a_oSender, System.Predicate<KeyValuePair<K, V>> a_oCompare, List<V> a_oOutValList) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);
		a_oOutValList = a_oOutValList ?? new List<V>();

		foreach(var stKeyVal in a_oSender) {
			// 값이 존재 할 경우
			if(a_oCompare(stKeyVal)) {
				a_oOutValList.Add(stKeyVal.Value);
			}
		}

		return a_oOutValList;
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
	public static void ExSetVal<T>(this T[,] a_oSender, Vector3Int a_stIdx, T a_tVal, bool a_bIsEnableAssert = true) {
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

	/** 값을 반환한다 */
	public static void ExSetVal<T>(this List<T[]> a_oSender, Vector3Int a_stIdx, T a_tVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 인덱스가 유효 할 경우
		if(a_oSender != null && a_oSender.ExIsValidIdx(a_stIdx)) {
			a_oSender[a_stIdx.y][a_stIdx.x] = a_tVal;
		}
	}

	/** 값을 반환한다 */
	public static void ExSetVal<T>(this List<T[,]> a_oSender, Vector3Int a_stIdx, T a_tVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 인덱스가 유효 할 경우
		if(a_oSender != null && a_oSender.ExIsValidIdx(a_stIdx)) {
			a_oSender[a_stIdx.z][a_stIdx.y, a_stIdx.x] = a_tVal;
		}
	}

	/** 값을 반환한다 */
	public static void ExSetVal<T>(this List<List<T>> a_oSender, Vector3Int a_stIdx, T a_tVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 인덱스가 유효 할 경우
		if(a_oSender != null && a_oSender.ExIsValidIdx(a_stIdx)) {
			a_oSender[a_stIdx.y][a_stIdx.x] = a_tVal;
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

	/** 값을 반환한다 */
	public static void ExSetVal<V>(this Dictionary<int, V> a_oSender, int a_nIdx, V a_tVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 키가 유효 할 경우
		if(a_oSender != null && a_oSender.ExIsValidIdx(a_nIdx)) {
			a_oSender[a_nIdx] = a_tVal;
		}
	}

	/** 값을 반환한다 */
	public static void ExSetVal<V>(this Dictionary<int, Dictionary<int, V>> a_oSender, Vector3Int a_stIdx, V a_tVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 키가 유효 할 경우
		if(a_oSender != null && a_oSender.ExIsValidIdx(a_stIdx)) {
			a_oSender[a_stIdx.y][a_stIdx.x] = a_tVal;
		}
	}

	/** 값을 반환한다 */
	public static void ExSetVal<V>(this Dictionary<int, Dictionary<int, Dictionary<int, V>>> a_oSender, Vector3Int a_stIdx, V a_tVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 키가 유효 할 경우
		if(a_oSender != null && a_oSender.ExIsValidIdx(a_stIdx)) {
			a_oSender[a_stIdx.z][a_stIdx.y][a_stIdx.x] = a_tVal;
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

	/** 값을 추가한다 */
	public static void ExAddVal<T>(this List<T> a_oSender, T a_tVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 추가가 가능 할 경우
		if(a_oSender != null && !a_oSender.IndexOf(a_tVal).ExIsValidIdx()) {
			a_oSender.Add(a_tVal);
		}
	}

	/** 값을 추가한다 */
	public static void ExAddVal<T>(this List<T> a_oSender, T a_tVal, System.Predicate<T> a_oCompare, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCompare != null));

		// 값 추가가 가능 할 경우
		if(a_oSender != null && a_oCompare != null && !a_oSender.FindIndex(a_oCompare).ExIsValidIdx()) {
			a_oSender.Add(a_tVal);
		}
	}

	/** 값을 추가한다 */
	public static void ExAddVal<K, V>(this Dictionary<K, V> a_oSender, K a_tKey, V a_tVal, System.Predicate<KeyValuePair<K, V>> a_oCompare, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCompare != null));

		// 값 추가가 가능 할 경우
		if(a_oSender != null && a_oCompare != null && !a_oSender.ExFindVal(a_oCompare).Item1) {
			a_oSender.TryAdd(a_tKey, a_tVal);
		}
	}

	/** 값을 추가한다 */
	public static void ExAddVals<T>(this List<T> a_oSender, List<T> a_oValList, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oValList != null));

		// 값 추가가 가능 할 경우
		if(a_oSender != null && a_oValList != null) {
			for(int i = 0; i < a_oValList.Count; ++i) {
				a_oSender.ExAddVal(a_oValList[i], a_bIsEnableAssert);
			}
		}
	}

	/** 값을 추가한다 */
	public static void ExAddVals<T>(this List<T> a_oSender, List<T> a_oValList, System.Predicate<T> a_oCompare, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oValList != null && a_oCompare != null));

		// 값 추가가 가능 할 경우
		if(a_oSender != null && a_oValList != null && a_oCompare != null) {
			for(int i = 0; i < a_oValList.Count; ++i) {
				a_oSender.ExAddVal(a_oValList[i], a_oCompare, a_bIsEnableAssert);
			}
		}
	}

	/** 값을 추가한다 */
	public static void ExAddVals<K, V>(this Dictionary<K, V> a_oSender, Dictionary<K, V> a_oValDict, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oValDict != null));

		// 값 추가가 가능 할 경우
		if(a_oSender != null && a_oValDict != null) {
			foreach(var stKeyVal in a_oValDict) {
				a_oSender.TryAdd(stKeyVal.Key, stKeyVal.Value);
			}
		}
	}

	/** 값을 추가한다 */
	public static void ExAddVals<K, V>(this Dictionary<K, V> a_oSender, Dictionary<K, V> a_oValDict, System.Predicate<KeyValuePair<K, V>> a_oCompare, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oValDict != null && a_oCompare != null));

		// 값 추가가 가능 할 경우
		if(a_oSender != null && a_oValDict != null && a_oCompare != null) {
			foreach(var stKeyVal in a_oValDict) {
				a_oSender.ExAddVal(stKeyVal.Key, stKeyVal.Value, a_oCompare, a_bIsEnableAssert);
			}
		}
	}

	/** 값을 추가한다 */
	public static void ExInsertVal<T>(this List<T> a_oSender, int a_nIdx, T a_tVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 추가가 가능 할 경우
		if(a_oSender != null && a_oSender.ExIsValidIdx(a_nIdx)) {
			a_oSender.Insert(a_nIdx, a_tVal);
		}
	}

	/** 값을 추가한다 */
	public static void ExInsertVal<T>(this List<T> a_oSender, System.Predicate<T> a_oCompare, T a_tVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCompare != null));

		// 값 추가가 가능 할 경우
		if(a_oSender != null && a_oCompare != null) {
			a_oSender.ExInsertVal(a_oSender.FindIndex(a_oCompare), a_tVal);
		}
	}

	/** 값을 추가한다 */
	public static void ExInsertVals<T>(this List<T> a_oSender, System.Predicate<T> a_oCompare, List<T> a_oValList, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oValList != null && a_oCompare != null));

		// 값 추가가 가능 할 경우
		if(a_oSender != null && a_oValList != null && a_oCompare != null) {
			for(int i = 0; i < a_oValList.Count; ++i) {
				a_oSender.ExInsertVal(a_oCompare, a_oValList[i]);
			}
		}
	}

	/** 값을 제거한다 */
	public static void ExRemoveVal<T>(this List<T> a_oSender, T a_tVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 제거가 가능 할 경우
		if(a_oSender != null && a_oSender.Contains(a_tVal)) {
			a_oSender.ExRemoveVal((a_tCompareVal) => a_tCompareVal.Equals(a_tVal), a_bIsEnableAssert);
		}
	}

	/** 값을 제거한다 */
	public static void ExRemoveVal<T>(this List<T> a_oSender, System.Predicate<T> a_oCompare, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCompare != null));

		// 값 제거가 가능 할 경우
		if(a_oSender != null && a_oCompare != null) {
			a_oSender.ExRemoveValAt(a_oSender.FindIndex(a_oCompare), a_bIsEnableAssert);
		}
	}

	/** 값을 제거한다 */
	public static void ExRemoveVal<K, V>(this Dictionary<K, V> a_oSender, K a_tKey, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 제거가 가능 할 경우
		if(a_oSender != null && a_oSender.ContainsKey(a_tKey)) {
			a_oSender.Remove(a_tKey);
		}
	}

	/** 값을 제거한다 */
	public static void ExRemoveVal<K, V>(this Dictionary<K, V> a_oSender, System.Predicate<KeyValuePair<K, V>> a_oCompare, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCompare != null));

		// 딕셔너리가 존재 할 경우
		if(a_oSender != null && a_oCompare != null) {
			var stResult = a_oSender.ExFindVal(a_oCompare);

			// 값 제거가 가능 할 경우
			if(stResult.Item1) {
				a_oSender.ExRemoveVal(stResult.Item2);
			}
		}
	}

	/** 값을 제거한다 */
	public static void ExRemoveValAt<T>(this List<T> a_oSender, int a_nIdx, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 제거가 가능 할 경우
		if(a_oSender != null && a_oSender.ExIsValidIdx(a_nIdx)) {
			a_oSender.RemoveAt(a_nIdx);
		}
	}

	/** 값을 제거한다 */
	public static void ExRemoveVals<T>(this List<T> a_oSender, List<T> a_oValList, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oValList != null));

		// 값 제거가 가능 할 경우
		if(a_oSender != null && a_oValList != null) {
			for(int i = 0; i < a_oValList.Count; ++i) {
				a_oSender.ExRemoveVal(a_oValList[i], a_bIsEnableAssert);
			}
		}
	}

	/** 값을 제거한다 */
	public static void ExRemoveVals<T>(this List<T> a_oSender, System.Predicate<T> a_oCompare, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCompare != null));

		// 값 제거가 가능 할 경우
		if(a_oSender != null && a_oCompare != null) {
			int nIdx = a_oSender.FindIndex(a_oCompare);

			do {
				a_oSender.ExRemoveValAt(nIdx, a_bIsEnableAssert);
			} while((nIdx = a_oSender.FindIndex(a_oCompare)).ExIsValidIdx());
		}
	}

	/** 값을 제거한다 */
	public static void ExRemoveVals<K, V>(this Dictionary<K, V> a_oSender, List<K> a_oKeyList, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oKeyList != null));

		// 값 제거가 가능 할 경우
		if(a_oSender != null && a_oKeyList != null) {
			for(int i = 0; i < a_oKeyList.Count; ++i) {
				a_oSender.ExRemoveVal(a_oKeyList[i], a_bIsEnableAssert);
			}
		}
	}

	/** 값을 제거한다 */
	public static void ExRemoveVals<K, V>(this Dictionary<K, V> a_oSender, System.Predicate<KeyValuePair<K, V>> a_oCompare, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCompare != null));

		// 값 제거가 가능 할 경우
		if(a_oSender != null && a_oCompare != null) {
			var stResult = a_oSender.ExFindVal(a_oCompare);

			do {
				a_oSender.ExRemoveVal(stResult.Item2, a_bIsEnableAssert);
			} while((stResult = a_oSender.ExFindVal(a_oCompare)).Item1);
		}
	}

	/** 값을 대체한다 */
	public static void ExReplaceVal<T>(this T[] a_oSender, T a_tReplaceVal, System.Predicate<T> a_oCompare, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCompare != null));

		// 배열이 존재 할 경우
		if(a_oSender != null && a_oCompare != null) {
			int nIdx = a_oSender.ExFindVal(a_oCompare);

			// 값 대체가 가능 할 경우
			if(a_oSender.ExIsValidIdx(nIdx)) {
				a_oSender[nIdx] = a_tReplaceVal;
			}
		}
	}

	/** 값을 대체한다 */
	public static void ExReplaceVal<T>(this T[,] a_oSender, T a_tReplaceVal, System.Predicate<T> a_oCompare, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCompare != null));

		// 배열이 존재 할 경우
		if(a_oSender != null && a_oCompare != null) {
			var stIdx = a_oSender.ExFindVal(a_oCompare);

			// 값 대체가 가능 할 경우
			if(a_oSender.ExIsValidIdx(stIdx)) {
				a_oSender[stIdx.y, stIdx.x] = a_tReplaceVal;
			}
		}
	}

	/** 값을 대체한다 */
	public static void ExReplaceVal<T>(this List<T> a_oSender, T a_tVal, T a_tReplaceVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 리스트가 존재 할 경우
		if(a_oSender != null) {
			int nIdx = a_oSender.FindIndex((a_tCompareVal) => a_tCompareVal.Equals(a_tVal));

			// 값 대체가 가능 할 경우
			if(a_oSender.ExIsValidIdx(nIdx)) {
				a_oSender[nIdx] = a_tReplaceVal;
			} else {
				a_oSender.Add(a_tReplaceVal);
			}
		}
	}

	/** 값을 대체한다 */
	public static void ExReplaceVal<T>(this List<T> a_oSender, T a_tReplaceVal, System.Predicate<T> a_oCompare, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCompare != null));

		// 리스트가 존재 할 경우
		if(a_oSender != null && a_oCompare != null) {
			int nIdx = a_oSender.FindIndex(a_oCompare);

			// 값 대체가 가능 할 경우
			if(a_oSender.ExIsValidIdx(nIdx)) {
				a_oSender[nIdx] = a_tReplaceVal;
			} else {
				a_oSender.Add(a_tReplaceVal);
			}
		}
	}

	/** 값을 대체한다 */
	public static void ExReplaceVal<K, V>(this Dictionary<K, V> a_oSender, K a_tKey, V a_tVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 딕셔너리가 존재 할 경우
		if(a_oSender != null) {
			// 값 대체가 가능 할 경우
			if(a_oSender.ContainsKey(a_tKey)) {
				a_oSender[a_tKey] = a_tVal;
			} else {
				a_oSender.Add(a_tKey, a_tVal);
			}
		}
	}

	/** 값을 대체한다 */
	public static void ExReplaceVal<K, V>(this Dictionary<K, V> a_oSender, V a_tVal, System.Predicate<KeyValuePair<K, V>> a_oCompare, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCompare != null));

		// 딕셔너리가 존재 할 경우
		if(a_oSender != null && a_oCompare != null) {
			var stResult = a_oSender.ExFindVal(a_oCompare);

			// 값 대체가 가능 할 경우
			if(stResult.Item1) {
				a_oSender[stResult.Item2] = a_tVal;
			} else {
				a_oSender.Add(stResult.Item2, a_tVal);
			}
		}
	}

	/** 값을 대체한다 */
	public static void ExReplaceVals<T>(this List<T> a_oSender, List<(T, T)> a_oValInfoList, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oValInfoList != null));

		// 값 대체가 가능 할 경우
		if(a_oSender != null && a_oValInfoList != null) {
			for(int i = 0; i < a_oValInfoList.Count; ++i) {
				a_oSender.ExReplaceVal(a_oValInfoList[i].Item1, a_oValInfoList[i].Item2, a_bIsEnableAssert);
			}
		}
	}

	/** 값을 대체한다 */
	public static void ExReplaceVals<K, V>(this Dictionary<K, V> a_oSender, Dictionary<K, V> a_oValDict, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oValDict != null));

		// 값 대체가 가능 할 경우
		if(a_oSender != null && a_oValDict != null) {
			foreach(var stKeyVal in a_oValDict) {
				a_oSender.ExReplaceVal(stKeyVal.Key, stKeyVal.Value, a_bIsEnableAssert);
			}
		}
	}

	/** 값을 탐색한다 */
	public static int ExFindVal<T>(this T[] a_oSender, System.Predicate<T> a_oCompare) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);

		for(int i = 0; i < a_oSender.Length; ++i) {
			// 값이 존재 할 경우
			if(a_oCompare(a_oSender[i])) {
				return i;
			}
		}

		return KCDefine.B_IDX_INVALID;
	}

	/** 값을 탐색한다 */
	public static Vector3Int ExFindVal<T>(this T[,] a_oSender, System.Predicate<T> a_oCompare) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);

		for(int i = 0; i < a_oSender.GetLength(KCDefine.B_VAL_0_INT); ++i) {
			for(int j = 0; j < a_oSender.GetLength(KCDefine.B_VAL_0_INT); ++j) {
				// 값이 존재 할 경우
				if(a_oCompare(a_oSender[i, j])) {
					return new Vector3Int(j, i, KCDefine.B_VAL_0_INT);
				}
			}
		}

		return new Vector3Int(KCDefine.B_IDX_INVALID, KCDefine.B_IDX_INVALID, KCDefine.B_IDX_INVALID);
	}

	/** 값을 탐색한다 */
	public static (bool, K) ExFindVal<K, V>(this Dictionary<K, V> a_oSender, System.Predicate<KeyValuePair<K, V>> a_oCompare) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);

		foreach(var stKeyVal in a_oSender) {
			// 값이 존재 할 경우
			if(a_oCompare(stKeyVal)) {
				return (true, stKeyVal.Key);
			}
		}

		return (false, default(K));
	}
	#endregion // 제네릭 클래스 함수
}

/** 기본 접근자 확장 클래스 - 추가 */
public static partial class CAccessExtension {
	#region 클래스 함수 (CExtension)
	/** 3 차원 => 2 차원으로 변환한다 */
	public static Vector2 ExTo2D(this Vector3 a_stSender) {
		return new Vector2(a_stSender.x, a_stSender.y);
	}

	/** 3 차원 => 2 차원으로 변환한다 */
	public static Vector2Int ExTo2D(this Vector3Int a_stSender) {
		return new Vector2Int(a_stSender.x, a_stSender.y);
	}

	/** 2 차원 => 3 차원으로 변환한다 */
	public static Vector3 ExTo3D(this Vector2 a_stSender, float a_fZ = KCDefine.B_VAL_0_REAL) {
		return new Vector3(a_stSender.x, a_stSender.y, a_fZ);
	}

	/** 2 차원 => 3 차원으로 변환한다 */
	public static Vector3Int ExTo3D(this Vector2Int a_stSender, int a_nZ = KCDefine.B_VAL_0_INT) {
		return new Vector3Int(a_stSender.x, a_stSender.y, a_nZ);
	}

	/** 4 차원 => 3 차원으로 변환한다 */
	public static Vector3 ExTo3D(this Vector4 a_stSender) {
		return new Vector3(a_stSender.x, a_stSender.y, a_stSender.z);
	}

	/** 3 차원 => 4 차원으로 변환한다 */
	public static Vector4 ExTo4D(this Vector3 a_stSender, bool a_bIsCoord = true) {
		return new Vector4(a_stSender.x, a_stSender.y, a_stSender.z, a_bIsCoord ? KCDefine.B_VAL_1_REAL : KCDefine.B_VAL_0_REAL);
	}
	#endregion // 클래스 함수 (CExtension)

	#region 제네릭 클래스 함수 (CExtension)
	/** 1 차원 배열 => 2 차원 배열로 변환한다 */
	public static T[,] ExToMultiArray<T>(this T[] a_oSender, int a_nNumRows, int a_nNumCols) {
		CAccess.Assert(a_oSender != null && a_nNumRows > KCDefine.B_VAL_0_INT && a_nNumCols > KCDefine.B_VAL_0_INT);
		var oVals = new T[a_nNumRows, a_nNumCols];

		a_oSender.ExCopyTo(oVals, (a_tVal) => a_tVal);
		return oVals;
	}

	/** 2 차원 배열 => 1 차원 배열로 변환한다 */
	public static T[] ExToSingleArray<T>(this T[,] a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());
		var oVals = new T[a_oSender.Length];

		a_oSender.ExCopyTo(oVals, (a_tVal) => a_tVal);
		return oVals;
	}

	/** 1 차원 배열 => 2 차원 배열로 복사한다 */
	public static void ExCopyTo<T01, T02>(this T01[] a_oSender, T02[,] a_oDestVals, System.Func<T01, T02> a_oCallback, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oDestVals != null));

		// 복사가 가능 할 경우
		if(a_oSender != null && a_oDestVals != null) {
			for(int i = 0; i < a_oSender.Length; ++i) {
				a_oDestVals.ExSetVal(new Vector3Int(i % a_oDestVals.GetLength(KCDefine.B_VAL_1_INT), i / a_oDestVals.GetLength(KCDefine.B_VAL_1_INT), KCDefine.B_VAL_0_INT), a_oCallback(a_oSender[i]), a_bIsEnableAssert);
			}
		}
	}

	/** 2 차원 배열 => 1 차원 배열로 복사한다 */
	public static void ExCopyTo<T01, T02>(this T01[,] a_oSender, T02[] a_oDestVals, System.Func<T01, T02> a_oCallback, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oDestVals != null));

		// 복사가 가능 할 경우
		if(a_oSender != null && a_oDestVals != null) {
			for(int i = 0; i < a_oSender.GetLength(KCDefine.B_VAL_0_INT); ++i) {
				for(int j = 0; j < a_oSender.GetLength(KCDefine.B_VAL_1_INT); ++j) {
					a_oDestVals.ExSetVal((i * a_oSender.GetLength(KCDefine.B_VAL_1_INT)) + j, a_oCallback(a_oSender[i, j]), a_bIsEnableAssert);
				}
			}
		}
	}

	/** 셋을 복사한다 */
	public static void ExCopyTo<T01, T02>(this HashSet<T01> a_oSender, HashSet<T02> a_oDestValSet, System.Func<T01, T02> a_oCallback, bool a_bIsClear = true, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oDestValSet != null && a_oCallback != null));

		// 복사가 가능 할 경우
		if(a_oSender != null && (a_oDestValSet != null && a_oCallback != null)) {
			// 클리어 모드 일 경우
			if(a_bIsClear) {
				a_oDestValSet.Clear();
			}

			foreach(var tVal in a_oSender) {
				a_oDestValSet.Add(a_oCallback(tVal));
			}
		}
	}

	/** 리스트를 복사한다 */
	public static void ExCopyTo<T01, T02>(this List<T01> a_oSender, List<T02> a_oDestValList, System.Func<T01, T02> a_oCallback, bool a_bIsClear = true, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oDestValList != null && a_oCallback != null));

		// 복사가 가능 할 경우
		if(a_oSender != null && (a_oDestValList != null && a_oCallback != null)) {
			// 클리어 모드 일 경우
			if(a_bIsClear) {
				a_oDestValList.Clear();
			}

			for(int i = 0; i < a_oSender.Count; ++i) {
				a_oDestValList.Add(a_oCallback(a_oSender[i]));
			}
		}
	}

	/** 스택을 복사한다 */
	public static void ExCopyTo<T01, T02>(this Stack<T01> a_oSender, Stack<T02> a_oDestValStack, System.Func<T01, T02> a_oCallback, bool a_bIsClear = true, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oDestValStack != null && a_oCallback != null));

		// 복사가 가능 할 경우
		if(a_oSender != null && (a_oDestValStack != null && a_oCallback != null)) {
			// 클리어 모드 일 경우
			if(a_bIsClear) {
				a_oDestValStack.Clear();
			}

			var oValStack = new Stack<T01>();

			while(a_oSender.ExIsValid()) {
				oValStack.Push(a_oSender.Pop());
			}

			while(oValStack.ExIsValid()) {
				a_oDestValStack.Push(a_oCallback(oValStack.Pop()));
			}
		}
	}

	/** 큐를 복사한다 */
	public static void ExCopyTo<T01, T02>(this Queue<T01> a_oSender, Queue<T02> a_oDestValQueue, System.Func<T01, T02> a_oCallback, bool a_bIsClear = true, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oDestValQueue != null && a_oCallback != null));

		// 복사가 가능 할 경우
		if(a_oSender != null && (a_oDestValQueue != null && a_oCallback != null)) {
			// 클리어 모드 일 경우
			if(a_bIsClear) {
				a_oDestValQueue.Clear();
			}

			while(a_oSender.ExIsValid()) {
				a_oDestValQueue.Enqueue(a_oCallback(a_oSender.Dequeue()));
			}
		}
	}

	/** 딕셔너리를 복사한다 */
	public static void ExCopyTo<K, V01, V02>(this Dictionary<K, V01> a_oSender, Dictionary<K, V02> a_oDestValDict, System.Func<V01, V02> a_oCallback, bool a_bIsClear = true, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oDestValDict != null && a_oCallback != null));

		// 복사가 가능 할 경우
		if(a_oSender != null && (a_oDestValDict != null && a_oCallback != null)) {
			// 클리어 모드 일 경우
			if(a_bIsClear) {
				a_oDestValDict.Clear();
			}

			foreach(var stKeyVal in a_oSender) {
				a_oDestValDict.TryAdd(stKeyVal.Key, a_oCallback(stKeyVal.Value));
			}
		}
	}
	#endregion // 제네릭 클래스 함수 (CExtension)
}

/** 기본 접근자 확장 클래스 - 기타 */
public static partial class CAccessExtension {
	#region 클래스 함수
	/** 값을 교환한다 */
	private static void LessCorrectSwap(ref float a_fLhs, ref float a_fRhs) {
		// 보정이 필요 할 경우
		if(a_fLhs.ExIsGreate(a_fRhs)) {
			CAccessExtension.Swap(ref a_fLhs, ref a_fRhs);
		}
	}

	/** 값을 교환한다 */
	private static void LessCorrectSwap(ref double a_dblLhs, ref double a_dblRhs) {
		// 보정이 필요 할 경우
		if(a_dblLhs.ExIsGreate(a_dblRhs)) {
			CAccessExtension.Swap(ref a_dblLhs, ref a_dblRhs);
		}
	}
	#endregion // 클래스 함수

	#region 제네릭 클래스 함수
	/** 값을 교환한다 */
	private static void Swap<T>(ref T a_tLhs, ref T a_tRhs) {
		T tTemp = a_tLhs;
		a_tLhs = a_tRhs;
		a_tRhs = tTemp;
	}

	/** 값을 교환한다 */
	private static void LessCorrectSwap<T>(ref T a_tLhs, ref T a_tRhs) where T : System.IComparable<T> {
		// 보정이 필요 할 경우
		if(a_tLhs.CompareTo(a_tRhs) > KCDefine.B_COMPARE_EQUALS) {
			CAccessExtension.Swap(ref a_tLhs, ref a_tRhs);
		}
	}
	#endregion // 제네릭 클래스 함수
}
