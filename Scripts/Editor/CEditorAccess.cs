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
	public static bool IsEnableUpdateState => !BuildPipeline.isBuildingPlayer && !EditorApplication.isCompiling && !EditorApplication.isUpdating && !EditorApplication.isPlayingOrWillChangePlaymode;
	public static System.Type GameViewType => typeof(Editor).Assembly.GetType(KCEditorDefine.B_VIEW_N_GAME);
	#endregion			// 클래스 프로퍼티

	#region 클래스 함수
	/** 에셋 존재 여부를 검사한다 */
	public static bool IsExistsAsset(string a_oFilePath) {
		return AssetDatabase.GetMainAssetTypeAtPath(a_oFilePath) != null;
	}

	/** 전처리기 심볼 포함 여부를 검사한다 */
	public static bool IsContainsDefineSymbol(BuildTargetGroup a_eTargetGroup, string a_oSymbol) {
		PlayerSettings.GetScriptingDefineSymbolsForGroup(a_eTargetGroup, out string[] oDefineSymbols);
		return oDefineSymbols.ExIsValid() && oDefineSymbols.ExIsContains(a_oSymbol);
	}

	/** 활성 객체를 반환한다 */
	public static GameObject GetActiveObj(bool a_bIsInHierarchy = true) {
		var oObj = Selection.activeGameObject;
		return (a_bIsInHierarchy && (oObj != null && !oObj.activeInHierarchy)) ? null : oObj;
	}

	/** iOS 프로젝트 이름을 반환한다 */
	public static string GetiOSProjName(EiOSType a_eType) {
		switch(a_eType) {
			default: return KCEditorDefine.B_IOS_APPLE_BUILD_PROJ_N_JENKINS;
		}
	}

	/** iOS 빌드 결과 경로를 반환한다 */
	public static string GetiOSBuildOutputPath(EiOSType a_eType, string a_oBuildFileExtension) {
		return string.Format(KCEditorDefine.B_BUILD_OUTPUT_P_FMT_IOS, CAccess.GetiOSName(a_eType), a_oBuildFileExtension);
	}

	/** 안드로이드 프로젝트 이름을 반환한다 */
	public static string GetAndroidProjName(EAndroidType a_eType) {
		switch(a_eType) {
			case EAndroidType.AMAZON: return KCEditorDefine.B_ANDROID_AMAZON_BUILD_PROJ_N_JENKINS;
			default: return KCEditorDefine.B_ANDROID_GOOGLE_BUILD_PROJ_N_JENKINS;
		}
	}

	/** 안드로이드 빌드 결과 경로를 반환한다 */
	public static string GetAndroidBuildOutputPath(EAndroidType a_eType, string a_oBuildFileExtension) {
		return string.Format(KCEditorDefine.B_BUILD_OUTPUT_P_FMT_ANDROID, CAccess.GetAndroidName(a_eType), a_oBuildFileExtension);
	}
	
	/** 독립 플랫폼 프로젝트 이름을 반환한다 */
	public static string GetStandaloneProjName(EStandaloneType a_eType) {
		switch(a_eType) {
			case EStandaloneType.WNDS_STEAM: return KCEditorDefine.B_STANDALONE_WNDS_STEAM_BUILD_PROJ_N_JENKINS;
			default: return KCEditorDefine.B_STANDALONE_MAC_STEAM_BUILD_PROJ_N_JENKINS;
		}
	}

	/** 독립 플랫폼 빌드 결과 경로를 반환한다 */
	public static string GetStandaloneBuildOutputPath(EStandaloneType a_eType, string a_oBuildFileExtension) {
		return string.Format(KCEditorDefine.B_BUILD_OUTPUT_P_FMT_STANDALONE, CAccess.GetStandaloneName(a_eType), a_oBuildFileExtension);
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
