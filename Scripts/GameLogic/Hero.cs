using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MathPuzzle
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float moveOffset = 80f;

        private void OnEnable()
        {
            FindOneSummand.GiveHeroTargetOnMoveEvent += MoveToTarget;
            FindTwoSummand.GiveHeroTargetOnMoveEvent += MoveToTarget;
            FindSum.GiveHeroTargetOnMoveEvent += MoveToTarget;
        }

        private void OnDisable()
        {
            FindOneSummand.GiveHeroTargetOnMoveEvent -= MoveToTarget;
            FindTwoSummand.GiveHeroTargetOnMoveEvent -= MoveToTarget;
            FindSum.GiveHeroTargetOnMoveEvent -= MoveToTarget;
        }

        private void MoveToTarget(Cell _targetCell)
        {
            gameObject.transform.DOMove(new Vector3(_targetCell.transform.position.x - moveOffset,
                                                    _targetCell.transform.position.y - moveOffset,
                                                    _targetCell.transform.position.z), 1f);
        }
    }
}
