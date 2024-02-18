using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using DG.Tweening;
using DG.Tweening.Core;

/** 애니메이션 팩토리 */
public static partial class CFactory {
	#region 클래스 함수
	/** 애니메이션을 생성한다 */
	public static DG.Tweening.Tween MakeAni(DOGetter<float> a_oGetter, 
		DOSetter<float> a_oSetter, System.Action a_oInitCallback, System.Action<float> a_oSetterCallback, float a_fVal, float a_fDuration, Ease a_eEase = KCDefine.U_EASE_DEF, bool a_bIsRealtime = false) {

		CAccess.Assert(a_oGetter != null && a_oSetter != null);
		a_oInitCallback?.Invoke();

		return DOTween.To(a_oGetter, (a_fAniVal) => {
			a_oSetter(a_fAniVal);
			a_oSetterCallback?.Invoke(a_fAniVal);
		}, a_fVal, a_fDuration).SetAutoKill().SetEase(a_eEase).SetUpdate(a_bIsRealtime);
	}

	/** 시퀀스를 생성한다 */
	public static Sequence MakeSequence(DG.Tweening.Tween a_oAni, 
		System.Action<Sequence> a_oCallback, float a_fDelay = KCDefine.B_VAL_0_REAL, bool a_bIsJoin = false, bool a_bIsRealtime = false) {

		CAccess.Assert(a_oAni != null);

		return CFactory.MakeSequence(new List<DG.Tweening.Tween>() {
			a_oAni
		}, a_oCallback, a_fDelay, a_bIsJoin, a_bIsRealtime);
	}

	/** 시퀀스를 생성한다 */
	public static Sequence MakeSequence(List<DG.Tweening.Tween> a_oAniList, 
		System.Action<Sequence> a_oCallback, float a_fDelay = KCDefine.B_VAL_0_REAL, bool a_bIsJoin = false, bool a_bIsRealtime = false) {

		CAccess.Assert(a_oAniList != null);
		var oAni = DOTween.Sequence().SetAutoKill().SetUpdate(a_bIsRealtime);

		for(int i = 0; i < a_oAniList.Count; ++i) {
			// 조인 모드 일 경우
			if(a_bIsJoin) {
				oAni.Join(a_oAniList[i]);
			} else {
				oAni.Append(a_oAniList[i]);
			}
		}

		var oSequence = DOTween.Sequence().SetAutoKill().SetDelay(a_fDelay).SetUpdate(a_bIsRealtime).Append(oAni);
		return oSequence.AppendCallback(() => a_oCallback?.Invoke(oSequence));
	}
	#endregion // 클래스 함수
}
