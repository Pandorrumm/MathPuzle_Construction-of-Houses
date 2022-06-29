using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

namespace MathPuzzle
{
    public class Memory : MonoBehaviour
    {     
        [SerializeField] private List<CellsForMemory> cells = new List<CellsForMemory>();
        [SerializeField] private List<Sprite> faces = new List<Sprite>();

        [Space]
        [SerializeField] private int numberPointsForAnswer = 10;

        [Space]
        public int numberIncorrectAnswers;

        public bool canOpen
        {
            get { return secondOpen == null; }
        }

        public int counterCorrectAnswers;

        private CellsForMemory firstOpen;
        private CellsForMemory secondOpen;

        private List<int> indexes = new List<int> { 0, 1, 2, 3, 0, 1, 2, 3, 0 };

        private int shuffleNumber;
        private static System.Random rnd = new System.Random();

        public static Action<int> NextTaskEvent;
        public static Action<int> AddScoreEvent;
        public static Action<Vector3> CreateBuildingMaterialsIconEvent;

        private void Start()
        {
            for (int i = 0; i < cells.Count; i++)
            {
                shuffleNumber = rnd.Next(0, indexes.Count);

                int id = indexes[shuffleNumber];
                cells[i].ChangeImage(id, faces[id]);

                indexes.Remove(indexes[shuffleNumber]);
            }
        }

        public void ImageOpened(CellsForMemory _startCell)
        {
            if (firstOpen == null)
            {
                firstOpen = _startCell;
            }
            else
            {
                secondOpen = _startCell;
                StartCoroutine(CheckGuessed());
            }
        }

        private IEnumerator CheckGuessed()
        {
            if (firstOpen.spriteId == secondOpen.spriteId)
            {
                counterCorrectAnswers++;

                NextTaskEvent?.Invoke(counterCorrectAnswers);
                AddScoreEvent?.Invoke(numberPointsForAnswer);
                CreateBuildingMaterialsIconEvent?.Invoke(secondOpen.transform.position);

                StatusButtonsAllCell(false);

                DOTween.Sequence()
                          .AppendInterval(2f)
                          .AppendCallback(() => StatusButtonsAllCell(true));
            }
            else
            {
                StatusButtonsAllCell(false);

                yield return new WaitForSeconds(0.5f);

                numberIncorrectAnswers++;

                firstOpen.Close();
                secondOpen.Close();

                StatusButtonsAllCell(true);
            }

            firstOpen = null;
            secondOpen = null;
        }

        public void ResetCounterCorrectAnswers()
        {
            counterCorrectAnswers = 0;
        }

        public void StatusButtonsAllCell(bool _enable)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].GetComponent<Button>().interactable = _enable;
            }
        }
    }
}
