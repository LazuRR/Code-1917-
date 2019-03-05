using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    [SerializeField] private Card card;
    [SerializeField] private Button agreeButton;
    [SerializeField] private Button disagreeButton;


    private bool isAnswerButtonsEnabled;


    private void OnEnable()
    {
        agreeButton.onClick.AddListener(AgreeButton_OnClick);
        disagreeButton.onClick.AddListener(DisagreeButton_OnClick);
    }

    private void OnDisable()
    {
        agreeButton.onClick.RemoveListener(AgreeButton_OnClick);
        disagreeButton.onClick.RemoveListener(DisagreeButton_OnClick);
    }

    private void Start()
    {
        SetNextText();
    }

    void AgreeButton_OnClick()
    {
        if (isAnswerButtonsEnabled)
        {
            SetNextText();
        }
    }

    void DisagreeButton_OnClick()
    {
        if (isAnswerButtonsEnabled)
        {
            SetNextText();
        }
    }

    void SetNextText()
    {
        StorySettings storySettings;
        if (GameManager.Instance.TryGetNextStorySettings(out storySettings))
        {
            isAnswerButtonsEnabled = false;
            card.HideText(() =>
            {
                card.ShowText(storySettings.message, () => { isAnswerButtonsEnabled = true; });
            });
        }
    }
}
