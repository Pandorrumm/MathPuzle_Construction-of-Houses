using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

namespace MathPuzzle
{
    public class FindOneSummand : MonoBehaviour
    {
        [System.Serializable]
        public class CellData
        {
            public List<Cell> cells;
            public List<int> numbers = new List<int>();
        }

        public CellData gameCells = new CellData();

        [Space]
        [SerializeField] private GameConfig gameConfig = null;

        [Space]
        [SerializeField] private int numberPointsForAnswer = 10;

        [Space]
        [SerializeField] private Cell currentGameCell = null;
        [SerializeField] private int numberInCurrentGameCell = 0;

        [Space]
        public int numberInTask;

        [Space]
        public int numberCorrectAnswersToNextTask = 0;
        public int counterCorrectAnswers;

        [Space]
        [Header("Cell Sprite")]
        [SerializeField] private Sprite defaultSprite = null;
        [SerializeField] private Sprite rightAnswerSprite = null;
        [SerializeField] private Sprite wrongAnswerSprite = null;

        [Space]
        [Header("Cell Text Color")]
        [SerializeField] private Color defaultColor = new Color();
        [SerializeField] private Color rightAnswerColor = new Color();
        [SerializeField] private Color wrongAnswerColor = new Color();

        [Space]
        [Header("DoTween")]
        [SerializeField] private float durationCellShakeWin = 0.5f;
        [SerializeField] private float strenghtCellShakeWin = 1f;
        [Space]
        [SerializeField] private float durationCellShakeLose = 0.5f;
        [SerializeField] private float strenghtCellShakeLose = 15f;

        private List<int> sumsForCurrentNumber = new List<int>();
        private List<int> allNumbersWithoutCurrent = new List<int>();

        [Space]
        public int numberIncorrectAnswers;

        public static Action<int> SetNumberTaskEvent;
        public static Action StartTimerMultiplyingScoreEvent;
        public static Action DisableTimerMultiplyingScoreEvent;
        public static Action PauseSliderEvent;
        public static Action<int> AddScoreEvent;
        public static Action MakeIncorrectAnswerTaskEffectEvent;
        public static Action<Cell> GiveHeroTargetOnMoveEvent;

        public static Action<int> TakeALifeEvent;

        public static Action<int> NextTaskEvent;
        public static Action<Vector3> CreateBuildingMaterialsIconEvent;

        public static Action<int> SetFirstSummandNumberEvent;

        private void OnEnable()
        {
            Lives.ChangeNumberIncorrectAnswersEvent += ChangeNumberIncorrectAnswers;
        }

        private void OnDisable()
        {
            Lives.ChangeNumberIncorrectAnswersEvent -= ChangeNumberIncorrectAnswers;
        }

        private void Start()
        {
            StartNewGame();
        }

        private void StartNewGame()
        {
            AssignNumbers();
            DeleteCurrentNumber(gameCells.numbers, currentGameCell);
          
            numberInCurrentGameCell = SearchNumberByCell(currentGameCell);

            SetFirstSummandNumberEvent(numberInCurrentGameCell);

            SetNumberTaskEvent?.Invoke(numberInTask);

            GameTypeUtilits.ChangeCellSprite(currentGameCell, rightAnswerSprite, rightAnswerColor);
            GameTypeUtilits.StatusButtonsAllCell(gameCells.cells, true);
        }

        public void AssignNumbers()
        {
            for (int i = 0; i < gameCells.numbers.Count; i++)
            {
                int index;

                while (true)
                {
                    index = UnityEngine.Random.Range(0, 20);

                    if (!GameTypeUtilits.SearchMatchesNumber(gameCells.numbers, index))
                    {
                        break;
                    }
                }

                gameCells.numbers[i] = index;

                gameCells.cells[i].GetComponentInChildren<TMP_Text>().text = "" + gameCells.numbers[i];
            }
        }

        private void DeleteCurrentNumber(List<int> _numbers, Cell _currentCell)
        {
            int currentNumber = SearchNumberByCell(_currentCell);

            allNumbersWithoutCurrent.AddRange(_numbers);

            for (int i = 0; i < allNumbersWithoutCurrent.Count; i++)
            {
                allNumbersWithoutCurrent.Remove(currentNumber);
            }

            SumNumbersWithCurrentNumber(allNumbersWithoutCurrent, currentNumber);

            allNumbersWithoutCurrent.Clear();
        }

        private void SumNumbersWithCurrentNumber(List<int> _numbers, int _currentNumber)
        {
            for (int i = 0; i < _numbers.Count; i++)
            {
                int sum = _currentNumber;

                sum += _numbers[i];
                sumsForCurrentNumber.Add(sum);
            }

            numberInTask = GetRandomNumber(sumsForCurrentNumber);

            sumsForCurrentNumber.Clear();
        }


        private int GetRandomNumber(List<int> _numbers)
        {
            int index = UnityEngine.Random.Range(0, _numbers.Count);
            return _numbers[index];
        }

        public int SearchNumberByCell(Cell _cell)
        {
            for (int i = 0; i < gameCells.cells.Count; i++)
            {
                if (gameCells.cells[i] == _cell)
                {
                    return gameCells.numbers[i];
                }
            }

            return 0;
        }

        public void CheckingCompletionTask(Cell _cell, int _numberInCell)
        {
            if ((numberInCurrentGameCell + _numberInCell) == numberInTask)
            {
                counterCorrectAnswers++;

                NextTaskEvent?.Invoke(counterCorrectAnswers);
                AddScoreEvent?.Invoke(numberPointsForAnswer);

                GameTypeUtilits.ChangeCellSprite(_cell, rightAnswerSprite, rightAnswerColor);

                currentGameCell.GetComponentInChildren<TMP_Text>().text = "";
                _cell.GetComponentInChildren<TMP_Text>().text = "" + numberInTask;

                _cell.gameObject.transform.DOShakeScale(durationCellShakeWin, strenghtCellShakeWin);

                GiveHeroTargetOnMoveEvent?.Invoke(_cell);

                CreateBuildingMaterialsIconEvent?.Invoke(_cell.transform.position);

                currentGameCell = _cell;

                PauseSliderEvent?.Invoke();

                GameTypeUtilits.StatusButtonsAllCell(gameCells.cells, false);

                DOTween.Sequence()
                          .AppendInterval(2f)
                          .AppendCallback(() => NewGame());

                void NewGame()
                {
                    if (PlayerPrefs.GetInt(gameConfig.globalMissionName + " isBuiltHouse") != 1)
                    {
                        GameTypeUtilits.AssignCellDefaultSprite(gameCells.cells, defaultSprite, defaultColor);
                        StartNewGame();

                        StartTimerMultiplyingScoreEvent?.Invoke();
                    }               
                }
            }
            else
            {
                numberIncorrectAnswers++;

                GameTypeUtilits.ChangeCellSprite(_cell, wrongAnswerSprite, wrongAnswerColor);

                _cell.gameObject.transform.DOShakeRotation(durationCellShakeLose, strenghtCellShakeLose);

                TakeALifeEvent?.Invoke(numberIncorrectAnswers);
                MakeIncorrectAnswerTaskEffectEvent?.Invoke();
                DisableTimerMultiplyingScoreEvent?.Invoke();
            }
        }

        private void ChangeNumberIncorrectAnswers()
        {
            numberIncorrectAnswers--;
        }

        public void ResetCounterCorrectAnswers()
        {
            counterCorrectAnswers = 0;
        }
    }
}
