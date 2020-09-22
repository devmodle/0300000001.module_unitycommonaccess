using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

#if UNITY_EDITOR
using UnityEditor;

//! 에디터 기본 접근
public static partial class CEditorAccess {
	#region 클래스 함수
	//! 상태 갱신 가능 여부를 검사한다
	public static bool IsEnableUpdateState() {
		return !Application.isPlaying && !EditorApplication.isCompiling && !BuildPipeline.isBuildingPlayer;
	}

	//! 기즈모 그리기 가능 여부를 검사한다
	public static bool IsEnableDrawGizmos() {
		return !EditorApplication.isCompiling && !BuildPipeline.isBuildingPlayer;
	}
	
	//! 활성된 객체를 반환한다
	public static GameObject GetActiveObj(bool a_bIsInHierarchy = true) {
		var oObj = Selection.activeGameObject;

		return (oObj == null || (a_bIsInHierarchy && !oObj.activeInHierarchy)) ? 
			null : Selection.activeGameObject;
	}

	//! 독립 플랫폼 이름을 반환한다
	public static string GetStandalonePlatformName(EStandalonePlatformType a_ePlatformType) {
		return (a_ePlatformType == EStandalonePlatformType.WINDOWS) ?
			KCDefine.B_PLATFORM_NAME_WINDOWS : KCDefine.B_PLATFORM_NAME_MAC;
	}

	//! 안드로이드 이름을 반환한다
	public static string GetAndroidPlatformName(EAndroidPlatformType a_ePlatformType) {
		// 원 스토어 플랫폼 일 경우
		if(a_ePlatformType == EAndroidPlatformType.ONE_STORE) {
			return KCDefine.B_PLATFORM_NAME_ONE_STORE;
		}

		return (a_ePlatformType == EAndroidPlatformType.GALAXY_STORE) ? 
			KCDefine.B_PLATFORM_NAME_GALAXY_STORE : KCDefine.B_PLATFORM_NAME_GOOGLE;
	}

	//! 그래픽 API 를 변경한다
	public static void SetGraphicAPI(BuildTarget a_eTarget, GraphicsDeviceType[] a_oDeviceTypes, bool a_bIsEnableAuto = true) {
		PlayerSettings.SetGraphicsAPIs(a_eTarget, a_oDeviceTypes);
		PlayerSettings.SetUseDefaultGraphicsAPIs(a_eTarget, a_bIsEnableAuto);
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
