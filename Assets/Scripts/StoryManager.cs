using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private StorySettings firstStory;
    [SerializeField] private List<AddedParameterInfo> beginParameterInfos;
    [SerializeField] private int maxValue;
    [SerializeField] private int minValueForLose;

    private Dictionary<ParameterType, int> valueByType;


    public Dictionary<ParameterType, int> ValueByType => valueByType;

    public int MaxValue => maxValue;


    private void Awake()
    {
        valueByType = new Dictionary<ParameterType, int>();

        for (int i = 0; i < beginParameterInfos.Count; i++)
        {
            valueByType.Add(beginParameterInfos[i].parameterType, beginParameterInfos[i].value);
        }
    }


    public StorySettings GetNextStory(StorySettings settings, bool isPositiveAnswer, out bool isLose)
    {
        isLose = false;
        if (settings == null)
        {
            return firstStory;
        }

        AnswerInfo answerInfo = isPositiveAnswer ? settings.positiveAnswerInfo : settings.negativeAnswerInfo;
        isLose = answerInfo.isLose;

        StorySettings nextStorySettings = answerInfo.nextStorySettings;

        if (nextStorySettings != null)
        {
            for (int i = 0; i < answerInfo.addedParameterInfos.Count; i++)
            {
                if (!valueByType.ContainsKey(answerInfo.addedParameterInfos[i].parameterType))
                {
                    valueByType.Add(answerInfo.addedParameterInfos[i].parameterType, 0);
                }

                int value = valueByType[answerInfo.addedParameterInfos[i].parameterType];
                value = Mathf.Clamp(value + answerInfo.addedParameterInfos[i].value, 0, maxValue);

                valueByType[answerInfo.addedParameterInfos[i].parameterType] = value;
            }
        }

        return nextStorySettings;
    }
}
