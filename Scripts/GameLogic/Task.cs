using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace MathPuzzle
{
    public class Task : MonoBehaviour
    {
        [SerializeField] private GameConfig gameConfig = null;

        [Space]
        [SerializeField] private TMP_Text numberTaskText = null;
        [SerializeField] private TMP_Text uiTaskText = null;

        [Space]
        [SerializeField] private float durationShake = 0.5f;

        private int firstSummandNumber;
        private int secondSummandNumber;

        private int numberInTask;


        private void OnEnable()
        {
            FindOneSummand.SetNumberTaskEvent += UpdateNumberTaskText;
            FindOneSummand.MakeIncorrectAnswerTaskEffectEvent += IncorrectAnswer;
            FindOneSummand.SetFirstSummandNumberEvent += GetFirstSummandNumber;

            FindTwoSummand.SetNumberTaskEvent += UpdateNumberTaskText;
            FindTwoSummand.MakeIncorrectAnswerTaskEffectEvent += IncorrectAnswer;

            FindSum.SetSummandsEvent += GetSummandsNumber;
            FindSum.MakeIncorrectAnswerTaskEffectEvent += IncorrectAnswer;
        }

        private void OnDisable()
        {
            FindOneSummand.SetNumberTaskEvent -= UpdateNumberTaskText;
            FindOneSummand.MakeIncorrectAnswerTaskEffectEvent -= IncorrectAnswer;
            FindOneSummand.SetFirstSummandNumberEvent -= GetFirstSummandNumber;

            FindTwoSummand.SetNumberTaskEvent -= UpdateNumberTaskText;
            FindTwoSummand.MakeIncorrectAnswerTaskEffectEvent -= IncorrectAnswer;

            FindSum.SetSummandsEvent -= GetSummandsNumber;
            FindSum.MakeIncorrectAnswerTaskEffectEvent -= IncorrectAnswer;
        }

        private void Start()
        {
            UpdateNumberTaskText(numberInTask);
        }

        private void GetFirstSummandNumber(int _number)
        {
            firstSummandNumber = _number;           
        }

        private void GetSummandsNumber(int _firstSummand, int _secondsSummand)
        {
            firstSummandNumber = _firstSummand;
            secondSummandNumber = _secondsSummand;

            UpdateNumberTaskText(0);
        }

        private void UpdateNumberTaskText(int _numberTask)
        {
            numberInTask = _numberTask;

            if (gameConfig.allMissions[gameConfig.currentMissionIndex].gameTypeData.gameType.type == GameTypeData.GameType.GameTypes.FIND_ONE_SUMMAND)
            {
                uiTaskText.text = firstSummandNumber + " +  ?  =  " + _numberTask;
               // numberTaskText.text = "" + _numberTask;
            }

            if (gameConfig.allMissions[gameConfig.currentMissionIndex].gameTypeData.gameType.type == GameTypeData.GameType.GameTypes.MEMORY)
            {
                uiTaskText.text = "MEMORY";
               // numberTaskText.text = "";
            }

            if (gameConfig.allMissions[gameConfig.currentMissionIndex].gameTypeData.gameType.type == GameTypeData.GameType.GameTypes.FIND_TWO_SUMMAND)
            {
                uiTaskText.text = "?  +  ?  =  " + _numberTask;
               // numberTaskText.text = "" + _numberTask;
            }

            if (gameConfig.allMissions[gameConfig.currentMissionIndex].gameTypeData.gameType.type == GameTypeData.GameType.GameTypes.FIND_SUM)
            {
                uiTaskText.text = firstSummandNumber + " + " + secondSummandNumber + " = ?";
                //numberTaskText.text = "";
            }
        }

        private void IncorrectAnswer()
        {
            //numberTaskText.gameObject.transform.DOShakeScale(durationShake, 1f).OnComplete(() => DefaultTextScale());

            //void DefaultTextScale()
            //{
            //    numberTaskText.gameObject.GetComponent<RectTransform>().DOScale(1f, 0.2f);
            //}
        }
    }
}
