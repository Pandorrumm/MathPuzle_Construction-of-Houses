using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPuzzle
{
    public class ChoosingGameType : MonoBehaviour
    {
        [SerializeField] private GameConfig gameConfig = null;

        [System.Serializable]
        public class GameTypeData
        {
            public string key;
            public GameObject gameType;
            public GameObject cellsForGame;
        }

        public List<GameTypeData> gameTypes = new List<GameTypeData>();

        private void Start()
        {
            ChoiceGames(gameConfig.currentGameTypeKey);
        }

        public void ChoiceGames(string _key)
        {
            foreach (GameTypeData type in gameTypes)
            {
                type.gameType.SetActive(false);
                type.cellsForGame.SetActive(false);

                if (type.key == _key)
                {
                    type.gameType.SetActive(true);
                    type.cellsForGame.SetActive(true);
                }
            }
        }
    }
}
