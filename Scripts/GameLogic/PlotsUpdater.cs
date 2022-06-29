using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPuzzle
{
    public class PlotsUpdater : MonoBehaviour
    {
        [System.Serializable]
        public class PlotData
        {
            public Plot plot;
            public Sprite readHouseSprite;
            public GameObject price;
            public bool isBuiltHouse = false;
        }

        public List<PlotData> plots = new List<PlotData>();

        private void Start()
        {
            UpdateStatusPlots();
        }

        public int SearchIndexPlot(string _key)
        {
            foreach (PlotData plotsData in plots)
            {
                if (plotsData.plot.keyPlot == _key)
                {
                    return plots.IndexOf(plotsData);
                }
            }

            return 0;
        }

        private void UpdateStatusPlots()
        {
            for (int i = 0; i < plots.Count; i++)
            {
                if (PlayerPrefs.GetInt(plots[i].plot.keyPlot + " isBuiltHouse") == 1)
                {
                    plots[i].isBuiltHouse = true;

                    plots[i].plot.gameObject.GetComponent<SpriteRenderer>().sprite = plots[i].readHouseSprite;
                    plots[i].plot.priceOfPlot = 0;

                    plots[i].price.SetActive(false);                   
                }
                else
                {
                    if (plots[i].plot.isPlotPurchased)
                    {
                        plots[i].price.SetActive(false);
                    }
                }
            }
        }
    }
}
