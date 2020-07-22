using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;

//! 기본 접근 확장 클래스
public static partial class CAccessExtension {
	#region 클래스 함수
	//! 유효 여부를 검사한다
	public static bool ExIsValid(this string a_oSender) {
		return a_oSender != null && a_oSender.Length >= 1;
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

	//! 비동기 작업 완료 여부를 검사한다
	public static bool ExIsComplete(this Task a_oSender) {
		return a_oSender != null && (a_oSender.IsCompleted && !a_oSender.IsFaulted && !a_oSender.IsCanceled);
	}

	//! 유럽 연합 여부를 검사한다
	public static bool ExIsEuropeanUnion(this string a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());
		string oCountryCode = a_oSender.ToUpper();

		for(int i = 0; i < KCDefine.B_EUROPEAN_UNION_COUNTRY_CODES.Length; ++i) {
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
		CAccess.Assert(a_oSender.ExIsValid());
		CAccess.Assert(a_nReplaceTimes >= 1 && (a_oSearch != null && a_oReplace != null));

		if(!a_oSearch.ExIsEquals(a_oReplace)) {
			for(int i = 0; i < a_nReplaceTimes && a_oSender.Contains(a_oSearch); ++i) {
				a_oSender = a_oSender.Replace(a_oSearch, a_oReplace);
			}
		}

		return a_oSender;
	}
	#endregion			// 클래스 함수

	#region 제네릭 클래스 함수
	//! 유효 여부를 검사한다
	public static bool ExIsValid<T>(this T[] a_oSender) {
		return a_oSender != null && a_oSender.Length >= 1;
	}

	//! 유효 여부를 검사한다
	public static bool ExIsValid<T>(this T[,] a_oSender) {
		return a_oSender != null && (a_oSender.GetLength(0) >= 1 && a_oSender.GetLength(1) >= 1);
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
		CAccess.Assert(a_oSender != null);
		return a_nIndex > KCDefine.B_INDEX_INVALID && a_nIndex < a_oSender.Length;
	}

	//! 인덱스 유효 여부룰 검사한다
	public static bool ExIsValidIndex<T>(this List<T> a_oSender, int a_nIndex) {
		CAccess.Assert(a_oSender != null);
		return a_nIndex > KCDefine.B_INDEX_INVALID && a_nIndex < a_oSender.Count;
	}

	//! 비동기 작업 완료 여부를 검사한다
	public static bool ExIsComplete<T>(this Task<T> a_oSender) {
		bool bIsComplete = a_oSender != null && (a_oSender.IsCompleted && !a_oSender.IsFaulted && !a_oSender.IsCanceled);
		return bIsComplete && a_oSender.Result != null;
	}

	//! 값을 반환한다
	public static T ExGetValue<T>(this T[] a_oSender, int a_nIndex, T a_tDefValue) {
		CAccess.Assert(a_oSender != null);
		return (a_nIndex < a_oSender.Length) ? a_oSender[a_nIndex] : a_tDefValue;
	}

	//! 값을 반환한다
	public static T ExGetValue<T>(this List<T> a_oSender, int a_nIndex, T a_tDefValue) {
		CAccess.Assert(a_oSender != null);
		return (a_nIndex < a_oSender.Count) ? a_oSender[a_nIndex] : a_tDefValue;
	}

	//! 값을 반환한다
	public static Value ExGetValue<Key, Value>(this Dictionary<Key, Value> a_oSender, Key a_tKey, Value a_tDefValue) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ContainsKey(a_tKey) ? a_oSender[a_tKey] : a_tDefValue;
	}

	//! 필드 값을 반환한다
	public static object ExGetFieldValue<T>(this object a_oSender, string a_oName, BindingFlags a_eBindingFlags) {
		var oType = typeof(T);
		var oFieldInfo = oType.GetField(a_oName, a_eBindingFlags);

		CAccess.Assert(oFieldInfo != null);
		return oFieldInfo.GetValue(a_oSender);
	}

	//! 런타임 필드 값을 반환한다
	public static object ExGetRuntimeFieldValue<T>(this object a_oSender, string a_oName) {
		var oType = typeof(T);
		var oFieldInfos = oType.GetRuntimeFields();

		CAccess.Assert(oFieldInfos != null);

		foreach(var oFieldInfo in oFieldInfos) {
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

		CAccess.Assert(oPropertyInfo != null);
		return oPropertyInfo.GetValue(a_oSender);
	}

	//! 런타임 프로퍼티 값을 반환한다
	public static object ExGetRuntimePropertyValue<T>(this object a_oSender, string a_oName) {
		var oType = typeof(T);
		var oPropertyInfos = oType.GetRuntimeProperties();

		CAccess.Assert(oPropertyInfos != null);

		foreach(var oPropertyInfo in oPropertyInfos) {
			if(oPropertyInfo.Name.ExIsEquals(a_oName)) {
				return oPropertyInfo.GetValue(a_oSender);
			}
		}

		return null;
	}

	//! 값을 변경한다
	public static void ExSetValue<T>(this T[] a_oSender, int a_nIndex, T a_tValue) {
		CAccess.Assert(a_oSender != null);

		if(a_nIndex >= 0 && a_nIndex < a_oSender.Length) {
			a_oSender[a_nIndex] = a_tValue;
		}
	}

	//! 값을 변경한다
	public static void ExSetValue<T>(this List<T> a_oSender, int a_nIndex, T a_tValue) {
		CAccess.Assert(a_oSender != null);

		if(a_nIndex >= 0 && a_nIndex < a_oSender.Count) {
			a_oSender[a_nIndex] = a_tValue;
		}
	}

	//! 값을 변경한다
	public static void ExSetValue<Key, Value>(this Dictionary<Key, Value> a_oSender, Key a_tKey, Value a_tValue) {
		CAccess.Assert(a_oSender != null);

		if(a_oSender.ContainsKey(a_tKey)) {
			a_oSender[a_tKey] = a_tValue;
		}
	}

	//! 필드 값을 변경한다
	public static void ExSetFieldValue<T>(this object a_oSender, string a_oName, BindingFlags a_eBindingFlags, object a_oValue) {
		var oType = typeof(T);
		var oFieldInfo = oType.GetField(a_oName, a_eBindingFlags);

		CAccess.Assert(oFieldInfo != null);
		oFieldInfo.SetValue(a_oSender, a_oValue);
	}

	//! 런타임 필드 값을 변경한다
	public static void ExSetRuntimeFieldValue<T>(this object a_oSender, string a_oName, object a_oValue) {
		var oType = typeof(T);
		var oFieldInfos = oType.GetRuntimeFields();

		CAccess.Assert(oFieldInfos != null && a_oName.ExIsValid());

		foreach(var oFieldInfo in oFieldInfos) {
			if(oFieldInfo.Name.ExIsEquals(a_oName)) {
				oFieldInfo.SetValue(a_oSender, a_oValue);
			}
		}
	}

	//! 프로퍼티 값을 변경한다
	public static void ExSetPropertyValue<T>(this object a_oSender, string a_oName, BindingFlags a_eBindingFlags, object a_oValue) {
		var oType = typeof(T);
		var oPropertyInfo = oType.GetProperty(a_oName, a_eBindingFlags);

		CAccess.Assert(oPropertyInfo != null);
		oPropertyInfo.SetValue(a_oSender, a_oValue);
	}

	//! 런타임 프로퍼티 값을 변경한다
	public static void ExSetRuntimePropertyValue<T>(this object a_oSender, string a_oName, object a_oValue) {
		var oType = typeof(T);
		var oPropertyInfos = oType.GetRuntimeProperties();

		CAccess.Assert(oPropertyInfos != null && a_oName.ExIsValid());

		foreach(var oPropertyInfo in oPropertyInfos) {
			if(oPropertyInfo.Name.ExIsEquals(a_oName)) {
				oPropertyInfo.SetValue(a_oSender, a_oValue);
			}
		}
	}
	#endregion			// 제네릭 클래스 함수
}
