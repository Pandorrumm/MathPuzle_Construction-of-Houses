using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;

namespace MathPuzzle
{
    public class DecoratingObjects : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private string key = null;
        [SerializeField] private int price = 0;
        [SerializeField] private Sprite spriteAfterPurchase = null;
        [SerializeField] private TMP_Text priceText = null;

        private bool isPurchase = false;

        private SpriteRenderer spriteRenderer;
        private Canvas canvasParent;

        public static Action UpdateUIScoreEvent;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            canvasParent = GetComponentInChildren<Canvas>();

            if (!PlayerPrefs.HasKey("DecoratingObject " + key))
            {
                PlayerPrefs.SetInt("DecoratingObject " + key, 0);
            }
            else
            {
                if (PlayerPrefs.GetInt("DecoratingObject " + key) == 1)
                {
                    canvasParent.gameObject.SetActive(false);
                    spriteRenderer.sprite = spriteAfterPurchase;
                    isPurchase = true;
                }
            }
        }

        private void Start()
        {
            priceText.text = "" + price + " $";
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isPurchase)
            {
                int totalScore = PlayerPrefs.GetInt("Score");

                if (price <= totalScore)
                {
                    totalScore -= price;

                    PlayerPrefs.SetInt("Score", totalScore);

                    UpdateUIScoreEvent?.Invoke();
                    canvasParent.gameObject.SetActive(false);

                    PlayerPrefs.SetInt("DecoratingObject " + key, 1);

                    spriteRenderer.sprite = spriteAfterPurchase;
                    isPurchase = true;
                }
            }
        }
    }
}
