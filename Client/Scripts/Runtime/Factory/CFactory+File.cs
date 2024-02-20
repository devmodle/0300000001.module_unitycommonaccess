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
	#endregion // 클래스 함수
}
