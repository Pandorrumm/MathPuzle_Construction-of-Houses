using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace MathPuzzle
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private GameConfig gameConfig = null;

        private int numberInCell;
        private FindOneSummand findOneSummand;
        private FindTwoSummand findTwoSummand;
        private FindSum findSum;

        private void Start()
        {
            findOneSummand = FindObjectOfType<FindOneSummand>();
            findTwoSummand = FindObjectOfType<FindTwoSummand>();
            findSum = FindObjectOfType<FindSum>();

            GetComponent<Button>().onClick.AddListener(GetNumberInCell);
        }

        private void GetNumberInCell()
        {
            if (gameConfig.allMissions[gameConfig.currentMissionIndex].gameTypeData.gameType.type == GameTypeData.GameType.GameTypes.FIND_ONE_SUMMAND)
            {
                numberInCell = findOneSummand.SearchNumberByCell(this);
                findOneSummand.CheckingCompletionTask(this, numberInCell);
            }

            if (gameConfig.allMissions[gameConfig.currentMissionIndex].gameTypeData.gameType.type == GameTypeData.GameType.GameTypes.FIND_TWO_SUMMAND)
            {
                numberInCell = findTwoSummand.SearchNumberByCell(this);
                findTwoSummand.CheckingCompletionTask(this, numberInCell);
            }

            if (gameConfig.allMissions[gameConfig.currentMissionIndex].gameTypeData.gameType.type == GameTypeData.GameType.GameTypes.FIND_SUM)
            {
                numberInCell = findSum.SearchNumberByCell(this);
                findSum.CheckingCompletionTask(this, numberInCell);
            }
        }

        public void DisableCell()
        {
            GetComponent<Image>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
