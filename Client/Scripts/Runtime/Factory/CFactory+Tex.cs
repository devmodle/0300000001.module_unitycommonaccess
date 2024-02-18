using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/** 텍스처 팩토리 */
public static partial class CFactory {
	#region 클래스 함수
	/** 스프라이트를 생성한다 */
	public static Sprite MakeSprite(string a_oName, 
		Texture2D a_oTex2D, Rect a_stRect, Vector3 a_stPivot, float a_fPixelsPerUnit = KCDefine.B_UNIT_PIXELS_PER_UNIT) {
			
		var oSprite = Sprite.Create(a_oTex2D, a_stRect, a_stPivot, a_fPixelsPerUnit);
		oSprite.name = a_oName;

		return oSprite;
	}

	/** 텍스처를 생성한다 */
	public static Texture2D MakeTex2D(string a_oName, 
		Vector3Int a_stSize, TextureFormat a_eFmt = TextureFormat.RGBA32, bool a_bIsEnableMipMap = true) {

		var oTex2D = new Texture2D(a_stSize.x, a_stSize.y, a_eFmt, a_bIsEnableMipMap);
		oTex2D.name = a_oName;

		return oTex2D;
	}
	#endregion // 클래스 함수
}
