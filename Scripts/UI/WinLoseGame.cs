using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

namespace MathPuzzle
{
    public class WinLoseGame : MonoBehaviour
    {
        [SerializeField] private GameObject winScreen = null;
        [SerializeField] private GameObject loseScreen = null;
        [Space]
        [Header("LoseScreenButton")]
        [SerializeField] private Button restartGameButton = null;
        [SerializeField] private Button purchaseLiveButton = null;
        [SerializeField] private Button worldMapButton = null;

        [Space]
        [SerializeField] private Button openWorldMapButton = null;

        public static Action PurchasingLiveAdEvent;

        private void OnEnable()
        {
            MissionsTracker.OpenVictoryScreenEvent += OpenWinScreen;
            Lives.OpenLoseScreenEvent += OpenLoseScreen;
        }

        private void OnDisable()
        {
            MissionsTracker.OpenVictoryScreenEvent -= OpenWinScreen;
            Lives.OpenLoseScreenEvent -= OpenLoseScreen;
        }

        private void Start()
        {
            winScreen.SetActive(false);
            loseScreen.SetActive(false);

            openWorldMapButton.onClick.AddListener(OpenWorldMap);
            worldMapButton.onClick.AddListener(OpenWorldMap);
            restartGameButton.onClick.AddListener(RestarGame);
            purchaseLiveButton.onClick.AddListener(PurchaseLive);
        }

        private void OpenWinScreen()
        {
            winScreen.SetActive(true);
        }

        private void OpenLoseScreen()
        {
            loseScreen.SetActive(true);
        }

        private void OpenWorldMap()
        {
            DOTween.KillAll();
            SceneManager.LoadScene("WorldScene");
        }

        private void RestarGame()
        {
            SceneManager.LoadScene("GameScene");
        }

        private void PurchaseLive()
        {
            loseScreen.SetActive(false);
            PurchasingLiveAdEvent?.Invoke();
        }
    }
}
