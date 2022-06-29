using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

namespace MathPuzzle
{
    public class Plot : MonoBehaviour, IPointerClickHandler
    {
        public string keyPlot = null;

        [Space]
        public int priceOfPlot = 0;
        [SerializeField] private TMP_Text priceText = null;

        [Space]
        [SerializeField] private GameConfig gameConfig = null;

        public bool isPlotPurchased = false;

        private PlotsUpdater plotsUpdater;

        private void Awake()
        {
            plotsUpdater = GetComponentInParent<PlotsUpdater>();

            if (!PlayerPrefs.HasKey(keyPlot + " PlotPurchased"))
            {
                PlayerPrefs.SetInt(keyPlot + " PlotPurchased", 0);
            }
            else
            {
                if (PlayerPrefs.GetInt(keyPlot + " PlotPurchased") == 1)
                {
                    isPlotPurchased = true;
                    priceOfPlot = 0;
                }
                else
                {
                    isPlotPurchased = false;
                }
            }

            if (priceText)
            {
                priceText.text = "" + priceOfPlot + " $";
            }         
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (PlayerPrefs.GetInt(keyPlot + " isBuiltHouse") != 1)
            {
                int currentScore = PlayerPrefs.GetInt("Score");

                gameConfig.globalMissionName = keyPlot;
                gameConfig.currentMissionIndex = plotsUpdater.SearchIndexPlot(keyPlot);
                gameConfig.currentGameTypeKey = gameConfig.allMissions[gameConfig.currentMissionIndex].gameTypeData.gameType.key;

                if (currentScore >= priceOfPlot)
                {
                    PlayerPrefs.SetInt("Score", currentScore - priceOfPlot);

                    PlayerPrefs.SetInt(keyPlot + " PlotPurchased", 1);

                    isPlotPurchased = true;
                    SceneManager.LoadScene("GameScene");
                }
            }                           
        }
    }
}
