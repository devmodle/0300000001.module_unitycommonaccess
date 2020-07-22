using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif			// #if UNITY_EDITOR

#if UNITY_IOS
using UnityEngine.iOS;
#endif			// #if UNITY_IOS

//! 유틸리티 접근자
public static partial class CAccess {
	#region 클래스 함수
	//! 에디터 플랫폼 여부를 검사한다
	public static bool IsEditorPlatform() {
		return Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor;
	}

	//! 데스크 탑 플랫폼 여부를 검사한다
	public static bool IsDesktopPlatform() {
		return CAccess.IsMacPlatform() || CAccess.IsWindowsPlatform();
	}

	//! 독립 플랫폼 여부를 검사한다
	public static bool IsStandalonePlatform() {
		return Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.WindowsPlayer;
	}

	//! 맥 플랫폼 여부를 검사한다
	public static bool IsMacPlatform() {
		return Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
	}

	//! 윈도우 플랫폼 여부를 검사한다
	public static bool IsWindowsPlatform() {
		return Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer;
	}

	//! 모바일 플랫폼 여부를 검사한다
	public static bool IsMobilePlatform() {
		return Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android;
	}

	//! 콘솔 플랫폼 여부를 검사한다
	public static bool IsConsolePlatform() {
		return Application.platform == RuntimePlatform.PS4 || Application.platform == RuntimePlatform.XboxOne;
	}

	//! 휴대용 콘솔 플랫폼 여부를 검사한다
	public static bool IsHandheldConsolePlatform() {
		return Application.platform == RuntimePlatform.Stadia || Application.platform == RuntimePlatform.Switch;
	}
	
	//! 안전 영역을 반환한다
	public static Rect GetSafeArea(bool a_bIsRuntime = true) {
		if(a_bIsRuntime) {
			return Screen.safeArea;
		}

		return new Rect(0.0f, 0.0f, Camera.main.pixelWidth, Camera.main.pixelHeight);
	}

	//! 디바이스 화면 크기를 반환한다
	public static Vector2 GetDeviceScreenSize(bool a_bIsRuntime = true) {
		if(a_bIsRuntime) {
#if UNITY_EDITOR
			return new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
#else
			return new Vector2(Screen.width, Screen.height);
#endif			// #if UNITY_EDITOR			
		}

		return new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
	}

	//! 해상도를 반환한다
	public static Vector2 GetResolution(bool a_bIsRuntime = true) {
		float fScale = CAccess.GetResolutionScale(a_bIsRuntime);
		return new Vector2(KCDefine.B_SCREEN_WIDTH, KCDefine.B_SCREEN_HEIGHT) * fScale;
	}

	//! 해상도 비율을 반환한다
	public static float GetResolutionScale(bool a_bIsRuntime = true) {
		float fScale = 1.0f;
		float fAspect = KCDefine.B_SCREEN_WIDTH / (float)KCDefine.B_SCREEN_HEIGHT;

		float fScreenWidth = CAccess.GetDeviceScreenSize(a_bIsRuntime).x;
		float fScreenHeight = CAccess.GetDeviceScreenSize(a_bIsRuntime).y;

		// 화면을 벗어났을 경우
		if(fScreenWidth.ExIsLess(fScreenHeight * fAspect)) {
			fScale = fScreenWidth / (fScreenHeight * fAspect);
		}
		
		return fScale;
	}

	//! 왼쪽 화면 비율을 반환한다
	public static float GetLeftScreenScale(bool a_bIsRuntime = true) {
		var stSafeArea = CAccess.GetSafeArea(a_bIsRuntime);
		return stSafeArea.x / CAccess.GetDeviceScreenSize(a_bIsRuntime).x;
	}

	//! 오른쪽 화면 비율을 반환한다
	public static float GetRightScreenScale(bool a_bIsRuntime = true) {
		var stSafeArea = CAccess.GetSafeArea(a_bIsRuntime);
		float fScreenWidth = CAccess.GetDeviceScreenSize(a_bIsRuntime).x;

		return (fScreenWidth - (stSafeArea.x + stSafeArea.width)) / fScreenWidth;
	}

	//! 상단 화면 비율을 반환한다
	public static float GetTopScreenScale(bool a_bIsRuntime = true) {
		var stSafeArea = CAccess.GetSafeArea(a_bIsRuntime);
		float fScreenHeight = CAccess.GetDeviceScreenSize(a_bIsRuntime).y;

		return (fScreenHeight - (stSafeArea.y + stSafeArea.height)) / fScreenHeight;
	}

	//! 하단 화면 비율을 반환한다
	public static float GetBottomScreenScale(bool a_bIsRuntime = true) {
		var stSafeArea = CAccess.GetSafeArea(a_bIsRuntime);
		return stSafeArea.y / CAccess.GetDeviceScreenSize(a_bIsRuntime).y;
	}

	//! 컴포넌트를 제거한다
	public static void RemoveComponent(Component a_oComponent) {
		CAccess.Assert(a_oComponent != null);

		if(Application.isPlaying) {
			GameObject.Destroy(a_oComponent);
		} else {
			GameObject.DestroyImmediate(a_oComponent);
		}
	}
	#endregion			// 클래스 함수

	#region 조건부 클래스 함수
#if UNITY_IOS
	//! 애플 로그인 지원 여부를 검사한다
	public static bool IsSupportLoginWithApple() {
		if(!CAccess.IsMobilePlatform()) {
			return false;
		}

		var oVersion = new System.Version(Device.systemVersion);
		return oVersion.CompareTo(KCDefine.U_MIN_VERSION_LOGIN_WITH_APPLE) >= KCDefine.B_COMPARE_RESULT_EQUALS;
	}
#endif			// UNITY_IOS

#if HAPTIC_FEEDBACK_ENABLE && (UNITY_IOS || UNITY_ANDROID)
	//! 햅틱 피드백 지원 여부를 검사한다
	public static bool IsSupportHapticFeedback() {
#if UNITY_IOS
		string oModel = Device.generation.ToString();
		bool bIsiPhone = oModel.Contains(KCDefine.U_MODEL_NAME_IPHONE);

		int nIndex = KCDefine.U_HAPTIC_FEEDBACK_SUPPORT_MODELS.ExFindValue((a_eDeviceGeneration) => {
			return bIsiPhone && a_eDeviceGeneration == Device.generation;
		});

		return nIndex > KCDefine.B_INDEX_INVALID;
#else
		return true;
#endif			// #if UNITY_IOS
	}
#endif			// #if HAPTIC_FEEDBACK_ENABLE && (UNITY_IOS || UNITY_ANDROID)

#if UNITY_EDITOR
	//! 스크립트 순서를 변경한다
	public static void SetScriptOrder(MonoScript a_oScript, int a_nOrder) {
		CAccess.Assert(a_oScript != null && (a_nOrder >= short.MinValue && a_nOrder <= short.MaxValue));
		int nOrder = MonoImporter.GetExecutionOrder(a_oScript);

		if(nOrder != a_nOrder) {
			MonoImporter.SetExecutionOrder(a_oScript, a_nOrder);
		}
	}
#endif			// #if UNITY_EDITOR
	#endregion			// 조건부 클래스 함수
}
