using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] private List<StorySettings> storiesSettings;

    private int currentStorySettingsIndex;


    public static GameManager Instance
    {
        get { return instance; }
    }


    private void Awake()
    {
        instance = this;
    }


    public bool TryGetNextStorySettings(out StorySettings storySettings)
    {
        storySettings = (currentStorySettingsIndex >= storiesSettings.Count)
            ? null
            : storiesSettings[currentStorySettingsIndex];
        currentStorySettingsIndex++;

        return storySettings != null;
    }
}
