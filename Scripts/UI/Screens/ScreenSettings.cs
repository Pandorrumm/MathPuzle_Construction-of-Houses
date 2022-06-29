using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MathPuzzle
{
    public class ScreenSettings : MonoBehaviour
    {
        [SerializeField] private Button closeButton = null;

        [Space]
        [SerializeField] private string keyGameMenuScreen = null;

        private MenuScreenController screenController;

        private void Start()
        {
            screenController = GetComponentInParent<MenuScreenController>();

            closeButton.onClick.AddListener(CloseSettings);            
        }

        private void CloseSettings()
        {
            screenController.OpenScreen(keyGameMenuScreen);
        }
    }
}
