using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/** 확장 클래스 - 딕셔너리 */
public static partial class CExtension {
	#region 제네릭 클래스 함수
	/** 딕셔너리를 복사한다 */
	public static void ExCopyTo<KSrc, VSrc, KDest, VDest>(this Dictionary<KSrc, VSrc> a_oSender,
		Dictionary<KDest, VDest> a_oDestValDict, System.Func<KSrc, VSrc, (KDest, VDest)> a_oCallback, bool a_bIsClear = true, bool a_bIsAssert = true) {

		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oDestValDict != null && a_oCallback != null));

		// 딕셔너리 복사가 불가능 할 경우
		if(a_oSender == null || a_oDestValDict == null || a_oCallback == null) {
			return;
		}

		// 클리어 모드 일 경우
		if(a_bIsClear) {
			a_oDestValDict.Clear();
		}

		foreach(var stKeyVal in a_oSender) {
			var stDestKeyVal = a_oCallback(stKeyVal.Key, stKeyVal.Value);
			a_oDestValDict.TryAdd(stDestKeyVal.Key, stDestKeyVal.Value);
		}
	}
	#endregion // 제네릭 클래스 함수
}
