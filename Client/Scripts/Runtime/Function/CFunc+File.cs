using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System.IO;

/** 함수 - 파일 */
public static partial class CFunc {
	#region 클래스 함수
	/** 파일을 복사한다 */
	public static void CopyFile(string a_oSrcPath,
		string a_oDestPath, bool a_bIsOverwrite = true, bool a_bIsAssert = true) {

		CAccess.Assert(!a_bIsAssert || (a_oSrcPath.ExIsValid() && a_oDestPath.ExIsValid()));

		// 파일 복사가 불가능 할 경우
		if(!CFunc.IsEnableCopy(a_oSrcPath, a_oDestPath, a_bIsOverwrite)) {
			return;
		}

		var oBytes = CFunc.ReadBytes(a_oSrcPath, false);
		CFunc.WriteBytes(a_oDestPath, oBytes, false, null, a_bIsAssert);
	}

	/** 파일을 복사한다 */
	public static void CopyFile(string a_oSrcPath,
		string a_oDestPath, string a_oIgnoreToken, System.Text.Encoding a_oEncoding = null, bool a_bIsOverwrite = true, bool a_bIsAssert = true) {

		CAccess.Assert(!a_bIsAssert || (a_oSrcPath.ExIsValid() && a_oDestPath.ExIsValid()));

		// 파일 복사가 불가능 할 경우
		if(!CFunc.IsEnableCopy(a_oSrcPath, a_oDestPath, a_bIsOverwrite)) {
			return;
		}

		var oEncoding = a_oEncoding ?? System.Text.Encoding.Default;
		var oStrLines = CFunc.ReadStrLines(a_oSrcPath, oEncoding);
		var oStrBuilder = new System.Text.StringBuilder();

		for(int i = 0; i < oStrLines.Length; ++i) {
			// 문자열 추가가 불가능 할 경우
			if(!oStrLines[i].ExIsValid() || oStrLines[i].Contains(a_oIgnoreToken)) {
				continue;
			}

			oStrBuilder.AppendLine(oStrLines[i]);
		}

		CFunc.WriteStr(a_oDestPath, oStrBuilder.ToString(), false, oEncoding, a_bIsAssert);
	}

	/** 파일을 복사한다 */
	public static void CopyFile(string a_oSrcPath,
		string a_oDestPath, string a_oTarget, string a_oReplace, System.Text.Encoding a_oEncoding = null, bool a_bIsOverwrite = true, bool a_bIsAssert = true) {

		CAccess.Assert(!a_bIsAssert || (a_oSrcPath.ExIsValid() && a_oDestPath.ExIsValid()));

		// 파일 복사가 불가능 할 경우
		if(!CFunc.IsEnableCopy(a_oSrcPath, a_oDestPath, a_bIsOverwrite)) {
			return;
		}

		var oEncoding = a_oEncoding ?? System.Text.Encoding.Default;
		var oStrLines = CFunc.ReadStrLines(a_oSrcPath, oEncoding);
		var oStrBuilder = new System.Text.StringBuilder();

		for(int i = 0; i < oStrLines.Length; ++i) {
			// 문자열이 유효 할 경우
			if(oStrLines[i] != null) {
				oStrBuilder.AppendLine(oStrLines[i].Replace(a_oTarget, a_oReplace));
			}
		}

		CFunc.WriteStr(a_oDestPath, oStrBuilder.ToString(), false, oEncoding, a_bIsAssert);
	}

	/** 디렉토리를 복사한다 */
	public static void CopyDir(string a_oSrcPath,
		string a_oDestPath, bool a_bIsOverwrite = true, bool a_bIsAssert = true) {

		CAccess.Assert(!a_bIsAssert || (a_oSrcPath.ExIsValid() && a_oDestPath.ExIsValid()));

		// 파일 복사가 불가능 할 경우
		if(!CFunc.IsEnableCopy(a_oSrcPath, a_oDestPath, a_bIsOverwrite)) {
			return;
		}

		CFactory.RemoveDir(a_oDestPath, a_bIsAssert: a_bIsAssert);

		CAccess.EnumerateDirectories(a_oSrcPath, (a_oDirPathList, a_oFilePathList) => {
			for(int i = 0; i < a_oFilePathList.Count; ++i) {
				string oDestFilePath = a_oFilePathList[i].Replace(a_oSrcPath, a_oDestPath);
				CFunc.CopyFile(a_oFilePathList[i], oDestFilePath, a_bIsOverwrite, a_bIsAssert);
			}

			return true;
		});
	}
	#endregion // 클래스 함수
}

/** 함수 - 파일 (Private) */
public static partial class CFunc {
	#region 클래스 함수
	/** 복사 가능 여부를 검사한다 */
	private static bool IsEnableCopy(string a_oSrcPath, string a_oDestPath, bool a_bIsOverwrite) {
		bool bIsValidA = a_oSrcPath.ExIsValid() && a_oDestPath.ExIsValid() && File.Exists(a_oSrcPath);
		bool bIsValidB = a_bIsOverwrite || !File.Exists(a_oDestPath);

		return bIsValidA && bIsValidB;
	}
	#endregion // 클래스 함수
}
