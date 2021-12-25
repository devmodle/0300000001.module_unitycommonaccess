using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

#if UNITY_EDITOR
using UnityEditor;

/** 에디터 기본 접근 */
public static partial class CEditorAccess {
	#region 클래스 프로퍼티
	public static bool IsEnableUpdateState => !Application.isPlaying && !EditorApplication.isCompiling && !BuildPipeline.isBuildingPlayer;
	public static System.Type GameViewType => typeof(Editor).Assembly.GetType(KCEditorDefine.B_VIEW_N_GAME);
	#endregion			// 클래스 프로퍼티

	#region 클래스 함수
	/** 에셋 존재 여부를 검사한다 */
	public static bool IsExistsAsset(string a_oFilePath) {
		return AssetDatabase.GetMainAssetTypeAtPath(a_oFilePath) != null;
	}

	/** 활성 객체를 반환한다 */
	public static GameObject GetActiveObj(bool a_bIsInHierarchy = true) {
		var oObj = Selection.activeGameObject;
		return (a_bIsInHierarchy && (oObj != null && !oObj.activeInHierarchy)) ? null : oObj;
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
			case EAndroidType.ONE_STORE: return KCDefine.B_PLATFORM_N_ANDROID_ONE_STORE;
			default: return KCDefine.B_PLATFORM_N_ANDROID_GOOGLE;
		}
	}

	/** 독립 플랫폼 이름을 반환한다 */
	public static string GetStandaloneName(EStandaloneType a_eType) {
		switch(a_eType) {
			case EStandaloneType.MAC_STEAM: return KCDefine.B_PLATFORM_N_STANDALONE_MAC_STEAM;
			case EStandaloneType.WNDS_STEAM: return KCDefine.B_PLATFORM_N_STANDALONE_WNDS_STEAM;
			default: return KCDefine.B_PLATFORM_N_STANDALONE_MAC_APPLE;
		}
	}

	/** 그래픽 API 를 변경한다 */
	public static void SetGraphicAPI(BuildTarget a_eTarget, List<GraphicsDeviceType> a_oDeviceTypeList, bool a_bIsEnableAuto = true, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oDeviceTypeList.ExIsValid());

		// 디바이스 타입이 존재할 경우
		if(a_oDeviceTypeList.ExIsValid()) {
			PlayerSettings.SetGraphicsAPIs(a_eTarget, a_oDeviceTypeList.ToArray());
			PlayerSettings.SetUseDefaultGraphicsAPIs(a_eTarget, a_bIsEnableAuto);
		}
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
