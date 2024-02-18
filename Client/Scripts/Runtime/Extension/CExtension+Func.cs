using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/** 확장 클래스 - 함수 */
public static partial class CExtension {
	#region 클래스 함수
	/** 함수를 지연 호출한다 */
	public static void ExLateCallFunc(this MonoBehaviour a_oSender, 
		System.Action<MonoBehaviour> a_oCallback, bool a_bIsAssert = true) {

		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 함수 호출이 불가능 할 경우
		if(a_oSender == null) {
			return;
		}

		a_oSender.StartCoroutine(a_oSender.ExCoLateCallFunc(a_oCallback));
	}

	/** 함수를 지연 호출한다 */
	public static void ExLateCallFunc(this MonoBehaviour a_oSender, 
		System.Action<MonoBehaviour> a_oCallback, float a_fDelay, bool a_bIsRealtime = false, bool a_bIsAssert = true) {

		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 함수 호출이 불가능 할 경우
		if(a_oSender == null) {
			return;
		}

		a_oSender.StartCoroutine(a_oSender.ExCoLateCallFunc(a_oCallback, a_fDelay, a_bIsRealtime));
	}

	/** 함수를 반복 호출한다 */
	public static void ExRepeatCallFunc(this MonoBehaviour a_oSender, 
		System.Func<MonoBehaviour, bool, bool> a_oCallback, float a_fDeltaTime, float a_fMaxDeltaTime, bool a_bIsRealtime = false, bool a_bIsAssert = true) {
			
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 함수 호출이 불가능 할 경우
		if(a_oSender == null) {
			return;
		}

		a_oSender.StartCoroutine(a_oSender.ExCoRepeatCallFunc(a_oCallback, a_fDeltaTime, a_fMaxDeltaTime, a_bIsRealtime));
	}
	#endregion // 클래스 함수
}
