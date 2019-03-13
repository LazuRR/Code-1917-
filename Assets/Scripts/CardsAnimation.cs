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
    [SerializeField] private GameObject textYes;
    [SerializeField] private GameObject textNo;

    private Vector2 diffPos;
    private RectTransform cardRectTransform;
    private Vector2 localpoint;
    private Quaternion rotate;
    
    void Start()
    {
        cardRectTransform = card.GetComponent<RectTransform>();
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                cardRectTransform, 
                Input.mousePosition, 
                GetComponentInParent<Canvas>().worldCamera, 
                out startPos);
            
            prevPos = startPos;

            isButtonUp = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isButtonUp = true;
        }
        
        if (!isButtonUp)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                cardRectTransform, 
                Input.mousePosition, 
                GetComponentInParent<Canvas>().worldCamera, 
                out localpoint);

            diffPos = localpoint - prevPos;
            diffPos.y = 0;
            cardRectTransform.anchoredPosition += diffPos;
            //cardRectTransform.anchoredPosition = new Vector2(diffPos.x + cardRectTransform.anchoredPosition.x, -Mathf.Abs(diffPos.x / 3));;

            if (cardRectTransform.anchoredPosition.x > 100)
            {
                if (OnAnswer != null)
                {
                    OnAnswer(false);
                }
                cardRectTransform.anchoredPosition = new Vector2(100, cardRectTransform.anchoredPosition.y);
            }
            if (cardRectTransform.anchoredPosition.x < -100)
            {
                if (OnAnswer != null)
                {
                    OnAnswer(true);
                }
                cardRectTransform.anchoredPosition = new Vector2(-100, cardRectTransform.anchoredPosition.y);
            }
            
            cardRectTransform.localEulerAngles = (new Vector3(0,0,-cardRectTransform.anchoredPosition.x / 10));
            if (cardRectTransform.anchoredPosition.y < -20)
            {
                cardRectTransform.anchoredPosition = new Vector2(cardRectTransform.anchoredPosition.x, -20);
            }
            //startPos = localpoint;
        }
        else
        {
            if (cardRectTransform.anchoredPosition.x > 0)
            {
                cardRectTransform.anchoredPosition -= Vector2.Lerp(Vector2.zero, new Vector2(100,0), 0.05f);
                cardRectTransform.localEulerAngles -= Vector3.Lerp(Vector3.zero, new Vector3(0, 0, -10f), 0.05f);
            }
            if (cardRectTransform.anchoredPosition.x < 0)
            {
                cardRectTransform.anchoredPosition -= Vector2.Lerp(Vector2.zero, new Vector2(-100,0), 0.05f);
                cardRectTransform.localEulerAngles -= Vector3.Lerp(Vector3.zero, new Vector3(0, 0, 10f), 0.05f);
            }
            
        }


        if (cardRectTransform.anchoredPosition.x > 0)
        {
            Color colorPanel = textPanel.GetComponent<Image>().color;
            colorPanel.a = cardRectTransform.anchoredPosition.x / 255;
            textPanel.GetComponent<Image>().color = colorPanel;
            
            Color colorNo = textNo.GetComponent<TextMeshProUGUI>().color;
            colorNo.a = cardRectTransform.anchoredPosition.x * 2.55f / 255f;
            textNo.GetComponent<TextMeshProUGUI>().color = colorNo;
        }
        else if (cardRectTransform.anchoredPosition.x < 0)
        {
            Color colorPanel = textPanel.GetComponent<Image>().color;
            colorPanel.a = -cardRectTransform.anchoredPosition.x / 255;
            textPanel.GetComponent<Image>().color = colorPanel;
            
            Color colorYes = textYes.GetComponent<TextMeshProUGUI>().color;
            colorYes.a = -cardRectTransform.anchoredPosition.x * 2.55f / 255f;
            textYes.GetComponent<TextMeshProUGUI>().color = colorYes;
        }
        else
        {
            Color colorNo = textNo.GetComponent<TextMeshProUGUI>().color;
            colorNo.a = 0;
            textNo.GetComponent<TextMeshProUGUI>().color = colorNo;
            Color colorYes = textYes.GetComponent<TextMeshProUGUI>().color;
            colorYes.a = 0;
            textYes.GetComponent<TextMeshProUGUI>().color = colorYes;
        }


        
        //normalizedPoint = Rect.PointToNormalized(cardRectTransform.rect, localpoint);
 
        //Debug.Log(localpoint.ToString());
    }
}
