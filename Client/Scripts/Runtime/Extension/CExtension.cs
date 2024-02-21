using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System.Reflection;
using System.Globalization;

/** 기본 확장 클래스 */
public static partial class CExtension {
	#region 클래스 함수
	/** 값 => 왼쪽 쉬프트 비트로 변환한다 */
	public static int ExToLShiftBits(this int a_nSender, int a_nOffset) {
		CAccess.Assert(a_nOffset >= KCDefine.B_VAL_0_INT);
		return a_nSender << a_nOffset;
	}

	/** 값 => 오른쪽 쉬프트 비트로 변환한다 */
	public static int ExToRShiftBits(this int a_nSender, int a_nOffset) {
		CAccess.Assert(a_nOffset >= KCDefine.B_VAL_0_INT);
		return a_nSender >> a_nOffset;
	}

	/** 비트 => 리스트로 변환한다 */
	public static List<int> ExToBits(this int a_nSender) {
		var oBitList = new List<int>();

		for(int i = 0; i < sizeof(int) * KCDefine.B_UNIT_BITS_PER_BYTE; ++i) {
			oBitList.Add(((a_nSender & KCDefine.B_VAL_1_INT.ExToLShiftBits(i)) != KCDefine.B_VAL_0_INT) ? KCDefine.B_VAL_1_INT : KCDefine.B_VAL_0_INT);
		}

		return oBitList;
	}

	/** 리스트 => 비트로 변환한다 */
	public static int ExToBits(this List<int> a_oSender) {
		CAccess.Assert(a_oSender != null);
		int nVal = KCDefine.B_VAL_0_INT;

		for(int i = 0; i < a_oSender.Count; ++i) {
			nVal |= KCDefine.B_VAL_1_INT.ExToLShiftBits(a_oSender[i]);
		}

		return nVal;
	}

	/** 숫자 => 개수 문자열로 변환한다 */
	public static string ExToNumStr(this decimal a_dmSender, bool a_bIsExtraNumStr = true, SystemLanguage a_eSystemLanguage = SystemLanguage.English) {
		var oNumTokenDict = (a_eSystemLanguage == SystemLanguage.Korean) ? KCDefine.B_NUM_TOKEN_DICT_KOREAN : KCDefine.B_NUM_TOKEN_DICT_ENGLISH;
		decimal dmDigitsUnit = (a_eSystemLanguage == SystemLanguage.Korean) ? KCDefine.B_UNIT_DIGITS_TEN_THOUSAND : KCDefine.B_UNIT_DIGITS_THOUSAND;

		foreach(var stKeyVal in oNumTokenDict) {
			decimal dmDigits = a_dmSender / stKeyVal.Value;

			// 변환 가능 할 경우
			if(dmDigits > KCDefine.B_VAL_0_INT && dmDigits < dmDigitsUnit) {
				string oNumStr = string.Format(KCDefine.B_TEXT_FMT_2_COMBINE, dmDigits, stKeyVal.Key);
				return (a_bIsExtraNumStr && (a_dmSender / (stKeyVal.Value * dmDigitsUnit)) > KCDefine.B_VAL_0_INT) ? string.Format(KCDefine.B_TEXT_FMT_2_SPACE_COMBINE, oNumStr, (a_dmSender / dmDigitsUnit).ExToNumStr(false)) : oNumStr;
			}
		}

		return $"{a_dmSender:0}";
	}

	/** 픽셀 => DPI 픽셀로 변환한다 */
	public static float ExPixelsToDPIPixels(this int a_nSender) {
		return a_nSender * (KCDefine.B_DEF_SCREEN_DPI / CAccess.ScreenDPI);
	}

	/** 픽셀 => DPI 픽셀로 변환한다 */
	public static float ExPixelsToDPIPixels(this float a_fSender) {
		return a_fSender * (KCDefine.B_DEF_SCREEN_DPI / CAccess.ScreenDPI);
	}

	/** 바이트 => 메가 바이트로 변환한다 */
	public static double ExByteToMegaByte(this uint a_oSender) {
		return a_oSender / (double)KCDefine.B_UNIT_BYTES_PER_MEGA_BYTE;
	}

	/** 바이트 => 메가 바이트로 변환한다 */
	public static double ExByteToMegaByte(this long a_oSender) {
		return a_oSender / (double)KCDefine.B_UNIT_BYTES_PER_MEGA_BYTE;
	}

	/** 중위 => 후위 수식으로 변환한다 */
	public static string ExInfixToPostfixCalc(this string a_oSender) {
		CAccess.Assert(a_oSender != null);

		// TODO 후위 수식 변환 로직 구현 필요

		return a_oSender;
	}

	/** 문자열 => 시간으로 변환한다 */
	public static System.DateTime ExToTime(this string a_oSender, string a_oFmt) {
		CAccess.Assert(a_oSender.ExIsValid() && a_oFmt.ExIsValid());
		return System.DateTime.ParseExact(a_oSender, a_oFmt, CultureInfo.InvariantCulture);
	}

	/** 시간 => 문자열로 변환한다 */
	public static string ExToStr(this System.DateTime a_stSender, string a_oFmt) {
		CAccess.Assert(a_stSender.ExIsValid());
		return a_stSender.ToString(a_oFmt, CultureInfo.InvariantCulture);
	}

	/** 시간 => 긴 문자열로 변환한다 */
	public static string ExToLongStr(this System.DateTime a_stSender, bool a_bIsEnableSlash = true) {
		CAccess.Assert(a_stSender.ExIsValid());
		return a_stSender.ExToStr(a_bIsEnableSlash ? KCDefine.B_DATE_T_FMT_SLASH_YYYY_MM_DD_HH_MM_SS : KCDefine.B_DATE_T_FMT_YYYY_MM_DD_HH_MM_SS);
	}

	/** 시간 => 짧은 문자열로 변환한다 */
	public static string ExToShortStr(this System.DateTime a_stSender, bool a_bIsEnableSlash = true) {
		CAccess.Assert(a_stSender.ExIsValid());
		return a_stSender.ExToStr(a_bIsEnableSlash ? KCDefine.B_DATE_T_FMT_SLASH_YYYY_MM_DD : KCDefine.B_DATE_T_FMT_YYYY_MM_DD);
	}

	/** 지역 시간 => PST 시간으로 변환한다 */
	public static System.DateTime ExToPSTTime(this System.DateTime a_stSender) {
		CAccess.Assert(a_stSender.ExIsValid());
		return a_stSender.ToUniversalTime().AddHours(KCDefine.B_DELTA_T_UTC_TO_PST);
	}

	/** 지역 시간 => 특정 지역 시간으로 변환한다 */
	public static System.DateTime ExToZoneTime(this System.DateTime a_stSender, string a_oTimeZoneID) {
		CAccess.Assert(a_stSender.ExIsValid() && a_oTimeZoneID.ExIsValid());
		return System.TimeZoneInfo.ConvertTime(a_stSender, System.TimeZoneInfo.Local, System.TimeZoneInfo.FindSystemTimeZoneById(a_oTimeZoneID));
	}

	/** PST 시간 => 지역 시간으로 변환한다 */
	public static System.DateTime ExPSTToLocalTime(this System.DateTime a_stSender) {
		CAccess.Assert(a_stSender.ExIsValid());
		return a_stSender.AddHours(-KCDefine.B_DELTA_T_UTC_TO_PST).ToLocalTime();
	}

	/** 특정 지역 시간 => 지역 시간으로 변환한다 */
	public static System.DateTime ExZoneToLocalTime(this System.DateTime a_stSender, string a_oTimeZoneID) {
		CAccess.Assert(a_stSender.ExIsValid() && a_oTimeZoneID.ExIsValid());
		return System.TimeZoneInfo.ConvertTime(a_stSender, System.TimeZoneInfo.FindSystemTimeZoneById(a_oTimeZoneID), System.TimeZoneInfo.Local);
	}

	/** 위치 => 기준 위치로 변환한다 */
	public static Vector3 ExPosToPivotPos(this Vector2 a_stSender, float a_fZ = KCDefine.B_VAL_0_REAL) {
		return a_stSender.ExTo3D(a_fZ).ExPosToPivotPos();
	}

	/** 위치 => 기준 위치로 변환한다 */
	public static Vector3 ExPosToPivotPos(this Vector3 a_stSender) {
		return new Vector3(a_stSender.x + (KCDefine.B_DESIGN_SCREEN_WIDTH / KCDefine.B_VAL_2_REAL), a_stSender.y + (KCDefine.B_DESIGN_SCREEN_HEIGHT / KCDefine.B_VAL_2_REAL), a_stSender.z);
	}

	/** 기준 위치 => 위치로 변환한다 */
	public static Vector3 ExPivotPosToPos(this Vector2 a_stSender, float a_fZ = KCDefine.B_VAL_0_REAL) {
		return a_stSender.ExTo3D(a_fZ).ExPivotPosToPos();
	}

	/** 기준 위치 => 위치로 변환한다 */
	public static Vector3 ExPivotPosToPos(this Vector3 a_stSender) {
		return new Vector3(a_stSender.x - (KCDefine.B_DESIGN_SCREEN_WIDTH / KCDefine.B_VAL_2_REAL), a_stSender.y - (KCDefine.B_DESIGN_SCREEN_HEIGHT / KCDefine.B_VAL_2_REAL), a_stSender.z);
	}

	/** JSON 배열 => 정보 값으로 변환한다 */
	public static List<List<string>> ExToInfoVals(this SimpleJSON.JSONArray a_oSender, List<STKeyInfo> a_oKeyInfoList) {
		CAccess.Assert(a_oSender != null && a_oKeyInfoList != null);
		var oInfoValListContainer = new List<List<string>>();

		// 배열이 유효 할 경우
		if(a_oSender.Count > KCDefine.B_VAL_0_INT) {
			var oKeyList = a_oKeyInfoList.ExToKeys(a_oSender[KCDefine.B_VAL_0_INT]);

			for(int i = 0; i < a_oSender.Count; ++i) {
				var oInfoValList = new List<string>();

				for(int j = 0; j < oKeyList.Count; ++j) {
					oInfoValList.Add(a_oSender[i].ExGetInfoVal(oKeyList[j]));
				}

				oInfoValListContainer.ExAddVal(oInfoValList);
			}
		}

		return oInfoValListContainer;
	}

	/** 정보 값을 반환한다 */
	private static string ExGetInfoVal(this SimpleJSON.JSONNode a_oSender, string a_oKey) {
		CAccess.Assert(a_oSender != null && a_oKey.ExIsValid());
		return a_oSender[a_oKey].ToString().Replace(KCDefine.B_TOKEN_D_QUOTES, string.Empty).Replace(KCDefine.B_TOKEN_O_BRACKETS, string.Empty).Replace(KCDefine.B_TOKEN_C_BRACKETS, string.Empty);
	}

	/** 키 정보 => 키로 변환한다 */
	private static List<string> ExToKeys(this List<STKeyInfo> a_oSender, SimpleJSON.JSONNode a_oJSONNode) {
		var oKeyList = new List<string>();

		for(int i = 0; i < a_oSender.Count; ++i) {
			switch(a_oSender[i].m_eKeyType) {
				case EKeyType.MULTI: {
					for(int j = 0; j < sbyte.MaxValue; ++j) {
						string oKey = string.Format(a_oSender[i].m_oKey, j + KCDefine.B_VAL_1_INT);

						// 값이 유효하지 않을 경우
						if(!a_oJSONNode[oKey].ExIsValid()) {
							break;
						}

						oKeyList.ExAddVal(oKey);
					}

					break;
				}
				case EKeyType.SINGLE: {
					oKeyList.ExAddVal(a_oSender[i].m_oKey);
					break;
				}
			}
		}

		return oKeyList;
	}
	#endregion // 클래스 함수

	#region 제네릭 클래스 함수
	/** 상태를 리셋한다 */
	public static void ExReset<T>(this T[] a_oSender, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 배열이 존재 할 경우
		if(a_oSender != null) {
			for(int i = 0; i < a_oSender.Length; ++i) {
				a_oSender[i] = default;
			}
		}
	}

	/** 상태를 리셋한다 */
	public static void ExReset<T>(this T[,] a_oSender, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 배열이 존재 할 경우
		if(a_oSender != null) {
			for(int i = 0; i < a_oSender.GetLength(KCDefine.B_VAL_0_INT); ++i) {
				for(int j = 0; j < a_oSender.GetLength(KCDefine.B_VAL_1_INT); ++j) {
					a_oSender[i, j] = default;
				}
			}
		}
	}

	/** 값을 안전 정렬한다 */
	public static void ExStableSort<T>(this T[] a_oSender, System.Comparison<T> a_oCompare, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oCompare != null));

		// 배열이 존재 할 경우
		if(a_oSender != null && a_oCompare != null) {
			for(int i = 1; i < a_oSender.Length; ++i) {
				int j = 0;
				var tVal = a_oSender[i];

				for(j = i - 1; j >= KCDefine.B_VAL_0_INT && a_oCompare(a_oSender[j], tVal) > KCDefine.B_COMPARE_EQUALS; --j) {
					a_oSender[j + KCDefine.B_VAL_1_INT] = a_oSender[j];
				}

				a_oSender[j + KCDefine.B_VAL_1_INT] = tVal;
			}
		}
	}

	/** 값을 안전 정렬한다 */
	public static void ExStableSort<T>(this List<T> a_oSender, System.Comparison<T> a_oCompare, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oCompare != null));

		// 리스트가 존재 할 경우
		if(a_oSender != null && a_oCompare != null) {
			for(int i = 1; i < a_oSender.Count; ++i) {
				int j = 0;
				var tVal = a_oSender[i];

				for(j = i - 1; j >= KCDefine.B_VAL_0_INT && a_oCompare(a_oSender[j], tVal) > KCDefine.B_COMPARE_EQUALS; --j) {
					a_oSender[j + KCDefine.B_VAL_1_INT] = a_oSender[j];
				}

				a_oSender[j + KCDefine.B_VAL_1_INT] = tVal;
			}
		}
	}

	/** 값을 안전 정렬한다 */
	public static void ExStableSort<V>(this Dictionary<int, V> a_oSender, System.Comparison<V> a_oCompare, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oCompare != null));

		// 리스트가 존재 할 경우
		if(a_oSender != null && a_oCompare != null) {
			for(int i = 1; i < a_oSender.Count; ++i) {
				int j = 0;
				var tVal = a_oSender[i];

				for(j = i - 1; j >= KCDefine.B_VAL_0_INT && a_oCompare(a_oSender[j], tVal) > KCDefine.B_COMPARE_EQUALS; --j) {
					a_oSender[j + KCDefine.B_VAL_1_INT] = a_oSender[j];
				}

				a_oSender[j + KCDefine.B_VAL_1_INT] = tVal;
			}
		}
	}

	/** 문자열 => 열거형 값으로 변환한다 */
	public static T ExToEnumVal<T>(this string a_oSender, bool a_bIsIgnoreCase = false) where T : struct {
		return (T)System.Enum.Parse(typeof(T), a_oSender, a_bIsIgnoreCase);
	}

	/** 문자열 => 열거형 값으로 변환한다 */
	public static bool ExToTryEnumVal<T>(this string a_oSender, out T a_tEnumVal, bool a_bIsIgnoreCase = false) where T : struct {
		return System.Enum.TryParse<T>(a_oSender, a_bIsIgnoreCase, out a_tEnumVal);
	}

	/** 배열 => 문자열로 변환한다 */
	public static string ExToStr<T>(this T[] a_oSender, string a_oToken) {
		CAccess.Assert(a_oSender != null && a_oToken.ExIsValid());
		var oStrBuilder = new System.Text.StringBuilder();

		for(int i = 0; i < a_oSender.Length; ++i) {
			oStrBuilder.Append(a_oSender[i]);
			oStrBuilder.Append((i < a_oSender.Length - KCDefine.B_VAL_1_INT) ? a_oToken : string.Empty);
		}

		return oStrBuilder.ToString();
	}

	/** 리스트 => 문자열로 변환한다 */
	public static string ExToStr<T>(this List<T> a_oSender, string a_oToken) {
		CAccess.Assert(a_oSender != null && a_oToken.ExIsValid());
		var oStrBuilder = new System.Text.StringBuilder();

		for(int i = 0; i < a_oSender.Count; ++i) {
			oStrBuilder.Append(a_oSender[i]);
			oStrBuilder.Append((i < a_oSender.Count - KCDefine.B_VAL_1_INT) ? a_oToken : string.Empty);
		}

		return oStrBuilder.ToString();
	}

	/** 사전 => 문자열로 변환한다 */
	public static string ExToStr<K, V>(this Dictionary<K, V> a_oSender, string a_oToken) {
		CAccess.Assert(a_oSender != null && a_oToken.ExIsValid());

		int i = KCDefine.B_VAL_0_INT;
		var oStrBuilder = new System.Text.StringBuilder();

		foreach(var stKeyVal in a_oSender) {
			oStrBuilder.AppendFormat(KCDefine.B_TEXT_FMT_DICT, stKeyVal.Key, stKeyVal.Value);
			oStrBuilder.Append((i < a_oSender.Count - KCDefine.B_VAL_1_INT) ? a_oToken : string.Empty);

			i += KCDefine.B_VAL_1_INT;
		}

		return oStrBuilder.ToString();
	}

	/** 객체 => 특정 타입으로 변환한다 */
	public static List<T> ExToTypes<T>(this object[] a_oSender) where T : class {
		CAccess.Assert(a_oSender != null);
		var oTypeList = new List<T>();

		for(int i = 0; i < a_oSender.Length; ++i) {
			// 타입 추가가 가능 할 경우
			if(a_oSender[i] as T != null) {
				oTypeList.Add(a_oSender[i] as T);
			}
		}

		return oTypeList;
	}

	/** 객체 => 특정 타입으로 변환한다 */
	public static List<TDest> ExToTypes<TSrc, TDest>(this List<TSrc> a_oSender) where TSrc : class where TDest : class {
		CAccess.Assert(a_oSender != null);
		var oTypeList = new List<TDest>();

		for(int i = 0; i < a_oSender.Count; ++i) {
			// 타입 추가가 가능 할 경우
			if(a_oSender[i] as TDest != null) {
				oTypeList.Add(a_oSender[i] as TDest);
			}
		}

		return oTypeList;
	}

	/** 객체 => 특정 타입으로 변환한다 */
	public static Dictionary<KDest, VDest> ExToTypes<KSrc, VSrc, KDest, VDest>(this Dictionary<KSrc, VSrc> a_oSender) where KSrc : class where VSrc : class where KDest : class where VDest : class {
		CAccess.Assert(a_oSender != null);
		var oTypeDict = new Dictionary<KDest, VDest>();

		foreach(var stKeyVal in a_oSender) {
			// 타입 추가가 가능 할 경우
			if(stKeyVal.Key as KDest != null && stKeyVal.Value as VDest != null) {
				oTypeDict.Add(stKeyVal.Key as KDest, stKeyVal.Value as VDest);
			}
		}

		return oTypeDict;
	}

	/** 객체 => 특정 리스트 타입으로 변환한다 */
	public static List<TDest> ExToListTypes<KSrc, VSrc, TDest>(this Dictionary<KSrc, VSrc> a_oSender) where KSrc : class where VSrc : class where TDest : class {
		CAccess.Assert(a_oSender != null);
		var oTypeList = new List<TDest>();

		foreach(var stKeyVal in a_oSender) {
			// 타입 추가가 가능 할 경우
			if(stKeyVal.Value as TDest != null) {
				oTypeList.Add(stKeyVal.Value as TDest);
			}
		}

		return oTypeList;
	}

	/** 리스트 => 딕셔너리로 변환한다 */
	public static Dictionary<K, V> ExToDict<T, K, V>(this List<T> a_oSender, System.Func<int, (K, V)> a_oCallback) {
		CAccess.Assert(a_oSender != null);
		var oDict = new Dictionary<K, V>();

		for(int i = 0; i < a_oSender.Count; ++i) {
			var stResult = a_oCallback(i);
			oDict.TryAdd(stResult.Item1, stResult.Item2);
		}

		return oDict;
	}

	/** 함수를 호출한다 */
	public static object ExCallFunc<T>(this object a_oSender, 
		string a_oName, List<object> a_oParamsList, BindingFlags a_eBindingFlags = KCDefine.B_BINDING_F_PUBLIC_INSTANCE) {

		CAccess.Assert(a_oName.ExIsValid());
		return typeof(T).GetMethod(a_oName, a_eBindingFlags).Invoke(a_oSender, a_oParamsList.ToArray());
	}

	/** 런타임 함수를 호출한다 */
	public static object ExRuntimeCallFunc<T>(this object a_oSender, string a_oName, List<object> a_oParamsList) {
		CAccess.Assert(a_oName.ExIsValid());
		var oMethodInfos = typeof(T).GetRuntimeMethods();

		foreach(var oMethodInfo in oMethodInfos) {
			// 메서드 이름이 동일 할 경우
			if(oMethodInfo.Name.Equals(a_oName)) {
				return oMethodInfo.Invoke(a_oSender, a_oParamsList.ToArray());
			}
		}

		return null;
	}
	#endregion // 제네릭 클래스 함수
}
