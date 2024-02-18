using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using AOP;

/** 텍스처 팩토리 */
public static partial class CFactory {
	#region 클래스 함수
	/** 게임 객체 풀을 생성한다 */
	public static ObjectPool CreateGameObjsPool(GameObject a_oOrigin, 
		GameObject a_oParent, int a_nNumObjs = KCDefine.U_SIZE_GAME_OBJS_POOL_50) {

		CAccess.Assert(a_oOrigin != null);
		return new ObjectPool(a_oOrigin, a_oParent?.transform, a_nNumObjs);
	}

	/** 게임 객체 풀을 생성한다 */
	public static ObjectPool CreateGameObjsPool(string a_oObjPath, 
		GameObject a_oParent, int a_nNumObjs = KCDefine.U_SIZE_GAME_OBJS_POOL_50) {
			
		CAccess.Assert(a_oObjPath.ExIsValid());
		return CFactory.CreateGameObjsPool(Resources.Load<GameObject>(a_oObjPath), a_oParent, a_nNumObjs);
	}
	#endregion // 클래스 함수
}
