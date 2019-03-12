using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsAnimation : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 prevPos;
    private bool isButtonUp = true;

    [SerializeField] private GameObject card;
    [SerializeField] private GameObject textPanel;

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
                cardRectTransform.anchoredPosition = new Vector2(100, cardRectTransform.anchoredPosition.y);
            }
            if (cardRectTransform.anchoredPosition.x < -100)
            {
                cardRectTransform.anchoredPosition = new Vector2(-100, cardRectTransform.anchoredPosition.y);
            }
            
            cardRectTransform.localEulerAngles = (new Vector3(0,0,-cardRectTransform.anchoredPosition.x / 10));
            if (cardRectTransform.anchoredPosition.y < -20)
            {
                cardRectTransform.anchoredPosition = new Vector2(cardRectTransform.anchoredPosition.x, -20);
            }
            //startPos = localpoint;
        }
        
        
        
        //normalizedPoint = Rect.PointToNormalized(cardRectTransform.rect, localpoint);
 
        //Debug.Log(localpoint.ToString());
    }
}
