using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MathPuzzle
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText = null;
        [SerializeField] private int currentScore = 0;
        [SerializeField] private TimerMultiplyingScore timerMultiplyingScore = null;

        private void OnEnable()
        {
            FindOneSummand.AddScoreEvent += AddScore;
            Memory.AddScoreEvent += AddScore;
            FindTwoSummand.AddScoreEvent += AddScore;
            FindSum.AddScoreEvent += AddScore;

            MissionsTracker.SaveScoreEvent += SaveScore;
        }

        private void OnDisable()
        {
            FindOneSummand.AddScoreEvent -= AddScore;
            Memory.AddScoreEvent -= AddScore;
            FindTwoSummand.AddScoreEvent -= AddScore;
            FindSum.AddScoreEvent -= AddScore;

            MissionsTracker.SaveScoreEvent -= SaveScore;
        }

        private void Start()
        {
            UpdateScoreText(0);
        }

        private void AddScore(int _score)
        {
            if (timerMultiplyingScore.isX2Score)
            {
                currentScore += _score * 2;
            }
            else if (timerMultiplyingScore.isX3Score)
            {
                currentScore += _score * 3;
            }
            else if (timerMultiplyingScore.isX4Score)
            {
                currentScore += _score * 4;
            }
            else if (timerMultiplyingScore.isX5Score)
            {
                currentScore += _score * 5;
            }
            else
            {
                currentScore += _score;
            }

            UpdateScoreText(currentScore);
        }

        private void UpdateScoreText(int _score)
        {
            scoreText.text = "" + _score;           
        }

        private void SaveScore()
        {
            int totalScore = currentScore + PlayerPrefs.GetInt("Score");
            PlayerPrefs.SetInt("Score", totalScore);
        }    
    }
}
