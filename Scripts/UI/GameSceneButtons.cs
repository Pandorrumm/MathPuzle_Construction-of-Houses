using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MathPuzzle
{
    public class GameSceneButtons : MonoBehaviour
    {
        [SerializeField] private Button openWorldMapButton = null;

        private void Start()
        {
            openWorldMapButton.onClick.AddListener(OpenWorldMap);
        }

        private void OpenWorldMap()
        {
            SceneManager.LoadScene("WorldScene");
        }
    }
}
