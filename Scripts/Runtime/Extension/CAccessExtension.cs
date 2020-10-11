using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;

//! 기본 접근 확장 클래스
public static partial class CAccessExtension {
	#region 클래스 함수
	//! 유효 여부를 검사한다
	public static bool ExIsValid(this SystemLanguage a_eSender) {
		return a_eSender >= SystemLanguage.Afrikaans && a_eSender < SystemLanguage.Unknown;
	}

	//! 유효 여부를 검사한다
	public static bool ExIsValid(this string a_oSender) {
		return a_oSender != null && a_oSender.Length >= 1;
	}

	//! 빌드 번호 유효 여부를 검사한다
	public static bool ExIsValidBuildNumber(this int a_nSender) {
		return a_nSender >= KCDefine.B_MIN_BUILD_NUMBER;
	}

	//! 언어 유효 여부를 검사한다
	public static bool ExIsValidLanguage(this string a_oSender) {
		return !a_oSender.ExIsEquals(KCDefine.B_UNKNOWN_LANGUAGE);
	}

	//! 국가 코드 유효 여부를 검사한다
	public static bool ExIsValidCountryCode(this string a_oSender) {
		string oCountryCode = a_oSender.ToUpper();
		return oCountryCode.ExIsValid() && !oCountryCode.ExIsEquals(KCDefine.B_UNKNOWN_COUNTRY_CODE);
	}

	//! 동일 여부를 검사한다
	public static bool ExIsEquals(this float a_fSender, float a_fRhs) {
		return Mathf.Approximately(a_fSender, a_fRhs);
	}

	//! 동일 여부를 검사한다
	public static bool ExIsEquals(this double a_dblSender, double a_dblRhs) {
		double dblDeltaTime = System.Math.Abs(a_dblSender) - System.Math.Abs(a_dblRhs);
		return dblDeltaTime >= -double.Epsilon && dblDeltaTime <= double.Epsilon;
	}

	//! 동일 여부를 검사한다
	public static bool ExIsEquals(this string a_oSender, string a_oRhs) {
		return a_oSender != null && a_oRhs != null && a_oSender.Equals(a_oRhs);
	}

	//! 작음 여부를 검사한다
	public static bool ExIsLess(this float a_fSender, float a_fRhs) {
		return a_fSender < a_fRhs - float.Epsilon;
	}

	//! 작거나 같음 여부를 검사한다
	public static bool ExIsLessEquals(this float a_fSender, float a_fRhs) {
		return a_fSender.ExIsLess(a_fRhs) || a_fSender.ExIsEquals(a_fRhs);
	}

	//! 큰 여부를 검사한다
	public static bool ExIsGreate(this float a_fSender, float a_fRhs) {
		return a_fSender > a_fRhs + float.Epsilon;
	}

	//! 크거나 같음 여부를 검사한다
	public static bool ExIsGreateEquals(this float a_fSender, float a_fRhs) {
		return a_fSender.ExIsGreate(a_fRhs) || a_fSender.ExIsEquals(a_fRhs);
	}

	//! 작음 여부를 검사한다
	public static bool ExIsLess(this double a_dblSender, double a_dblRhs) {
		return a_dblSender < a_dblRhs - double.Epsilon;
	}

	//! 작거나 같음 여부를 검사한다
	public static bool ExIsLessEquals(this double a_dblSender, double a_dblRhs) {
		return a_dblSender.ExIsLess(a_dblRhs) || a_dblSender.ExIsEquals(a_dblRhs);
	}

	//! 큰 여부를 검사한다
	public static bool ExIsGreate(this double a_dblSender, double a_dblRhs) {
		return a_dblSender > a_dblRhs + double.Epsilon;
	}

	//! 크거나 같음 여부를 검사한다
	public static bool ExIsGreateEquals(this double a_dblSender, double a_dblRhs) {
		return a_dblSender.ExIsGreate(a_dblRhs) || a_dblSender.ExIsEquals(a_dblRhs);
	}

	//! 완료 여부를 검사한다
	public static bool ExIsComplete(this Task a_oSender) {
		return a_oSender != null && (a_oSender.IsCompleted && !a_oSender.IsFaulted && !a_oSender.IsCanceled);
	}

	//! 유럽 연합 여부를 검사한다
	public static bool ExIsEuropeanUnion(this string a_oSender) {
		string oCountryCode = a_oSender.ToUpper();

		for(int i = KCDefine.B_INDEX_START; i < KCDefine.B_EUROPEAN_UNION_COUNTRY_CODES.Length; ++i) {
			// 유럽 연합 일 경우
			if(oCountryCode.ExIsEquals(KCDefine.B_EUROPEAN_UNION_COUNTRY_CODES[i])) {
				return true;
			}
		}

		return false;
	}

	//! 시간 간격을 반환한다
	public static double ExGetDeltaTime(this System.DateTime a_stSender, System.DateTime a_stRhs) {
		return (a_stSender - a_stRhs).TotalSeconds;
	}

	//! 시간 간격을 반환한다
	public static double ExGetDeltaTimePerMinutes(this System.DateTime a_stSender, System.DateTime a_stRhs) {
		return (a_stSender - a_stRhs).TotalMinutes;
	}

	//! 시간 간격을 반환한다
	public static double ExGetDeltaTimePerHours(this System.DateTime a_stSender, System.DateTime a_stRhs) {
		return (a_stSender - a_stRhs).TotalHours;
	}

	//! 시간 간격을 반환한다
	public static double ExGetDeltaTimePerDays(this System.DateTime a_stSender, System.DateTime a_stRhs) {
		return (a_stSender - a_stRhs).TotalDays;
	}

	//! 변경 된 문자열을 반환한다
	public static string ExGetReplaceString(this string a_oSender, string a_oSearch, string a_oReplace, int a_nReplaceTimes = 1) {
		// 검색과 변경 문자열이 다를 경우
		if(!a_oSearch.ExIsEquals(a_oReplace)) {
			for(int i = KCDefine.B_INDEX_START; i < a_nReplaceTimes && a_oSender.Contains(a_oSearch); ++i) {
				a_oSender = a_oSender.Replace(a_oSearch, a_oReplace);
			}
		}

		return a_oSender;
	}

	//! 파일 이름이 변경 된 경로를 반환한다
	public static string ExGetReplaceFilenamePath(this string a_oSender, string a_oFilename) {
		var oFilename = Path.GetFileNameWithoutExtension(a_oSender);
		return a_oSender.ExGetReplaceString(oFilename, a_oFilename);
	}
	#endregion			// 클래스 함수

	#region 제네릭 클래스 함수
	//! 유효 여부를 검사한다
	public static bool ExIsValid<T>(this T[] a_oSender) {
		return a_oSender != null && a_oSender.Length >= 1;
	}

	//! 유효 여부를 검사한다
	public static bool ExIsValid<T>(this T[,] a_oSender) {
		return a_oSender != null && 
			(a_oSender.GetLength(KCDefine.B_INDEX_START) >= 1 && a_oSender.GetLength(KCDefine.B_INDEX_START + 1) >= 1);
	}

	//! 유효 여부를 검사한다
	public static bool ExIsValid<T>(this List<T> a_oSender) {
		return a_oSender != null && a_oSender.Count >= 1;
	}

	//! 유효 여부를 검사한다
	public static bool ExIsValid<Key, Value>(this Dictionary<Key, Value> a_oSender) {
		return a_oSender != null && a_oSender.Count >= 1;
	}

	//! 인덱스 유효 여부를 검사한다
	public static bool ExIsValidIndex<T>(this T[] a_oSender, int a_nIndex) {
		return a_nIndex > KCDefine.B_INDEX_INVALID && a_nIndex < a_oSender.Length;
	}

	//! 인덱스 유효 여부룰 검사한다
	public static bool ExIsValidIndex<T>(this List<T> a_oSender, int a_nIndex) {
		return a_nIndex > KCDefine.B_INDEX_INVALID && a_nIndex < a_oSender.Count;
	}

	//! 완료 여부를 검사한다
	public static bool ExIsComplete<T>(this Task<T> a_oSender) {
		bool bIsComplete = a_oSender != null && (a_oSender.IsCompleted && !a_oSender.IsFaulted && !a_oSender.IsCanceled);
		return bIsComplete && a_oSender.Result != null;
	}

	//! 값을 반환한다
	public static T ExGetValue<T>(this T[] a_oSender, int a_nIndex, T a_tDefValue) {
		return (a_nIndex < a_oSender.Length) ? a_oSender[a_nIndex] : a_tDefValue;
	}

	//! 값을 반환한다
	public static T ExGetValue<T>(this List<T> a_oSender, int a_nIndex, T a_tDefValue) {
		return (a_nIndex < a_oSender.Count) ? a_oSender[a_nIndex] : a_tDefValue;
	}

	//! 값을 반환한다
	public static Value ExGetValue<Key, Value>(this Dictionary<Key, Value> a_oSender, Key a_tKey, Value a_tDefValue) {
		return a_oSender.ContainsKey(a_tKey) ? a_oSender[a_tKey] : a_tDefValue;
	}

	//! 필드 값을 반환한다
	public static object ExGetFieldValue<T>(this object a_oSender, string a_oName, BindingFlags a_eBindingFlags) {
		var oType = typeof(T);
		var oFieldInfo = oType.GetField(a_oName, a_eBindingFlags);

		return oFieldInfo.GetValue(a_oSender);
	}

	//! 런타임 필드 값을 반환한다
	public static object ExGetRuntimeFieldValue<T>(this object a_oSender, string a_oName) {
		var oType = typeof(T);
		var oFieldInfos = oType.GetRuntimeFields();

		foreach(var oFieldInfo in oFieldInfos) {
			// 필드 이름이 동일 할 경우
			if(oFieldInfo.Name.ExIsEquals(a_oName)) {
				return oFieldInfo.GetValue(a_oSender);
			}
		}

		return null;
	}

	//! 프로퍼티 값을 반환한다
	public static object ExGetPropertyValue<T>(this object a_oSender, string a_oName, BindingFlags a_eBindingFlags) {
		var oType = typeof(T);
		var oPropertyInfo = oType.GetProperty(a_oName, a_eBindingFlags);

		return oPropertyInfo.GetValue(a_oSender);
	}

	//! 런타임 프로퍼티 값을 반환한다
	public static object ExGetRuntimePropertyValue<T>(this object a_oSender, string a_oName) {
		var oType = typeof(T);
		var oPropertyInfos = oType.GetRuntimeProperties();
		
		foreach(var oPropertyInfo in oPropertyInfos) {
			// 프로퍼티 이름과 동일 할 경우
			if(oPropertyInfo.Name.ExIsEquals(a_oName)) {
				return oPropertyInfo.GetValue(a_oSender);
			}
		}

		return null;
	}

	//! 값을 변경한다
	public static void ExSetValue<T>(this T[] a_oSender, int a_nIndex, T a_tValue) {
		// 인덱스가 유효 할 경우
		if(a_oSender.ExIsValidIndex(a_nIndex)) {
			a_oSender[a_nIndex] = a_tValue;
		}
	}

	//! 값을 변경한다
	public static void ExSetValue<T>(this List<T> a_oSender, int a_nIndex, T a_tValue) {
		// 인덱스가 유효 할 경우
		if(a_oSender.ExIsValidIndex(a_nIndex)) {
			a_oSender[a_nIndex] = a_tValue;
		}
	}

	//! 값을 변경한다
	public static void ExSetValue<Key, Value>(this Dictionary<Key, Value> a_oSender, Key a_tKey, Value a_tValue) {
		// 키가 유효 할 경우
		if(a_oSender.ContainsKey(a_tKey)) {
			a_oSender[a_tKey] = a_tValue;
		}
	}

	//! 값을 변경한다
	public static void ExSetValues<T>(this T[] a_oSender, List<int> a_oIndexList, List<T> a_oValueList) {
		for(int i = KCDefine.B_INDEX_START; i < a_oIndexList.Count; ++i) {
			a_oSender.ExSetValue(a_oIndexList[i], a_oValueList[i]);
		}
	}

	//! 값을 변경한다
	public static void ExSetValues<T>(this List<T> a_oSender, List<int> a_oIndexList, List<T> a_oValueList) {
		for(int i = KCDefine.B_INDEX_START; i < a_oIndexList.Count; ++i) {
			a_oSender.ExSetValue(a_oIndexList[i], a_oValueList[i]);
		}
	}

	//! 값을 변경한다
	public static void ExSetValues<Key, Value>(this Dictionary<Key, Value> a_oSender, Dictionary<Key, Value> a_oValueList) {
		foreach(var stKeyValue in a_oValueList) {
			a_oSender.ExSetValue(stKeyValue.Key, stKeyValue.Value);
		}
	}

	//! 필드 값을 변경한다
	public static void ExSetFieldValue<T>(this object a_oSender, string a_oName, BindingFlags a_eBindingFlags, object a_oValue) {
		var oType = typeof(T);
		var oFieldInfo = oType.GetField(a_oName, a_eBindingFlags);

		oFieldInfo.SetValue(a_oSender, a_oValue);
	}

	//! 런타임 필드 값을 변경한다
	public static void ExSetRuntimeFieldValue<T>(this object a_oSender, string a_oName, object a_oValue) {
		var oType = typeof(T);
		var oFieldInfos = oType.GetRuntimeFields();

		foreach(var oFieldInfo in oFieldInfos) {
			// 필드 이름이 동일 할 경우
			if(oFieldInfo.Name.ExIsEquals(a_oName)) {
				oFieldInfo.SetValue(a_oSender, a_oValue);
			}
		}
	}

	//! 프로퍼티 값을 변경한다
	public static void ExSetPropertyValue<T>(this object a_oSender, string a_oName, BindingFlags a_eBindingFlags, object a_oValue) {
		var oType = typeof(T);
		var oPropertyInfo = oType.GetProperty(a_oName, a_eBindingFlags);

		oPropertyInfo.SetValue(a_oSender, a_oValue);
	}

	//! 런타임 프로퍼티 값을 변경한다
	public static void ExSetRuntimePropertyValue<T>(this object a_oSender, string a_oName, object a_oValue) {
		var oType = typeof(T);
		var oPropertyInfos = oType.GetRuntimeProperties();
		
		foreach(var oPropertyInfo in oPropertyInfos) {
			// 프로퍼티 이름이 동일 할 경우
			if(oPropertyInfo.Name.ExIsEquals(a_oName)) {
				oPropertyInfo.SetValue(a_oSender, a_oValue);
			}
		}
	}
	#endregion			// 제네릭 클래스 함수
}
