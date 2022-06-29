using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MathPuzzle
{
    public class ScreenShop : MonoBehaviour
    {
        [SerializeField] private Button closeButton = null;

        [Space]
        [SerializeField] private string keyGameMenuScreen = null;

        private MenuScreenController screenController;

        private void Start()
        {
            screenController = GetComponentInParent<MenuScreenController>();

            closeButton.onClick.AddListener(CloseShop);
        }

        private void CloseShop()
        {
            screenController.OpenScreen(keyGameMenuScreen);
        }
    }
}
