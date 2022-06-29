using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPuzzle
{
    [CreateAssetMenu(fileName = "CellStatusData", menuName = "GameData/CellStatusData")]
    public class CellStatusData : ScriptableObject
    {
        [System.Serializable]
        public class CellsStatus
        {
            public Status statusCell;

            public enum Status
            {
                ACTIVE,
                INACTIVE
            }
        }

        public List<CellsStatus> cellStatuses = new List<CellsStatus>();
    }
}
