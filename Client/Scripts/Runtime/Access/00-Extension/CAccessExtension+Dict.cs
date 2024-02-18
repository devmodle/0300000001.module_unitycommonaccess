using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System.Linq;
using DG.Tweening;

/** 접근자 확장 클래스 - 딕셔너리 */
public static partial class CAccessExtension {
	#region 내부 클래스 함수
	/** 값을 반환한다 */
	private static V ExGetVal<V>(this Dictionary<int, V> a_oSender, Vector3Int a_stIdx, V a_tDefVal = default) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExGetVal(a_stIdx.x, a_tDefVal);
	}
	#endregion // 내부 클래스 함수

	#region 클래스 접근 함수
	/** 유효 여부를 검사한다 */
	public static bool ExIsValid<K, V>(this Dictionary<K, V> a_oSender) {
		return a_oSender != null && a_oSender.Count > KCDefine.B_VAL_0_INT;
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
	public static bool ExIsContainsAny<K, V>(this Dictionary<K, V> a_oSender, List<K> a_oKeyList) {
		CAccess.Assert(a_oSender != null && a_oKeyList != null);
		return a_oKeyList.Any((a_tKey) => a_oSender.ContainsKey(a_tKey));
	}

	/** 포함 여부를 검사한다 */
	public static bool ExIsContainsAll<K, V>(this Dictionary<K, V> a_oSender, List<K> a_oKeyList) {
		CAccess.Assert(a_oSender != null && a_oKeyList != null);
		return a_oKeyList.All((a_tKey) => a_oSender.ContainsKey(a_tKey));
	}

	/** 값을 반환한다 */
	public static V ExGetVal<K, V>(this Dictionary<K, V> a_oSender, K a_tKey, V a_tDefVal = default) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.GetValueOrDefault(a_tKey, a_tDefVal);
	}

	/** 값을 반환한다 */
	public static V ExGetVal<K, V>(this Dictionary<K, V> a_oSender, System.Predicate<KeyValuePair<K, V>> a_oCompare, V a_tDefVal = default) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);
		var stResult = a_oSender.ExFindVal(a_oCompare);

		return stResult.Item1 ? a_oSender[stResult.Item2] : a_tDefVal;
	}

	/** 값을 반환한다 */
	public static V ExGetVal<V>(this Dictionary<int, V> a_oSender, int a_nIdx, V a_tDefVal = default) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.GetValueOrDefault(a_nIdx, a_tDefVal);
	}

	/** 값을 반환한다 */
	public static V ExGetVal<V>(this Dictionary<int, Dictionary<int, V>> a_oSender, Vector3Int a_stIdx, V a_tDefVal = default) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_stIdx) ? a_oSender[a_stIdx.y].ExGetVal(a_stIdx, a_tDefVal) : a_tDefVal;
	}

	/** 값을 반환한다 */
	public static V ExGetVal<V>(this Dictionary<int, Dictionary<int, Dictionary<int, V>>> a_oSender, Vector3Int a_stIdx, V a_tDefVal = default) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_stIdx) ? a_oSender[a_stIdx.z].ExGetVal(a_stIdx, a_tDefVal) : a_tDefVal;
	}
	#endregion // 클래스 접근 함수

	#region 제네릭 클래스 접근 함수
	/** 값을 할당한다 */
	public static void ExAssignVal<K>(this Dictionary<K, Tween> a_oSender, K a_tKey, Tween a_oRhs, Tween a_oDefVal = null) {
		a_oSender.ExGetVal(a_tKey)?.Kill();
		a_oSender.ExReplaceVal(a_tKey, a_oRhs ?? a_oDefVal, false);
	}

	/** 값을 할당한다 */
	public static void ExAssignVal<K>(this Dictionary<K, Sequence> a_oSender, K a_tKey, Tween a_oRhs, Tween a_oDefVal = null) {
		a_oSender.ExGetVal(a_tKey)?.Kill();
		a_oSender.ExReplaceVal(a_tKey, (a_oRhs ?? a_oDefVal) as Sequence, false);
	}
	#endregion // 제네릭 클래스 접근 함수
}
