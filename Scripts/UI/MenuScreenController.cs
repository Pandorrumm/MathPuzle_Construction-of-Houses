using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPuzzle
{
    public class MenuScreenController : MonoBehaviour
    {
        [System.Serializable]
        public class ScreenData
        {
            public string key;
            public GameObject screenPanel;
        }

        public List<ScreenData> screensData = new List<ScreenData>();

        public void OpenScreen(string _key)
        {
            foreach (ScreenData screenData in screensData)
            {
                screenData.screenPanel.SetActive(false);

                if (screenData.key == _key)
                {
                    screenData.screenPanel.SetActive(true);
                }
            }
        }

        public void CloseAllScreen()
        {
            foreach (ScreenData screenData in screensData)
            {
                screenData.screenPanel.SetActive(false);
            }
        }
    }
}
