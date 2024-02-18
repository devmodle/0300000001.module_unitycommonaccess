using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/** 확장 클래스 - 리스트 */
public static partial class CExtension {
	#region 제네릭 클래스 함수
	/** 값을 추가한다 */
	public static void ExAddVal<T>(this List<T> a_oSender, T a_tVal, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 값 추가가 불가능 할 경우
		if(a_oSender == null || a_oSender.Contains(a_tVal)) {
			return;
		}

		a_oSender.Add(a_tVal);
	}

	/** 값을 추가한다 */
	public static void ExAddVal<T>(this List<T> a_oSender,
		T a_tVal, System.Predicate<T> a_oCompare, bool a_bIsAssert = true) {

		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oCompare != null));

		// 값 추가가 불가능 할 경우
		if(a_oSender == null || a_oCompare == null || a_oSender.ExFindVal(a_oCompare).ExIsValidIdx()) {
			return;
		}

		a_oSender.Add(a_tVal);
	}

	/** 값을 추가한다 */
	public static void ExAddVals<T>(this List<T> a_oSender, List<T> a_oValList, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oValList != null));

		// 값 추가가 불가능 할 경우
		if(a_oSender == null || a_oValList == null) {
			return;
		}

		for(int i = 0; i < a_oValList.Count; ++i) {
			a_oSender.ExAddVal(a_oValList[i], a_bIsAssert);
		}
	}

	/** 값을 추가한다 */
	public static void ExAddVals<T>(this List<T> a_oSender,
		List<T> a_oValList, System.Predicate<T> a_oCompare, bool a_bIsAssert = true) {

		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oValList != null && a_oCompare != null));

		// 값 추가가 불가능 할 경우
		if(a_oSender == null || a_oValList == null || a_oCompare == null) {
			return;
		}

		for(int i = 0; i < a_oValList.Count; ++i) {
			a_oSender.ExAddVal(a_oValList[i], a_oCompare, a_bIsAssert);
		}
	}

	/** 값을 추가한다 */
	public static void ExInsertVal<T>(this List<T> a_oSender,
		int a_nIdx, T a_tVal, bool a_bIsAssert = true) {

		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 값 추가가 불가능 할 경우
		if(a_oSender == null || a_nIdx <= KCDefine.B_IDX_INVALID || a_nIdx > a_oSender.Count) {
			return;
		}

		a_oSender.Insert(a_nIdx, a_tVal);
	}

	/** 값을 추가한다 */
	public static void ExInsertVal<T>(this List<T> a_oSender,
		System.Predicate<T> a_oCompare, T a_tVal, bool a_bIsAssert = true) {

		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oCompare != null));

		// 값 추가가 불가능 할 경우
		if(a_oSender == null || a_oCompare == null) {
			return;
		}

		a_oSender.ExInsertVal(a_oSender.ExFindVal(a_oCompare), a_tVal);
	}

	/** 값을 추가한다 */
	public static void ExInsertVals<T>(this List<T> a_oSender,
		System.Predicate<T> a_oCompare, List<T> a_oValList, bool a_bIsAssert = true) {

		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oCompare != null && a_oValList != null));

		// 값 추가가 불가능 할 경우
		if(a_oSender == null || a_oCompare == null || a_oValList == null) {
			return;
		}

		for(int i = 0; i < a_oValList.Count; ++i) {
			a_oSender.ExInsertVal(a_oCompare, a_oValList[i]);
		}
	}

	/** 값을 대체한다 */
	public static void ExReplaceVal<T>(this List<T> a_oSender, 
		T a_tVal, T a_tReplaceVal, bool a_bIsAssert = true) {

		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 값 대체가 불가능 할 경우
		if(a_oSender == null) {
			return;
		}

		a_oSender.ExReplaceVal(a_tReplaceVal, (a_tCompareVal) => a_tCompareVal.Equals(a_tVal), a_bIsAssert);
	}

	/** 값을 대체한다 */
	public static void ExReplaceVal<T>(this List<T> a_oSender,
		T a_tReplaceVal, System.Predicate<T> a_oCompare, bool a_bIsAssert = true) {

		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oCompare != null));

		// 값 대체가 불가능 할 경우
		if(a_oSender == null || a_oCompare == null) {
			return;
		}

		int nIdx = a_oSender.ExFindVal(a_oCompare);

		// 값이 존재 할 경우
		if(a_oSender.ExIsValidIdx(nIdx)) {
			a_oSender[nIdx] = a_tReplaceVal;
		} else {
			a_oSender.Add(a_tReplaceVal);
		}
	}

	/** 값을 대체한다 */
	public static void ExReplaceVals<T>(this List<T> a_oSender, 
		List<(T, T)> a_oValInfoList, bool a_bIsAssert = true) {

		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oValInfoList != null));

		// 값 대체가 불가능 할 경우
		if(a_oSender == null || a_oValInfoList == null) {
			return;
		}

		for(int i = 0; i < a_oValInfoList.Count; ++i) {
			a_oSender.ExReplaceVal(a_oValInfoList[i].Item1, a_oValInfoList[i].Item2, a_bIsAssert);
		}
	}

	/** 값을 제거한다 */
	public static void ExRemoveVal<T>(this List<T> a_oSender, T a_tVal, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 값 제거가 불가능 할 경우
		if(a_oSender == null) {
			return;
		}

		a_oSender.ExRemoveVal((a_tCompareVal) => a_tCompareVal.Equals(a_tVal), a_bIsAssert);
	}

	/** 값을 제거한다 */
	public static void ExRemoveVal<T>(this List<T> a_oSender,
		System.Predicate<T> a_oCompare, bool a_bIsAssert = true) {

		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oCompare != null));

		// 값 제거가 불가능 할 경우
		if(a_oSender == null || a_oCompare == null) {
			return;
		}

		a_oSender.ExRemoveValAt(a_oSender.ExFindVal(a_oCompare), a_bIsAssert);
	}

	/** 값을 제거한다 */
	public static void ExRemoveVals<T>(this List<T> a_oSender, List<T> a_oValList, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oValList != null));

		// 값 제거가 불가능 할 경우
		if(a_oSender == null || a_oValList == null) {
			return;
		}

		for(int i = 0; i < a_oValList.Count; ++i) {
			a_oSender.ExRemoveVal(a_oValList[i], a_bIsAssert);
		}
	}

	/** 값을 제거한다 */
	public static void ExRemoveVals<T>(this List<T> a_oSender,
		System.Predicate<T> a_oCompare, bool a_bIsAssert = true) {

		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oCompare != null));

		// 값 제거가 불가능 할 경우
		if(a_oSender == null || a_oCompare == null) {
			return;
		}

		int nIdx = a_oSender.ExFindVal(a_oCompare);

		do {
			a_oSender.ExRemoveValAt(nIdx, a_bIsAssert);
		} while((nIdx = a_oSender.ExFindVal(a_oCompare)).ExIsValidIdx());
	}

	/** 값을 제거한다 */
	public static void ExRemoveValAt<T>(this List<T> a_oSender, int a_nIdx, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 값 제거가 불가능 할 경우
		if(a_oSender == null || !a_oSender.ExIsValidIdx(a_nIdx)) {
			return;
		}

		a_oSender.RemoveAt(a_nIdx);
	}

	/** 값을 탐색한다 */
	public static int ExFindVal<T>(this List<T> a_oSender, System.Predicate<T> a_oCompare) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);
		return a_oSender.FindIndex(a_oCompare);
	}

	/** 리스트를 복사한다 */
	public static void ExCopyTo<TA, TB>(this List<TA> a_oSender,
		List<TB> a_oDestValList, System.Func<TA, TB> a_oCallback, bool a_bIsClear = true, bool a_bIsAssert = true) {

		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oDestValList != null && a_oCallback != null));

		// 리스트 복사가 불가능 할 경우
		if(a_oSender == null || a_oDestValList == null || a_oCallback == null) {
			return;
		}

		// 클리어 모드 일 경우
		if(a_bIsClear) {
			a_oDestValList.Clear();
		}

		for(int i = 0; i < a_oSender.Count; ++i) {
			a_oDestValList.Add(a_oCallback(a_oSender[i]));
		}
	}
	#endregion // 제네릭 클래스 함수
}
