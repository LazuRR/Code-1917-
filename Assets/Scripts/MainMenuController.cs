using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    
    public void ContinueGame()
    {
        SceneManager.LoadScene(1);
    }
    
    public void NewGame()
    {
        SceneManager.LoadScene(1);
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
