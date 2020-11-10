using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
			null : oObj;
	}

	//! 독립 플랫폼 이름을 반환한다
	public static string GetStandaloneName(EStandaloneType a_eType) {
		return (a_eType == EStandaloneType.WINDOWS) ?
			KCDefine.B_PLATFORM_WINDOWS : KCDefine.B_PLATFORM_MAC;
	}

	//! 안드로이드 이름을 반환한다
	public static string GetAndroidName(EAndroidType a_eType) {
		// 원 스토어 일 경우
		if(a_eType == EAndroidType.ONE_STORE) {
			return KCDefine.B_PLATFORM_ONE_STORE;
		}

		return (a_eType == EAndroidType.GALAXY_STORE) ? 
			KCDefine.B_PLATFORM_GALAXY_STORE : KCDefine.B_PLATFORM_GOOGLE;
	}

	//! 그래픽 API 를 변경한다
	public static void SetGraphicAPI(BuildTarget a_eTarget, GraphicsDeviceType[] a_oDeviceTypes, bool a_bIsEnableAuto = true) {
		PlayerSettings.SetGraphicsAPIs(a_eTarget, a_oDeviceTypes);
		PlayerSettings.SetUseDefaultGraphicsAPIs(a_eTarget, a_bIsEnableAuto);
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
