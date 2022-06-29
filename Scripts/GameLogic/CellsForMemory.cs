using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace MathPuzzle
{
    public class CellsForMemory : MonoBehaviour
    {
        [SerializeField] private GameConfig gameConfig;

        [Header("Memory")]
        [SerializeField] private GameObject imageUnknown = null;
        [SerializeField] private GameObject imageFace = null;

        private int _spriteId;
        public int spriteId
        {
            get { return _spriteId; }
        }

        private Memory memory;

        public static Action<Button> ClickButtonEvent;

        private void Start()
        {
            memory = FindObjectOfType<Memory>();

            GetComponent<Button>().onClick.AddListener(GetNumberInCell);

            if (imageFace != null)
            {
                imageFace.SetActive(false);
            }
        }

        private void GetNumberInCell()
        {
            if (gameConfig.allMissions[gameConfig.currentMissionIndex].gameTypeData.gameType.type == GameTypeData.GameType.GameTypes.MEMORY)
            {
                ClickButtonEvent?.Invoke(GetComponent<Button>());

                if (imageUnknown.activeSelf && memory.canOpen)
                {
                    imageUnknown.SetActive(false);
                    imageFace.SetActive(true);
                    memory.ImageOpened(this);
                }
            }
        }

        public void ChangeImage(int _id, Sprite _image)
        {
            _spriteId = _id;
            imageFace.GetComponent<Image>().sprite = _image;
        }

        public void Close()
        {
            imageUnknown.SetActive(true);
            imageFace.SetActive(false);
        }
    }
}
