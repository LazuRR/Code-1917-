using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGamePopup : MonoBehaviour
{
    [SerializeField] private Text endGameTExt;
    [SerializeField] private Button tapToContinueButton;
    public Transform showTR;
    
    public void Show(string text)
    {
        tapToContinueButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });

        endGameTExt.text = text.ToUpper();

        showTR.localScale = Vector3.one * 0.001f;
        showTR.DOScale(Vector3.one, 0.2f);
    }
}
