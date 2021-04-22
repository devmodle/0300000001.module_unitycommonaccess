using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

//! 기본 접근자
public static partial class CAccess {
	#region 클래스 프로퍼티
	public static string MidnightDeltaTimeStr {
		get {
			var stDateTime = new System.DateTime(CAccess.MidnightDeltaTime.Ticks);
			return stDateTime.ToString(KCDefine.B_DATE_T_FMT_HH_MM_SS);
		}
	}

	public static System.DateTime MidnightTime => System.DateTime.Today.AddDays(KCDefine.B_VAL_1_DBL);
	public static System.TimeSpan MidnightDeltaTime => CAccess.MidnightTime - System.DateTime.Now;
	#endregion			// 클래스 프로퍼티

	#region 클래스 함수
	//! 버전 문자열을 반환한다
	public static string GetVerStr(string a_oVer, EUserType a_eUserType) {
		// 유저 타입이 유효하지 않을 경우
		if(a_eUserType != EUserType.USER_A && a_eUserType != EUserType.USER_B) {
			return string.Format(KCDefine.B_TEXT_FMT_VER, a_oVer, KCDefine.B_TOKEN_USER_UNKNOWN);
		}

		string oUserToken = (a_eUserType == EUserType.USER_A) ? KCDefine.B_TOKEN_USER_A : KCDefine.B_TOKEN_USER_B;
		return string.Format(KCDefine.B_TEXT_FMT_VER, a_oVer, oUserToken);
	}

	//! 읽기용 스트림을 반환한다
	public static FileStream GetReadStream(string a_oFilePath) {
		CAccess.Assert(a_oFilePath.ExIsValid());
		return File.Exists(a_oFilePath) ? File.Open(a_oFilePath, FileMode.Open, FileAccess.Read) : null;
	}

	//! 쓰기용 스트림을 반환한다
	public static FileStream GetWriteStream(string a_oFilePath, bool a_bIsAutoCreateDir = true, bool a_bIsAutoBackup = false, string a_oBackupDirName = KCDefine.B_EMPTY_STR) {
		CAccess.Assert(a_oFilePath.ExIsValid());

		string oDirPath = Path.GetDirectoryName(a_oFilePath);
		bool bIsEnableCreateDir = a_bIsAutoCreateDir && oDirPath.ExIsValid();

		// 디렉토리 자동 생성 모드 일 경우
		if(bIsEnableCreateDir && !Directory.Exists(oDirPath)) {
			Directory.CreateDirectory(oDirPath);
		}

		// 자동 백업이 가능 할 경우
		if(a_bIsAutoBackup && File.Exists(a_oFilePath)) {
			string oFileName = Path.GetFileName(a_oFilePath);
			string oOriginFileName = Path.GetFileNameWithoutExtension(a_oFilePath);

			string oDateStr = System.DateTime.Now.ToString(KCDefine.B_NAME_FMT_BACKUP);
			string oBackupFileName = string.Format(KCDefine.B_NAME_FMT_BACKUP_COMBINE, oOriginFileName, oDateStr);

			string oBackupDirName = a_oBackupDirName.ExIsValid() ? a_oBackupDirName : KCDefine.B_DIR_N_BACKUP;
			string oBackupPath = oDirPath.ExIsValid() ? Path.Combine(oDirPath, oBackupDirName) : Path.Combine(string.Empty, oBackupDirName);

			// 디렉토리가 없을 경우
			if(!Directory.Exists(oBackupPath)) {
				Directory.CreateDirectory(oBackupPath);
			}

			string oBackupFilePath = Path.Combine(oBackupPath, oFileName.ExGetReplaceStr(oOriginFileName, oBackupFileName));

			// 백업 파일 생성이 가능 할 경우
			if(!File.Exists(oBackupFilePath)) {
				var oFilePaths = Directory.GetFiles(oBackupPath);

				// 최대 개수를 벗어났을 경우
				if(oFilePaths.Length >= KCDefine.B_MAX_NUM_BACKUP_FILES) {
					System.Array.Sort(oFilePaths, (a_oLhs, a_oRhs) => a_oRhs.CompareTo(a_oLhs));

					for(int i = KCDefine.B_MAX_NUM_BACKUP_FILES - 1; i < oFilePaths.Length; ++i) {
						File.Delete(oFilePaths[i]);
					}
				}

				File.Copy(a_oFilePath, oBackupFilePath);
			}
		}

		return File.Open(a_oFilePath, FileMode.Create, FileAccess.Write);
	}

	//! 조건을 검사한다
	[Conditional("LOGIC_TEST_ENABLE"), Conditional("DEBUG"), Conditional("DEVELOPMENT_BUILD")]
	public static void Assert(bool a_bIsTrue, string a_oMsg = KCDefine.B_EMPTY_STR) {
		UnityEngine.Assertions.Assert.IsTrue(a_bIsTrue, a_oMsg);
	}
	#endregion			// 클래스 함수

	#region 제네릭 클래스 함수
	//! 열거형 값을 반환한다
	public static T[] GetEnumVals<T>() {
		return System.Enum.GetValues(typeof(T)) as T[];
	}

	//! 열거형 문자열을 반환한다
	public static string[] GetEnumStrs<T>() {
		var oEnumVals = CAccess.GetEnumVals<T>();
		var oEnumStrs = new string[oEnumVals.Length];

		for(int i = 0; i < oEnumVals.Length; ++i) {
			oEnumStrs[i] = oEnumVals[i].ToString();
		}

		return oEnumStrs;
	}
	#endregion			// 제네릭 클래스 함수
}
