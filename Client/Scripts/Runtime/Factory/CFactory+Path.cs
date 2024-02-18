using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/** 경로 팩토리 */
public static partial class CFactory {
	#region 클래스 함수
	/** 지역화 경로를 생성한다 */
	public static string MakeLocalizePath(string a_oBasePath, string a_oLanguage) {
		CAccess.Assert(a_oBasePath.ExIsValid() && a_oLanguage.ExIsValid());
		return a_oBasePath.ExGetReplaceFileNamePath(string.Format(KCDefine.B_TEXT_FMT_2_UNDER_SCORE_COMBINE, a_oBasePath.ExGetFileName(false), a_oLanguage));
	}

	/** 지역화 경로를 생성한다 */
	public static string MakeLocalizePath(string a_oBasePath, string a_oDefLocalizePath, string a_oCountryCode, string a_oLanguage) {
		CAccess.Assert(a_oBasePath.ExIsValid() && a_oDefLocalizePath.ExIsValid() && a_oCountryCode.ExIsValid());
		string oFilePath = a_oLanguage.ExIsValidLanguage() ? CFactory.MakeLocalizePath(a_oBasePath, a_oLanguage) : CFactory.MakeLocalizePath(a_oBasePath, a_oCountryCode);

		return CAccess.IsExistsRes<TextAsset>(oFilePath, true) ? oFilePath : a_oDefLocalizePath;
	}

	/** 경로 정보를 생성한다 */
	public static CPathInfo MakePathInfo(Vector3Int a_stIdx, int a_nCost = KCDefine.B_VAL_0_INT) {
		return new CPathInfo() {
			m_nCost = a_nCost, m_stIdx = a_stIdx, m_oPrevPathInfo = null
		};
	}
	#endregion // 클래스 함수
}
