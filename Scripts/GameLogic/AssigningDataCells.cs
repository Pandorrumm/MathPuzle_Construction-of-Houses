using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

namespace MathPuzzle
{
    public class AssigningDataCells : MonoBehaviour
    {
        [System.Serializable]
        public class CellData
        {
            public List<Cell> cells;
            public List<int> number = new List<int>();
        }

        public CellData gameCells = new CellData();

        [Space]
        [SerializeField] private GameConfig gameConfig = null;

        private List<Cell> leaveCell = new List<Cell>();


        private void Awake()
        {
            ActivateCell();
        }

        private void ActivateCell()
        {
            for (int i = 0; i < gameConfig.allMissions[gameConfig.currentMissionIndex].cellStatusData.cellStatuses.Count; i++)
            {
                if (gameConfig.allMissions[gameConfig.currentMissionIndex].cellStatusData.cellStatuses[i].statusCell == CellStatusData.CellsStatus.Status.INACTIVE)
                {
                    gameCells.cells[i].DisableCell();
                }
                else
                {
                    leaveCell.Add(gameCells.cells[i]);
                }
            }

            gameCells.number.RemoveRange(0, gameCells.cells.Count - leaveCell.Count);

            gameCells.cells.Clear();
            gameCells.cells.AddRange(leaveCell);
        }
    }
}
