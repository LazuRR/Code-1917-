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
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image characterImage;
    [SerializeField] private Sprite backgroundSprite;
    [SerializeField] private Sprite paperSprite;
    
    public List<ContentSizeFitter> updatedElements;
    public RectTransform content;

    private string characterID = "characterID";


    private void OnDisable()
    {
        DOTween.Kill(this);
    }


    public void ShowText(StorySettings storySettings, Action callback, bool isImmediatelly)
    {
        Canvas.ForceUpdateCanvases();
        foreach (var contentSizeFitter in updatedElements)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)contentSizeFitter.transform );
            contentSizeFitter.enabled = false;
            contentSizeFitter.SetLayoutVertical();
        }

        Vector2 pos = content.anchoredPosition;
        pos.y = 0;
        content.anchoredPosition = pos;

        StartCoroutine(End());
        if (storySettings.isPaper)
        {
            DOTween.Kill(characterID);
            characterImage.DOFade(0f, 0.1f).SetId(characterID);
            backgroundImage.sprite = paperSprite;
        }
        else
        {
            DOTween.Kill(characterID);
            characterImage.DOFade(1f, 0.3f).SetId(characterID);
            backgroundImage.sprite = backgroundSprite;
        }
        
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

        if (isImmediatelly)
        {
            DOTween.Complete(characterID);
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

    IEnumerator End()
    {
        yield return new WaitForEndOfFrame();
        foreach (var contentSizeFitter in updatedElements)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)contentSizeFitter.transform );
            contentSizeFitter.enabled = true;
            contentSizeFitter.SetLayoutVertical();
        }
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
