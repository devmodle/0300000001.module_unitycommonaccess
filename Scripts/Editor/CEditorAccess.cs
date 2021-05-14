using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

#if UNITY_EDITOR
using UnityEditor;

//! 에디터 기본 접근
public static partial class CEditorAccess {
	#region 클래스 프로퍼티
	public static bool IsEnableDrawGizmos => !EditorApplication.isCompiling && !BuildPipeline.isBuildingPlayer;
	public static bool IsEnableUpdateState => !Application.isPlaying && !EditorApplication.isCompiling && !BuildPipeline.isBuildingPlayer;
	#endregion			// 클래스 프로퍼티

	#region 클래스 함수
	//! 활성 객체를 반환한다
	public static GameObject GetActiveObj(bool a_bIsInHierarchy = true) {
		var oObj = Selection.activeGameObject;
		bool bIsInvalidObj = a_bIsInHierarchy && (oObj != null && !oObj.activeInHierarchy);

		return bIsInvalidObj ? null : oObj;
	}

	//! 독립 플랫폼 이름을 반환한다
	public static string GetStandaloneName(EStandaloneType a_eType) {
		return (a_eType == EStandaloneType.WNDS) ? KCDefine.B_PLATFORM_N_WNDS : KCDefine.B_PLATFORM_N_MAC;
	}
	
	//! 안드로이드 이름을 반환한다
	public static string GetAndroidName(EAndroidType a_eType) {
		// 원 스토어 일 경우
		if(a_eType == EAndroidType.ONE_STORE) {
			return KCDefine.B_PLATFORM_N_ONE_STORE;
		}

		return (a_eType == EAndroidType.GALAXY_STORE) ? KCDefine.B_PLATFORM_N_GALAXY_STORE : KCDefine.B_PLATFORM_N_GOOGLE;
	}

	//! 그래픽 API 를 변경한다
	public static void SetGraphicAPI(BuildTarget a_eTarget, GraphicsDeviceType[] a_oDeviceTypes, bool a_bIsEnableAuto = true) {
		CAccess.Assert(a_oDeviceTypes.ExIsValid());

		PlayerSettings.SetGraphicsAPIs(a_eTarget, a_oDeviceTypes);
		PlayerSettings.SetUseDefaultGraphicsAPIs(a_eTarget, a_bIsEnableAuto);
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
