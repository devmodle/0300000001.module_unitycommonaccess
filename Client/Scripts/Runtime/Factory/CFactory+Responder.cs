using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/** 응답자 팩토리 */
public static partial class CFactory {
	#region 클래스 함수
	/** 터치 응답자를 생성한다 */
	public static GameObject CreateTouchResponder(string a_oName, 
		GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stSize, Vector3 a_stPos, Color a_stColor) {

		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());

		var oGameObj = CFactory.CreateCloneGameObj(a_oName, a_oOrigin, a_oParent);
		oGameObj.GetComponentInChildren<Image>().color = a_stColor;

		// 2 차원 트랜스 폼이 존재 할 경우
		if(oGameObj.transform as RectTransform != null) {
			(oGameObj.transform as RectTransform).pivot = KCDefine.B_ANCHOR_MID_CENTER;
			(oGameObj.transform as RectTransform).anchorMin = KCDefine.B_ANCHOR_MID_CENTER;
			(oGameObj.transform as RectTransform).anchorMax = KCDefine.B_ANCHOR_MID_CENTER;
			
			(oGameObj.transform as RectTransform).sizeDelta = a_stSize;
			(oGameObj.transform as RectTransform).anchoredPosition = a_stPos;
		}

		return oGameObj;
	}

	/** 터치 응답자를 생성한다 */
	public static GameObject CreateTouchResponder(string a_oName, 
		string a_oObjPath, GameObject a_oParent, Vector3 a_stSize, Vector3 a_stPos, Color a_stColor) {

		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());

		return CFactory.CreateTouchResponder(a_oName, 
			Resources.Load<GameObject>(a_oObjPath), a_oParent, a_stSize, a_stPos, a_stColor);
	}

	/** 드래그 응답자를 생성한다 */
	public static GameObject CreateDragResponder(string a_oName, 
		GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stSize, Vector3 a_stPos, Color a_stColor) {

		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CFactory.CreateTouchResponder(a_oName, a_oOrigin, a_oParent, a_stSize, a_stPos, a_stColor);
	}	

	/** 드래그 응답자를 생성한다 */
	public static GameObject CreateDragResponder(string a_oName, 
		string a_oObjPath, GameObject a_oParent, Vector3 a_stSize, Vector3 a_stPos, Color a_stColor) {

		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());

		return CFactory.CreateDragResponder(a_oName, 
			Resources.Load<GameObject>(a_oObjPath), a_oParent, a_stSize, a_stPos, a_stColor);
	}
	#endregion // 클래스 함수

	#region 제네릭 클래스 함수
	/** 터치 응답자를 생성한다 */
	public static T CreateTouchResponder<T>(string a_oName, 
		GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stSize, Vector3 a_stPos, Color a_stColor) where T : Component {

		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		var oGameObj = CFactory.CreateTouchResponder(a_oName, a_oOrigin, a_oParent, a_stSize, a_stPos, a_stColor);

		return oGameObj?.GetComponentInChildren<T>();
	}

	/** 터치 응답자를 생성한다 */
	public static T CreateTouchResponder<T>(string a_oName, 
		string a_oObjPath, GameObject a_oParent, Vector3 a_stSize, Vector3 a_stPos, Color a_stColor) where T : Component {

		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());

		return CFactory.CreateTouchResponder<T>(a_oName, 
			Resources.Load<GameObject>(a_oObjPath), a_oParent, a_stSize, a_stPos, a_stColor);
	}

	/** 드래그 응답자를 생성한다 */
	public static T CreateDragResponder<T>(string a_oName, 
		GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stSize, Vector3 a_stPos, Color a_stColor) where T : Component {

		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		var oGameObj = CFactory.CreateDragResponder(a_oName, a_oOrigin, a_oParent, a_stSize, a_stPos, a_stColor);
		
		return oGameObj?.GetComponentInChildren<T>();
	}

	/** 드래그 응답자를 생성한다 */
	public static T CreateDragResponder<T>(string a_oName, 
		string a_oObjPath, GameObject a_oParent, Vector3 a_stSize, Vector3 a_stPos, Color a_stColor) where T : Component {

		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());

		return CFactory.CreateDragResponder<T>(a_oName, 
			Resources.Load<GameObject>(a_oObjPath), a_oParent, a_stSize, a_stPos, a_stColor);
	}
	#endregion // 제네릭 클래스 함수
}
