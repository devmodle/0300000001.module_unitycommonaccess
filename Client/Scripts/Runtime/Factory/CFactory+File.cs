using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System.IO;

/** 파일 팩토리 */
public static partial class CFactory {
	#region 클래스 함수
	/** 디렉토리를 생성한다 */
	public static DirectoryInfo MakeDirectories(string a_oDirPath) {
		CAccess.Assert(a_oDirPath.ExIsValid());

		// 디렉토리가 없을 경우
		if(!Directory.Exists(a_oDirPath)) {
			Directory.CreateDirectory(a_oDirPath);
		}

		return new DirectoryInfo(a_oDirPath);
	}

	/** 디렉토리를 제거한다 */
	public static void RemoveDir(string a_oDirPath, bool a_bIsRecursive = true, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oDirPath.ExIsValid());

		// 디렉토리가 존재 할 경우
		if(a_oDirPath.ExIsValid() && Directory.Exists(a_oDirPath)) {
			Directory.Delete(a_oDirPath, a_bIsRecursive);
		}
	}

	/** 파일을 제거한다 */
	public static void RemoveFile(string a_oFilePath, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oFilePath.ExIsValid());

		// 파일이 존재 할 경우
		if(a_oFilePath.ExIsValid() && File.Exists(a_oFilePath)) {
			File.Delete(a_oFilePath);
		}
	}
	#endregion // 클래스 함수
}
