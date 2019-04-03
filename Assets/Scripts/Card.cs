using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image image;
    [SerializeField] private float endAlphaValue;
    [SerializeField] private Text yesText;
    [SerializeField] private Text noText;


    private void OnDisable()
    {
        DOTween.Kill(this);
    }


    public void ShowText(StorySettings storySettings, Action callback)
    {
        text.text = storySettings.message;

        if (storySettings.isMessageOnly)
        {
            yesText.enabled = false;
            noText.enabled = false;
        }
        else
        {
            yesText.enabled = true;
            noText.enabled = true;
            yesText.text = storySettings.positiveAnswerInfo.answerText;
            noText.text = storySettings.negativeAnswerInfo.answerText;
        }

        if (storySettings.sprite != null)
        {
            image.sprite = storySettings.sprite;

            canvasGroup.DOFade(endAlphaValue, 0.4f).SetId(this);
        }

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
        canvasGroup.DOFade(0f, 0.4f).SetId(this);

        text.DOColor(endColor, 0.2f).SetId(this).OnComplete(() =>
        {
            if (callback != null)
            {
                callback();
            }
        });
    }
}
