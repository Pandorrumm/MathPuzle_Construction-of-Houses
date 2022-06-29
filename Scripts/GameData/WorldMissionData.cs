using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPuzzle
{
    [System.Serializable]
    public class WorldMissionData
    {
        public string elementID;
        public Sprite constructionElementSprite;
        public int numberCorrectAnswersToNextTask;

        public WorldMissionData Copy()
        {
            WorldMissionData result = new WorldMissionData();

            result.elementID = elementID;
            result.constructionElementSprite = constructionElementSprite;
            result.numberCorrectAnswersToNextTask = numberCorrectAnswersToNextTask;

            return result;
        }
    }
}
