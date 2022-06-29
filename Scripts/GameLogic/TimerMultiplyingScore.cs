using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;

namespace MathPuzzle
{
    public class TimerMultiplyingScore : MonoBehaviour
    {
        [SerializeField] private Slider slider = null;
        [SerializeField] private float durationChangeValue = 0;
        [SerializeField] private TMP_Text scoreMultiplierText = null;

        [Space]
        [SerializeField] private int showTimerOftenIndex = 3;

        public bool isActivateTimer = false;

        [Space]
        public bool isX2Score = false;
        public bool isX3Score = false;
        public bool isX4Score = false;
        public bool isX5Score = false;

        private int indexCorrectAnswers = 1;
        private int indexCorrectAnswersWithTimerActive = 0;

        private IEnumerator sliderCoroutine;

        private void OnEnable()
        {
            FindOneSummand.StartTimerMultiplyingScoreEvent += StartTimer;
            FindOneSummand.DisableTimerMultiplyingScoreEvent += DisableSlider;
            FindOneSummand.PauseSliderEvent += PauseSlider;

            FindTwoSummand.StartTimerMultiplyingScoreEvent += StartTimer;
            FindTwoSummand.DisableTimerMultiplyingScoreEvent += DisableSlider;
            FindTwoSummand.PauseSliderEvent += PauseSlider;

            FindSum.StartTimerMultiplyingScoreEvent += StartTimer;
            FindSum.DisableTimerMultiplyingScoreEvent += DisableSlider;
            FindSum.PauseSliderEvent += PauseSlider;

        }

        private void OnDisable()
        {
            FindOneSummand.StartTimerMultiplyingScoreEvent -= StartTimer;
            FindOneSummand.DisableTimerMultiplyingScoreEvent -= DisableSlider;
            FindOneSummand.PauseSliderEvent -= PauseSlider;

            FindTwoSummand.StartTimerMultiplyingScoreEvent -= StartTimer;
            FindTwoSummand.DisableTimerMultiplyingScoreEvent -= DisableSlider;
            FindTwoSummand.PauseSliderEvent -= PauseSlider;

            FindSum.StartTimerMultiplyingScoreEvent -= StartTimer;
            FindSum.DisableTimerMultiplyingScoreEvent -= DisableSlider;
            FindSum.PauseSliderEvent -= PauseSlider;
        }

        private void Start()
        {
            sliderCoroutine = StartSlider();
            slider.gameObject.SetActive(false);
        }

        private void StartTimer()
        {
            if (!slider.gameObject.activeSelf)
            {
                indexCorrectAnswers++;

                if (indexCorrectAnswers % showTimerOftenIndex == 0)
                {
                    slider.gameObject.SetActive(true);

                    isX2Score = true;
                    scoreMultiplierText.text = "X2";

                    isActivateTimer = true;

                    ChangingSliderValue();
                }
            }
            else
            {
                indexCorrectAnswersWithTimerActive++;

                if (indexCorrectAnswersWithTimerActive == 1)
                {
                    ChangingScoreMultiplier("X3", ChangingMultiplierX3);

                    void ChangingMultiplierX3()
                    {
                        isX2Score = false;
                        isX3Score = true;
                    }
                }
                else if (indexCorrectAnswersWithTimerActive == 2)
                {
                    ChangingScoreMultiplier("X4", ChangingMultiplierX4);

                    void ChangingMultiplierX4()
                    {
                        isX3Score = false;
                        isX4Score = true;
                    }
                }
                else if (indexCorrectAnswersWithTimerActive > 2)
                {
                    ChangingScoreMultiplier("X5", ChangingMultiplierX5);

                    void ChangingMultiplierX5()
                    {
                        isX4Score = false;
                        isX5Score = true;
                    }
                }
            }
        }

        private void ChangingSliderValue()
        {
            slider.value = slider.maxValue;
            StartCoroutine(sliderCoroutine);
        }


        private IEnumerator StartSlider()
        {
            while (slider.value > 0)
            {
                slider.value -= Time.deltaTime;

                if (slider.value <= 0)
                {
                    DisableSlider();
                }

                yield return null;
            }
        }

        private void PauseSlider()
        {
            StopCoroutine(sliderCoroutine);
        }

        private void ChangingScoreMultiplier(string _textMultiplier, Action ChangingMultiplier)
        {
            DOTween.Sequence()
                       .AppendInterval(0.3f)
                       .AppendCallback(() => ChangingMultiplier?.Invoke());

            ChangingSliderValue();

            scoreMultiplierText.text = _textMultiplier;
        }


        private void DisableSlider()
        {
            PauseSlider();
            isActivateTimer = false;

            slider.gameObject.SetActive(false);

            indexCorrectAnswersWithTimerActive = 0;

            isX2Score = false;
            isX3Score = false;
            isX4Score = false;
            isX5Score = false;
        }
    }
}
