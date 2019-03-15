using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardsAnimation : MonoBehaviour
{
    public static event Action<bool> OnAnswer;

    private Vector2 startPos;
    private Vector2 prevPos;
    private bool isButtonUp = true;

    [SerializeField] private GameObject card;
    [SerializeField] private GameObject textPanel;
    [SerializeField] private TextMeshProUGUI textMeshProYes;
    [SerializeField] private TextMeshProUGUI textMeshProNo;

    private Vector2 diffPos;
    private RectTransform cardRectTransform;
    private Vector2 localpoint;
    private Quaternion rotate;

    [SerializeField] private Canvas canvas;

    private Image imagePanel;

    [SerializeField] private float maxBorder = 100;
    [SerializeField] private float maxAngle = 10;
    [SerializeField] private float speed = 5f;
    
    void Start()
    {
        cardRectTransform = card.GetComponent<RectTransform>();
        imagePanel = textPanel.GetComponent<Image>();
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                cardRectTransform, 
                Input.mousePosition, 
                canvas.worldCamera, 
                out startPos);
            
            prevPos = startPos;

            isButtonUp = false;
        }
        
        Vector3 cardPos = cardRectTransform.anchoredPosition;
        
        if (Input.GetMouseButtonUp(0))
        {
            isButtonUp = true;
            if (cardPos.x > maxBorder - 1f)
            {
                if (OnAnswer != null)
                {
                    OnAnswer(false);
                }
            }
            else if (cardPos.x < -maxBorder + 1f)
            {
                if (OnAnswer != null)
                {
                    OnAnswer(true);
                }
            }
        }
        
        if (!isButtonUp)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                cardRectTransform, 
                Input.mousePosition, 
                canvas.worldCamera, 
                out localpoint);

            diffPos = localpoint - prevPos;
            diffPos.y = 0;
            cardRectTransform.anchoredPosition += diffPos;
            //cardRectTransform.anchoredPosition = new Vector2(diffPos.x + cardRectTransform.anchoredPosition.x, -Mathf.Abs(diffPos.x / 3));;

            if (cardRectTransform.anchoredPosition.x > maxBorder)
            {
                cardRectTransform.anchoredPosition = new Vector2(maxBorder, cardRectTransform.anchoredPosition.y);
            }
            if (cardRectTransform.anchoredPosition.x < -maxBorder)
            {
                cardRectTransform.anchoredPosition = new Vector2(-maxBorder, cardRectTransform.anchoredPosition.y);
            }
            
            cardPos = cardRectTransform.anchoredPosition;
            cardRectTransform.localEulerAngles = (new Vector3(0,0,-cardPos.x / maxAngle));
        }
        else
        {
            if (cardPos.x > 0)
            {
                cardRectTransform.anchoredPosition -= Vector2.Lerp(Vector2.zero, new Vector2(maxBorder,0), speed / 100f);
                cardRectTransform.localEulerAngles -= Vector3.Lerp(Vector3.zero, new Vector3(0, 0, -maxAngle), speed / 100f);
            }
            if (cardPos.x < 0)
            {
                cardRectTransform.anchoredPosition -= Vector2.Lerp(Vector2.zero, new Vector2(-maxBorder,0), speed / 100f);
                cardRectTransform.localEulerAngles -= Vector3.Lerp(Vector3.zero, new Vector3(0, 0, maxAngle), speed / 100f);
            }
            
        }

        #region TransparencyChange

        Color colorPanel = imagePanel.color;
        Color colorNo = textMeshProNo.color;
        Color colorYes = textMeshProYes.color;
        float posX = cardRectTransform.anchoredPosition.x;
        
        if (posX > 0)
        {
            colorPanel.a = posX / 255;
            imagePanel.color = colorPanel;

            colorNo.a = posX * 2.55f / 255f;
            textMeshProNo.color = colorNo;
        }
        else
        {
            if (posX < 0)
            {
                colorPanel.a = - posX / 255;
                imagePanel.color = colorPanel;

                colorYes.a = - posX * 2.55f / 255f;
                textMeshProYes.color = colorYes;
            }
            else
            {
                colorNo.a = 0;
                textMeshProNo.color = colorNo;

                colorYes.a = 0;
                textMeshProYes.color = colorYes;
            }
        }

        #endregion
        
    }
}
