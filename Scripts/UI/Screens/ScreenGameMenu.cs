using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MathPuzzle
{
    public class ScreenGameMenu : MonoBehaviour
    {
        [SerializeField] private Button startGameButton = null;
        [SerializeField] private Button openSettingsButton = null;       
        [SerializeField] private Button openShopButton = null;

        [Space]
        [SerializeField] private string keySettingsScreen = null;
        [SerializeField] private string keyShopScreen = null;

        private MenuScreenController screenController;

        private void Start()
        {
            screenController = GetComponentInParent<MenuScreenController>();

            startGameButton.onClick.AddListener(StartGame);
            openSettingsButton.onClick.AddListener(OpenSettings);
            openShopButton.onClick.AddListener(OpenShop);
        }

        private void StartGame()
        {
            SceneManager.LoadScene("WorldScene");
        }

        private void OpenSettings()
        {
            screenController.OpenScreen(keySettingsScreen);
        }

        private void OpenShop()
        {
            screenController.OpenScreen(keyShopScreen);
        }
    }
}
