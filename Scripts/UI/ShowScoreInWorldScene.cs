using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MathPuzzle
{
    public class ShowScoreInWorldScene : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText = null;

        private void OnEnable()
        {
            DecoratingObjects.UpdateUIScoreEvent += ShowScore;
        }

        private void OnDisable()
        {
            DecoratingObjects.UpdateUIScoreEvent -= ShowScore;
        }

        private void Start()
        {
            ShowScore();
        }

        private void ShowScore()
        {
            scoreText.text = "" + PlayerPrefs.GetInt("Score");
        }
    }
}
