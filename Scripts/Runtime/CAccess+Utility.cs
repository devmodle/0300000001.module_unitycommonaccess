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

/** 유틸리티 접근자 */
public static partial class CAccess {
	#region 클래스 프로퍼티
	public static bool IsNeedsTrackingConsent {
		get {
#if !UNITY_EDITOR && UNITY_IOS
			var oVer = new System.Version(Device.systemVersion);
			return oVer.CompareTo(KCDefine.U_MIN_VER_TRACKING_CONSENT_VIEW) >= KCDefine.B_COMPARE_EQUALS;
#elif !UNITY_EDITOR && UNITY_ANDROID
			return false;
#else
			return true;
#endif			// #if !UNITY_EDITOR && UNITY_IOS
		}
	}

	public static bool IsSupportsHapticFeedback {
		get {
#if !UNITY_EDITOR && UNITY_IOS
			var oVer = new System.Version(Device.systemVersion);
			int nCompare = oVer.CompareTo(KCDefine.U_MIN_VER_HAPTIC_FEEDBACK);

			// 햅틱 피드백 지원 버전 일 경우
			if(nCompare >= KCDefine.B_COMPARE_EQUALS) {
				string oModel = Device.generation.ToString();
				return oModel.Contains(KCDefine.U_MODEL_N_IPHONE) && KCDefine.U_HAPTIC_FEEDBACK_SUPPORTS_MODEL_LIST.Contains(Device.generation);
			}

			return false;
#elif !UNITY_EDITOR && UNITY_ANDROID
			return true;
#else
			return false;
#endif			// #if !UNITY_EDITOR && UNITY_IOS
		}
	}

	public static EDeviceType DeviceType {
		get {
#if UNITY_IOS
			string oModel = Device.generation.ToString();
			return oModel.Contains(KCDefine.U_MODEL_N_IPAD) ? EDeviceType.TABLET : EDeviceType.PHONE;
#elif UNITY_ANDROID
			// TODO: 테블릿 여부 검사 로직 구현 필요
			return EDeviceType.PHONE;
#else
			switch(Application.platform) {
				case RuntimePlatform.PS4: case RuntimePlatform.PS5: case RuntimePlatform.XboxOne: case RuntimePlatform.GameCoreXboxOne: case RuntimePlatform.GameCoreXboxSeries: return EDeviceType.CONSOLE;
				case RuntimePlatform.Stadia: case RuntimePlatform.Switch: return EDeviceType.HANDHELD_CONSOLE;
				default: return EDeviceType.UNKNOWN;
			}
#endif			// #if UNITY_IOS
		}
	}
	
	public static Vector3 ScreenSize {
		get {
#if UNITY_EDITOR
			return new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, KCDefine.B_VAL_0_FLT);
#else
			return new Vector3(Screen.width, Screen.height, KCDefine.B_VAL_0_FLT);
#endif			// #if UNITY_EDITOR
		}
	}

	public static Rect SafeArea {
		get {
#if UNITY_EDITOR
			return new Rect(KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT, Camera.main.pixelWidth, Camera.main.pixelHeight);
#else
			return Screen.safeArea;
#endif			// #if UNITY_EDITOR
		}
	}
	
	public static float UpScreenScale => (CAccess.ScreenSize.y - (CAccess.SafeArea.y + CAccess.SafeArea.height)) / CAccess.ScreenSize.y;
	public static float DownScreenScale => CAccess.SafeArea.y / CAccess.ScreenSize.y;
	public static float LeftScreenScale => CAccess.SafeArea.x / CAccess.ScreenSize.x;
	public static float RightScreenScale => (CAccess.ScreenSize.x - (CAccess.SafeArea.x + CAccess.SafeArea.width)) / CAccess.ScreenSize.x;

	public static float ScreenDPI => Screen.dpi;
	public static float ResolutionScale => CAccess.ScreenSize.x.ExIsLess(CAccess.DesignScreenSize.x) ? CAccess.ScreenSize.x / CAccess.DesignScreenSize.x : KCDefine.B_VAL_1_FLT;
	public static float DesktopResolutionScale => CAccess.DesktopScreenSize.x.ExIsLess(CAccess.DesignDesktopScreenSize.x) ? CAccess.DesktopScreenSize.x / CAccess.DesignDesktopScreenSize.x : KCDefine.B_VAL_1_FLT;

	public static Vector3 Resolution => KCDefine.B_SCREEN_SIZE * CAccess.ResolutionScale;
	public static Vector3 DesignScreenSize => new Vector3(CAccess.ScreenSize.y * (KCDefine.B_SCREEN_WIDTH / (float)KCDefine.B_SCREEN_HEIGHT), CAccess.ScreenSize.y, CAccess.ScreenSize.z);
	public static Vector3 DesktopScreenSize => new Vector3(Screen.currentResolution.width, Screen.currentResolution.height, KCDefine.B_VAL_0_FLT);
	public static Vector3 DesignDesktopScreenSize => new Vector3(CAccess.DesktopScreenSize.y * (KCDefine.B_LANDSCAPE_SCREEN_WIDTH / (float)KCDefine.B_LANDSCAPE_SCREEN_HEIGHT), CAccess.DesktopScreenSize.y, CAccess.DesktopScreenSize.z);
	public static Vector3 CorrectDesktopScreenSize => CAccess.DesignCorrectDesktopScreenSize * CAccess.DesktopResolutionScale;
	
	private static Vector3 DesignCorrectDesktopScreenSize => CAccess.DesignDesktopScreenSize * KCDefine.B_DESKTOP_SCREEN_RATE;
	#endregion			// 클래스 프로퍼티

	#region 클래스 함수
	/** 권한 유효 여부를 검사한다 */
	public static bool IsEnablePermission(string a_oPermission) {
		CAccess.Assert(a_oPermission.ExIsValid());

#if UNITY_ANDROID
		return Permission.HasUserAuthorizedPermission(a_oPermission);
#else
		return false;
#endif			// #if UNITY_ANDROID
	}
	
	/** 배너 광고 높이를 반환한다 */
	public static float GetBannerAdsHeight(float a_fHeight) {
		CAccess.Assert(a_fHeight.ExIsGreateEquals(KCDefine.B_VAL_0_FLT));
		return (a_fHeight.ExDPIPixelsToPixels() * (KCDefine.B_SCREEN_HEIGHT / CAccess.ScreenSize.y)) / CAccess.ResolutionScale;
	}

	/** iOS 이름을 반환한다 */
	public static string GetiOSName(EiOSType a_eType) {
		switch(a_eType) {
			default: return KCDefine.B_PLATFORM_N_IOS_APPLE;
		}
	}

	/** 안드로이드 이름을 반환한다 */
	public static string GetAndroidName(EAndroidType a_eType) {
		switch(a_eType) {
			case EAndroidType.AMAZON: return KCDefine.B_PLATFORM_N_ANDROID_AMAZON;
			default: return KCDefine.B_PLATFORM_N_ANDROID_GOOGLE;
		}
	}

	/** 독립 플랫폼 이름을 반환한다 */
	public static string GetStandaloneName(EStandaloneType a_eType) {
		switch(a_eType) {
			case EStandaloneType.WNDS_STEAM: return KCDefine.B_PLATFORM_N_STANDALONE_WNDS_STEAM;
			default: return KCDefine.B_PLATFORM_N_STANDALONE_MAC_STEAM;
		}
	}

	/** 렌더링 파이프라인 경로를 반환한다 */
	public static string GetRenderingPipelinePath(EQualityLevel a_eQualityLevel) {
		switch(a_eQualityLevel) {
			case EQualityLevel.HIGH: return KCDefine.U_ASSET_P_G_HIGH_QUALITY_UNIVERSAL_RP;
			case EQualityLevel.ULTRA: return KCDefine.U_ASSET_P_G_ULTRA_QUALITY_UNIVERSAL_RP;
			default: return KCDefine.U_ASSET_P_G_NORM_QUALITY_UNIVERSAL_RP;
		}
	}

	/** 포스트 프로세싱 설정 경로를 반환한다 */
	public static string GetPostProcessingSettingsPath(EQualityLevel a_eQualityLevel) {
		switch(a_eQualityLevel) {
			case EQualityLevel.HIGH: return KCDefine.U_ASSET_P_G_HIGH_QUALITY_POST_PROCESSING_SETTINGS;
			case EQualityLevel.ULTRA: return KCDefine.U_ASSET_P_G_ULTRA_QUALITY_POST_PROCESSING_SETTINGS;
			default: return KCDefine.U_ASSET_P_G_NORM_QUALITY_POST_PROCESSING_SETTINGS;
		}
	}
		
	/** 값을 할당한다 */
	public static void AssignVal(ref DG.Tweening.Tween a_rLhs, DG.Tweening.Tween a_oRhs, DG.Tweening.Tween a_oDefVal = null) {
		a_rLhs?.Kill();
		a_rLhs = a_oRhs ?? a_oDefVal;
	}
	
	/** 값을 할당한다 */
	public static void AssignVal(ref Sequence a_rLhs, Sequence a_oRhs, Sequence a_oDefVal = null) {
		a_rLhs?.Kill();
		a_rLhs = a_oRhs ?? a_oDefVal;
	}

	/** DPI 픽셀 => 픽셀로 변환한다 */
	private static float ExDPIPixelsToPixels(this float a_fSender) {
		return a_fSender * (CAccess.ScreenDPI / KCDefine.B_DEF_SCREEN_DPI);
	}
	#endregion			// 클래스 함수

	#region 제네릭 클래스 함수
	/** 리소스 존재 여부를 검사한다 */
	public static bool IsExistsRes<T>(string a_oFilePath, bool a_bIsAutoUnload = false) where T : Object {
		CAccess.Assert(a_oFilePath.ExIsValid());
		
		var oRes = Resources.Load<T>(a_oFilePath);
		bool bIsExistsRes = typeof(T).Equals(typeof(TextAsset)) ? (oRes as TextAsset).ExIsValid() : oRes != null;
		
		// 자동 제거 모드 일 경우
		if(bIsExistsRes && a_bIsAutoUnload) {
			Resources.UnloadAsset(oRes);
		}

		return bIsExistsRes;
	}
	#endregion			// 제네릭 클래스 함수

	#region 조건부 클래스 함수
#if UNITY_EDITOR
	/** 스크립트 순서를 변경한다 */
	public static void SetScriptOrder(MonoScript a_oScript, int a_nOrder, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oScript != null);

		// 스크립트가 존재 할 경우
		if(a_oScript != null) {
			int nOrder = MonoImporter.GetExecutionOrder(a_oScript);

			// 기존 순서와 다를 경우
			if(nOrder != a_nOrder) {
				MonoImporter.SetExecutionOrder(a_oScript, a_nOrder);
			}
		}
	}
#endif			// #if UNITY_EDITOR

#if PURCHASE_MODULE_ENABLE
	/** 가격 문자열을 반환한다 */
	public static string GetPriceStr(Product a_oProduct) {
		CAccess.Assert(a_oProduct != null);
		return string.Format(KCDefine.B_TEXT_FMT_2_SPACE_COMBINE, a_oProduct.metadata.isoCurrencyCode, a_oProduct.metadata.localizedPrice);
	}
#endif			// #if PURCHASE_MODULE_ENABLE
	#endregion			// 조건부 클래스 함수
}
