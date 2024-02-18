using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/** 기본 팩토리 */
public static partial class CFactory {
	#region 클래스 함수	
	/** 정수 값을 생성한다 */
	public static List<int> MakeIntVals(int a_nMin, int a_nNumVals) {
		CAccess.Assert(a_nNumVals > KCDefine.B_VAL_0_INT);
		return CFactory.MakeVals<int>(a_nNumVals, (a_nIdx) => a_nMin + a_nIdx);
	}

	/** 정수 재배치 값을 생성한다 */
	public static List<int> MakeIntShuffleVals(int a_nMin, int a_nNumVals) {
		CAccess.Assert(a_nNumVals > KCDefine.B_VAL_0_INT);
		return CFactory.MakeShuffleVals<int>(a_nNumVals, (a_nIdx) => a_nMin + a_nIdx);
	}
	#endregion // 클래스 함수

	#region 제네릭 클래스 함수
	/** 값을 생성한다 */
	public static List<T> MakeVals<T>(int a_nNumVals, System.Func<int, T> a_oCallback) {
		CAccess.Assert(a_oCallback != null && a_nNumVals > KCDefine.B_VAL_0_INT);
		var oValList = new List<T>();

		for(int i = 0; i < a_nNumVals; ++i) {
			oValList.Add(a_oCallback.Invoke(i));
		}

		return oValList;
	}

	/** 재배치 값을 생성한다 */
	public static List<T> MakeShuffleVals<T>(int a_nNumVals, System.Func<int, T> a_oCallback) {
		CAccess.Assert(a_oCallback != null && a_nNumVals > KCDefine.B_VAL_0_INT);

		var oValList = CFactory.MakeVals<T>(a_nNumVals, a_oCallback);
		oValList.ExShuffle();

		return oValList;
	}

	/** 키 정보를 생성한다 */
	public static List<(TA, TB, TC, TD, TE, TF, TG, TH)> MakeKeyInfos<TA, TB, TC, TD, TE, TF, TG, TH, TI>(List<(TA, TB, TC, TD, TE, TF, TG, TH, TI)> a_oKeyInfoList) {
		CAccess.Assert(a_oKeyInfoList != null);
		var oKeyInfoList = new List<(TA, TB, TC, TD, TE, TF, TG, TH)>();

		for(int i = 0; i < a_oKeyInfoList.Count; ++i) {
			var stKeyInfo = (a_oKeyInfoList[i].Item1,
				a_oKeyInfoList[i].Item2, a_oKeyInfoList[i].Item3, a_oKeyInfoList[i].Item4, a_oKeyInfoList[i].Item5, a_oKeyInfoList[i].Item6, a_oKeyInfoList[i].Item7, a_oKeyInfoList[i].Item8);

			oKeyInfoList.Add(stKeyInfo);
		}

		return oKeyInfoList;
	}

	/** 키 정보를 생성한다 */
	public static List<(TA, TB, TC, TD, TE, TF, TG)> MakeKeyInfos<TA, TB, TC, TD, TE, TF, TG, TH>(List<(TA, TB, TC, TD, TE, TF, TG, TH)> a_oKeyInfoList) {
		CAccess.Assert(a_oKeyInfoList != null);
		var oKeyInfoList = new List<(TA, TB, TC, TD, TE, TF, TG)>();

		for(int i = 0; i < a_oKeyInfoList.Count; ++i) {
			var stKeyInfo = (a_oKeyInfoList[i].Item1,
				a_oKeyInfoList[i].Item2, a_oKeyInfoList[i].Item3, a_oKeyInfoList[i].Item4, a_oKeyInfoList[i].Item5, a_oKeyInfoList[i].Item6, a_oKeyInfoList[i].Item7);

			oKeyInfoList.Add(stKeyInfo);
		}

		return oKeyInfoList;
	}

	/** 키 정보를 생성한다 */
	public static List<(TA, TB, TC, TD, TE, TF)> MakeKeyInfos<TA, TB, TC, TD, TE, TF, TG>(List<(TA, TB, TC, TD, TE, TF, TG)> a_oKeyInfoList) {
		CAccess.Assert(a_oKeyInfoList != null);
		var oKeyInfoList = new List<(TA, TB, TC, TD, TE, TF)>();

		for(int i = 0; i < a_oKeyInfoList.Count; ++i) {
			var stKeyInfo = (a_oKeyInfoList[i].Item1,
				a_oKeyInfoList[i].Item2, a_oKeyInfoList[i].Item3, a_oKeyInfoList[i].Item4, a_oKeyInfoList[i].Item5, a_oKeyInfoList[i].Item6);

			oKeyInfoList.Add(stKeyInfo);
		}

		return oKeyInfoList;
	}

	/** 키 정보를 생성한다 */
	public static List<(TA, TB, TC, TD, TE)> MakeKeyInfos<TA, TB, TC, TD, TE, TF>(List<(TA, TB, TC, TD, TE, TF)> a_oKeyInfoList) {
		CAccess.Assert(a_oKeyInfoList != null);
		var oKeyInfoList = new List<(TA, TB, TC, TD, TE)>();

		for(int i = 0; i < a_oKeyInfoList.Count; ++i) {
			var stKeyInfo = (a_oKeyInfoList[i].Item1,
				a_oKeyInfoList[i].Item2, a_oKeyInfoList[i].Item3, a_oKeyInfoList[i].Item4, a_oKeyInfoList[i].Item5);

			oKeyInfoList.Add(stKeyInfo);
		}

		return oKeyInfoList;
	}

	/** 키 정보를 생성한다 */
	public static List<(TA, TB, TC, TD)> MakeKeyInfos<TA, TB, TC, TD, TE>(List<(TA, TB, TC, TD, TE)> a_oKeyInfoList) {
		CAccess.Assert(a_oKeyInfoList != null);
		var oKeyInfoList = new List<(TA, TB, TC, TD)>();

		for(int i = 0; i < a_oKeyInfoList.Count; ++i) {
			var stKeyInfo = (a_oKeyInfoList[i].Item1, 
				a_oKeyInfoList[i].Item2, a_oKeyInfoList[i].Item3, a_oKeyInfoList[i].Item4);

			oKeyInfoList.Add(stKeyInfo);
		}

		return oKeyInfoList;
	}

	/** 키 정보를 생성한다 */
	public static List<(TA, TB, TC)> MakeKeyInfos<TA, TB, TC, TD>(List<(TA, TB, TC, TD)> a_oKeyInfoList) {
		CAccess.Assert(a_oKeyInfoList != null);
		var oKeyInfoList = new List<(TA, TB, TC)>();

		for(int i = 0; i < a_oKeyInfoList.Count; ++i) {
			var stKeyInfo = (a_oKeyInfoList[i].Item1, a_oKeyInfoList[i].Item2, a_oKeyInfoList[i].Item3);
			oKeyInfoList.Add(stKeyInfo);
		}

		return oKeyInfoList;
	}

	/** 키 정보를 생성한다 */
	public static List<(TA, TB)> MakeKeyInfos<TA, TB, TC>(List<(TA, TB, TC)> a_oKeyInfoList) {
		CAccess.Assert(a_oKeyInfoList != null);
		var oKeyInfoList = new List<(TA, TB)>();

		for(int i = 0; i < a_oKeyInfoList.Count; ++i) {
			var stKeyInfo = (a_oKeyInfoList[i].Item1, a_oKeyInfoList[i].Item2);
			oKeyInfoList.Add(stKeyInfo);
		}

		return oKeyInfoList;
	}

	/** 키 정보를 생성한다 */
	public static List<TA> MakeKeyInfos<TA, TB>(List<(TA, TB)> a_oKeyInfoList) {
		CAccess.Assert(a_oKeyInfoList != null);
		var oKeyInfoList = new List<TA>();

		for(int i = 0; i < a_oKeyInfoList.Count; ++i) {
			oKeyInfoList.Add(a_oKeyInfoList[i].Item1);
		}

		return oKeyInfoList;
	}
#endregion // 제네릭 클래스 함수
}
