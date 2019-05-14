using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private StorySettings firstStory;
    [SerializeField] private List<StorySettings> allStories;
    [SerializeField] private List<AddedParameterInfo> beginParameterInfos;
    [SerializeField] private int maxValue;

    private Dictionary<ParameterType, int> valueByType;


    public Dictionary<ParameterType, int> ValueByType => valueByType;

    public int MaxValue => maxValue;


    private void Awake()
    {
        valueByType = new Dictionary<ParameterType, int>();

        for (int i = 0; i < beginParameterInfos.Count; i++)
        {
            if (PlayerPrefs.HasKey(beginParameterInfos[i].parameterType.ToString()))
            {
                valueByType.Add(beginParameterInfos[i].parameterType,
                    PlayerPrefs.GetInt(beginParameterInfos[i].parameterType.ToString()));
            }
            else
            {
                valueByType.Add(beginParameterInfos[i].parameterType, beginParameterInfos[i].value);
            }
        }
    }


    public StorySettings GetNextStory(StorySettings settings, bool isPositiveAnswer, out bool isLose)
    {
        isLose = false;
        if (settings == null)
        {
            if (PlayerPrefs.HasKey("last_story_number"))
            {
                int storyNumber = PlayerPrefs.GetInt("last_story_number");

                return allStories[storyNumber];
            }

            int index = allStories.FindIndex((val) => val == firstStory);
            PlayerPrefs.SetInt("last_story_number", index);
            
            return firstStory;
        }

        AnswerInfo answerInfo = isPositiveAnswer ? settings.positiveAnswerInfo : settings.negativeAnswerInfo;
        isLose = answerInfo.isLose;

        StorySettings nextStorySettings = answerInfo.nextStorySettings;
        
        List<AddedParameterInfo> randomParaments = new List<AddedParameterInfo>();

        if (Random.value < 0.7f)
        {
            randomParaments.Add(new AddedParameterInfo()
            {
                parameterType = ParameterType.Money,
                value = Random.Range(-1, 2)
            });
        }
        
        if (Random.value < 0.5f)
        {
            randomParaments.Add(new AddedParameterInfo()
            {
                parameterType = ParameterType.Trust,
                value = Random.Range(-1, 2)
            });
        }
        
        if (Random.value < 0.45f)
        {
            randomParaments.Add(new AddedParameterInfo()
            {
                parameterType = ParameterType.Information,
                value = Random.Range(-1, 2)
            });
        }
        
        if (Random.value < 0.3f)
        {
            randomParaments.Add(new AddedParameterInfo()
            {
                parameterType = ParameterType.Personalisation,
                value = Random.Range(-1, 2)
            });
        }
        

        if (nextStorySettings != null)
        {
            for (int i = 0; i < randomParaments.Count; i++)
            {
                if (!valueByType.ContainsKey(randomParaments[i].parameterType))
                {
                    valueByType.Add(randomParaments[i].parameterType, 0);
                }

                int value = valueByType[randomParaments[i].parameterType];
                value = Mathf.Clamp(value + randomParaments[i].value, 0, maxValue);

                valueByType[randomParaments[i].parameterType] = value;

                PlayerPrefs.SetInt(randomParaments[i].parameterType.ToString(), value);
            }

            int index = allStories.IndexOf(nextStorySettings);

            if (index >= 0)
            {
                PlayerPrefs.SetInt("last_story_number", index);
            }
        }

        return nextStorySettings;
    }
}
