using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/** 게임 객체 팩토리 */
public static partial class CFactory {
	#region 클래스 함수
	/** 게임 객체를 생선한다 */
	public static GameObject CreateGameObj(string a_oName, 
		GameObject a_oParent, Vector3 a_stScale, Vector3 a_stAngle, Vector3 a_stPos, bool a_bIsSetupWorldState = false) {

		CAccess.Assert(a_oName.ExIsValid());

		var oGameObj = new GameObject(a_oName);
		oGameObj.ExSetParent(a_oParent);
		oGameObj.transform.localScale = a_stScale;

		// 월드 상태 설정 모드 일 경우
		if(a_bIsSetupWorldState) {
			oGameObj.transform.position = a_stPos;
			oGameObj.transform.eulerAngles = a_stAngle;
		} else {
			oGameObj.transform.localPosition = a_stPos;
			oGameObj.transform.localEulerAngles = a_stAngle;
		}

		return oGameObj;
	}

	/** 게임 객체를 생선한다 */
	public static GameObject CreateGameObj(string a_oName, 
		GameObject a_oParent, Vector3 a_stPos, bool a_bIsSetupWorldState = false) {

		CAccess.Assert(a_oName.ExIsValid());
		return CFactory.CreateGameObj(a_oName, a_oParent, Vector3.one, Vector3.zero, a_stPos, a_bIsSetupWorldState);
	}
	
	/** 게임 객체를 생선한다 */
	public static GameObject CreateGameObj(string a_oName, 
		GameObject a_oParent, bool a_bIsSetupWorldState = false) {

		CAccess.Assert(a_oName.ExIsValid());
		return CFactory.CreateGameObj(a_oName, a_oParent, Vector3.zero, a_bIsSetupWorldState);
	}

	/** 사본 게임 객체를 생성한다 */
	public static GameObject CreateCloneGameObj(string a_oName, 
		GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stScale, Vector3 a_stAngle, Vector3 a_stPos, bool a_bIsSetupWorldState = false) {

		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());

		var oGameObj = GameObject.Instantiate(a_oOrigin, a_oParent?.transform);
		oGameObj.name = a_oName;
		oGameObj.transform.localScale = a_stScale;

		// 월드 상태 설정 모드 일 경우
		if(a_bIsSetupWorldState) {
			oGameObj.transform.position = a_stPos;
			oGameObj.transform.eulerAngles = a_stAngle;
		} else {
			oGameObj.transform.localPosition = a_stPos;
			oGameObj.transform.localEulerAngles = a_stAngle;
		}

		return oGameObj;
	}

	/** 사본 게임 객체를 생성한다 */
	public static GameObject CreateCloneGameObj(string a_oName, 
		GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stPos, bool a_bIsSetupWorldState = false) {

		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());

		return CFactory.CreateCloneGameObj(a_oName, 
			a_oOrigin, a_oParent, Vector3.one, Vector3.zero, a_stPos, a_bIsSetupWorldState);
	}

	/** 사본 게임 객체를 생성한다 */
	public static GameObject CreateCloneGameObj(string a_oName, 
		GameObject a_oOrigin, GameObject a_oParent, bool a_bIsSetupWorldState = false) {

		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CFactory.CreateCloneGameObj(a_oName, a_oOrigin, a_oParent, Vector3.zero, a_bIsSetupWorldState);
	}

	/** 사본 게임 객체를 생성한다 */
	public static GameObject CreateCloneGameObj(string a_oName, 
		string a_oObjPath, GameObject a_oParent, Vector3 a_stScale, Vector3 a_stAngle, Vector3 a_stPos, bool a_bIsSetupWorldState = false) {

		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());

		return CFactory.CreateCloneGameObj(a_oName, 
			Resources.Load<GameObject>(a_oObjPath), a_oParent, a_stScale, a_stAngle, a_stPos, a_bIsSetupWorldState);
	}

	/** 사본 게임 객체를 생성한다 */
	public static GameObject CreateCloneGameObj(string a_oName, 
		string a_oObjPath, GameObject a_oParent, Vector3 a_stPos, bool a_bIsSetupWorldState = false) {

		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());

		return CFactory.CreateCloneGameObj(a_oName, 
			a_oObjPath, a_oParent, Vector3.one, Vector3.zero, a_stPos, a_bIsSetupWorldState);
	}

	/** 사본 게임 객체를 생성한다 */
	public static GameObject CreateCloneGameObj(string a_oName, 
		string a_oObjPath, GameObject a_oParent, bool a_bIsSetupWorldState = false) {

		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CFactory.CreateCloneGameObj(a_oName, a_oObjPath, a_oParent, Vector3.zero, a_bIsSetupWorldState);
	}

	/** 객체를 제거한다 */
	public static void RemoveObj(Object a_oObj, bool a_bIsRemoveAsset = false, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oObj != null);

		// 객체가 존재 할 경우
		if(a_oObj != null) {
			// 앱이 실행 중 일 경우
			if(Application.isPlaying) {
				GameObject.Destroy(a_oObj);
			} else {
				GameObject.DestroyImmediate(a_oObj, a_bIsRemoveAsset);
			}
		}
	}
	#endregion // 클래스 함수

	#region 제네릭 클래스 함수
	/** 게임 객체를 생선한다 */
	public static T CreateGameObj<T>(string a_oName, 
		GameObject a_oParent, Vector3 a_stScale, Vector3 a_stAngle, Vector3 a_stPos, bool a_bIsSetupWorldState = false) where T : Component {
			
		CAccess.Assert(a_oName.ExIsValid());

		return CFactory.CreateGameObj(a_oName, 
			a_oParent, a_stScale, a_stAngle, a_stPos, a_bIsSetupWorldState).ExAddComponent<T>();
	}

	/** 게임 객체를 생선한다 */
	public static T CreateGameObj<T>(string a_oName, 
		GameObject a_oParent, Vector3 a_stPos, bool a_bIsSetupWorldState = false) where T : Component {

		CAccess.Assert(a_oName.ExIsValid());
		return CFactory.CreateGameObj<T>(a_oName, a_oParent, Vector3.one, Vector3.zero, a_stPos, a_bIsSetupWorldState);
	}

	/** 게임 객체를 생선한다 */
	public static T CreateGameObj<T>(string a_oName, 
		GameObject a_oParent, bool a_bIsSetupWorldState = false) where T : Component {

		CAccess.Assert(a_oName.ExIsValid());
		return CFactory.CreateGameObj<T>(a_oName, a_oParent, Vector3.zero, a_bIsSetupWorldState);
	}

	/** 사본 게임 객체를 생성한다 */
	public static T CreateCloneGameObj<T>(string a_oName, 
		GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stScale, Vector3 a_stAngle, Vector3 a_stPos, bool a_bIsSetupWorldState = false) where T : Component {

		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());

		return CFactory.CreateCloneGameObj(a_oName, 
			a_oOrigin, a_oParent, a_stScale, a_stAngle, a_stPos, a_bIsSetupWorldState)?.GetComponentInChildren<T>();
	}

	/** 사본 게임 객체를 생성한다 */
	public static T CreateCloneGameObj<T>(string a_oName, 
		GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stPos, bool a_bIsSetupWorldState = false) where T : Component {

		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());

		return CFactory.CreateCloneGameObj<T>(a_oName, 
			a_oOrigin, a_oParent, Vector3.one, Vector3.zero, a_stPos, a_bIsSetupWorldState);
	}

	/** 사본 게임 객체를 생성한다 */
	public static T CreateCloneGameObj<T>(string a_oName, 
		GameObject a_oOrigin, GameObject a_oParent, bool a_bIsSetupWorldState = false) where T : Component {

		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CFactory.CreateCloneGameObj<T>(a_oName, a_oOrigin, a_oParent, Vector3.zero, a_bIsSetupWorldState);
	}

	/** 사본 게임 객체를 생성한다 */
	public static T CreateCloneGameObj<T>(string a_oName, 
		string a_oObjPath, GameObject a_oParent, Vector3 a_stScale, Vector3 a_stAngle, Vector3 a_stPos, bool a_bIsSetupWorldState = false) where T : Component {

		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());

		return CFactory.CreateCloneGameObj<T>(a_oName, 
			Resources.Load<GameObject>(a_oObjPath), a_oParent, a_stScale, a_stAngle, a_stPos, a_bIsSetupWorldState);
	}

	/** 사본 게임 객체를 생성한다 */
	public static T CreateCloneGameObj<T>(string a_oName, 
		string a_oObjPath, GameObject a_oParent, Vector3 a_stPos, bool a_bIsSetupWorldState = false) where T : Component {

		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());

		return CFactory.CreateCloneGameObj<T>(a_oName, 
			a_oObjPath, a_oParent, Vector3.one, Vector3.zero, a_stPos, a_bIsSetupWorldState);
	}

	/** 사본 게임 객체를 생성한다 */
	public static T CreateCloneGameObj<T>(string a_oName, 
		string a_oObjPath, GameObject a_oParent, bool a_bIsSetupWorldState = false) where T : Component {

		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CFactory.CreateCloneGameObj<T>(a_oName, a_oObjPath, a_oParent, Vector3.zero, a_bIsSetupWorldState);
	}
	#endregion // 제네릭 클래스 함수
}
