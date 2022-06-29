using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MathPuzzle
{
    public class BuildingMaterialsAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject buildingPlot = null;
        [SerializeField] private GameObject buildingMaterialsIcon = null;

        [Space]
        [SerializeField] private float durationMove = 2f;

        private GameObject icon;

        private void OnEnable()
        {
            FindOneSummand.CreateBuildingMaterialsIconEvent += CreateIcon;
            Memory.CreateBuildingMaterialsIconEvent += CreateIcon;
            FindTwoSummand.CreateBuildingMaterialsIconEvent += CreateIcon;
            FindSum.CreateBuildingMaterialsIconEvent += CreateIcon;
        }

        private void OnDisable()
        {
            FindOneSummand.CreateBuildingMaterialsIconEvent -= CreateIcon;
            Memory.CreateBuildingMaterialsIconEvent -= CreateIcon;
            FindTwoSummand.CreateBuildingMaterialsIconEvent -= CreateIcon;
            FindSum.CreateBuildingMaterialsIconEvent -= CreateIcon;
        }

        private void CreateIcon(Vector3 _position)
        {
            icon = Instantiate(buildingMaterialsIcon, _position, Quaternion.identity);
            icon.transform.SetParent(gameObject.transform);

            MoveIcon();
        }

        private void MoveIcon()
        {
            icon.transform.DOMove(buildingPlot.transform.position, durationMove).OnComplete(() => Destroy(icon));
        }
    }
}
