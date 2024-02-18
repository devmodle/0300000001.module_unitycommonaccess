using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using UnityEngine.SceneManagement;

/** 확장 클래스 - 메세지 */
public static partial class CExtension {
	#region 클래스 함수
	/** 메세지를 전송한다 */
	public static void ExSendMsg(this Scene a_stSender, 
		string a_oName, string a_oFuncName, object a_oParams = null, bool a_bIsAssert = true) {

		CAccess.Assert(!a_bIsAssert || (a_oName.ExIsValid() && a_oFuncName.ExIsValid()));

		// 메세지 전송이 불가능 할 경우
		if(!a_oName.ExIsValid() || !a_oFuncName.ExIsValid()) {
			return;
		}

		var oGameObj = a_stSender.ExFindChild(a_oName);
		oGameObj?.ExSendMsg(string.Empty, a_oFuncName, a_oParams, a_bIsAssert);
	}

	/** 메세지를 전송한다 */
	public static void ExSendMsg(this GameObject a_oSender, 
		string a_oName, string a_oFuncName, object a_oParams = null, bool a_bIsAssert = true) {

		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oFuncName.ExIsValid()));

		// 메세지 전송이 불가능 할 경우
		if(a_oSender == null || !a_oFuncName.ExIsValid()) {
			return;
		}

		// 이름이 존재 할 경우
		if(a_oName.ExIsValid()) {
			var oGameObj = a_oSender.ExFindChild(a_oName);
			oGameObj?.ExSendMsg(string.Empty, a_oFuncName, a_oParams, a_bIsAssert);
		} else {
			a_oSender.SendMessage(a_oFuncName, a_oParams, SendMessageOptions.DontRequireReceiver);
		}
	}

	/** 메세지를 전송한다 */
	public static void ExBroadcastMsg(this Scene a_stSender, 
		string a_oFuncName, object a_oParams = null, bool a_bIsAssert = true) {

		CAccess.Assert(!a_bIsAssert || a_oFuncName.ExIsValid());

		// 메세지 전파가 불가능 할 경우
		if(!a_oFuncName.ExIsValid()) {
			return;
		}

		var oObjs = a_stSender.GetRootGameObjects();

		for(int i = 0; i < oObjs.Length; ++i) {
			oObjs[i].ExBroadcastMsg(a_oFuncName, a_oParams, a_bIsAssert);
		}
	}

	/** 메세지를 전송한다 */
	public static void ExBroadcastMsg(this GameObject a_oSender, 
		string a_oFuncName, object a_oParams = null, bool a_bIsAssert = true) {

		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oFuncName.ExIsValid()));

		// 메세지 전파가 불가능 할 경우
		if(a_oSender == null || !a_oFuncName.ExIsValid()) {
			return;
		}

		a_oSender.BroadcastMessage(a_oFuncName, a_oParams, SendMessageOptions.DontRequireReceiver);
	}
	#endregion // 클래스 함수
}
