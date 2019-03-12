using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public const float MAX_SCORE_COUNT = 6f;
    
    [SerializeField] private StorySettings firstStory;
    
    // TODO bad code :( sorry
    public float resourcefulness;
    public float courage;
    public float indifference;
    public float happy;
    
    public StorySettings GetNextStory(StorySettings settings, bool isPositiveAnswer, out bool isLose)
    {
        isLose = false;
        if (settings == null)
        {
            resourcefulness = MAX_SCORE_COUNT / 2f;
            courage = MAX_SCORE_COUNT / 2f;
            indifference = MAX_SCORE_COUNT / 2f;
            happy = MAX_SCORE_COUNT / 2f;
            
            return firstStory;
        }

        AnswerInfo answerInfo = isPositiveAnswer ? settings.positiveAnswerInfo : settings.negativeAnswerInfo;
        isLose = answerInfo.isLose;

        StorySettings nextStorySettings = answerInfo.nextStorySettings;

        if (nextStorySettings != null)
        {
            resourcefulness = Mathf.Clamp(resourcefulness + answerInfo.addedResourcefulness, 0f, MAX_SCORE_COUNT);
            courage = Mathf.Clamp(courage + answerInfo.addedCourage, 0f, MAX_SCORE_COUNT);
            indifference = Mathf.Clamp(indifference + answerInfo.addedIndifference, 0f, MAX_SCORE_COUNT);
            happy = Mathf.Clamp(happy + answerInfo.addedHappy, 0f, MAX_SCORE_COUNT);
        }

        return nextStorySettings;
    }
}
