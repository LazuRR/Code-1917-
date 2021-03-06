﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardsAnimation : MonoBehaviour, IPointerDownHandler
{
    public static event Action<bool> OnAnswer;

    private Vector2 startPos;
    private Vector2 prevPos;
    private bool isButtonUp = true;

    [SerializeField] private GameObject card;
    [SerializeField] private GameObject textPanel;
    [SerializeField] private Text textMeshProYes;
    [SerializeField] private Text textMeshProNo;

    private Vector2 diffPos;
    private RectTransform cardRectTransform;
    private Vector2 localpoint;
    private Quaternion rotate;

    [SerializeField] private Canvas canvas;

    private Image imagePanel;

    [SerializeField] private float maxBorder = 100;
    [SerializeField] private float maxAngle = 10;
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float backSpeed = 5f;

    private GameObject currentCard = null;
    private List<GameObject> prevCards = new List<GameObject>();

    private bool isPrevRight;
    Vector3 defaultPosition;

    [SerializeField] private float fadeSpeed = 5f;
    [SerializeField] private float fadeRotateSpeed = 5f;
    
    void Start()
    {
        cardRectTransform = card.GetComponent<RectTransform>();
        imagePanel = textPanel.GetComponent<Image>();

        defaultPosition = cardRectTransform.anchoredPosition;

        currentCard = this.gameObject;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
//        Debug.Log("click");
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            cardRectTransform, 
            Input.mousePosition, 
            canvas.worldCamera, 
            out startPos);
            
        prevPos = startPos;

        isButtonUp = false;
    }
    
    void Update()
    {
        if (prevCards.Count > 0)
        {
            foreach (GameObject obj in prevCards)
            {
                if (obj == null)
                {
                    prevCards.Remove(obj);
                    break;
                }
                
                RectTransform rectTransform = obj.GetComponent<RectTransform>();
                if (isPrevRight)
                {
                    rectTransform.Translate(Vector3.right * fadeSpeed, Space.World);
                    rectTransform.localEulerAngles += Vector3.back * fadeRotateSpeed;
                }
                else
                {
                    rectTransform.Translate(Vector3.left * fadeSpeed, Space.World);
                    rectTransform.localEulerAngles += Vector3.forward * fadeRotateSpeed;
                }
            }
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
                    
                    if (cardRectTransform.anchoredPosition.x > 0)
                    {
                        isPrevRight = true;
                    }
                    else if (cardRectTransform.anchoredPosition.x < 0)
                    {
                        isPrevRight = false;
                    }

                    GameObject clone = Instantiate(card, transform.parent);
                    prevCards.Add(clone);
                    Destroy(clone, 0.3f);
                    Color color = clone.GetComponent<Image>().color = new Color32(40,40,50,255);

                    cardRectTransform.anchoredPosition = defaultPosition;
                    cardRectTransform.localEulerAngles = Vector3.zero;
                }
            }
            else if (cardPos.x < -maxBorder + 1f)
            {
                if (OnAnswer != null)
                {
                    OnAnswer(true);
                    
                    if (cardRectTransform.anchoredPosition.x > 0)
                    {
                        isPrevRight = true;
                    }
                    else if (cardRectTransform.anchoredPosition.x < 0)
                    {
                        isPrevRight = false;
                    }

                    GameObject clone = Instantiate(card, transform.parent);
                    prevCards.Add(clone);
                    Destroy(clone, 0.3f);
                    Color color = clone.GetComponent<Image>().color = new Color32(40,40,50,255);

                    cardRectTransform.anchoredPosition = defaultPosition;
                    cardRectTransform.localEulerAngles = Vector3.zero;
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
            cardRectTransform.anchoredPosition += diffPos * Time.smoothDeltaTime * speed;
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
            if (cardPos.x > 5)
            {
                cardRectTransform.anchoredPosition -= Vector2.Lerp(Vector2.zero, new Vector2(maxBorder,0), backSpeed / 100f);
                cardRectTransform.localEulerAngles -= Vector3.Lerp(Vector3.zero, new Vector3(0, 0, -maxAngle), backSpeed / 100f);
            }
            else if (cardPos.x < -5)
            {
                cardRectTransform.anchoredPosition -= Vector2.Lerp(Vector2.zero, new Vector2(-maxBorder,0), backSpeed / 100f);
                cardRectTransform.localEulerAngles -= Vector3.Lerp(Vector3.zero, new Vector3(0, 0, maxAngle), backSpeed / 100f);
            }
            else
            {
                cardRectTransform.anchoredPosition = defaultPosition;
                cardRectTransform.localEulerAngles = Vector3.zero;
            }
            
        }

        #region TransparencyChange

        Color colorPanel = imagePanel.color;
        Color colorNo = textMeshProNo.color;
        Color colorYes = textMeshProYes.color;
        float posX = cardRectTransform.anchoredPosition.x;
        
        if (posX > 5)
        {
            colorPanel.a = posX / 255;
            imagePanel.color = colorPanel;

            colorNo.a = posX * 2.55f / 255f;
            textMeshProNo.color = colorNo;
            
            colorYes.a = 0;
            textMeshProYes.color = colorYes;
        }
        else if (posX < -5)
        {
            colorPanel.a = - posX / 255;
            imagePanel.color = colorPanel;

            colorYes.a = - posX * 2.55f / 255f;
            textMeshProYes.color = colorYes;
                
            colorNo.a = 0;
            textMeshProNo.color = colorNo;
        }
        else
        {
            colorNo.a = 0;
            textMeshProNo.color = colorNo;

            colorYes.a = 0;
            textMeshProYes.color = colorYes;


            colorPanel.a = 0f;
            imagePanel.color = colorPanel;
        }
        #endregion
    }
}
