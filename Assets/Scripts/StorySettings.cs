using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Story", menuName = "Settings/Story")]
public class StorySettings : ScriptableObject
{
    public string message;
    public bool isMessageOnly;

    public AnswerInfo positiveAnswerInfo;
    public AnswerInfo negativeAnswerInfo;
}


[Serializable]
public class AnswerInfo
{
    public string answerText;

    public float addedResourcefulness;
    public float addedCourage;
    public float addedIndifference;
    public float addedHappy;

    public bool isLose;
    public StorySettings nextStorySettings;
}  