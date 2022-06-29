using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using DG.Tweening;

namespace MathPuzzle
{
    public class MissionsTracker : MonoBehaviour
    {
        [SerializeField] private GameConfig gameConfig = null;

        [Header("Game Types")]
        [SerializeField] private FindOneSummand findOneSummand = null;
        [SerializeField] private Memory memory = null;
        [SerializeField] private FindTwoSummand findTwoSummand = null;
        [SerializeField] private FindSum findSum = null; 

        [Space]
        [SerializeField] private Image housePlot = null;
        [SerializeField] private int constructionIndex = 0;

        [Space]
        [SerializeField] private float delayOpenVictoryScreen = 2f;
        [SerializeField] private float delayChangeSpriteHouse = 2f;

        public static Action OpenVictoryScreenEvent;
        public static Action SaveScoreEvent;

        private void OnEnable()
        {
            FindOneSummand.NextTaskEvent += TaskTracker;
            Memory.NextTaskEvent += TaskTracker;
            FindTwoSummand.NextTaskEvent += TaskTracker;
            FindSum.NextTaskEvent += TaskTracker;
        }

        private void OnDisable()
        {
            FindOneSummand.NextTaskEvent -= TaskTracker;
            Memory.NextTaskEvent -= TaskTracker;
            FindTwoSummand.NextTaskEvent -= TaskTracker;
            FindSum.NextTaskEvent -= TaskTracker;
        }

        private void Start()
        {
            housePlot.sprite = gameConfig.allMissions[gameConfig.currentMissionIndex].constructionElements[0].data.constructionElementSprite;
        }

        public void TaskTracker(int _indexCorrectAnswers)
        {
            if (_indexCorrectAnswers == gameConfig.allMissions[gameConfig.currentMissionIndex].constructionElements[constructionIndex].data.numberCorrectAnswersToNextTask)
            {
                constructionIndex++;

                findOneSummand.ResetCounterCorrectAnswers();
                memory.ResetCounterCorrectAnswers();
                findTwoSummand.ResetCounterCorrectAnswers();
                findSum.ResetCounterCorrectAnswers();

                findOneSummand.numberCorrectAnswersToNextTask = gameConfig.allMissions[gameConfig.currentMissionIndex].constructionElements[constructionIndex].data.numberCorrectAnswersToNextTask;

                DOTween.Sequence()
                    .AppendInterval(delayChangeSpriteHouse)
                    .AppendCallback(() => housePlot.sprite = gameConfig.allMissions[gameConfig.currentMissionIndex].constructionElements[constructionIndex].data.constructionElementSprite);

                if (constructionIndex == gameConfig.allMissions[gameConfig.currentMissionIndex].constructionElements.Count - 1)
                {
                    PlayerPrefs.SetInt(gameConfig.globalMissionName + " isBuiltHouse", 1);

                    DOTween.Sequence()
                     .AppendInterval(delayOpenVictoryScreen)
                     .AppendCallback(() => VictoryGame());

                    void VictoryGame()
                    {
                        OpenVictoryScreenEvent?.Invoke();
                        SaveScoreEvent?.Invoke();

                        constructionIndex = 0;                     
                    }
                }
            }
        }
    }
}
