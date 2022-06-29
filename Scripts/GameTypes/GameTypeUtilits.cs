using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MathPuzzle
{
    public static class GameTypeUtilits
    {
        public static bool SearchMatchesNumber(List<int> _numbers, int _value)
        {
            for (int i = 0; i < _numbers.Count; i++)
            {
                if (_numbers[i] == _value)
                {
                    return true;
                }
            }
            return false;
        }

        public static void AssignCellDefaultSprite(List<Cell> _cells, Sprite _defaultSprite, Color _defaultColor)
        {
            for (int i = 0; i < _cells.Count; i++)
            {
                _cells[i].gameObject.GetComponent<Image>().sprite = _defaultSprite;
                _cells[i].gameObject.GetComponentInChildren<TMP_Text>().color = _defaultColor;
            }
        }

        public static void ChangeCellSprite(Cell _cell, Sprite _sprite, Color _color)
        {
            _cell.gameObject.GetComponent<Image>().sprite = _sprite;
            _cell.GetComponentInChildren<TMP_Text>().color = _color;
        }

        public static void StatusButtonsAllCell(List<Cell> _cells, bool _enable)
        {
            for (int i = 0; i < _cells.Count; i++)
            {
                _cells[i].GetComponent<Button>().interactable = _enable;
            }
        }
    }
}
