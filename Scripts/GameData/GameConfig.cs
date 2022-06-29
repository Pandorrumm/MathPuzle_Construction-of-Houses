using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPuzzle
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "GameData/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public string globalMissionName = "";
        public int currentMissionIndex = -1;
        public string currentGameTypeKey = "";

        [System.Serializable]
        public class MissionData
        {
            public List<MissionConfig> constructionElements = new List<MissionConfig>();

            public CellStatusData cellStatusData;

            public GameTypeData gameTypeData;
        }

        public List<MissionData> allMissions = new List<MissionData>();
    }
}
