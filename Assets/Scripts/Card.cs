using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Text text;


    private void OnDisable()
    {
        DOTween.Kill(this);
    }


    public void ShowText(string message, Action callback)
    {
        text.text = message;

        Color endColor = text.color;
        endColor.a = 1f;
        text.DOColor(endColor, 0.3f).SetId(this).OnComplete(() =>
        {
            if (callback != null)
            {
                callback();
            }
        });
    }


    public void HideText(Action callback)
    {
        Color endColor = text.color;
        endColor.a = 0f;
        text.DOColor(endColor, 0.2f).SetId(this).OnComplete(() =>
        {
            if (callback != null)
            {
                callback();
            }
        });
    }
}
