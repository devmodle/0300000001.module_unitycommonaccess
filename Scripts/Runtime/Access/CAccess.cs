using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

//! 기본 접근자
public static partial class CAccess {
	#region 클래스 함수
	//! 읽기용 스트림을 반환한다
	public static FileStream GetReadStream(string a_oFilepath) {
		return File.Exists(a_oFilepath) ? 
			File.Open(a_oFilepath, FileMode.Open, FileAccess.Read) : null;
	}

	//! 쓰기용 스트림을 반환한다
	public static FileStream GetWriteStream(string a_oFilepath,
		bool a_bIsAutoCreateDir = true, 
		bool a_bIsAutoBackup = false, 
		string a_oBackupDirname = KCDefine.B_EMPTY_STRING) 
	{
		string oDirpath = Path.GetDirectoryName(a_oFilepath);

		// 디렉토리 자동 생성 모드 일 경우
		if(a_bIsAutoCreateDir) {
			CAccess.CreateDirectory(oDirpath);
		}

		// 자동 백업이 가능 할 경우
		if(a_bIsAutoBackup && File.Exists(a_oFilepath)) {
			string oFilename = Path.GetFileName(a_oFilepath);
			string oOriginFilename = Path.GetFileNameWithoutExtension(a_oFilepath);

			string oBackupFilename = string.Format(KCDefine.B_FILENAME_FORMAT_BACKUP, 
				oOriginFilename, System.DateTime.Now.ToString(KCDefine.B_NAME_FORMAT_BACKUP));

			string oBackupDirname = a_oBackupDirname.ExIsValid() ? 
				a_oBackupDirname : KCDefine.B_DIR_NAME_BACKUP;

			string oBackupDirpath = Path.Combine(oDirpath, oBackupDirname);
			CAccess.CreateDirectory(oBackupDirpath);

			string oBackupFilepath = Path.Combine(oBackupDirpath, 
				oFilename.ExGetReplaceString(oOriginFilename, oBackupFilename));

			// 백업 파일 생성이 가능 할 경우
			if(!File.Exists(oBackupFilepath)) {
				var oFilepaths = Directory.GetFiles(oBackupDirpath);
				int nMaxNumBackupFiles = KCDefine.B_MAX_NUM_BACKUP_FILES - KCDefine.B_VALUE_INT_1;

				// 파일 최대 개수를 벗어났을 경우
				if(oFilepaths.Length >= nMaxNumBackupFiles) {
					System.Array.Sort(oFilepaths, (a_oLhs, a_oRhs) => a_oRhs.CompareTo(a_oLhs));

					for(int i = nMaxNumBackupFiles; i < oFilepaths.Length; ++i) {
						File.Delete(oFilepaths[i]);
					}
				}

				File.Copy(a_oFilepath, oBackupFilepath);
			}
		}

		return File.Open(a_oFilepath, FileMode.Create, FileAccess.Write);
	}

	//! 조건을 검사한다
	[Conditional("DEBUG"), Conditional("DEVELOPMENT_BUILD")]
	public static void Assert(bool a_bIsTrue, string a_oMsg = KCDefine.B_EMPTY_STRING) {
		// 메세지가 유효 할 경우
		if(a_oMsg != null && a_oMsg.Length > KCDefine.B_VALUE_INT_0) {
			UnityEngine.Assertions.Assert.IsTrue(a_bIsTrue, a_oMsg);
		} else {
			UnityEngine.Assertions.Assert.IsTrue(a_bIsTrue);
		}
	}

	//! 디렉토리를 생성한다
	public static DirectoryInfo CreateDirectory(string a_oDirpath) {
		// 디렉토리가 없을 경우
		if(!Directory.Exists(a_oDirpath)) {
			Directory.CreateDirectory(a_oDirpath);
		}

		return new DirectoryInfo(a_oDirpath);
	}

	//! 디렉토리를 제거한다
	public static void RemoveDirectory(string a_oDirpath, bool a_bIsRecursive = true) {
		// 디렉토리가 존재 할 경우
		if(Directory.Exists(a_oDirpath)) {
			Directory.Delete(a_oDirpath, a_bIsRecursive);
		}
	}
	#endregion			// 클래스 함수
}
