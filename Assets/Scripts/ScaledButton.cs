using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class ScaledButton : Button
{
    
    [SerializeField] private float pressedScale = 0.95f;
    [SerializeField] private AnimationCurve scaleCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    [SerializeField] private float time = 0.2f;


    private Button button;
    private bool isPointerUp;
    private float beginScale;

    protected override void Start()
    {
        base.Start();

        isPointerUp = true;
        beginScale = transform.localScale.x;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        DOTween.Kill(this);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        isPointerUp = false;
        
        ScaleToPressed();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        isPointerUp = true;
        
        ScaleToNormal();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        if (!isPointerUp)
        {
            ScaleToNormal();
        }
    }


    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        if (!isPointerUp)
        {
            ScaleToPressed();
        }
    }

    void ScaleToPressed()
    {
        DOTween.Kill(this);

        float currentScale = transform.localScale.x;
        float neededTime = currentScale * time / beginScale;

        transform.DOScale(Vector3.one * pressedScale, neededTime).SetId(this).SetEase(scaleCurve).OnComplete(() =>
            {
                ScaleEnded(true);
            });
    }


    void ScaleToNormal()
    {
        DOTween.Kill(this);
        
        float currentScale = transform.localScale.x;
        float neededTime = currentScale * time / pressedScale;

        transform.DOScale(Vector3.one * beginScale, neededTime).SetId(this).SetEase(scaleCurve).OnComplete(() =>
            {
                ScaleEnded(false);
            });
    }

    protected virtual void ScaleEnded(bool isPressedScale)
    {
        
    }
}
