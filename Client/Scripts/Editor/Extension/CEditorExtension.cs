using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;

#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif // #if UNITY_IOS

/** 에디터 기본 접근 확장 */
public static partial class CEditorExtension {
	#region 클래스 함수
	/** 상태를 리셋한다 */
	public static void ExReset(this LightProbeGroup a_oSender, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 라인 효과가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.probePositions = new Vector3[] {
				new Vector3(-KCDefine.B_VAL_1_REAL, -KCDefine.B_VAL_1_REAL, -KCDefine.B_VAL_1_REAL),
				new Vector3(-KCDefine.B_VAL_1_REAL, KCDefine.B_VAL_1_REAL, -KCDefine.B_VAL_1_REAL),
				new Vector3(KCDefine.B_VAL_1_REAL, KCDefine.B_VAL_1_REAL, -KCDefine.B_VAL_1_REAL),
				new Vector3(KCDefine.B_VAL_1_REAL, -KCDefine.B_VAL_1_REAL, -KCDefine.B_VAL_1_REAL),

				new Vector3(-KCDefine.B_VAL_1_REAL, -KCDefine.B_VAL_1_REAL, KCDefine.B_VAL_1_REAL),
				new Vector3(-KCDefine.B_VAL_1_REAL, KCDefine.B_VAL_1_REAL, KCDefine.B_VAL_1_REAL),
				new Vector3(KCDefine.B_VAL_1_REAL, KCDefine.B_VAL_1_REAL, KCDefine.B_VAL_1_REAL),
				new Vector3(KCDefine.B_VAL_1_REAL, -KCDefine.B_VAL_1_REAL, KCDefine.B_VAL_1_REAL)
			};
		}
	}

	/** 직렬화 프로퍼티 값을 변경한다 */
	public static void ExSetPropertyVal(this SerializedObject a_oSender, 
		string a_oName, System.Action<SerializedProperty> a_oCallback, bool a_bIsAssert = true) {
			
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oName.ExIsValid()));

		// 객체가 존재 할 경우
		if(a_oSender != null && a_oName.ExIsValid()) {
			try {
				a_oCallback?.Invoke(a_oSender.FindProperty(a_oName));
			} finally {
				a_oSender.ApplyModifiedProperties();
				a_oSender.Update();
			}
		}
	}

#if UNITY_IOS
	/** 값을 추가한다 */
	public static void ExAddVal(this PlistElementArray a_oSender, string a_oStr) {
		CAccess.Assert(a_oSender != null && a_oStr.ExIsValid());

		// 값 추가가 가능 할 경우
		if(a_oSender != null && !a_oSender.ExIsContains(a_oStr)) {
			a_oSender.AddString(a_oStr);
		}
	}

	/** 값을 추가한다 */
	public static void ExAddVal(this PlistElementDict a_oSender, string a_oKey, bool a_bIsTrue) {
		CAccess.Assert(a_oSender != null);

		// 값 추가가 가능 할 경우
		if(a_oSender != null && !a_oSender.ExIsContainsKey(a_oKey)) {
			a_oSender.SetBoolean(a_oKey, a_bIsTrue);
		}
	}

	/** 값을 추가한다 */
	public static void ExAddVal(this PlistElementDict a_oSender, string a_oKey, string a_oStr) {
		CAccess.Assert(a_oSender != null && a_oStr.ExIsValid());

		// 값 추가가 가능 할 경우
		if(a_oSender != null && !a_oSender.ExIsContainsKey(a_oKey)) {
			a_oSender.SetString(a_oKey, a_oStr);
		}
	}

	/** 값을 제거한다 */
	public static void ExRemoveVal(this PlistElementDict a_oSender, string a_oKey) {
		CAccess.Assert(a_oSender != null && a_oKey.ExIsValid());

		// 값 제거가 가능 할 경우
		if(a_oSender != null && !a_oSender.values.ContainsKey(a_oKey)) {
			a_oSender.values.Remove(a_oKey);
		}
	}
#endif // #if UNITY_IOS
	#endregion // 클래스 함수
}
#endif // #if UNITY_EDITOR
