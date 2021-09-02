using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif			// #if UNITY_IOS

//! 에디터 기본 접근 확장 클래스
public static partial class CEditorAccessExtension {
	#region 클래스 함수
	//! 유효 여부를 검사한다
	public static bool ExIsValid(this EBuildType a_eSender) {
		return a_eSender > EBuildType.NONE && a_eSender < EBuildType.MAX_VAL;
	}

	//! 유효 여부를 검사한다
	public static bool ExIsValid(this BuildTarget a_eSender) {
		bool bIsMobile = a_eSender == BuildTarget.iOS || a_eSender == BuildTarget.Android;
		bool bIsStandalone = a_eSender == BuildTarget.StandaloneOSX || a_eSender == BuildTarget.StandaloneWindows || a_eSender == BuildTarget.StandaloneWindows64;
		
		return bIsMobile || bIsStandalone;
	}

	//! 완료 여부를 검사한다
	public static bool ExIsComplete(this Request a_oSender) {
		// 요청이 유효하지 않을 경우
		if(a_oSender == null) {
			return false;
		}

		return a_oSender.IsCompleted && a_oSender.Status == StatusCode.Success;
	}

	
	#endregion			// 클래스 함수

	#region 조건부 클래스 함수
#if UNITY_IOS
	//! 유효 여부를 검사한다
	public static bool ExIsValid(this PlistDocument a_oSender) {
		return a_oSender != null && a_oSender.root != null;
	}

	//! 포함 여부를 검사한다
	public static bool ExIsContainsAdsNetworkID(this PlistElementArray a_oSender, string a_oNetworkID) {
		CAccess.Assert(a_oSender != null && a_oNetworkID.ExIsValid());

		for(int i = 0; i < a_oSender.values.Count; ++i) {
			var oAdsNetworkIDInfo = a_oSender.values[i].AsDict();
			var oAdsNetworkIDs = oAdsNetworkIDInfo.values;

			// 광고 네트워크 식별자가 존재 할 경우
			if(oAdsNetworkIDs.TryGetValue(KCEditorDefine.B_KEY_IOS_ADS_NETWORK_ID, out PlistElement oVal) && oVal.AsString().Equals(a_oNetworkID)) {
				return true;
			}
		}

		return false;
	}

	//! 배열을 반환한다
	public static PlistElementArray ExGetArray(this PlistDocument a_oSender, string a_oKey) {
		CAccess.Assert(a_oSender.ExIsValid());

		try {
			return a_oSender.root[a_oKey].AsArray();
		} catch {
			return a_oSender.root.CreateArray(a_oKey);
		}
	}

	//! 딕셔너리를 반환한다
	public static PlistElementDict ExGetDict(this PlistDocument a_oSender, string a_oKey) {
		CAccess.Assert(a_oSender.ExIsValid());

		try {
			return a_oSender.root[a_oKey].AsDict();
		} catch {
			return a_oSender.root.CreateDict(a_oKey);
		}
	}
#endif			// #if UNITY_IOS
	#endregion			// 조건부 클래스 함수
}
#endif			// #if UNITY_EDITOR
