using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/** 파이어 베이스 팩토리 */
public static partial class CFactory {
	#region 클래스 함수
#if FIREBASE_MODULE_ENABLE
	/** 유저 정보 노드를 생성한다 */
	public static List<string> MakeUserInfoNodes() {
		return new List<string>() {
			KCDefine.B_NODE_FIREBASE_USER_INFOS
		};
	}

	/** 타겟 정보 노드를 생성한다 */
	public static List<string> MakeTargetInfoNodes() {
		return new List<string>() {
			KCDefine.B_NODE_FIREBASE_TARGET_INFOS
		};
	}

	/** 결제 정보 노드를 생성한다 */
	public static List<string> MakePurchaseInfoNodes() {
		return new List<string>() {
			KCDefine.B_NODE_FIREBASE_PURCHASE_INFOS
		};
	}
#endif // #if FIREBASE_MODULE_ENABLE
	#endregion // 클래스 함수
}
