using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

namespace MathPuzzle
{
    public class FindTwoSummand : MonoBehaviour
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
        public int numberIncorrectAnswers;

        [Space]
        public int numberInTask;

        [Space]
        [SerializeField] private int numberPointsForAnswer = 10;

        [Space]
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

        private List<int> copyNumbers = new List<int>();
        private int summa;  
        
        private int firstNumber;
        private int secondNumber;

        private int currentFirstNumber;
        private int currentSecondNumber;

        private Cell firstCell;
        private Cell secondCell;    

        private int numberSelectedCells;

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
            GetRandomSum();
            SetNumberTaskEvent?.Invoke(numberInTask);
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
            numberSelectedCells++;

            if (numberSelectedCells == 1)
            {
                currentFirstNumber = _numberInCell;
                firstCell = _cell;
                GameTypeUtilits.ChangeCellSprite(firstCell, rightAnswerSprite, rightAnswerColor);
            }

            if (numberSelectedCells == 2)
            {
                currentSecondNumber = _numberInCell;
                secondCell = _cell;

                if ((currentFirstNumber + currentSecondNumber) == summa)
                {
                    counterCorrectAnswers++;

                    NextTaskEvent?.Invoke(counterCorrectAnswers);
                    AddScoreEvent?.Invoke(numberPointsForAnswer);

                    GameTypeUtilits.ChangeCellSprite(secondCell, rightAnswerSprite, rightAnswerColor);

                    CreateBuildingMaterialsIconEvent?.Invoke(secondCell.transform.position);

                    PauseSliderEvent?.Invoke();

                    GiveHeroTargetOnMoveEvent?.Invoke(secondCell);

                    GameTypeUtilits.StatusButtonsAllCell(gameCells.cells, false);

                    firstCell.gameObject.transform.DOShakeScale(durationCellShakeWin, strenghtCellShakeWin);
                    secondCell.gameObject.transform.DOShakeScale(durationCellShakeWin, strenghtCellShakeWin);


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

                    GameTypeUtilits.ChangeCellSprite(secondCell, wrongAnswerSprite, wrongAnswerColor);
                    GameTypeUtilits.ChangeCellSprite(firstCell, wrongAnswerSprite, wrongAnswerColor);

                    secondCell.gameObject.transform.DOShakeRotation(durationCellShakeLose, strenghtCellShakeLose);
                    firstCell.gameObject.transform.DOShakeRotation(durationCellShakeLose, strenghtCellShakeLose);

                    TakeALifeEvent?.Invoke(numberIncorrectAnswers);
                    MakeIncorrectAnswerTaskEffectEvent?.Invoke();
                    DisableTimerMultiplyingScoreEvent?.Invoke();

                    GameTypeUtilits.StatusButtonsAllCell(gameCells.cells, false);

                    Debug.Log("НЕПравильно");

                    DOTween.Sequence()
                         .AppendInterval(2f)
                         .AppendCallback(() => ContinueGame());

                    void ContinueGame()
                    {
                        GameTypeUtilits.AssignCellDefaultSprite(gameCells.cells, defaultSprite, defaultColor);
                        GameTypeUtilits.StatusButtonsAllCell(gameCells.cells, true);
                        // StartNewGame();
                        //  StartTimerMultiplyingScoreEvent?.Invoke();
                    }
                }
                
                numberSelectedCells = 0;
            }         
        }

        private void GetRandomSum()
        {
            copyNumbers.Clear();
            copyNumbers.AddRange(gameCells.numbers);

            firstNumber = copyNumbers[UnityEngine.Random.Range(0, copyNumbers.Count)];
            copyNumbers.Remove(firstNumber);
           
            secondNumber = copyNumbers[UnityEngine.Random.Range(0, copyNumbers.Count)];
            copyNumbers.Remove(secondNumber);
            
            summa = firstNumber + secondNumber;
            numberInTask = summa;           
        }

        public void ResetCounterCorrectAnswers()
        {
            counterCorrectAnswers = 0;
        }

        private void ChangeNumberIncorrectAnswers()
        {
            numberIncorrectAnswers--;
        }
    }
}
