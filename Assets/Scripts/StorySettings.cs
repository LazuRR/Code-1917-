using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Story", menuName = "Settings/Story")]
public class StorySettings : ScriptableObject
{
    public string message;
    public bool isMessageOnly;
    public Sprite sprite;

    public AnswerInfo positiveAnswerInfo;
    public AnswerInfo negativeAnswerInfo;
}


[Serializable]
public class AnswerInfo
{
    public string answerText;

    public List<AddedParameterInfo> addedParameterInfos;

    public bool isLose;
    public StorySettings nextStorySettings;
}


public enum ParameterType
{
    Information = 1,
    Personalisation = 2,
    Trust = 3,
    Money = 4
}


[Serializable]
public struct AddedParameterInfo
{
    public ParameterType parameterType;
    public int value;
}