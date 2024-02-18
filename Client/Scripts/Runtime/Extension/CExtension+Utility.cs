using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;

using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using TMPro;

using MessagePack;
using DG.Tweening;
using Coffee.UIExtensions;
using DanielLochner.Assets.SimpleScrollSnap;

#if UNITY_ANDROID
using UnityEngine.Android;
#endif // #if UNITY_ANDROID

#if NEWTON_SOFT_JSON_SERIALIZE_DESERIALIZE_ENABLE
using Newtonsoft.Json;
#endif // #if NEWTON_SOFT_JSON_SERIALIZE_DESERIALIZE_ENABLE

/** 유틸리티 확장 클래스 */
public static partial class CExtension {
	#region 클래스 함수
	/** 상태를 리셋한다 */
	public static void ExReset(this Text a_oSender, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 텍스트가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.fontSize = (a_oSender.fontSize <= KCDefine.U_DEF_MIN_SIZE_FONT) ? KCDefine.U_DEF_MIN_SIZE_FONT : a_oSender.fontSize;
			a_oSender.resizeTextMinSize = KCDefine.U_DEF_MIN_SIZE_FONT;
			a_oSender.resizeTextMaxSize = (a_oSender.fontSize <= KCDefine.U_DEF_MAX_SIZE_FONT) ? KCDefine.U_DEF_MAX_SIZE_FONT : a_oSender.fontSize;

			a_oSender.alignment = TextAnchor.MiddleLeft;
			a_oSender.verticalOverflow = VerticalWrapMode.Truncate;
			a_oSender.horizontalOverflow = HorizontalWrapMode.Wrap;

			// 컨텐츠 크기 조정자가 없을 경우
			if(!a_oSender.TryGetComponent(out ContentSizeFitter oContentsSizeFitter)) {
				oContentsSizeFitter = a_oSender.gameObject.ExAddComponent<ContentSizeFitter>();
				oContentsSizeFitter.verticalFit = a_oSender.rectTransform.anchorMin.y.ExIsEquals(a_oSender.rectTransform.anchorMax.y) ? ContentSizeFitter.FitMode.PreferredSize : ContentSizeFitter.FitMode.Unconstrained;
				oContentsSizeFitter.horizontalFit = a_oSender.rectTransform.anchorMin.x.ExIsEquals(a_oSender.rectTransform.anchorMax.x) ? ContentSizeFitter.FitMode.PreferredSize : ContentSizeFitter.FitMode.Unconstrained;
			}
		}
	}

	/** 상태를 리셋한다 */
	public static void ExReset(this TMP_Text a_oSender, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// TMP 텍스트가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.fontSize = (a_oSender.fontSize <= KCDefine.U_DEF_MIN_SIZE_FONT) ? KCDefine.U_DEF_MIN_SIZE_FONT : a_oSender.fontSize;
			a_oSender.fontSizeMin = KCDefine.U_DEF_MIN_SIZE_FONT;
			a_oSender.fontSizeMax = (a_oSender.fontSize <= KCDefine.U_DEF_MAX_SIZE_FONT) ? KCDefine.U_DEF_MAX_SIZE_FONT : a_oSender.fontSize;

			a_oSender.enableWordWrapping = true;

			a_oSender.overflowMode = TextOverflowModes.Truncate;
			a_oSender.verticalAlignment = VerticalAlignmentOptions.Middle;
			a_oSender.horizontalAlignment = HorizontalAlignmentOptions.Left;

			// 컨텐츠 크기 조정자가 없을 경우
			if(!a_oSender.TryGetComponent(out ContentSizeFitter oContentsSizeFitter)) {
				oContentsSizeFitter = a_oSender.gameObject.ExAddComponent<ContentSizeFitter>();
				oContentsSizeFitter.verticalFit = a_oSender.rectTransform.anchorMin.y.ExIsEquals(a_oSender.rectTransform.anchorMax.y) ? ContentSizeFitter.FitMode.PreferredSize : ContentSizeFitter.FitMode.Unconstrained;
				oContentsSizeFitter.horizontalFit = a_oSender.rectTransform.anchorMin.x.ExIsEquals(a_oSender.rectTransform.anchorMax.x) ? ContentSizeFitter.FitMode.PreferredSize : ContentSizeFitter.FitMode.Unconstrained;
			}
		}
	}

	/** 상태를 리셋한다 */
	public static void ExReset(this Image a_oSender, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 이미지가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.pixelsPerUnitMultiplier = KCDefine.B_VAL_1_REAL;
		}
	}

	/** 상태를 리셋한다 */
	public static void ExReset(this Mask a_oSender, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 마스크가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.showMaskGraphic = false;
		}
	}

	/** 상태를 리셋한다 */
	public static void ExReset(this Selectable a_oSender, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 상호 작용자가 존재 할 경우
		if(a_oSender != null) {
			var stColorBlock = a_oSender.colors;
			stColorBlock.fadeDuration = KCDefine.U_DURATION_ANI;
			stColorBlock.colorMultiplier = KCDefine.B_VAL_1_REAL;

			a_oSender.colors = stColorBlock;
			a_oSender.colors = a_oSender.colors.ExGetNormColorBlock(KCDefine.B_COLOR_NORM);
			a_oSender.colors = a_oSender.colors.ExGetPressColorBlock(KCDefine.B_COLOR_PRESS);
			a_oSender.colors = a_oSender.colors.ExGetSelColorBlock(KCDefine.B_COLOR_SEL);
			a_oSender.colors = a_oSender.colors.ExGetHighlightColorBlock(KCDefine.B_COLOR_HIGHLIGHT);
			a_oSender.colors = a_oSender.colors.ExGetDisableColorBlock(KCDefine.B_COLOR_DISABLE);
		}
	}

	/** 상태를 리셋한다 */
	public static void ExReset(this ScrollRect a_oSender, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 리셋이 불가능 할 경우
		if(a_oSender == null) {
			return;
		}

		a_oSender.scrollSensitivity = KCDefine.U_UNIT_SCROLL_SENSITIVITY;
	}

	/** 상태를 리셋한다 */
	public static void ExReset(this Canvas a_oSender, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 리셋이 불가능 할 경우
		if(a_oSender == null) {
			return;
		}

		a_oSender.pixelPerfect = false;
		a_oSender.targetDisplay = KCDefine.B_VAL_0_INT;
		a_oSender.additionalShaderChannels = (AdditionalCanvasShaderChannels)int.MaxValue;
	}

	/** 상태를 리셋한다 */
	public static void ExReset(this CanvasScaler a_oSender, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 리셋이 불가능 할 경우
		if(a_oSender == null) {
			return;
		}

		a_oSender.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
		a_oSender.referencePixelsPerUnit = KCDefine.B_UNIT_REF_PIXELS_PER_UNIT;
	}

	/** 상태를 리셋한다 */
	public static void ExReset(this CanvasRenderer a_oSender, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 리셋이 불가능 할 경우
		if(a_oSender == null) {
			return;
		}

		a_oSender.cullTransparentMesh = true;
	}

	/** 상태를 리셋한다 */
	public static void ExReset(this HorizontalOrVerticalLayoutGroup a_oSender, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 레이아웃 그룹이 존재 할 경우
		if(a_oSender != null) {
			a_oSender.childScaleWidth = false;
			a_oSender.childScaleHeight = false;

			a_oSender.childControlWidth = true;
			a_oSender.childControlHeight = true;

			a_oSender.childForceExpandWidth = true;
			a_oSender.childForceExpandHeight = true;

			a_oSender.childAlignment = TextAnchor.UpperLeft;
			a_oSender.gameObject.ExSetSizeDelta((a_oSender.transform.parent.GetComponentInParent<HorizontalOrVerticalLayoutGroup>() != null) ? a_oSender.gameObject.ExGetSizeDelta() : Vector3.zero, a_bIsAssert);

			// 컨텐츠 크기 조정자가 존재 할 경우
			if(a_oSender.TryGetComponent(out ContentSizeFitter oContentsSizeFitter)) {
				oContentsSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
				oContentsSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			}
		}
	}

	/** 상태를 리셋한다 */
	public static void ExReset(this Camera a_oSender, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 카메라가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.allowHDR = true;
			a_oSender.allowDynamicResolution = false;

			a_oSender.useOcclusionCulling = true;
			a_oSender.usePhysicalProperties = false;

			a_oSender.targetDisplay = KCDefine.B_VAL_0_INT;
			a_oSender.stereoTargetEye = StereoTargetEyeMask.Both;

			a_oSender.rect = KCDefine.B_RECT_CAMERA_VIEWPORT;
			a_oSender.allowMSAA = CAccess.IsSupportsMSAA;
			a_oSender.farClipPlane = KCDefine.U_DISTANCE_CAMERA_FAR_PLANE;
			a_oSender.nearClipPlane = KCDefine.U_DISTANCE_CAMERA_NEAR_PLANE;
		}
	}

	/** 상태를 리셋한다 */
	public static void ExReset(this Renderer a_oSender, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 렌더러가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.receiveShadows = true;
			a_oSender.staticShadowCaster = true;
			a_oSender.allowOcclusionWhenDynamic = true;

			a_oSender.lightProbeUsage = LightProbeUsage.BlendProbes;
			a_oSender.shadowCastingMode = ShadowCastingMode.On;
		}
	}

	/** 상태를 리셋한다 */
	public static void ExReset(this LineRenderer a_oSender, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 라인 효과가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.useWorldSpace = false;

			a_oSender.alignment = LineAlignment.TransformZ;
			a_oSender.textureMode = LineTextureMode.Tile;
		}
	}

	/** 상태를 리셋한다 */
	public static void ExReset(this ParticleSystem a_oSender, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 파티클 효과가 존재 할 경우
		if(a_oSender != null) {
			var oMainModule = a_oSender.main;
			oMainModule.prewarm = true;
			oMainModule.playOnAwake = false;
			oMainModule.stopAction = (a_oSender.transform.parent.GetComponentInParent<ParticleSystem>() != null) ? ParticleSystemStopAction.None : ParticleSystemStopAction.Callback;
			oMainModule.cullingMode = ParticleSystemCullingMode.Automatic;
			oMainModule.scalingMode = ParticleSystemScalingMode.Hierarchy;
		}
	}

	/** 상태를 리셋한다 */
	public static void ExReset(this GameObject a_oSender, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.localScale = Vector3.one;
			a_oSender.transform.localEulerAngles = Vector3.zero;
			a_oSender.transform.localPosition = Vector3.zero;
		}
	}

	/** 비율을 추가한다 */
	public static void ExAddScale(this GameObject a_oSender, Vector3 a_stScale, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.localScale += a_stScale;
		}
	}

	/** X 축 비율을 추가한다 */
	public static void ExAddScaleX(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddScale(new Vector3(a_fVal, KCDefine.B_VAL_0_REAL, KCDefine.B_VAL_0_REAL), a_bIsAssert);
	}

	/** Y 축 비율을 추가한다 */
	public static void ExAddScaleY(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddScale(new Vector3(KCDefine.B_VAL_0_REAL, a_fVal, KCDefine.B_VAL_0_REAL), a_bIsAssert);
	}

	/** Z 축 비율을 추가한다 */
	public static void ExAddScaleZ(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddScale(new Vector3(KCDefine.B_VAL_0_REAL, KCDefine.B_VAL_0_REAL, a_fVal), a_bIsAssert);
	}

	/** 월드 각도를 추가한다 */
	public static void ExAddWorldAngle(this GameObject a_oSender, Vector3 a_stAngle, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.eulerAngles += a_stAngle;
		}
	}

	/** 월드 X 축 각도를 추가한다 */
	public static void ExAddWorldAngleX(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddWorldAngle(new Vector3(a_fVal, KCDefine.B_VAL_0_REAL, KCDefine.B_VAL_0_REAL), a_bIsAssert);
	}

	/** 월드 Y 축 각도를 추가한다 */
	public static void ExAddWorldAngleY(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddWorldAngle(new Vector3(KCDefine.B_VAL_0_REAL, a_fVal, KCDefine.B_VAL_0_REAL), a_bIsAssert);
	}

	/** 월드 Z 축 각도를 추가한다 */
	public static void ExAddWorldAngleZ(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddWorldAngle(new Vector3(KCDefine.B_VAL_0_REAL, KCDefine.B_VAL_0_REAL, a_fVal), a_bIsAssert);
	}

	/** 로컬 각도를 추가한다 */
	public static void ExAddLocalAngle(this GameObject a_oSender, Vector3 a_stAngle, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.localEulerAngles += a_stAngle;
		}
	}

	/** 로컬 X 축 각도를 추가한다 */
	public static void ExAddLocalAngleX(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddLocalAngle(new Vector3(a_fVal, KCDefine.B_VAL_0_REAL, KCDefine.B_VAL_0_REAL), a_bIsAssert);
	}

	/** 로컬 Y 축 각도를 추가한다 */
	public static void ExAddLocalAngleY(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddLocalAngle(new Vector3(KCDefine.B_VAL_0_REAL, a_fVal, KCDefine.B_VAL_0_REAL), a_bIsAssert);
	}

	/** 로컬 Z 축 각도를 추가한다 */
	public static void ExAddLocalAngleZ(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddLocalAngle(new Vector3(KCDefine.B_VAL_0_REAL, KCDefine.B_VAL_0_REAL, a_fVal), a_bIsAssert);
	}

	/** 월드 각도를 추가한다 */
	public static void ExAddWorldAxisAngle(this GameObject a_oSender, float a_fVal, Vector3 a_stAxis, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.rotation = Quaternion.AngleAxis(a_fVal, a_stAxis.normalized) * a_oSender.transform.rotation;
		}
	}

	/** 월드 X 축 각도를 추가한다 */
	public static void ExAddWorldAxisAngleX(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddWorldAxisAngle(a_fVal, Vector3.right, a_bIsAssert);
	}

	/** 월드 Y 축 각도를 추가한다 */
	public static void ExAddWorldAxisAngleY(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddWorldAxisAngle(a_fVal, Vector3.up, a_bIsAssert);
	}

	/** 월드 Z 축 각도를 추가한다 */
	public static void ExAddWorldAxisAngleZ(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddWorldAxisAngle(a_fVal, Vector3.forward, a_bIsAssert);
	}

	/** 로컬 각도를 추가한다 */
	public static void ExAddLocalAxisAngle(this GameObject a_oSender, float a_fVal, Vector3 a_stAxis, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.localRotation = Quaternion.AngleAxis(a_fVal, a_stAxis) * a_oSender.transform.localRotation;
		}
	}

	/** 로컬 X 축 각도를 추가한다 */
	public static void ExAddLocalAxisAngleX(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddLocalAxisAngle(a_fVal, a_oSender.transform.right, a_bIsAssert);
	}

	/** 로컬 Y 축 각도를 추가한다 */
	public static void ExAddLocalAxisAngleY(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddLocalAxisAngle(a_fVal, a_oSender.transform.up, a_bIsAssert);
	}

	/** 로컬 Z 축 각도를 추가한다 */
	public static void ExAddLocalAxisAngleZ(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddLocalAxisAngle(a_fVal, a_oSender.transform.forward, a_bIsAssert);
	}

	/** 월드 위치를 추가한다 */
	public static void ExAddWorldPos(this GameObject a_oSender, Vector3 a_stPos, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.position += a_stPos;
		}
	}

	/** 월드 X 축 위치를 추가한다 */
	public static void ExAddWorldPosX(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddWorldPos(new Vector3(a_fVal, KCDefine.B_VAL_0_REAL, KCDefine.B_VAL_0_REAL), a_bIsAssert);
	}

	/** 월드 Y 축 위치를 추가한다 */
	public static void ExAddWorldPosY(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddWorldPos(new Vector3(KCDefine.B_VAL_0_REAL, a_fVal, KCDefine.B_VAL_0_REAL), a_bIsAssert);
	}

	/** 월드 Z 축 위치를 추가한다 */
	public static void ExAddWorldPosZ(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddWorldPos(new Vector3(KCDefine.B_VAL_0_REAL, KCDefine.B_VAL_0_REAL, a_fVal), a_bIsAssert);
	}

	/** 로컬 위치를 추가한다 */
	public static void ExAddLocalPos(this GameObject a_oSender, Vector3 a_stPos, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.localPosition += a_stPos;
		}
	}

	/** 로컬 X 축 위치를 추가한다 */
	public static void ExAddLocalPosX(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddLocalPos(new Vector3(a_fVal, KCDefine.B_VAL_0_REAL, KCDefine.B_VAL_0_REAL), a_bIsAssert);
	}

	/** 로컬 Y 축 위치를 추가한다 */
	public static void ExAddLocalPosY(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddLocalPos(new Vector3(KCDefine.B_VAL_0_REAL, a_fVal, KCDefine.B_VAL_0_REAL), a_bIsAssert);
	}

	/** 로컬 Z 축 위치를 추가한다 */
	public static void ExAddLocalPosZ(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddLocalPos(new Vector3(KCDefine.B_VAL_0_REAL, KCDefine.B_VAL_0_REAL, a_fVal), a_bIsAssert);
	}

	/** 월드 위치를 추가한다 */
	public static void ExAddWorldAxisPos(this GameObject a_oSender, float a_fVal, Vector3 a_stAxis, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.position += a_stAxis.normalized * a_fVal;
		}
	}

	/** 월드 X 축 위치를 추가한다 */
	public static void ExAddWorldAxisPosX(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddWorldAxisPos(a_fVal, Vector3.right, a_bIsAssert);
	}

	/** 월드 Y 축 위치를 추가한다 */
	public static void ExAddWorldAxisPosY(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddWorldAxisPos(a_fVal, Vector3.up, a_bIsAssert);
	}

	/** 월드 Z 축 위치를 추가한다 */
	public static void ExAddWorldAxisPosZ(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddWorldAxisPos(a_fVal, Vector3.forward, a_bIsAssert);
	}

	/** 로컬 위치를 추가한다 */
	public static void ExAddLocalAxisPos(this GameObject a_oSender, float a_fVal, Vector3 a_stAxis, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.localPosition += a_stAxis.normalized * a_fVal;
		}
	}

	/** 로컬 X 축 위치를 추가한다 */
	public static void ExAddLocalAxisPosX(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddLocalAxisPos(a_fVal, a_oSender.transform.right);
	}

	/** 로컬 Y 축 위치를 추가한다 */
	public static void ExAddLocalAxisPosY(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddLocalAxisPos(a_fVal, a_oSender.transform.up);
	}

	/** 로컬 Z 축 위치를 추가한다 */
	public static void ExAddLocalAxisPosZ(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddLocalAxisPos(a_fVal, a_oSender.transform.forward);
	}

	/** 크기 간격을 추가한다 */
	public static void ExAddSizeDelta(this GameObject a_oSender, Vector3 a_stSize, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oSender.transform as RectTransform != null));

		// 객체가 존재 할 경우
		if(a_oSender != null && a_oSender.transform as RectTransform != null) {
			(a_oSender.transform as RectTransform).sizeDelta += (Vector2)a_stSize;
		}
	}

	/** X 축 크기 간격을 추가한다 */
	public static void ExAddSizeDeltaX(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddSizeDelta(new Vector3(a_fVal, KCDefine.B_VAL_0_REAL, KCDefine.B_VAL_0_REAL), a_bIsAssert);
	}

	/** Y 축 크기 간격을 추가한다 */
	public static void ExAddSizeDeltaY(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddSizeDelta(new Vector3(KCDefine.B_VAL_0_REAL, a_fVal, KCDefine.B_VAL_0_REAL), a_bIsAssert);
	}

	/** 앵커 위치를 추가한다 */
	public static void ExAddAnchorPos(this GameObject a_oSender, Vector3 a_stPos, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oSender.transform as RectTransform != null));

		// 객체가 존재 할 경우
		if(a_oSender != null && a_oSender.transform as RectTransform != null) {
			(a_oSender.transform as RectTransform).anchoredPosition += (Vector2)a_stPos;
		}
	}

	/** X 축 앵커 위치를 추가한다 */
	public static void ExAddAnchorPosX(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddAnchorPos(new Vector3(a_fVal, KCDefine.B_VAL_0_REAL, KCDefine.B_VAL_0_REAL), a_bIsAssert);
	}

	/** Y 축 앵커 위치를 추가한다 */
	public static void ExAddAnchorPosY(this GameObject a_oSender, float a_fVal, bool a_bIsAssert = true) {
		a_oSender?.ExAddAnchorPos(new Vector3(KCDefine.B_VAL_0_REAL, a_fVal, KCDefine.B_VAL_0_REAL), a_bIsAssert);
	}

	/** 리스너를 추가한다 */
	public static void ExAddListener(this Button a_oSender, UnityAction a_oCallback, bool a_bIsReset = true, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oCallback != null));

		// 버튼이 존재 할 경우
		if(a_oSender != null && a_oCallback != null) {
			// 리셋 모드 일 경우
			if(a_bIsReset) {
				a_oSender.onClick.RemoveAllListeners();
			}

			a_oSender.onClick.AddListener(a_oCallback);
		}
	}

	/** 리스너를 추가한다 */
	public static void ExAddListener(this Slider a_oSender, UnityAction<float> a_oCallback, bool a_bIsReset = true, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oCallback != null));

		// 슬라이더가 존재 할 경우
		if(a_oSender != null && a_oCallback != null) {
			// 리셋 모드 일 경우
			if(a_bIsReset) {
				a_oSender.onValueChanged.RemoveAllListeners();
			}

			a_oSender.onValueChanged.AddListener(a_oCallback);
		}
	}

	/** 리스너를 추가한다 */
	public static void ExAddListener(this Scrollbar a_oSender, UnityAction<float> a_oCallback, bool a_bIsReset = true, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oCallback != null));

		// 스크롤 바가 존재 할 경우
		if(a_oSender != null && a_oCallback != null) {
			// 리셋 모드 일 경우
			if(a_bIsReset) {
				a_oSender.onValueChanged.RemoveAllListeners();
			}

			a_oSender.onValueChanged.AddListener(a_oCallback);
		}
	}

	/** 리스너를 추가한다 */
	public static void ExAddListener(this Dropdown a_oSender, UnityAction<int> a_oCallback, bool a_bIsReset = true, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oCallback != null));

		// 드롭이 존재 할 경우
		if(a_oSender != null && a_oCallback != null) {
			// 리셋 모드 일 경우
			if(a_bIsReset) {
				a_oSender.onValueChanged.RemoveAllListeners();
			}

			a_oSender.onValueChanged.AddListener(a_oCallback);
		}
	}

	/** 리스너를 추가한다 */
	public static void ExAddListener(this TMP_Dropdown a_oSender, UnityAction<int> a_oCallback, bool a_bIsReset = true, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oCallback != null));

		// 드롭이 존재 할 경우
		if(a_oSender != null && a_oCallback != null) {
			// 리셋 모드 일 경우
			if(a_bIsReset) {
				a_oSender.onValueChanged.RemoveAllListeners();
			}

			a_oSender.onValueChanged.AddListener(a_oCallback);
		}
	}

	/** 리스너를 추가한다 */
	public static void ExAddListener(this InputField a_oSender, UnityAction<string> a_oCallback, bool a_bIsReset = true, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oCallback != null));

		// 입력 필드가 존재 할 경우
		if(a_oSender != null && a_oCallback != null) {
			// 리셋 모드 일 경우
			if(a_bIsReset) {
				a_oSender.onValueChanged.RemoveAllListeners();
			}

			a_oSender.onValueChanged.AddListener(a_oCallback);
		}
	}

	/** 리스너를 추가한다 */
	public static void ExAddListener(this TMP_InputField a_oSender, UnityAction<string> a_oCallback, bool a_bIsReset = true, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oCallback != null));

		// 입력 필드가 존재 할 경우
		if(a_oSender != null && a_oCallback != null) {
			// 리셋 모드 일 경우
			if(a_bIsReset) {
				a_oSender.onValueChanged.RemoveAllListeners();
			}

			a_oSender.onValueChanged.AddListener(a_oCallback);
		}
	}

	/** 리스너를 추가한다 */
	public static void ExAddListener(this SimpleScrollSnap a_oSender, UnityAction<int, int> a_oCallback, bool a_bIsReset = true, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oCallback != null));

		// 스크롤 스냅이 존재 할 경우
		if(a_oSender != null && a_oCallback != null) {
			// 리셋 모드 일 경우
			if(a_bIsReset) {
				a_oSender.OnPanelCentered.RemoveAllListeners();
			}

			a_oSender.OnPanelCentered.AddListener(a_oCallback);
		}
	}

	/** 편집 종료 리스너를 추가한다 */
	public static void ExAddEndEditListener(this InputField a_oSender, UnityAction<string> a_oCallback, bool a_bIsReset = true, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oCallback != null));

		// 입력 필드가 존재 할 경우
		if(a_oSender != null && a_oCallback != null) {
			// 리셋 모드 일 경우
			if(a_bIsReset) {
				a_oSender.onEndEdit.RemoveAllListeners();
			}

			a_oSender.onEndEdit.AddListener(a_oCallback);
		}
	}

	/** 편집 종료 리스너를 추가한다 */
	public static void ExAddEndEditListener(this TMP_InputField a_oSender, UnityAction<string> a_oCallback, bool a_bIsReset = true, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oCallback != null));

		// 입력 필드가 존재 할 경우
		if(a_oSender != null && a_oCallback != null) {
			// 리셋 모드 일 경우
			if(a_bIsReset) {
				a_oSender.onEndEdit.RemoveAllListeners();
			}

			a_oSender.onEndEdit.AddListener(a_oCallback);
		}
	}

	/** 효과를 실행한다 */
	public static void ExPlay(this AudioSource a_oSender, AudioClip a_oClip, bool a_bIsLoop, bool a_bIs3DSnd, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oClip != null));

		// 오디오 소스가 존재 할 경우
		if(a_oSender != null && a_oClip != null) {
			a_oSender.clip = a_oClip;
			a_oSender.loop = a_bIsLoop;

			a_oSender.spread = a_bIs3DSnd ? KCDefine.B_ANGLE_360_DEG : KCDefine.B_ANGLE_0_DEG;
			a_oSender.dopplerLevel = a_bIs3DSnd ? KCDefine.B_VAL_1_REAL : KCDefine.B_VAL_0_REAL;
			a_oSender.spatialBlend = a_bIs3DSnd ? KCDefine.B_VAL_1_REAL : KCDefine.B_VAL_0_REAL;
			a_oSender.reverbZoneMix = a_bIs3DSnd ? KCDefine.B_VAL_1_REAL : KCDefine.B_VAL_0_REAL;

			a_oSender.Play();
		}
	}

	/** 효과를 실행한다 */
	public static void ExPlay(this Animation a_oSender, PlayMode a_ePlayMode = PlayMode.StopAll, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 애니메이션이 존재 할 경우
		if(a_oSender != null) {
			a_oSender.Stop();
			a_oSender.Play(a_ePlayMode);
		}
	}

	/** 효과를 실행한다 */
	public static void ExPlay(this ParticleSystem a_oSender, bool a_bIsPlayChildren = true, bool a_bIsStopChildren = true, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 파티클 효과가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.Stop(a_bIsStopChildren, ParticleSystemStopBehavior.StopEmittingAndClear);
			a_oSender.Play(a_bIsPlayChildren);
		}
	}

	/** 애니메이션을 시작한다 */
	public static Sequence ExStartAni(this CFXBase a_oSender, float a_fStartVal, float a_fEndVal, float a_fDuration, System.Action<CFXBase, Sequence> a_oCallback, Ease a_eEase = KCDefine.U_EASE_DEF, float a_fDelay = KCDefine.B_VAL_0_REAL, bool a_bIsRealtime = false) {
		CAccess.Assert(a_oSender != null);
		return CFactory.MakeSequence(CFactory.MakeAni(() => a_oSender.effectFactor, (a_fVal) => a_oSender.effectFactor = a_fVal, () => a_oSender.effectFactor = a_fStartVal, null, a_fEndVal, a_fDuration, a_eEase, a_bIsRealtime), (a_oAniSender) => a_oCallback?.Invoke(a_oSender, a_oAniSender), a_fDelay, a_bIsRealtime: a_bIsRealtime);
	}

	/** 게이지 애니메이션을 시작한다 */
	public static Sequence ExStartGaugeAni(this Image a_oSender, float a_fStartVal, float a_fEndVal, float a_fDuration, System.Action<Image, Sequence> a_oCallback, Ease a_eEase = KCDefine.U_EASE_DEF, float a_fDelay = KCDefine.B_VAL_0_REAL, bool a_bIsRealtime = false) {
		CAccess.Assert(a_oSender != null);
		return CFactory.MakeSequence(CFactory.MakeAni(() => a_oSender.fillAmount, (a_fVal) => a_oSender.fillAmount = a_fVal, () => a_oSender.fillAmount = a_fStartVal, null, a_fEndVal, a_fDuration, a_eEase, a_bIsRealtime), (a_oAniSender) => a_oCallback?.Invoke(a_oSender, a_oAniSender), a_fDelay, a_bIsRealtime: a_bIsRealtime);
	}

	/** 비율 애니메이션을 시작한다 */
	public static Sequence ExStartScaleAni(this GameObject a_oSender, Vector3 a_stScale, float a_fDuration, System.Action<GameObject, Sequence> a_oCallback, Ease a_eEase = KCDefine.U_EASE_DEF, float a_fDelay = KCDefine.B_VAL_0_REAL, bool a_bIsRealtime = false) {
		CAccess.Assert(a_oSender != null);
		return CFactory.MakeSequence(a_oSender.transform.DOScale(a_stScale, a_fDuration).SetAutoKill().SetEase(a_eEase).SetUpdate(a_bIsRealtime), (a_oAniSender) => a_oCallback?.Invoke(a_oSender, a_oAniSender), a_fDelay, a_bIsRealtime: a_bIsRealtime);
	}

	/** 월드 이동 애니메이션을 시작한다 */
	public static Sequence ExStartWorldMoveAni(this GameObject a_oSender, Vector3 a_stPos, float a_fDuration, System.Action<GameObject, Sequence> a_oCallback, Ease a_eEase = KCDefine.U_EASE_DEF, float a_fDelay = KCDefine.B_VAL_0_REAL, bool a_bIsRealtime = false) {
		CAccess.Assert(a_oSender != null);
		return CFactory.MakeSequence(a_oSender.transform.DOMove(a_stPos, a_fDuration).SetAutoKill().SetEase(a_eEase).SetUpdate(a_bIsRealtime), (a_oAniSender) => a_oCallback?.Invoke(a_oSender, a_oAniSender), a_fDelay, a_bIsRealtime: a_bIsRealtime);
	}

	/** 로컬 이동 애니메이션을 시작한다 */
	public static Sequence ExStartLocalMoveAni(this GameObject a_oSender, Vector3 a_stPos, float a_fDuration, System.Action<GameObject, Sequence> a_oCallback, Ease a_eEase = KCDefine.U_EASE_DEF, float a_fDelay = KCDefine.B_VAL_0_REAL, bool a_bIsRealtime = false) {
		CAccess.Assert(a_oSender != null);
		return CFactory.MakeSequence(a_oSender.transform.DOLocalMove(a_stPos, a_fDuration).SetAutoKill().SetEase(a_eEase).SetUpdate(a_bIsRealtime), (a_oAniSender) => a_oCallback?.Invoke(a_oSender, a_oAniSender), a_fDelay, a_bIsRealtime: a_bIsRealtime);
	}

	/** 월드 경로 애니메이션을 시작한다 */
	public static Tween ExStartWorldPathAni(this GameObject a_oSender, List<Vector3> a_oPosList, float a_fDuration, System.Action<GameObject, Sequence> a_oCallback, Ease a_eEase = KCDefine.U_EASE_DEF, bool a_bIsRealtime = false, bool a_bIsLinearPath = false, float a_fDelay = KCDefine.B_VAL_0_REAL) {
		CAccess.Assert(a_oSender != null && a_oPosList != null);
		return CFactory.MakeSequence(a_oSender.transform.DOPath(a_oPosList.ToArray(), a_fDuration, a_bIsLinearPath ? PathType.Linear : PathType.CatmullRom).SetAutoKill().SetEase(a_eEase).SetUpdate(a_bIsRealtime), (a_oAniSender) => a_oCallback?.Invoke(a_oSender, a_oAniSender), a_fDelay, a_bIsRealtime: a_bIsRealtime);
	}

	/** 로컬 경로 애니메이션을 시작한다 */
	public static Tween ExStartLocalPathAni(this GameObject a_oSender, List<Vector3> a_oPosList, float a_fDuration, System.Action<GameObject, Sequence> a_oCallback, Ease a_eEase = KCDefine.U_EASE_DEF, bool a_bIsRealtime = false, bool a_bIsLinearPath = false, float a_fDelay = KCDefine.B_VAL_0_REAL) {
		CAccess.Assert(a_oSender != null && a_oPosList != null);
		return CFactory.MakeSequence(a_oSender.transform.DOLocalPath(a_oPosList.ToArray(), a_fDuration, a_bIsLinearPath ? PathType.Linear : PathType.CatmullRom).SetAutoKill().SetEase(a_eEase).SetUpdate(a_bIsRealtime), (a_oAniSender) => a_oCallback?.Invoke(a_oSender, a_oAniSender), a_fDelay, a_bIsRealtime: a_bIsRealtime);
	}

	/** 종류 => 타입으로 변환한다 */
	public static int ExKindsToType(this int a_nSender) {
		return a_nSender.ExIsValidIdx() ? a_nSender / KCDefine.B_UNIT_KINDS_PER_TYPE : KCDefine.B_IDX_INVALID;
	}

	/** 종류 => 타입 종류로 변환한다 */
	public static int ExKindsToTypeKinds(this int a_nSender) {
		return a_nSender.ExIsValidIdx() ? a_nSender.ExKindsToType() * KCDefine.B_UNIT_KINDS_PER_TYPE : KCDefine.B_IDX_INVALID;
	}

	/** 종류 => 서브 타입으로 변환한다 */
	public static int ExKindsToSubType(this int a_nSender) {
		int nSubType = a_nSender % KCDefine.B_UNIT_KINDS_PER_TYPE;
		return a_nSender.ExIsValidIdx() ? a_nSender.ExKindsToTypeKinds() + ((nSubType / KCDefine.B_UNIT_KINDS_PER_SUB_TYPE) * KCDefine.B_UNIT_KINDS_PER_SUB_TYPE) : KCDefine.B_IDX_INVALID;
	}

	/** 종류 => 서브 타입 값으로 변환한다 */
	public static int ExKindsToSubTypeVal(this int a_nSender) {
		return a_nSender.ExIsValidIdx() ? (a_nSender % KCDefine.B_UNIT_KINDS_PER_TYPE) / KCDefine.B_UNIT_KINDS_PER_SUB_TYPE : KCDefine.B_IDX_INVALID;
	}

	/** 종류 => 종류 타입으로 변환한다 */
	public static int ExKindsToKindsType(this int a_nSender) {
		int nKindsType = a_nSender % KCDefine.B_UNIT_KINDS_PER_SUB_TYPE;
		return a_nSender.ExIsValidIdx() ? a_nSender.ExKindsToSubType() + ((nKindsType / KCDefine.B_UNIT_KINDS_PER_KINDS_TYPE) * KCDefine.B_UNIT_KINDS_PER_KINDS_TYPE) : KCDefine.B_IDX_INVALID;
	}

	/** 종류 => 종류 타입 값으로 변환한다 */
	public static int ExKindsToKindsTypeVal(this int a_nSender) {
		return a_nSender.ExIsValidIdx() ? (a_nSender % KCDefine.B_UNIT_KINDS_PER_TYPE) / KCDefine.B_UNIT_KINDS_PER_KINDS_TYPE : KCDefine.B_IDX_INVALID;
	}

	/** 종류 => 서브 종류 타입으로 변환한다 */
	public static int ExKindsToSubKindsType(this int a_nSender) {
		int nSubKindsType = a_nSender % KCDefine.B_UNIT_KINDS_PER_KINDS_TYPE;
		return a_nSender.ExIsValidIdx() ? a_nSender.ExKindsToKindsType() + ((nSubKindsType / KCDefine.B_UNIT_KINDS_PER_SUB_KINDS_TYPE) * KCDefine.B_UNIT_KINDS_PER_SUB_KINDS_TYPE) : KCDefine.B_IDX_INVALID;
	}

	/** 종류 => 서브 종류 타입 값으로 변환한다 */
	public static int ExKindsToSubKindsTypeVal(this int a_nSender) {
		return a_nSender.ExIsValidIdx() ? (a_nSender % KCDefine.B_UNIT_KINDS_PER_KINDS_TYPE) / KCDefine.B_UNIT_KINDS_PER_SUB_KINDS_TYPE : KCDefine.B_IDX_INVALID;
	}

	/** 종류 => 세부 서브 종류 타입으로 변환한다 */
	public static int ExKindsToDetailSubKindsType(this int a_nSender) {
		int nDetailSubKindsType = a_nSender % KCDefine.B_UNIT_KINDS_PER_SUB_KINDS_TYPE;
		return a_nSender.ExIsValidIdx() ? a_nSender.ExKindsToSubKindsType() + nDetailSubKindsType : KCDefine.B_IDX_INVALID;
	}

	/** 종류 => 세부 서브 종류 타입 값으로 변환한다 */
	public static int ExKindsToDetailSubKindsTypeVal(this int a_nSender) {
		return a_nSender.ExIsValidIdx() ? a_nSender % KCDefine.B_UNIT_KINDS_PER_SUB_KINDS_TYPE : KCDefine.B_IDX_INVALID;
	}

	/** 종류 => 보정 된 종류로 변환한다 */
	public static int ExKindsToCorrectKinds(this int a_nSender, EKindsGroupType a_eKindsGroupType) {
		switch(a_eKindsGroupType) {
			case EKindsGroupType.TYPE: return a_nSender.ExKindsToTypeKinds();
			case EKindsGroupType.SUB_TYPE: return a_nSender.ExKindsToSubType();
			case EKindsGroupType.KINDS_TYPE: return a_nSender.ExKindsToKindsType();
			case EKindsGroupType.SUB_KINDS_TYPE: return a_nSender.ExKindsToSubKindsType();
		}

		return a_nSender.ExKindsToDetailSubKindsType();
	}

	/** 고유 레벨 식별자 => 식별자로 변환한다 */
	public static int ExULevelIDToLevelID(this ulong a_nSender) {
		return (int)(a_nSender % KCDefine.B_UNIT_IDS_PER_IDS_02);
	}

	/** 고유 레벨 식별자 => 스테이지 식별자로 변환한다 */
	public static int ExULevelIDToStageID(this ulong a_nSender) {
		return (int)((a_nSender % KCDefine.B_UNIT_IDS_PER_IDS_03) / KCDefine.B_UNIT_IDS_PER_IDS_02);
	}

	/** 고유 레벨 식별자 => 챕터 식별자로 변환한다 */
	public static int ExULevelIDToChapterID(this ulong a_nSender) {
		return (int)(a_nSender / KCDefine.B_UNIT_IDS_PER_IDS_03);
	}

	/** 교유 레벨 식별자 => 식별자 정보로 변환한다 */
	public static STIDInfo ExULevelIDToIDInfo(this ulong a_nSender) {
		return new STIDInfo(a_nSender.ExULevelIDToLevelID(), a_nSender.ExULevelIDToStageID(), a_nSender.ExULevelIDToChapterID());
	}

	/** 로컬 => 월드로 변환한다 */
	public static Vector3 ExToWorld(this Vector2 a_stSender, GameObject a_oParent, bool a_bIsCoord = true, float a_fZ = KCDefine.B_VAL_0_REAL) {
		CAccess.Assert(a_oParent != null);
		return a_stSender.ExTo3D(a_fZ).ExToWorld(a_oParent, a_bIsCoord);
	}

	/** 로컬 => 월드로 변환한다 */
	public static Vector3 ExToWorld(this Vector3 a_stSender, GameObject a_oParent, bool a_bIsCoord = true) {
		CAccess.Assert(a_oParent != null);
		return a_oParent.transform.localToWorldMatrix * a_stSender.ExTo4D(a_bIsCoord);
	}

	/** 월드 => 캔버스로 변환한다 */
	public static Vector3 ExWorldToCanvas(this Vector2 a_stSender, Canvas a_oCanvas, GameObject a_oParent, float a_fZ = KCDefine.B_VAL_0_REAL) {
		CAccess.Assert(a_oCanvas != null);
		return a_stSender.ExTo3D(a_fZ).ExWorldToCanvas(a_oCanvas, a_oParent);
	}

	/** 월드 => 캔버스로 변환한다 */
	public static Vector3 ExWorldToCanvas(this Vector2 a_stSender, Canvas a_oCanvas, Camera a_oCamera, GameObject a_oParent, float a_fZ = KCDefine.B_VAL_0_REAL) {
		CAccess.Assert(a_oCanvas != null);
		return a_stSender.ExTo3D(a_fZ).ExWorldToCanvas(a_oCanvas, a_oCamera, a_oParent);
	}

	/** 월드 => 캔버스로 변환한다 */
	public static Vector3 ExWorldToCanvas(this Vector3 a_stSender, Canvas a_oCanvas, GameObject a_oParent) {
		CAccess.Assert(a_oCanvas != null);
		a_stSender.z = KCDefine.B_VAL_0_REAL;

		return ((a_stSender.ExGetScaleVec(a_oCanvas.transform.localScale / KCDefine.B_UNIT_SCALE) + a_oCanvas.transform.position)).ExToLocal(a_oParent);
	}

	/** 월드 => 캔버스로 변환한다 */
	public static Vector3 ExWorldToCanvas(this Vector3 a_stSender, Canvas a_oCanvas, Camera a_oCamera, GameObject a_oParent) {
		CAccess.Assert(a_oCanvas != null);
		return (a_oCamera.cameraToWorldMatrix * a_stSender.ExTo4D()).ExTo3D().ExWorldToCanvas(a_oCanvas, a_oParent);
	}

	/** 캔버스 => 월드로 변환한다 */
	public static Vector3 ExCanvasToWorld(this Vector2 a_stSender, Canvas a_oCanvas, GameObject a_oParent, float a_fZ = KCDefine.B_VAL_0_REAL) {
		CAccess.Assert(a_oCanvas != null);
		return a_stSender.ExTo3D(a_fZ).ExCanvasToWorld(a_oCanvas, a_oParent);
	}

	/** 캔버스 => 월드로 변환한다 */
	public static Vector3 ExCanvasToWorld(this Vector2 a_stSender, Canvas a_oCanvas, Camera a_oCamera, GameObject a_oParent, float a_fZ = KCDefine.B_VAL_0_REAL) {
		CAccess.Assert(a_oCanvas != null);
		return a_stSender.ExTo3D(a_fZ).ExCanvasToWorld(a_oCanvas, a_oCamera, a_oParent);
	}

	/** 캔버스 => 월드로 변환한다 */
	public static Vector3 ExCanvasToWorld(this Vector3 a_stSender, Canvas a_oCanvas, GameObject a_oParent) {
		CAccess.Assert(a_oCanvas != null);
		var stScale = new Vector3(KCDefine.B_VAL_1_REAL / a_oCanvas.transform.localScale.x, KCDefine.B_VAL_1_REAL / a_oCanvas.transform.localScale.y, KCDefine.B_VAL_1_REAL / a_oCanvas.transform.localScale.z);

		return ((a_stSender.ExGetScaleVec(stScale) - a_oCanvas.transform.position)).ExToLocal(a_oParent);
	}

	/** 캔버스 => 월드로 변환한다 */
	public static Vector3 ExCanvasToWorld(this Vector3 a_stSender, Canvas a_oCanvas, Camera a_oCamera, GameObject a_oParent) {
		CAccess.Assert(a_oCanvas != null);
		return (a_oCamera.worldToCameraMatrix * a_stSender.ExTo4D()).ExTo3D().ExCanvasToWorld(a_oCanvas, a_oParent);
	}

	/** 위치 => 인덱스로 변환한다 */
	public static Vector3Int ExToIdx(this Vector2 a_stSender, Vector3 a_stPivotPos, Vector3 a_stSize, int a_nZ = KCDefine.B_VAL_0_INT) {
		return a_stSender.ExTo3D(a_nZ).ExToIdx(a_stPivotPos, a_stSize);
	}

	/** 위치 => 인덱스로 변환한다 */
	public static Vector3Int ExToIdx(this Vector3 a_stSender, Vector3 a_stPivotPos, Vector3 a_stSize) {
		CAccess.Assert(a_stSize.x.ExIsGreatEquals(KCDefine.B_VAL_0_REAL) && a_stSize.y.ExIsGreatEquals(KCDefine.B_VAL_0_REAL) && a_stSize.z.ExIsGreatEquals(KCDefine.B_VAL_0_REAL));
		var stDelta = a_stSender - a_stPivotPos;

		return (stDelta.x.ExIsGreatEquals(KCDefine.B_VAL_0_REAL) && stDelta.y.ExIsLessEquals(KCDefine.B_VAL_0_REAL) && stDelta.z.ExIsLessEquals(KCDefine.B_VAL_0_REAL)) ? new Vector3Int((int)(stDelta.x / a_stSize.x), (int)(stDelta.y / -a_stSize.y), a_stSize.z.ExIsLessEquals(KCDefine.B_VAL_0_REAL) ? KCDefine.B_VAL_0_INT : (int)(stDelta.z / -a_stSize.z)) : KCDefine.B_IDX_INVALID_3D;
	}

	/** 인덱스 정보 => 인덱스로 변환한다 */
	public static Vector3Int ExToIdx(this STIdxInfo a_stSender) {
		return new Vector3Int(a_stSender.m_nIdx01, a_stSender.m_nIdx02, a_stSender.m_nIdx03);
	}

	/** 인덱스 => 위치로 변환한다 */
	public static Vector3 ExToPos(this Vector2Int a_stSender, Vector3 a_stOffset, Vector3 a_stSize, int a_nZ = KCDefine.B_VAL_0_INT) {
		return a_stSender.ExTo3D(a_nZ).ExToPos(a_stOffset, a_stSize);
	}

	/** 인덱스 => 위치로 변환한다 */
	public static Vector3 ExToPos(this Vector3Int a_stSender, Vector3 a_stOffset, Vector3 a_stSize) {
		return new Vector3((a_stSender.x * a_stSize.x) + a_stOffset.x, (a_stSender.y * -a_stSize.y) + a_stOffset.y, (a_stSender.z * -a_stSize.z) + a_stOffset.z);
	}

	/** 인덱스 => 인덱스 정보로 변환한다 */
	public static STIdxInfo ExToIdxInfo(this Vector2Int a_stSender, int a_nZ = KCDefine.B_VAL_0_INT) {
		return a_stSender.ExTo3D(a_nZ).ExToIdxInfo();
	}

	/** 인덱스 => 인덱스 정보로 변환한다 */
	public static STIdxInfo ExToIdxInfo(this Vector3Int a_stSender) {
		return new STIdxInfo(a_stSender.x, a_stSender.y, a_stSender.z);
	}

	/** 인덱스 정보 => 인덱스로 변환한다 */
	public static List<Vector3Int> ExToIndices(this List<STIdxInfo> a_oSender) {
		CAccess.Assert(a_oSender != null);
		var oIdxList = new List<Vector3Int>();

		for(int i = 0; i < a_oSender.Count; ++i) {
			oIdxList.Add(a_oSender[i].ExToIdx());
		}

		return oIdxList;
	}

	/** 인덱스 => 위치로 변환한다 */
	public static List<Vector3> ExToPositions(this List<Vector2Int> a_oSender, Vector3 a_stOffset, Vector3 a_stSize) {
		CAccess.Assert(a_oSender != null);
		var oPosList = new List<Vector3>();

		for(int i = 0; i < a_oSender.Count; ++i) {
			oPosList.Add(a_oSender[i].ExToPos(a_stOffset, a_stSize));
		}

		return oPosList;
	}

	/** 인덱스 => 위치로 변환한다 */
	public static List<Vector3> ExToPositions(this List<Vector3Int> a_oSender, Vector3 a_stOffset, Vector3 a_stSize) {
		CAccess.Assert(a_oSender != null);
		var oPosList = new List<Vector3>();

		for(int i = 0; i < a_oSender.Count; ++i) {
			oPosList.Add(a_oSender[i].ExToPos(a_stOffset, a_stSize));
		}

		return oPosList;
	}

	/** 인덱스 => 인덱스 정보로 변환한다 */
	public static List<STIdxInfo> ExToIdxInfos(this List<Vector3Int> a_oSender) {
		CAccess.Assert(a_oSender != null);
		var oIdxInfoList = new List<STIdxInfo>();

		for(int i = 0; i < a_oSender.Count; ++i) {
			oIdxInfoList.Add(a_oSender[i].ExToIdxInfo());
		}

		return oIdxInfoList;
	}

	/** 문자열 => Base64 문자열로 변환한다 */
	public static string ExToBase64Str(this string a_oSender, System.Text.Encoding a_oEncoding = null) {
		CAccess.Assert(a_oSender.ExIsValid());
		return System.Convert.ToBase64String((a_oEncoding ?? System.Text.Encoding.Default).GetBytes(a_oSender));
	}

	/** 문자열 => Base64 Zip 문자열로 변환한다 */
	public static string ExToBase64ZipStr(this string a_oSender, System.Text.Encoding a_oEncoding = null) {
		CAccess.Assert(a_oSender.ExIsValid());

		using(var oMemoryStream = new MemoryStream()) {
			using(var oGZipStream = new GZipStream(oMemoryStream, CompressionMode.Compress)) {
				var oBytes = (a_oEncoding ?? System.Text.Encoding.Default).GetBytes(a_oSender);
				oGZipStream.Write(oBytes, 0, oBytes.Length);
			}

			var oZipBytes = oMemoryStream.ToArray();
			return System.Convert.ToBase64String(oZipBytes, 0, oZipBytes.Length);
		}
	}

	/** Base64 문자열 => 문자열로 변환한다 */
	public static string ExBase64StrToStr(this string a_oSender, System.Text.Encoding a_oEncoding = null) {
		CAccess.Assert(a_oSender.ExIsValid());
		return (a_oEncoding ?? System.Text.Encoding.Default).GetString(System.Convert.FromBase64String(a_oSender));
	}

	/** 압축 된 문자열 => 문자열로 변환한다 */
	public static string ExBase64ZipStrToStr(this string a_oSender, System.Text.Encoding a_oEncoding = null) {
		CAccess.Assert(a_oSender.ExIsValid());
		var oBytes = System.Convert.FromBase64String(a_oSender);

		using(var oResultStream = new MemoryStream()) {
			using(var oMemoryStream = new MemoryStream(oBytes, 0, oBytes.Length)) {
				using(var oGZipStream = new GZipStream(oMemoryStream, CompressionMode.Decompress)) {
					oGZipStream.CopyTo(oResultStream);
				}

				oResultStream.Seek(0, SeekOrigin.Begin);
				return (a_oEncoding ?? System.Text.Encoding.Default).GetString(oResultStream.ToArray());
			}
		}
	}

	/** JSON 문자열 => JSON 배열로 변환한다 */
	public static SimpleJSON.JSONNode ExJSONStrToJSONArray(this string a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());

		var oTokens = Regex.Replace(a_oSender, KCDefine.B_PATTERN_SPACE, string.Empty).Split(KCDefine.B_TOKEN_COMMA);
		var oJSONArray = new SimpleJSON.JSONArray();

		for(int i = 0; i < oTokens.Length; ++i) {
			oJSONArray.Add(oTokens[i]);
		}

		return oJSONArray;
	}

	/** 유저 권한을 요청한다 */
	public static void ExRequestUserPermission(this MonoBehaviour a_oSender, string a_oPermission, System.Action<string, bool> a_oCallback, bool a_bIsRealtime = false, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || (a_oSender != null && a_oPermission.ExIsValid()));

		// 컴포넌트가 존재 할 경우
		if(a_oSender != null && a_oPermission.ExIsValid()) {
#if UNITY_ANDROID
			// 유저 권한이 유효 할 경우
			if(CAccess.IsEnableUserPermission(a_oPermission)) {
				a_oCallback?.Invoke(a_oPermission, true);
			} else {
				Permission.RequestUserPermission(a_oPermission);

				a_oSender.ExRepeatCallFunc((a_oSender, a_bIsComplete) => {
					// 완료되었을 경우
					if(a_bIsComplete) {
						a_oCallback?.Invoke(a_oPermission, CAccess.IsEnableUserPermission(a_oPermission));
					}

					return !CAccess.IsEnableUserPermission(a_oPermission);
				}, KCDefine.B_DELTA_T_PERMISSION_M_REQUEST_CHECK, KCDefine.B_MAX_DELTA_T_PERMISSION_M_REQUEST_CHECK, a_bIsRealtime);
			}
#else
			a_oCallback?.Invoke(a_oPermission, false);
#endif // #if UNITY_ANDROID
		}
	}

	/** 객체를 제거한다 */
	private static void RemoveObj(Object a_oObj, bool a_bIsRemoveAsset = false, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oObj != null);

		// 객체가 존재 할 경우
		if(a_oObj != null) {
			// 앱이 실행 중 일 경우
			if(Application.isPlaying) {
				GameObject.Destroy(a_oObj);
			} else {
				GameObject.DestroyImmediate(a_oObj, a_bIsRemoveAsset);
			}
		}
	}

	/** 함수를 지연 호출한다 */
	private static IEnumerator ExCoLateCallFunc(this MonoBehaviour a_oSender, System.Action<MonoBehaviour> a_oCallback) {
		CAccess.Assert(a_oSender != null);

		try {
			yield return CAccess.CoGetWaitForEndOfFrame();
		} finally {
			a_oCallback?.Invoke(a_oSender);
		}
	}

	/** 함수를 지연 호출한다 */
	private static IEnumerator ExCoLateCallFunc(this MonoBehaviour a_oSender, System.Action<MonoBehaviour> a_oCallback, float a_fDelay, bool a_bIsRealtime) {
		CAccess.Assert(a_oSender != null && a_fDelay.ExIsGreatEquals(KCDefine.B_VAL_0_REAL));

		try {
			yield return CAccess.CoGetWaitForSecs(a_fDelay, a_bIsRealtime);
		} finally {
			a_oCallback?.Invoke(a_oSender);
		}
	}

	/** 함수를 반복 호출한다 */
	private static IEnumerator ExCoRepeatCallFunc(this MonoBehaviour a_oSender, System.Func<MonoBehaviour, bool, bool> a_oCallback, float a_fDeltaTime, double a_dblMaxDeltaTime, bool a_bIsRealtime) {
		CAccess.Assert(a_oSender != null && a_oCallback != null && a_fDeltaTime.ExIsGreatEquals(KCDefine.B_VAL_0_REAL));

		var stStartTime = System.DateTime.Now;
		double dblDeltaTime = KCDefine.B_VAL_0_REAL;

		do {
			yield return CAccess.CoGetWaitForSecs(a_fDeltaTime, a_bIsRealtime);
			dblDeltaTime = System.DateTime.Now.ExGetDeltaTime(stStartTime);
		} while(a_oCallback(a_oSender, false) && dblDeltaTime.ExIsLess(a_dblMaxDeltaTime));

		a_oCallback(a_oSender, true);
	}
	#endregion // 클래스 함수

	#region 제네릭 클래스 함수
	/** 컴포넌트를 추가한다 */
	public static T ExAddComponent<T>(this GameObject a_oSender, bool a_bIsAssert = true) where T : Component {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);
		return (a_oSender != null) ? a_oSender.TryGetComponent(out T oComponent) ? oComponent : a_oSender.AddComponent<T>() : null;
	}

	/** 컴포넌트를 제거한다 */
	public static void ExRemoveComponent<T>(this GameObject a_oSender, bool a_bIsAssert = true) where T : Component {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			CExtension.RemoveObj(a_oSender.GetComponentInChildren<T>(), false, a_bIsAssert);
		}
	}

	/** 컴포넌트를 제거한다 */
	public static void ExRemoveComponents<T>(this GameObject a_oSender, bool a_bIsIncludeSelf = true, bool a_bIsAssert = true) where T : Component {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.ExEnumerateComponents<T>((a_oComponent) => {
				// 컴포넌트 제거가 가능 할 경우
				if(a_bIsIncludeSelf || a_oSender != a_oComponent.gameObject) {
					CExtension.RemoveObj(a_oComponent, false, a_bIsAssert);
				}

				return true;
			});
		}
	}

	/** 컴포넌트를 제거한다 */
	public static void ExRemoveComponentInParent<T>(this GameObject a_oSender, bool a_bIsAssert = true) where T : Component {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			CExtension.RemoveObj(a_oSender.GetComponentInParent<T>(), false, a_bIsAssert);
		}
	}

	/** 컴포넌트를 제거한다 */
	public static void ExRemoveComponentsInParent<T>(this GameObject a_oSender, bool a_bIsIncludeSelf = true, bool a_bIsAssert = true) where T : Component {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			var oComponents = a_oSender.GetComponentsInParent<T>();

			for(int i = 0; i < oComponents.Length; ++i) {
				// 컴포넌트 제거가 가능 할 경우
				if(a_bIsIncludeSelf || a_oSender != oComponents[i].gameObject) {
					CExtension.RemoveObj(oComponents[i], false, a_bIsAssert);
				}
			}
		}
	}

	/** 객체 => JSON 문자열로 변환한다 */
	public static string ExToMsgPackJSONStr<T>(this T a_tSender) {
		return MessagePackSerializer.ConvertToJson(MessagePackSerializer.Serialize<T>(a_tSender));
	}

	/** 객체 => Base64 문자열로 변환한다 */
	public static string ExToMsgPackBase64Str<T>(this T a_tSender) {
		return System.Convert.ToBase64String(MessagePackSerializer.Serialize<T>(a_tSender));
	}

	/** JSON 문자열 => 객체로 변환한다 */
	public static T ExMsgPackJSONStrToObj<T>(this string a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());
		return MessagePackSerializer.Deserialize<T>(MessagePackSerializer.ConvertFromJson(a_oSender));
	}

	/** Base64 문자열 => 객체로 변환한다 */
	public static T ExMsgPackBase64StrToObj<T>(this string a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());
		return MessagePackSerializer.Deserialize<T>(System.Convert.FromBase64String(a_oSender));
	}
	#endregion // 제네릭 클래스 함수

	#region 조건부 클래스 함수
	/** 애니메이션을 실행한다 */
	public static void ExStartAni(this DOTweenAnimation a_oSender, UnityAction a_oCallback, UnityAction a_oStepCallback = null, bool a_bIsAssert = true) {
		CAccess.Assert(!a_bIsAssert || a_oSender != null);

		// 애니메이션이 존재 할 경우
		if(a_oSender != null) {
			a_oSender.hasOnComplete = true;
			a_oSender.hasOnStepComplete = true;

			a_oSender.onComplete.RemoveAllListeners();
			a_oSender.onComplete.AddListener(a_oCallback);

			a_oSender.onStepComplete.RemoveAllListeners();
			a_oSender.onStepComplete.AddListener(a_oStepCallback);

			a_oSender.CreateTween(true);
		}
	}
	#endregion // 조건부 클래스 함수

	#region 조건부 제네릭 클래스 함수
#if NEWTON_SOFT_JSON_SERIALIZE_DESERIALIZE_ENABLE
	/** 객체 => JSON 문자열로 변환한다 */
	public static string ExToJSONStr<T>(this T a_tSender, bool a_bIsNeedsRoot = false, bool a_bIsPretty = false) {
		object oObj = !a_bIsNeedsRoot ? a_tSender as object : new Dictionary<string, object>() {
			[KCDefine.B_KEY_ROOT] = a_tSender
		};

		return JsonConvert.SerializeObject(oObj, a_bIsPretty ? Formatting.Indented : Formatting.None);
	}

	/** JSON 문자열 => 객체로 변환한다 */
	public static T ExJSONStrToObj<T>(this string a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());
		return JsonConvert.DeserializeObject<T>(a_oSender);
	}
#endif // #if NEWTON_SOFT_JSON_SERIALIZE_DESERIALIZE_ENABLE
	#endregion // 조건부 제네릭 클래스 함수
}
