using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System.Linq;

/** 접근자 확장 클래스 - 리스트 */
public static partial class CAccessExtension {
	#region 클래스 접근 함수
	/** 유효 여부를 검사한다 */
	public static bool ExIsValid<T>(this List<T> a_oSender) {
		return a_oSender != null && a_oSender.Count > KCDefine.B_VAL_0_INT;
	}

	/** 포함 여부를 검사한다 */
	public static bool ExIsContains<T>(this List<T> a_oSender, List<T> a_oValList) {
		CAccess.Assert(a_oSender != null && a_oValList != null);
		return a_oValList.All((a_tVal) => a_oSender.Contains(a_tVal));
	}

	/** 포함 여부를 검사한다 */
	public static bool ExIsContainsAny<T>(this List<T> a_oSender, List<T> a_oValList) {
		CAccess.Assert(a_oSender != null && a_oValList != null);
		return a_oValList.Any((a_tVal) => a_oSender.Contains(a_tVal));
	}

	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx<T>(this List<T> a_oSender, int a_nIdx) {
		CAccess.Assert(a_oSender != null);
		return a_nIdx > KCDefine.B_IDX_INVALID && a_nIdx < a_oSender.Count;
	}

	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx<T>(this List<List<T>> a_oSender, Vector3Int a_stIdx) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_stIdx.y) && a_oSender[a_stIdx.y].ExIsValidIdx(a_stIdx.x);
	}

	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx<T>(this List<List<List<T>>> a_oSender, Vector3Int a_stIdx) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_stIdx.z) && a_oSender[a_stIdx.z].ExIsValidIdx(a_stIdx);
	}

	/** 값을 반환한다 */
	public static T ExGetVal<T>(this List<T> a_oSender, int a_nIdx, T a_tDefVal = default) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_nIdx) ? a_oSender[a_nIdx] : a_tDefVal;
	}

	/** 값을 반환한다 */
	public static T ExGetVal<T>(this List<T> a_oSender, System.Predicate<T> a_oCompare, T a_tDefVal = default) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);
		return a_oSender.ExGetVal(a_oSender.FindIndex(a_oCompare), a_tDefVal);
	}

	/** 값을 반환한다 */
	public static T ExGetVal<T>(this List<List<T>> a_oSender, Vector3Int a_stIdx, T a_tDefVal = default) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_stIdx) ? a_oSender[a_stIdx.y].ExGetVal(a_stIdx, a_tDefVal) : a_tDefVal;
	}

	/** 값을 반환한다 */
	public static T ExGetVal<T>(this List<List<T>> a_oSender, System.Predicate<T> a_oCompare, T a_tDefVal = default) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);

		for(int i = 0; i < a_oSender.Count; ++i) {
			int nResult = a_oSender[i].FindIndex(a_oCompare);

			// 값이 존재 할 경우
			if(a_oSender[i].ExIsValidIdx(nResult)) {
				return a_oSender[i].ExGetVal(nResult, a_tDefVal);
			}
		}

		return a_tDefVal;
	}

	/** 값을 반환한다 */
	public static T ExGetVal<T>(this List<List<List<T>>> a_oSender, Vector3Int a_stIdx, T a_tDefVal = default) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_stIdx) ? a_oSender[a_stIdx.z].ExGetVal(a_stIdx, a_tDefVal) : a_tDefVal;
	}

	/** 값을 반환한다 */
	public static T ExGetVal<T>(this List<List<List<T>>> a_oSender, System.Predicate<T> a_oCompare, T a_tDefVal = default) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);

		for(int i = 0; i < a_oSender.Count; ++i) {
			for(int j = 0; j < a_oSender[i].Count; ++j) {
				int nResult = a_oSender[i][j].FindIndex(a_oCompare);

				// 값이 존재 할 경우
				if(a_oSender[i][j].ExIsValidIdx(nResult)) {
					return a_oSender[i][j].ExGetVal(nResult, a_tDefVal);
				}
			}
		}

		return a_tDefVal;
	}

	/** 값을 반환한다 */
	public static List<T> ExGetVals<T>(this List<T> a_oSender, List<int> a_oIdxList, List<T> a_oOutValList, T a_tDefVal = default) {
		CAccess.Assert(a_oSender != null && a_oIdxList != null);
		a_oOutValList = a_oOutValList ?? new List<T>();

		for(int i = 0; i < a_oIdxList.Count; ++i) {
			var tVal = a_oSender.ExGetVal(a_oIdxList[i], a_tDefVal);
			a_oOutValList.Add(tVal);
		}

		return a_oOutValList;
	}

	/** 값을 반환한다 */
	public static List<T> ExGetVals<T>(this List<T> a_oSender, System.Predicate<T> a_oCompare, List<T> a_oOutValList) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);
		a_oOutValList = a_oOutValList ?? new List<T>();

		for(int i = 0; i < a_oSender.Count; ++i) {
			// 값이 다를 경우
			if(!a_oCompare(a_oSender[i])) {
				continue;
			}

			a_oOutValList.Add(a_oSender[i]);
		}

		return a_oOutValList;
	}

	/** 값을 반환한다 */
	public static List<T> ExGetVals<T>(this List<List<T>> a_oSender, List<Vector3Int> a_oIdxList, List<T> a_oOutValList, T a_tDefVal = default) {
		CAccess.Assert(a_oSender != null && a_oIdxList != null);
		a_oOutValList = a_oOutValList ?? new List<T>();

		for(int i = 0; i < a_oIdxList.Count; ++i) {
			var tVal = a_oSender.ExGetVal(a_oIdxList[i], a_tDefVal);
			a_oOutValList.Add(tVal);
		}

		return a_oOutValList;
	}

	/** 값을 반환한다 */
	public static List<T> ExGetVals<T>(this List<List<T>> a_oSender, System.Predicate<T> a_oCompare, List<T> a_oOutValList) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);
		a_oOutValList = a_oOutValList ?? new List<T>();

		for(int i = 0; i < a_oSender.Count; ++i) {
			a_oSender[i].ExGetVals(a_oCompare, a_oOutValList);
		}

		return a_oOutValList;
	}

	/** 값을 반환한다 */
	public static List<T> ExGetVals<T>(this List<List<List<T>>> a_oSender, List<Vector3Int> a_oIdxList, List<T> a_oOutValList, T a_tDefVal = default) {
		CAccess.Assert(a_oSender != null && a_oIdxList != null);
		a_oOutValList = a_oOutValList ?? new List<T>();

		for(int i = 0; i < a_oIdxList.Count; ++i) {
			var tVal = a_oSender.ExGetVal(a_oIdxList[i], a_tDefVal);
			a_oOutValList.Add(tVal);
		}

		return a_oOutValList;
	}

	/** 값을 반환한다 */
	public static List<T> ExGetVals<T>(this List<List<List<T>>> a_oSender, System.Predicate<T> a_oCompare, List<T> a_oOutValList) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);
		a_oOutValList = a_oOutValList ?? new List<T>();

		for(int i = 0; i < a_oSender.Count; ++i) {
			a_oSender[i].ExGetVals(a_oCompare, a_oOutValList);
		}

		return a_oOutValList;
	}

	/** 값을 변경한다 */
	public static void ExSetVal<T>(this List<T> a_oSender, int a_nIdx, T a_tVal, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 값 변경이 불가능 할 경우
		if(a_oSender == null || !a_oSender.ExIsValidIdx(a_nIdx)) {
			return;
		}

		a_oSender[a_nIdx] = a_tVal;
	}

	/** 값을 변경한다 */
	public static void ExSetVal<T>(this List<T> a_oSender, System.Predicate<T> a_oCompare, T a_tVal, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oCompare != null));

		// 값 변경이 불가능 할 경우
		if(a_oSender == null || a_oCompare == null) {
			return;
		}

		a_oSender.ExSetVal(a_oSender.ExFindVal(a_oCompare), a_tVal, a_bIsAssert);
	}

	/** 값을 변경한다 */
	public static void ExSetVal<T>(this List<List<T>> a_oSender, Vector3Int a_stIdx, T a_tVal, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 값 변경이 불가능 할 경우
		if(a_oSender == null || !a_oSender.ExIsValidIdx(a_stIdx)) {
			return;
		}

		a_oSender[a_stIdx.y].ExSetVal(a_stIdx, a_tVal, a_bIsAssert);
	}

	/** 값을 변경한다 */
	public static void ExSetVal<T>(this List<List<List<T>>> a_oSender, Vector3Int a_stIdx, T a_tVal, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 값 변경이 불가능 할 경우
		if(a_oSender == null || !a_oSender.ExIsValidIdx(a_stIdx)) {
			return;
		}

		a_oSender[a_stIdx.z].ExSetVal(a_stIdx, a_tVal, a_bIsAssert);
	}

	/** 값을 변경한다 */
	public static void ExSetVals<T>(this List<T> a_oSender, List<(int, T)> a_oValInfoList, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oValInfoList != null));

		// 값 변경이 불가능 할 경우
		if(a_oSender == null || a_oValInfoList == null) {
			return;
		}

		for(int i = 0; i < a_oValInfoList.Count; ++i) {
			a_oSender.ExSetVal(a_oValInfoList[i].Item1, a_oValInfoList[i].Item2, a_bIsAssert);
		}
	}

	/** 값을 변경한다 */
	public static void ExSetVals<T>(this List<T> a_oSender, System.Predicate<T> a_oCompare, T a_tVal, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oCompare != null));

		// 값 변경이 불가능 할 경우
		if(a_oSender == null || a_oCompare == null) {
			return;
		}

		for(int i = 0; i < a_oSender.Count; ++i) {
			int nResult = a_oSender.ExFindVal(a_oCompare);

			// 값이 다를 경우
			if(!a_oSender.ExIsValidIdx(nResult)) {
				continue;
			}

			a_oSender.ExSetVal(nResult, a_tVal, a_bIsAssert);
		}
	}

	/** 값을 변경한다 */
	public static void ExSetVals<T>(this List<List<T>> a_oSender, System.Predicate<T> a_oCompare, T a_tVal, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oCompare != null));

		// 값 변경이 불가능 할 경우
		if(a_oSender == null || a_oCompare == null) {
			return;
		}

		for(int i = 0; i < a_oSender.Count; ++i) {
			a_oSender[i].ExSetVals(a_oCompare, a_tVal, a_bIsAssert);
		}
	}

	/** 값을 변경한다 */
	public static void ExSetVals<T>(this List<List<List<T>>> a_oSender, System.Predicate<T> a_oCompare, T a_tVal, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oCompare != null));

		// 값 변경이 불가능 할 경우
		if(a_oSender == null || a_oCompare == null) {
			return;
		}

		for(int i = 0; i < a_oSender.Count; ++i) {
			a_oSender[i].ExSetVals(a_oCompare, a_tVal, a_bIsAssert);
		}
	}
	#endregion // 클래스 접근 함수
}

/** 접근자 확장 클래스 - 리스트 */
public static partial class CAccessExtension {
	#region 클래스 함수
	/** 값을 반환한다 */
	private static T ExGetVal<T>(this List<T> a_oSender, Vector3Int a_stIdx, T a_tDefVal = default) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExGetVal(a_stIdx.x, a_tDefVal);
	}

	/** 값을 반환한다 */
	private static List<T> ExGetVals<T>(this List<T> a_oSender, List<Vector3Int> a_oIdxList, List<T> a_oOutValList, T a_tDefVal = default) {
		CAccess.Assert(a_oSender != null && a_oIdxList != null);
		a_oOutValList = a_oOutValList ?? new List<T>();

		for(int i = 0; i < a_oIdxList.Count; ++i) {
			var tVal = a_oSender.ExGetVal(a_oIdxList[i].x, a_tDefVal);
			a_oOutValList.Add(tVal);
		}

		return a_oOutValList;
	}

	/** 값을 변경한다 */
	private static void ExSetVal<T>(this List<T> a_oSender, Vector3Int a_stIdx, T a_tVal, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 값 변경이 불가능 할 경우
		if(a_oSender == null || !a_oSender.ExIsValidIdx(a_stIdx.x)) {
			return;
		}

		a_oSender.ExSetVal(a_stIdx.y, a_tVal, a_bIsAssert);
	}

	/** 값을 변경한다 */
	private static void ExSetVals<T>(this List<T> a_oSender, List<(Vector3Int, T)> a_oIdxList, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 값 변경이 불가능 할 경우
		if(a_oSender == null) {
			return;
		}

		for(int i = 0; i < a_oIdxList.Count; ++i) {
			a_oSender.ExSetVal(a_oIdxList[i].Item1.x, a_oIdxList[i].Item2, a_bIsAssert);
		}
	}
	#endregion // 클래스 함수
}
