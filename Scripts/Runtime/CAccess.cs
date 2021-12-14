using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

/** 기본 접근자 */
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
	/** 한글 여부를 검사한다 */
	public static bool IsKorean(string a_oStr) {
		CAccess.Assert(a_oStr != null);
		return Regex.IsMatch(a_oStr, KCDefine.B_STR_KOREAN_PATTERN);
	}

	/** 유저 문자열을 반환한다 */
	public static string GetUserStr(EUserType a_eUserType) {
		// 유저 타입이 유효하지 않을 경우
		if(!a_eUserType.ExIsValid()) {
			return KCDefine.B_TOKEN_USER_UNKNOWN;
		}

		return (a_eUserType == EUserType.USER_A) ? KCDefine.B_TOKEN_USER_A : KCDefine.B_TOKEN_USER_B;
	}

	/** 버전 문자열을 반환한다 */
	public static string GetVerStr(string a_oVer, EUserType a_eUserType) {
		string oUserStr = CAccess.GetUserStr(a_eUserType);
		return string.Format(KCDefine.B_TEXT_FMT_VER, a_oVer, oUserStr);
	}

	/** 읽기용 스트림을 반환한다 */
	public static FileStream GetReadStream(string a_oFilePath) {
		CAccess.Assert(a_oFilePath.ExIsValid());
		return File.Exists(a_oFilePath) ? File.Open(a_oFilePath, FileMode.Open, FileAccess.Read) : null;
	}

	/** 쓰기용 스트림을 반환한다 */
	public static FileStream GetWriteStream(string a_oFilePath, bool a_bIsAutoCreateDir = true) {
		CAccess.Assert(a_oFilePath.ExIsValid());
		string oDirPath = Path.GetDirectoryName(a_oFilePath);

		// 디렉토리 자동 생성 모드 일 경우
		if(a_bIsAutoCreateDir && oDirPath.ExIsValid() && !Directory.Exists(oDirPath)) {
			Directory.CreateDirectory(oDirPath);
		}
		
		return File.Open(a_oFilePath, FileMode.Create, FileAccess.Write);
	}

	/** 조건을 검사한다 */
	[Conditional("DEBUG"), Conditional("DEVELOPMENT_BUILD")]
	public static void Assert(bool a_bIsTrue, string a_oMsg = KCDefine.B_EMPTY_STR) {
		UnityEngine.Assertions.Assert.IsTrue(a_bIsTrue, a_oMsg);
	}
	#endregion			// 클래스 함수

	#region 제네릭 클래스 함수
	/** 열거형 값을 반환한다 */
	public static List<T> GetEnumVals<T>() {
		var oEnumVals = System.Enum.GetValues(typeof(T));
		return (oEnumVals as T[]).ToList();
	}

	/** 열거형 문자열을 반환한다 */
	public static List<string> GetEnumStrs<T>() {
		var oEnumStrList = new List<string>();
		var oEnumValList = CAccess.GetEnumVals<T>();

		for(int i = 0; i < oEnumValList.Count; ++i) {
			oEnumStrList.Add(oEnumValList[i].ToString());
		}

		return oEnumStrList;
	}
	#endregion			// 제네릭 클래스 함수
}
