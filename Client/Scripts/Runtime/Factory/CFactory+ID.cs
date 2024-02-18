using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/** 식별자 팩토리 */
public static partial class CFactory {
	#region 클래스 함수
	/** 고유 챕터 식별자를 생성한다 */
	public static ulong MakeUChapterID(int a_nChapterID) {
		return (ulong)a_nChapterID * KCDefine.B_UNIT_IDS_PER_IDS_03;
	}

	/** 고유 스테이지 식별자를 생성한다 */
	public static ulong MakeUStageID(int a_nStageID, int a_nChapterID = KCDefine.B_VAL_0_INT) {
		return CFactory.MakeUChapterID(a_nChapterID) + ((ulong)a_nStageID * KCDefine.B_UNIT_IDS_PER_IDS_02);
	}

	/** 고유 레벨 식별자를 생성한다 */
	public static ulong MakeULevelID(int a_nLevelID, int a_nStageID = KCDefine.B_VAL_0_INT, int a_nChapterID = KCDefine.B_VAL_0_INT) {
		return CFactory.MakeUStageID(a_nStageID, a_nChapterID) + (ulong)a_nLevelID;
	}
	#endregion // 클래스 함수
}
