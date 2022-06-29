
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MathPuzzle
{
    public class Lives : MonoBehaviour
    {
        [SerializeField] private GameObject[] lives = null;

        public static Action ChangeNumberIncorrectAnswersEvent;
        public static Action OpenLoseScreenEvent;

        private void Start()
        {
            for (int i = 0; i < lives.Length; i++)
            {
                lives[i].SetActive(true);
            }
        }

        private void OnEnable()
        {
            FindOneSummand.TakeALifeEvent += TakeALife;
            FindTwoSummand.TakeALifeEvent += TakeALife;
            FindSum.TakeALifeEvent += TakeALife;
            WinLoseGame.PurchasingLiveAdEvent += PurchaseLive;
        }

        private void OnDisable()
        {
            FindOneSummand.TakeALifeEvent -= TakeALife;
            FindTwoSummand.TakeALifeEvent -= TakeALife;
            FindSum.TakeALifeEvent -= TakeALife;
            WinLoseGame.PurchasingLiveAdEvent -= PurchaseLive;
        }

        private void TakeALife(int _numberIncorrectAnswers)
        {
            if (_numberIncorrectAnswers == 1)
            {
                lives[2].SetActive(false);
            }
            else if (_numberIncorrectAnswers == 2)
            {
                lives[1].SetActive(false);
            }
            else if (_numberIncorrectAnswers == 3)
            {
                lives[0].SetActive(false);

                OpenLoseScreenEvent?.Invoke();
            }
        }

        private void PurchaseLive()
        {
            lives[0].SetActive(true);

            ChangeNumberIncorrectAnswersEvent?.Invoke();
        }
    }
}
