using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPuzzle
{
    [CreateAssetMenu(fileName = "GameTypeData", menuName = "GameData/GameTypeData")]
    public class GameTypeData : ScriptableObject
    {
        [System.Serializable]
        public class GameType
        {
            public string key;
            public GameTypes type;

            public enum GameTypes
            {
                MEMORY,
                FIND_ONE_SUMMAND,
                FIND_TWO_SUMMAND,
                FIND_SUM
            }
        }

        public GameType gameType;
    }
}
