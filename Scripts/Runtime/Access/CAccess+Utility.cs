using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif			// #if UNITY_EDITOR

#if UNITY_IOS
using UnityEngine.iOS;
#endif			// #if UNITY_IOS

#if UNITY_ANDROID
using UnityEngine.Android;
#endif			// #if UNITY_ANDROID

//! 유틸리티 접근자
public static partial class CAccess {
	#region 클래스 함수
	//! 에디터 여부를 검사한다
	public static bool IsEditor() {
		return Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor;
	}

	//! 데스크 탑 여부를 검사한다
	public static bool IsDesktop() {
		return CAccess.IsMac() || CAccess.IsWindows();
	}

	//! 독립 플랫폼 여부를 검사한다
	public static bool IsStandalone() {
		return Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.WindowsPlayer;
	}

	//! 맥 여부를 검사한다
	public static bool IsMac() {
		return Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
	}

	//! 윈도우 여부를 검사한다
	public static bool IsWindows() {
		return Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer;
	}

	//! 모바일 여부를 검사한다
	public static bool IsMobile() {
		return Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android;
	}

	//! 콘솔 여부를 검사한다
	public static bool IsConsole() {
		return Application.platform == RuntimePlatform.PS4 || Application.platform == RuntimePlatform.XboxOne;
	}

	//! 휴대용 콘솔 여부를 검사한다
	public static bool IsHandheldConsole() {
		return Application.platform == RuntimePlatform.Stadia || Application.platform == RuntimePlatform.Switch;
	}

	//! 권한 유효 여부를 검사한다
	public static bool IsEnablePermission(string a_oPermission) {
#if UNITY_ANDROID
		return a_oPermission.ExIsValid() && 
			Permission.HasUserAuthorizedPermission(a_oPermission);
#else
		return false;
#endif			// #if UNITY_ANDROID
	}

	//! 햅틱 피드백 지원 여부를 검사한다
	public static bool IsSupportHapticFeedback() {
#if HAPTIC_FEEDBACK_ENABLE && (UNITY_IOS || UNITY_ANDROID)
#if UNITY_IOS
		var oVersion = new System.Version(Device.systemVersion);
		int nCompareResult = oVersion.CompareTo(KCDefine.U_MIN_VERSION_HAPTIC_FEEDBACK);

		// 햅틱 피드백 지원 버전 일 경우
		if(nCompareResult >= KCDefine.B_COMPARE_RESULT_EQUALS) {
			string oModel = Device.generation.ToString();
			bool bIsiPhone = oModel.Contains(KCDefine.U_MODEL_NAME_IPHONE);

			for(int i = 0; i < KCDefine.U_HAPTIC_FEEDBACK_SUPPORT_MODELS.Length; ++i) {
				var eModel = KCDefine.U_HAPTIC_FEEDBACK_SUPPORT_MODELS[i];

				// 햅틱 피드백 지원 모델 일 경우
				if(bIsiPhone && eModel == Device.generation) {
					return true;
				}
			}
		}

		return false;
#else
		return true;
#endif			// #if UNITY_IOS
#else
		return false;
#endif			// #if HAPTIC_FEEDBACK_ENABLE && (UNITY_IOS || UNITY_ANDROID)
	}

	//! DPI 를 반환한다
	public static float GetDPI() {
		return Screen.dpi;
	}

	//! 디바이스 타입을 반환한다
	public static EDeviceType GetDeviceType() {
#if UNITY_IOS
		string oModel = Device.generation.ToString();

		bool bIsiPhone = oModel.Contains(KCDefine.U_MODEL_NAME_IPHONE);
		bool bIsiPad = oModel.Contains(KCDefine.U_MODEL_NAME_IPAD);

		// iPhone, iPad 디바이스 일 경우
		if(bIsiPhone || bIsiPad) {
			return bIsiPhone ? EDeviceType.PHONE : EDeviceType.TABLET;
		}
#endif			// #if UNITY_IOS

		var stScreenSize = CAccess.GetScreenSize();

		float fInches = CAccess.GetScreenInches();
		float fAspect = Mathf.Max(stScreenSize.x, stScreenSize.y) / Mathf.Min(stScreenSize.x, stScreenSize.y);

		bool bIsTablet = fInches.ExIsGreate(KCDefine.U_UNIT_TABLET_INCHES) && 
			fAspect.ExIsLess(KCDefine.U_UNIT_TABLET_ASPECT);

		return bIsTablet ? EDeviceType.TABLET : EDeviceType.PHONE;
	}
	
	//! 안전 영역을 반환한다
	public static Rect GetSafeArea() {
		// 앱이 실행 중 일 경우
		if(Application.isPlaying) {
			return Screen.safeArea;
		}

		return new Rect(KCDefine.B_VALUE_FLOAT_0, 
			KCDefine.B_VALUE_FLOAT_0, Camera.main.pixelWidth, Camera.main.pixelHeight);
	}

	//! 배너 광고 높이를 반환한다
	public static float GetBannerAdsHeight(float a_fHeight) {
		float fScale = CAccess.GetResolutionScale();
		float fPercent = KCDefine.B_SCREEN_HEIGHT / CAccess.GetScreenSize().y;

		float fDPI = CAccess.GetDPI();
		float fBannerAdsHeight = a_fHeight * (fDPI / KCDefine.B_DEF_DPI);

		return (fBannerAdsHeight * fPercent) / fScale;
	}

	//! 화면 인치를 반환한다
	public static float GetScreenInches() {
		var stScreenSize = CAccess.GetScreenSize();
		stScreenSize = new Vector2(stScreenSize.x / Screen.dpi, stScreenSize.y / Screen.dpi);

		return stScreenSize.magnitude;
	}

	//! 화면 크기를 반환한다
	public static Vector2 GetScreenSize() {
		// 앱이 실행 중 일 경우
		if(Application.isPlaying) {
#if UNITY_EDITOR
			return new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
#else
			return new Vector2(Screen.width, Screen.height);
#endif			// #if UNITY_EDITOR			
		}

		return new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
	}

	//! 해상도를 반환한다
	public static Vector2 GetResolution() {
		float fScale = CAccess.GetResolutionScale();
		return new Vector2(KCDefine.B_SCREEN_WIDTH, KCDefine.B_SCREEN_HEIGHT) * fScale;
	}

	//! 해상도 비율을 반환한다
	public static float GetResolutionScale() {
		float fScale = KCDefine.B_VALUE_FLOAT_1;
		float fAspect = KCDefine.B_SCREEN_WIDTH / (float)KCDefine.B_SCREEN_HEIGHT;

		float fScreenWidth = CAccess.GetScreenSize().x;
		float fScreenHeight = CAccess.GetScreenSize().y;

		// 화면 너비를 벗어났을 경우
		if(fScreenWidth.ExIsLess(fScreenHeight * fAspect)) {
			fScale = fScreenWidth / (fScreenHeight * fAspect);
		}
		
		return fScale;
	}

	//! 왼쪽 화면 비율을 반환한다
	public static float GetLeftScreenScale() {
		var stSafeArea = CAccess.GetSafeArea();
		return stSafeArea.x / CAccess.GetScreenSize().x;
	}

	//! 오른쪽 화면 비율을 반환한다
	public static float GetRightScreenScale() {
		var stSafeArea = CAccess.GetSafeArea();
		float fScreenWidth = CAccess.GetScreenSize().x;

		return (fScreenWidth - (stSafeArea.x + stSafeArea.width)) / fScreenWidth;
	}

	//! 상단 화면 비율을 반환한다
	public static float GetTopScreenScale() {
		var stSafeArea = CAccess.GetSafeArea();
		float fScreenHeight = CAccess.GetScreenSize().y;

		return (fScreenHeight - (stSafeArea.y + stSafeArea.height)) / fScreenHeight;
	}

	//! 하단 화면 비율을 반환한다
	public static float GetBottomScreenScale() {
		var stSafeArea = CAccess.GetSafeArea();
		return stSafeArea.y / CAccess.GetScreenSize().y;
	}

	//! 객체를 제거한다
	public static void RemoveObj(Object a_oObj, bool a_bIsRemoveAsset = false) {
		CAccess.Assert(a_oObj != null);

		// 앱이 실행 중 일 경우
		if(Application.isPlaying) {
			GameObject.Destroy(a_oObj);
		} else {
			GameObject.DestroyImmediate(a_oObj, a_bIsRemoveAsset);
		}
	}
	#endregion			// 클래스 함수

	#region 제네릭 클래스 함수
	//! 리소스 존재 여부를 검사한다
	public static bool IsExistsRes<T>(string a_oFilepath, 
		bool a_bIsAutoUnload = false) where T : Object 
	{
		var oRes = Resources.Load<T>(a_oFilepath);
		bool bIsExists = oRes != null;

		if(a_bIsAutoUnload && oRes != null) {
			Resources.UnloadAsset(oRes);
		}

		return bIsExists;
	}
	#endregion			// 제네릭 클래스 함수

	#region 조건부 클래스 함수
#if UNITY_EDITOR
	//! 스크립트 순서를 변경한다
	public static void SetScriptOrder(MonoScript a_oScript, int a_nOrder) {
		int nOrder = MonoImporter.GetExecutionOrder(a_oScript);

		// 기존 순서와 다를 경우
		if(nOrder != a_nOrder) {
			MonoImporter.SetExecutionOrder(a_oScript, a_nOrder);
		}
	}
#endif			// #if UNITY_EDITOR

#if UNITY_IOS
	//! 애플 로그인 지원 여부를 검사한다
	public static bool IsSupportLoginWithApple() {
#if UNITY_EDITOR
		return false;
#else
		var oVersion = new System.Version(Device.systemVersion);
		return oVersion.CompareTo(KCDefine.U_MIN_VERSION_LOGIN_WITH_APPLE) >= KCDefine.B_COMPARE_RESULT_EQUALS;
#endif			// #if UNITY_EDITOR
	}
#endif			// #if UNITY_IOS
	#endregion			// 조건부 클래스 함수
}
