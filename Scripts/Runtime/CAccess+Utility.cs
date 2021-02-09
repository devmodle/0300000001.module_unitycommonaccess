using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

#if UNITY_EDITOR
using UnityEditor;
#endif			// #if UNITY_EDITOR

#if UNITY_IOS
using UnityEngine.iOS;
#endif			// #if UNITY_IOS

#if UNITY_ANDROID
using UnityEngine.Android;
#endif			// #if UNITY_ANDROID

#if PURCHASE_MODULE_ENABLE
using UnityEngine.Purchasing;
#endif			// #if PURCHASE_MODULE_ENABLE

//! 유틸리티 접근자
public static partial class CAccess {
	#region 클래스 함수
	//! 에디터 여부를 검사한다
	public static bool IsEditor() {
		return Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor;
	}

	//! 데스크 탑 여부를 검사한다
	public static bool IsDesktop() {
		return CAccess.IsMac() || CAccess.IsWnds();
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
	public static bool IsWnds() {
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
		CAccess.Assert(a_oPermission.ExIsValid());

#if UNITY_ANDROID
		return Permission.HasUserAuthorizedPermission(a_oPermission);
#else
		return false;
#endif			// #if UNITY_ANDROID
	}

	//! 햅틱 피드백 지원 여부를 검사한다
	public static bool IsSupportsHapticFeedback() {
#if UNITY_EDITOR || !(HAPTIC_FEEDBACK_ENABLE && (UNITY_IOS || UNITY_ANDROID))
		return false;
#else
#if UNITY_IOS
		var oVersion = new System.Version(Device.systemVersion);
		int nCompareResult = oVersion.CompareTo(KCDefine.U_MIN_VERSION_HAPTIC_FEEDBACK);

		// 햅틱 피드백 지원 버전 일 경우
		if(nCompareResult >= KCDefine.B_COMPARE_R_EQUALS) {
			string oModel = Device.generation.ToString();
			bool bIsiPhone = oModel.Contains(KCDefine.U_MODEL_N_IPHONE);

			for(int i = 0; i < KCDefine.U_HAPTIC_FEEDBACK_SUPPORTS_MODELS.Length; ++i) {
				var eModel = KCDefine.U_HAPTIC_FEEDBACK_SUPPORTS_MODELS[i];

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
#endif			// #if UNITY_EDITOR
	}

	//! DPI 를 반환한다
	public static float GetDPI() {
		return Screen.dpi;
	}

	//! 디바이스 타입을 반환한다
	public static EDeviceType GetDeviceType() {
#if UNITY_IOS
		string oModel = Device.generation.ToString();
		bool bIsiPhone = oModel.Contains(KCDefine.U_MODEL_N_IPHONE);

		// iPhone, iPad 일 경우
		if(bIsiPhone || oModel.Contains(KCDefine.U_MODEL_N_IPAD)) {
			return bIsiPhone ? EDeviceType.PHONE : EDeviceType.TABLET;
		}
#endif			// #if UNITY_IOS

		var stScreenSize = CAccess.GetScreenSize();
		float fScreenInches = CAccess.GetScreenInches();

		float fMaxLength = Mathf.Max(stScreenSize.x, stScreenSize.y);
		float fMinLength = Mathf.Min(stScreenSize.x, stScreenSize.y);

		float fAspect = fMaxLength / fMinLength;
		bool bIsTablet = fScreenInches.ExIsGreate(KCDefine.U_UNIT_TABLET_INCHES) && fAspect.ExIsLess(KCDefine.U_UNIT_TABLET_ASPECT);

		return bIsTablet ? EDeviceType.TABLET : EDeviceType.PHONE;
	}
	
	//! 안전 영역을 반환한다
	public static Rect GetSafeArea() {
		// 앱이 실행 중 일 경우
		if(Application.isPlaying) {
			return Screen.safeArea;
		}

		return new Rect(KCDefine.B_VALUE_FLT_0, KCDefine.B_VALUE_FLT_0, Camera.main.pixelWidth, Camera.main.pixelHeight);
	}

	//! 배너 광고 높이를 반환한다
	public static float GetBannerAdsHeight(float a_fHeight) {
		CAccess.Assert(a_fHeight.ExIsGreateEquals(KCDefine.B_VALUE_FLT_0));

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
		float fScale = KCDefine.B_VALUE_FLT_1;
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
	
	//! 값을 할당한다
	public static void AssignValue(ref Sequence a_oLhs, Sequence a_oRhs) {
		a_oLhs?.Kill();
		a_oLhs = a_oRhs;
	}
	#endregion			// 클래스 함수

	#region 제네릭 클래스 함수
	//! 리소스 존재 여부를 검사한다
	public static bool IsExistsRes<T>(string a_oFilePath, bool a_bIsAutoUnload = false) where T : Object {
		CAccess.Assert(a_oFilePath.ExIsValid());
		var oRes = Resources.Load<T>(a_oFilePath);

		// 자동 제거 모드 일 경우
		if(a_bIsAutoUnload && oRes != null) {
			Resources.UnloadAsset(oRes);
		}

		return oRes != null;
	}
	#endregion			// 제네릭 클래스 함수

	#region 조건부 클래스 함수
#if UNITY_EDITOR
	//! 스크립트 순서를 변경한다
	public static void SetScriptOrder(MonoScript a_oScript, int a_nOrder) {
		CAccess.Assert(a_oScript != null);
		int nOrder = MonoImporter.GetExecutionOrder(a_oScript);

		// 기존 순서와 다를 경우
		if(nOrder != a_nOrder) {
			MonoImporter.SetExecutionOrder(a_oScript, a_nOrder);
		}
	}
#endif			// #if UNITY_EDITOR

#if UNITY_IOS
	//! 애플 로그인 지원 여부를 검사한다
	public static bool IsSupportsLoginWithApple() {
#if UNITY_EDITOR
		return false;
#else
		var oVersion = new System.Version(Device.systemVersion);
		int nCompareResult = oVersion.CompareTo(KCDefine.U_MIN_VERSION_LOGIN_WITH_APPLE);
		
		return nCompareResult >= KCDefine.B_COMPARE_R_EQUALS;
#endif			// #if UNITY_EDITOR
	}
#endif			// #if UNITY_IOS

#if PURCHASE_MODULE_ENABLE
	//! 가격 문자열을 반환한다
	public static string GetPriceString(Product a_oProduct) {
		CAccess.Assert(a_oProduct != null);

		decimal dclPrice = a_oProduct.metadata.localizedPrice;
		string oCurrencyCode = a_oProduct.metadata.isoCurrencyCode;
		
		return string.Format(KCDefine.B_TEXT_FMT_PRICE, oCurrencyCode, dclPrice);		
	}
#endif			// #if PURCHASE_MODULE_ENABLE
	#endregion			// 조건부 클래스 함수
}
