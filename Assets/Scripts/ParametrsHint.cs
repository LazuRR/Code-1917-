using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


[Serializable]
public class AnimationInfo
{
    public float duration = 0.2f;
    public AnimationCurve curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
}


public class ParametrsHint : MonoBehaviour
{
    [SerializeField] private AnimationInfo showScaleAnimationInfo;
    [SerializeField] private AnimationInfo showFadeAnimationInfo;
    [SerializeField] private AnimationInfo hideScaleAnimationInfo;
    [SerializeField] private AnimationInfo hideFadeAnimationInfo;
    [SerializeField] private CanvasGroup fadedGroup;

    private bool isShowed = true;

    private void Awake()
    {
        HideHint();
    }

    private void OnDisable()
    {
        DOTween.Kill(this);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HideHint();
        }
    }

    public void ShowHint()
    {
        if (!isShowed)
        {
            DOTween.Kill(this);

            DOTween.Sequence()
                .Join(transform.DOScale(Vector3.one, showScaleAnimationInfo.duration)
                    .SetEase(showScaleAnimationInfo.curve))
                .Join(fadedGroup.DOFade(1f, showFadeAnimationInfo.duration).SetEase(showFadeAnimationInfo.curve))
                .SetId(this).OnComplete(
                    () => { });
            
            isShowed = true;
        }
    }

    void HideHint()
    {
        if (isShowed)
        {
            DOTween.Kill(this);

            DOTween.Sequence()
                .Join(transform.DOScale(0.001f * Vector3.one, hideScaleAnimationInfo.duration)
                    .SetEase(hideScaleAnimationInfo.curve))
                .Join(fadedGroup.DOFade(0f, hideFadeAnimationInfo.duration).SetEase(hideFadeAnimationInfo.curve))
                .SetId(this).OnComplete(() => { isShowed = false; });

        }
    }
}
