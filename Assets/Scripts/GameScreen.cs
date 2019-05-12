using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    [Serializable]
    class BarInfo
    {
        public Image image;
        public ParameterType parameterType;
    }
    
    
    [SerializeField] private Card card;
    [SerializeField] private EndGamePopup endGamePopupPrefab;
    [SerializeField] private List<BarInfo> barsInfo;


    private StorySettings currentStorySettings;
    private bool isAnswerButtonsEnabled;


    private void OnEnable()
    {
        CardsAnimation.OnAnswer += CardsAnimationOnOnAnswer;
    }

    private void CardsAnimationOnOnAnswer(bool isPositive)
    {
        if (isPositive)
        {
            AgreeButton_OnClick();
        }
        else
        {
            DisagreeButton_OnClick();
        }
    }

    private void OnDisable()
    {
        CardsAnimation.OnAnswer -= CardsAnimationOnOnAnswer;

        DOTween.Kill(this);
    }

    private void Start()
    {
        SetNextText(true, true);
    }

    void AgreeButton_OnClick()
    {
        if (isAnswerButtonsEnabled)
        {
            SetNextText(true);
        }
    }

    void DisagreeButton_OnClick()
    {
        if (isAnswerButtonsEnabled)
        {
            SetNextText(false);
        }
    }

    void SetNextText(bool isPositiveAnswer, bool isImmediatelly = false)
    {
        bool isLose;
        currentStorySettings = GameManager.Instance.StoryManager.GetNextStory(currentStorySettings, isPositiveAnswer, out isLose);
        
        if (currentStorySettings != null)
        {
            isAnswerButtonsEnabled = false;
            card.HideText(() =>
            {
                card.ShowText(currentStorySettings, () => { isAnswerButtonsEnabled = true; }, isImmediatelly);
            });
        }
        else
        {
            EndGamePopup endGamePopup = Instantiate(endGamePopupPrefab, transform);
            endGamePopup.transform.localPosition = Vector3.zero;
            endGamePopup.Show("End game");
            PlayerPrefs.DeleteAll();
        }

        StoryManager storyManager = GameManager.Instance.StoryManager;

        foreach (var parameter in storyManager.ValueByType)
        {
            BarInfo barInfo = barsInfo.Find((item) => item.parameterType == parameter.Key);

            if (barInfo != null)
            {
                Image image = barInfo.image;
                float current = image.fillAmount;
                float delta = current - (float)parameter.Value / storyManager.MaxValue;

                if (!Mathf.Approximately(delta, 0f))
                {
                    DOTween.To(() => 0f, (value) =>
                        {
                            image.fillAmount = current - value * delta;
                        }, 1, 0.1f)
                        .SetId(this);
                }
            }
        }
    }
}
