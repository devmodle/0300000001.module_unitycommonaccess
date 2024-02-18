using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/** 색상 팩토리 */
public static partial class CFactory {
	#region 클래스 함수
	/** 그래디언트를 생성한다 */
	public static Gradient MakeGradient(Color a_stColor) {
		return new Gradient() {
			colorKeys = new GradientColorKey[] {
				new GradientColorKey(a_stColor, KCDefine.B_VAL_0_REAL), 
				new GradientColorKey(a_stColor, KCDefine.B_VAL_1_REAL)
			},

			alphaKeys = new GradientAlphaKey[] {
				new GradientAlphaKey(a_stColor.a, KCDefine.B_VAL_0_REAL), 
				new GradientAlphaKey(a_stColor.a, KCDefine.B_VAL_1_REAL)
			}
		};
	}
	#endregion // 클래스 함수
}
