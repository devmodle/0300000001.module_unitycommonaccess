using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System.IO;
using UnityEngine.SceneManagement;

using DG.Tweening;

#if UNITY_EDITOR
using UnityEditor;
#endif // #if UNITY_EDITOR

#if UNITY_IOS
using UnityEngine.iOS;
#endif // #if UNITY_IOS

#if UNITY_ANDROID
using UnityEngine.Android;
#endif // #if UNITY_ANDROID

#if ADS_MODULE_ENABLE && ADMOB_ADS_ENABLE
using GoogleMobileAds.Api;
#endif // #if ADS_MODULE_ENABLE && ADMOB_ADS_ENABLE

#if PURCHASE_MODULE_ENABLE
using UnityEngine.Purchasing;
#endif // #if PURCHASE_MODULE_ENABLE

/** 유틸리티 접근자 */
public static partial class CAccess {
	#region 클래스 변수
	private static Dictionary<RuntimePlatform, EDeviceType> m_oDeviceTypeDict = new Dictionary<RuntimePlatform, EDeviceType>() {
		// 데스크 탑 {
		[RuntimePlatform.OSXEditor] = EDeviceType.DESKTOP,
		[RuntimePlatform.OSXPlayer] = EDeviceType.DESKTOP,

		[RuntimePlatform.WindowsEditor] = EDeviceType.DESKTOP,
		[RuntimePlatform.WindowsPlayer] = EDeviceType.DESKTOP,
		// 데스크 탑 }

		// 콘솔
		[RuntimePlatform.PS4] = EDeviceType.CONSOLE,
		[RuntimePlatform.PS5] = EDeviceType.CONSOLE,
		[RuntimePlatform.XboxOne] = EDeviceType.CONSOLE,

		// 휴대용 콘솔
		[RuntimePlatform.Switch] = EDeviceType.HANDHELD_CONSOLE
	};
	#endregion // 클래스 변수

	#region 클래스 프로퍼티
	public static bool IsNeedsTrackingConsent {
		get {
#if !UNITY_EDITOR && UNITY_IOS
			var oVer = new System.Version(Device.systemVersion);
			return oVer.CompareTo(KCDefine.B_MIN_VER_TRACKING_CONSENT_VIEW) >= KCDefine.B_COMPARE_EQUALS;
#elif !UNITY_EDITOR && UNITY_ANDROID
			return false;
#else
			return true;
#endif // #if !UNITY_EDITOR && UNITY_IOS
		}
	}

	public static bool IsSupportsHapticFeedback {
		get {
#if !UNITY_EDITOR && UNITY_IOS
			var oVer = new System.Version(Device.systemVersion);

			// 햅틱 피드백 지원 버전 일 경우
			if(oVer.CompareTo(KCDefine.B_MIN_VER_HAPTIC_FEEDBACK) >= KCDefine.B_COMPARE_EQUALS) {
				string oModel = Device.generation.ToString().ToUpper();
				return Device.generation >= iPhone7 && oModel.Contains(KCDefine.B_MODEL_N_IPHONE.ToUpper());
			}

			return false;
#elif !UNITY_EDITOR && UNITY_ANDROID
			return true;
#else
			return false;
#endif // #if !UNITY_EDITOR && UNITY_IOS
		}
	}

	public static KeyCode SubCmdKeyCode {
		get {
#if UNITY_EDITOR_WIN
			return KeyCode.LeftAlt;
#elif UNITY_EDITOR_OSX
			return KeyCode.LeftCommand;
#else
			return (Application.platform == RuntimePlatform.WindowsPlayer) ? KeyCode.LeftAlt : KeyCode.LeftCommand;
#endif // #if UNITY_EDITOR_WIN
		}
	}

	public static EDeviceType DeviceType {
		get {
#if UNITY_IOS || UNITY_ANDROID
			return EDeviceType.PHONE;
#else
			return CAccess.m_oDeviceTypeDict.GetValueOrDefault(Application.platform, EDeviceType.UNKNOWN);
#endif // #if UNITY_IOS
		}
	}

	public static Rect SafeArea {
		get {
#if UNITY_EDITOR
			return UnityEngine.Device.Application.isEditor ? new Rect(KCDefine.B_VAL_0_REAL, KCDefine.B_VAL_0_REAL, Camera.main.pixelWidth, Camera.main.pixelHeight) : Screen.safeArea;
#else
			return Screen.safeArea;
#endif // #if UNITY_EDITOR
		}
	}

	public static Vector3 DeviceScreenSize {
		get {
#if UNITY_EDITOR
			return new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, KCDefine.B_VAL_0_REAL);
#else
			return new Vector3(Screen.width, Screen.height, KCDefine.B_VAL_0_REAL);
#endif // #if UNITY_EDITOR
		}
	}

	public static float UpSafeAreaScale => (CAccess.DeviceScreenSize.y - (CAccess.SafeArea.y + CAccess.SafeArea.height)) / CAccess.DeviceScreenSize.y;
	public static float DownSafeAreaScale => CAccess.SafeArea.y / CAccess.DeviceScreenSize.y;
	public static float LeftSafeAreaScale => CAccess.SafeArea.x / CAccess.DeviceScreenSize.x;
	public static float RightSafeAreaScale => (CAccess.DeviceScreenSize.x - (CAccess.SafeArea.x + CAccess.SafeArea.width)) / CAccess.DeviceScreenSize.x;

	public static float ResolutionScale => CAccess.DeviceScreenSize.x.ExIsLess(CAccess.ResolutionScreenSize.x) ? CAccess.DeviceScreenSize.x / CAccess.ResolutionScreenSize.x : KCDefine.B_VAL_1_REAL;
	public static float ResolutionUnitScale => KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale;
	public static float DesktopResolutionScale => CAccess.DesktopScreenSize.x.ExIsLess(CAccess.ResolutionDesktopScreenSize.x) ? CAccess.DesktopScreenSize.x / CAccess.ResolutionDesktopScreenSize.x : KCDefine.B_VAL_1_REAL;

	public static string ProductInfoTableLoadPath => File.Exists(KCDefine.U_RUNTIME_TABLE_P_G_PRODUCT_INFO) ? KCDefine.U_RUNTIME_TABLE_P_G_PRODUCT_INFO : KCDefine.U_TABLE_P_G_PRODUCT_INFO;
	public static string ProductInfoTableSavePath => KCDefine.U_RUNTIME_TABLE_P_G_PRODUCT_INFO;

	public static KeyCode CmdKeyCode => KeyCode.LeftShift;
	public static Vector3 DesktopScreenSize => new Vector3(Screen.currentResolution.width, Screen.currentResolution.height, CAccess.DeviceScreenSize.z);
	public static Vector3 CorrectDesktopScreenSize => CAccess.ResulitionCorrectDesktopScreenSize * CAccess.DesktopResolutionScale;

	private static Vector3 ResolutionScreenSize => new Vector3(CAccess.DeviceScreenSize.y * (KCDefine.B_DESIGN_SCREEN_WIDTH / (float)KCDefine.B_DESIGN_SCREEN_HEIGHT), CAccess.DeviceScreenSize.y, CAccess.DeviceScreenSize.z);
	private static Vector3 ResolutionDesktopScreenSize => new Vector3(CAccess.DesktopScreenSize.y * (KCDefine.B_DESIGN_L_SCREEN_WIDTH / (float)KCDefine.B_DESIGN_L_SCREEN_HEIGHT), CAccess.DesktopScreenSize.y, CAccess.DesktopScreenSize.z);
	private static Vector3 ResulitionCorrectDesktopScreenSize => CAccess.ResolutionDesktopScreenSize * KCDefine.B_DESKTOP_SCREEN_RATE;

#if UNITY_EDITOR || UNITY_STANDALONE
	public static float ScreenDPI => KCDefine.B_PLATFORM_SCREEN_DPI * (CAccess.DeviceScreenSize.y / KCDefine.B_DESIGN_SCREEN_HEIGHT);
#else
	public static float ScreenDPI => Screen.dpi;
#endif // #if UNITY_EDITOR || UNITY_STANDALONE
	#endregion // 클래스 프로퍼티

	#region 클래스 함수
	/** 유저 권한 유효 여부를 검사한다 */
	public static bool IsEnableUserPermission(string a_oPermission) {
		CAccess.Assert(a_oPermission.ExIsValid());

#if UNITY_ANDROID
		return Permission.HasUserAuthorizedPermission(a_oPermission);
#else
		return false;
#endif // #if UNITY_ANDROID
	}

	/** 배너 광고 높이를 반환한다 */
	public static float GetBannerAdsHeight(float a_fHeight) {
		CAccess.Assert(a_fHeight.ExIsGreatEquals(KCDefine.B_VAL_0_REAL));
		return (a_fHeight.ExDPIPixelsToPixels() * (KCDefine.B_DESIGN_SCREEN_HEIGHT / CAccess.DeviceScreenSize.y)) / CAccess.ResolutionScale;
	}

	/** iOS 플랫폼을 반환한다 */
	public static string GetiOSPlatform(EiOSType a_eType) {
		switch(a_eType) {
			// Do Something
		}

		return KCDefine.B_PLATFORM_IOS_APPLE;
	}

	/** 안드로이드 이름을 반환한다 */
	public static string GetAndroidPlatform(EAndroidType a_eType) {
		switch(a_eType) {
			case EAndroidType.AMAZON: return KCDefine.B_PLATFORM_ANDROID_AMAZON;
		}

		return KCDefine.B_PLATFORM_ANDROID_GOOGLE;
	}

	/** 독립 플랫폼을 반환한다 */
	public static string GetStandalonePlatform(EStandaloneType a_eType) {
		switch(a_eType) {
			case EStandaloneType.MAC_EDITOR: return KCDefine.B_PLATFORM_STANDALONE_MAC_EDITOR;
			case EStandaloneType.WNDS_STEAM: return KCDefine.B_PLATFORM_STANDALONE_WNDS_STEAM;
			case EStandaloneType.WNDS_EDITOR: return KCDefine.B_PLATFORM_STANDALONE_WNDS_EDITOR;
		}

		return KCDefine.B_PLATFORM_STANDALONE_MAC_STEAM;
	}

	/** URP 에셋 경로를 반환한다 */
	public static string GetURPAssetPath(EQualityLevel a_eQualityLevel) {
		switch(a_eQualityLevel) {
			case EQualityLevel.HIGH: return KCDefine.U_ASSET_P_G_HIGH_QUALITY_URP;
			case EQualityLevel.ULTRA: return KCDefine.U_ASSET_P_G_ULTRA_QUALITY_URP;
		}

		return KCDefine.U_ASSET_P_G_NORM_QUALITY_URP;
	}

	/** 포스트 프로세스 설정 경로를 반환한다 */
	public static string GetPostProcessingSettingsPath(EQualityLevel a_eQualityLevel) {
		switch(a_eQualityLevel) {
			case EQualityLevel.HIGH: return KCDefine.U_ASSET_P_G_HIGH_QUALITY_POST_PROCESSING_SETTINGS;
			case EQualityLevel.ULTRA: return KCDefine.U_ASSET_P_G_ULTRA_QUALITY_POST_PROCESSING_SETTINGS;
		}

		return KCDefine.U_ASSET_P_G_NORM_QUALITY_POST_PROCESSING_SETTINGS;
	}

	/** 시간 비율을 변경한다 */
	public static void SetTimeScale(float a_fScale) {
		Time.timeScale = a_fScale.ExGetClampVal(KCDefine.B_VAL_0_REAL, KCDefine.B_VAL_9_REAL);
	}

	/** 값을 할당한다 */
	public static void AssignVal(ref DG.Tweening.Tween a_rLhs, DG.Tweening.Tween a_oRhs, DG.Tweening.Tween a_oDefVal = null) {
		a_rLhs?.Kill();
		a_rLhs = a_oRhs ?? a_oDefVal;
	}

	/** 값을 할당한다 */
	public static void AssignVal(ref Sequence a_rLhs, DG.Tweening.Tween a_oRhs, DG.Tweening.Tween a_oDefVal = null) {
		a_rLhs?.Kill();
		a_rLhs = (a_oRhs ?? a_oDefVal) as Sequence;
	}

	/** 씬을 순회한다 */
	public static void EnumerateScenes(System.Func<Scene, bool> a_oCallback, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oCallback != null);

		// 씬 순회가 불가능 할 경우
		if(a_oCallback == null) {
			return;
		}

		for(int i = 0; i < SceneManager.sceneCount; ++i) {
			// 씬 순회가 불가능 할 경우
			if(!a_oCallback(SceneManager.GetSceneAt(i))) {
				break;
			}
		}
	}

	/** 객체를 순회한다 */
	public static void EnumerateRootObjs(System.Func<GameObject, bool> a_oCallback, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oCallback != null);

		// 객체 순회가 불가능 할 경우
		if(a_oCallback == null) {
			return;
		}

		CAccess.EnumerateScenes((a_stScene) => {
			bool bIsTrue = true;

			a_stScene.ExEnumerateRootObjs((a_oObj) => {
				return bIsTrue = a_oCallback(a_oObj);
			}, a_bIsAssert);

			return bIsTrue;
		}, a_bIsAssert);
	}

	/** 대기 객체를 반환한다 */
	private static WaitForSeconds GetWaitForSeconds(float a_fDelay) {
		var oWaitForSecs = CAccess.m_oWaitForSecsDict.GetValueOrDefault(a_fDelay) ?? 
			new WaitForSeconds(a_fDelay);

		CAccess.m_oWaitForSecsDict.TryAdd(a_fDelay, oWaitForSecs);
		return oWaitForSecs;
	}

	/** 대기 객체를 반환한다 */
	private static WaitForSecondsRealtime GetWaitForSecondsRealtime(float a_fDelay) {
		var oWaitForSecs = CAccess.m_oWaitForSecsRealtimeDict.GetValueOrDefault(a_fDelay) ?? 
			new WaitForSecondsRealtime(a_fDelay);

		CAccess.m_oWaitForSecsRealtimeDict.TryAdd(a_fDelay, oWaitForSecs);
		return oWaitForSecs;
	}
	#endregion // 클래스 함수

	#region 제네릭 클래스 함수
	/** 리소스 존재 여부를 검사한다 */
	public static bool IsExistsRes<T>(string a_oFilePath, bool a_bIsAutoUnload = false) where T : Object {
		CAccess.Assert(a_oFilePath.ExIsValid());
		var oRes = Resources.Load<T>(a_oFilePath);

		bool bIsExistsRes = typeof(T).Equals(typeof(TextAsset)) ? 
			(oRes as TextAsset).ExIsValid() : oRes != null;

		// 자동 제거 모드 일 경우
		if(bIsExistsRes && a_bIsAutoUnload) {
			Resources.UnloadAsset(oRes);
		}

		return bIsExistsRes;
	}

	/** 컴포넌트를 순회한다 */
	public static void EnumerateComponents<T>(System.Func<T, bool> a_oCallback, 
		bool a_bIsIncludeInactive = false, bool a_bIsAssert = true) where T : Component {

		CAccess.Assert(!a_bIsAssert || a_oCallback != null);

		// 컴포넌트 순회가 불가능 할 경우
		if(a_oCallback == null) {
			return;
		}

		CAccess.EnumerateScenes((a_stScene) => {
			bool bIsTrue = true;

			a_stScene.ExEnumerateComponents<T>((a_oComponent) => {
				return bIsTrue = a_oCallback(a_oComponent);
			}, a_bIsIncludeInactive, a_bIsAssert);

			return bIsTrue;
		}, a_bIsAssert);
	}
	#endregion // 제네릭 클래스 함수

	#region 조건부 클래스 함수
#if UNITY_EDITOR
	/** 스크립트 순서를 변경한다 */
	public static void SetScriptOrder(MonoScript a_oScript, int a_nOrder, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oScript != null);

		// 스크립트가 존재 할 경우
		if(a_oScript != null && MonoImporter.GetExecutionOrder(a_oScript) != a_nOrder) {
			MonoImporter.SetExecutionOrder(a_oScript, a_nOrder);
		}
	}
#endif // #if UNITY_EDITOR

#if ADS_MODULE_ENABLE && ADMOB_ADS_ENABLE
	/** 값을 할당한다 */
	public static void AssignVal(ref BannerView a_rLhs, BannerView a_oRhs, BannerView a_oDefVal = null) {
		a_rLhs?.Destroy();
		a_rLhs = a_oRhs ?? a_oDefVal;
	}

	/** 값을 할당한다 */
	public static void AssignVal(ref RewardedAd a_rLhs, RewardedAd a_oRhs, RewardedAd a_oDefVal = null) {
		a_rLhs?.Destroy();
		a_rLhs = a_oRhs ?? a_oDefVal;
	}

	/** 값을 할당한다 */
	public static void AssignVal(ref InterstitialAd a_rLhs, InterstitialAd a_oRhs, InterstitialAd a_oDefVal = null) {
		a_rLhs?.Destroy();
		a_rLhs = a_oRhs ?? a_oDefVal;
	}
#endif // #if ADS_MODULE_ENABLE && ADMOB_ADS_ENABLE

#if PURCHASE_MODULE_ENABLE
	/** 가격 문자열을 반환한다 */
	public static string GetPriceStr(Product a_oProduct) {
		CAccess.Assert(a_oProduct != null);
		
		return string.Format(KCDefine.B_TEXT_FMT_2_SPACE_COMBINE, 
			a_oProduct.metadata.isoCurrencyCode, a_oProduct.metadata.localizedPrice);
	}
#endif // #if PURCHASE_MODULE_ENABLE
	#endregion // 조건부 클래스 함수
}

/** 유틸리티 접근자 - 코루틴 */
public static partial class CAccess {
	/** 실수 비교자 */
	private class CRealComparer : IEqualityComparer<float> {
		#region 함수
		/** 동일 여부를 검사한다 */
		public bool Equals(float a_fLhs, float a_fRhs) {
			return a_fLhs.ExIsEquals(a_fRhs);
		}

		/** 해시 코드를 반환한다 */
		public int GetHashCode(float a_fVal) {
			return a_fVal.GetHashCode();
		}
		#endregion // 함수
	}

	#region 클래스 변수
	private static CRealComparer m_oRealComparer = new CRealComparer();
	private static WaitForEndOfFrame m_oWaitForEndOfFrame = new WaitForEndOfFrame();
	private static WaitForFixedUpdate m_oWaitForFixedUpdate = new WaitForFixedUpdate();

	private static Dictionary<float, WaitForSeconds> m_oWaitForSecsDict = new Dictionary<float, WaitForSeconds>(CAccess.m_oRealComparer);
	private static Dictionary<float, WaitForSecondsRealtime> m_oWaitForSecsRealtimeDict = new Dictionary<float, WaitForSecondsRealtime>(CAccess.m_oRealComparer);
	#endregion // 클래스 변수

	#region 클래스 함수
	/** 대기 객체를 리셋한다 */
	public static void ResetWaitForSecs() {
		CAccess.m_oWaitForSecsDict.Clear();
		CAccess.m_oWaitForSecsRealtimeDict.Clear();
	}

	/** 대기 객체를 반환한다 */
	public static IEnumerator CoGetWaitForSecs(float a_fDeltaTime, bool a_bIsRealtime = false) {
		CAccess.Assert(a_fDeltaTime.ExIsGreatEquals(KCDefine.B_VAL_0_REAL));

		yield return a_bIsRealtime ? 
			CAccess.GetWaitForSecondsRealtime(a_fDeltaTime) : CAccess.GetWaitForSeconds(a_fDeltaTime);
	}

	/** 대기 객체를 반환한다 */
	public static IEnumerator CoGetWaitForFixedUpdate() {
		yield return CAccess.m_oWaitForFixedUpdate;
	}

	/** 대기 객체를 반환한다 */
	public static IEnumerator CoGetWaitForEndOfFrame() {
		yield return CAccess.m_oWaitForEndOfFrame;
	}
	#endregion // 클래스 함수
}
