using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    
    public void ContinueGame()
    {
        if (PlayerPrefs.HasKey("last_story_number"))
        {
            SceneManager.LoadScene(1);   
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }
    
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(2);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
