using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPuzzle
{
    [CreateAssetMenu(fileName = "ConstructionElement", menuName = "GameData/ConstructionElement")]
    public class MissionConfig : ScriptableObject
    {
        public WorldMissionData data;
    }
}
