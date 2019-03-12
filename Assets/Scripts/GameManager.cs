using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] private StoryManager storyManager;
    
    private int currentStorySettingsIndex;


    public static GameManager Instance
    {
        get { return instance; }
    }


    public StoryManager StoryManager
    {
        get { return storyManager; }
    }
    

    private void Awake()
    {
        instance = this;
    }
}
