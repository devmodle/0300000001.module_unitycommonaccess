using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System.IO;

/** 접근자 - 파일 */
public static partial class CAccess {
	#region 클래스 함수
	/** 읽기용 스트림을 반환한다 */
	public static FileStream GetReadStream(string a_oFilePath) {
		CAccess.Assert(a_oFilePath.ExIsValid());
		return File.Exists(a_oFilePath) ? File.Open(a_oFilePath, FileMode.Open, FileAccess.Read) : null;
	}

	/** 쓰기용 스트림을 반환한다 */
	public static FileStream GetWriteStream(string a_oFilePath) {
		CAccess.Assert(a_oFilePath.ExIsValid());
		string oDirPath = Path.GetDirectoryName(a_oFilePath).Replace(KCDefine.B_TOKEN_R_SLASH, KCDefine.B_TOKEN_SLASH);

		// 디렉토리 생성이 필요 할 경우
		if(oDirPath.ExIsValid() && !Directory.Exists(oDirPath)) {
			Directory.CreateDirectory(oDirPath);
		}

		return File.Open(a_oFilePath, FileMode.Create, FileAccess.Write);
	}
	#endregion // 클래스 함수
}
