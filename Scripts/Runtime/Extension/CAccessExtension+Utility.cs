using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif			// #if UNITY_EDITOR

#if UNITY_IOS && NOTI_MODULE_ENABLE
using Unity.Notifications.iOS;
#endif			// #if UNITY_IOS && NOTI_MODULE_ENABLE

//! 유틸리티 접근 확장 클래스
public static partial class CAccessExtension {
	#region 클래스 변수
#if UNITY_IOS && NOTI_MODULE_ENABLE
	private static AuthorizationOption[] m_oAuthOpts = new AuthorizationOption[] {
		AuthorizationOption.Badge,
		AuthorizationOption.Sound,
		AuthorizationOption.Alert,
		AuthorizationOption.CarPlay
	};
#endif			// #if UNITY_IOS && NOTI_MODULE_ENABLE
	#endregion			// 클래스 변수

	#region 클래스 함수
	//! 유효 여부를 검사한다
	public static bool ExIsValid(this EUserType a_eSender) {
		return a_eSender > EUserType.NONE && a_eSender < EUserType.MAX_VALUE;
	}

	//! 유효 여부를 검사한다
	public static bool ExIsValid(this TextAsset a_oSender) {
		return a_oSender != null && (a_oSender.text.ExIsValid() || a_oSender.bytes.ExIsValid());
	}

	//! 동일 여부를 검사한다
	public static bool ExIsEquals(this Vector2 a_stSender, Vector2 a_stRhs) {
		return a_stSender.x.ExIsEquals(a_stRhs.x) && a_stSender.y.ExIsEquals(a_stRhs.y);
	}

	//! 동일 여부를 검사한다
	public static bool ExIsEquals(this Vector3 a_stSender, Vector3 a_stRhs) {
		return a_stSender.x.ExIsEquals(a_stRhs.x) && 
			a_stSender.y.ExIsEquals(a_stRhs.y) && a_stSender.z.ExIsEquals(a_stRhs.z);
	}

	//! 동일 여부를 검사한다
	public static bool ExIsEquals(this Vector4 a_stSender, Vector4 a_stRhs) {
		return a_stSender.x.ExIsEquals(a_stRhs.x) && a_stSender.y.ExIsEquals(a_stRhs.y) &&
			a_stSender.z.ExIsEquals(a_stRhs.z) && a_stSender.w.ExIsEquals(a_stRhs.w);
	}

	//! 색상을 반환한다
	public static Color ExGetAlphaColor(this Color a_stSender, float a_fAlpha) {
		a_stSender.a = a_fAlpha;
		return a_stSender;
	}

	//! X 축 간격을 반환한다
	public static float ExGetDeltaX(this Vector3 a_stSender, Vector3 a_stRhs) {
		return (a_stSender - a_stRhs).x;
	}

	//! Y 축 간격을 반환한다
	public static float ExGetDeltaY(this Vector3 a_stSender, Vector3 a_stRhs) {
		return (a_stSender - a_stRhs).y;
	}

	//! Z 축 간격을 반환한다
	public static float ExGetDeltaZ(this Vector3 a_stSender, Vector3 a_stRhs) {
		return (a_stSender - a_stRhs).z;
	}

	//! 크기 형식 문자열을 반환한다
	public static string ExGetSizeFormatString(this string a_oSender, int a_nSize) {
		return string.Format(KCDefine.B_SIZE_FORMAT_STRING, a_nSize, a_oSender);
	}

	//! 색상 형식 문자열을 반환한다
	public static string ExGetColorFormatString(this string a_oSender, Color a_stColor) {
		return string.Format(KCDefine.B_COLOR_FORMAT_STRING, ColorUtility.ToHtmlStringRGBA(a_stColor), a_oSender);
	}

	//! 위치를 변경한다
	public static void ExSetPos(this Transform a_oSender, Vector3 a_stPos, bool a_bIsWorld = false) {
		// 월드 모드 일 경우
		if(a_bIsWorld) {
			a_oSender.position = a_stPos;
		} else {
			a_oSender.localPosition = a_stPos;
		}
	}

	//! 회전을 변경한다
	public static void ExSetRotation(this Transform a_oSender, Vector3 a_stRotation, bool a_bIsWorld = false) {
		// 월드 모드 일 경우
		if(a_bIsWorld) {
			a_oSender.eulerAngles = a_stRotation;
		} else {
			a_oSender.localEulerAngles = a_stRotation;
		}
	}

	//! X 축 위치를 변경한다
	public static void ExSetPosX(this Transform a_oSender, float a_fValue, bool a_bIsWorld = false) {
		var stPos = a_bIsWorld ? a_oSender.position : a_oSender.localPosition;
		a_oSender.ExSetPos(new Vector3(a_fValue, stPos.y, stPos.z), a_bIsWorld);
	}
	
	//! Y 축 위치를 변경한다
	public static void ExSetPosY(this Transform a_oSender, float a_fValue, bool a_bIsWorld = false) {
		var stPos = a_bIsWorld ? a_oSender.position : a_oSender.localPosition;
		a_oSender.ExSetPos(new Vector3(stPos.x, a_fValue, stPos.z), a_bIsWorld);
	}

	//! Z 축 위치를 변경한다
	public static void ExSetPosZ(this Transform a_oSender, float a_fValue, bool a_bIsWorld = false) {
		var stPos = a_bIsWorld ? a_oSender.position : a_oSender.localPosition;
		a_oSender.ExSetPos(new Vector3(stPos.x, stPos.y, a_fValue), a_bIsWorld);
	}

	//! X 축 비율을 변경한다
	public static void ExSetScaleX(this Transform a_oSender, float a_fValue) {
		a_oSender.localScale = new Vector3(a_fValue, a_oSender.localScale.y, a_oSender.localScale.z);
	}

	//! Y 축 비율을 변경한다
	public static void ExSetScaleY(this Transform a_oSender, float a_fValue) {
		a_oSender.localScale = new Vector3(a_oSender.localScale.x, a_fValue, a_oSender.localScale.z);
	}

	//! Z 축 비율을 변경한다
	public static void ExSetScaleZ(this Transform a_oSender, float a_fValue) {
		a_oSender.localScale = new Vector3(a_oSender.localScale.x, a_oSender.localScale.y, a_fValue);
	}

	//! X 축 각도를 변경한다
	public static void ExSetRotationX(this Transform a_oSender, float a_fValue, bool a_bIsWorld = false) {
		var stRotation = a_bIsWorld ? a_oSender.eulerAngles : a_oSender.localEulerAngles;
		a_oSender.ExSetRotation(new Vector3(a_fValue, stRotation.y, stRotation.z), a_bIsWorld);
	}
	
	//! Y 축 각도를 변경한다
	public static void ExSetRotationY(this Transform a_oSender, float a_fValue, bool a_bIsWorld = false) {
		var stRotation = a_bIsWorld ? a_oSender.eulerAngles : a_oSender.localEulerAngles;
		a_oSender.ExSetRotation(new Vector3(stRotation.x, a_fValue, stRotation.z), a_bIsWorld);
	}

	//! Z 축 각도를 변경한다
	public static void ExSetRotationZ(this Transform a_oSender, float a_fValue, bool a_bIsWorld = false) {
		var stRotation = a_bIsWorld ? a_oSender.eulerAngles : a_oSender.localEulerAngles;
		a_oSender.ExSetRotation(new Vector3(stRotation.x, stRotation.y, a_fValue), a_bIsWorld);
	}

	//! 앵커 위치를 변경한다
	public static void ExSetAnchorPos(this RectTransform a_oSender, Vector2 a_stPos) {
		a_oSender.anchoredPosition = a_stPos;
	}

	//! 크기 간격을 변경한다
	public static void ExSetSizeDelta(this RectTransform a_oSender, Vector2 a_stDelta) {
		a_oSender.sizeDelta = a_stDelta;
	}

	//! X 축 앵커 위치를 변경한다
	public static void ExSetAnchorPosX(this RectTransform a_oSender, float a_fValue) {
		a_oSender.ExSetAnchorPos(new Vector2(a_fValue, a_oSender.anchoredPosition.y));
	}

	//! Y 축 앵커 위치를 변경한다
	public static void ExSetAnchorPosY(this RectTransform a_oSender, float a_fValue) {
		a_oSender.ExSetAnchorPos(new Vector2(a_oSender.anchoredPosition.x, a_fValue));
	}

	//! X 축 크기 간격을 변경한다
	public static void ExSetSizeDeltaX(this RectTransform a_oSender, float a_fValue) {
		a_oSender.ExSetSizeDelta(new Vector2(a_fValue, a_oSender.sizeDelta.y));
	}

	//! Y 축 크기 간격을 변경한다
	public static void ExSetSizeDeltaY(this RectTransform a_oSender, float a_fValue) {
		a_oSender.ExSetSizeDelta(new Vector2(a_oSender.sizeDelta.x, a_fValue));
	}
	#endregion			// 클래스 함수
	
	#region 조건부 클래스 함수
#if UNITY_EDITOR
	//! 스크립트 순서를 변경한다
	public static void ExSetScriptOrder(this MonoBehaviour a_oSender, int a_nOrder) {
		var oMonoScript = MonoScript.FromMonoBehaviour(a_oSender);
		CAccess.SetScriptOrder(oMonoScript, a_nOrder);
	}
#endif			// #if UNITY_EDITOR

#if UNITY_IOS && NOTI_MODULE_ENABLE
	//! 인증 옵션 유효 여부를 검사한다
	public static bool ExIsValidAuthOpts(this AuthorizationOption a_eSender) {
		int nSumValue = KCDefine.B_ZERO_VALUE_INT;

		for(int i = 0; i < CAccessExtension.m_oAuthOpts.Length; ++i) {
			nSumValue += (int)(a_eSender & CAccessExtension.m_oAuthOpts[i]);
		}

		return nSumValue != KCDefine.B_ZERO_VALUE_INT;
	}

	//! 인증 요청 완료 여부를 검사한다
	public static bool ExIsCompleteRequest(this AuthorizationRequest a_oSender) {
		return a_oSender != null && a_oSender.IsFinished;
	}
#endif			// #if UNITY_IOS && NOTI_MODULE_ENABLE
	#endregion			// 조건부 클래스 함수
}
