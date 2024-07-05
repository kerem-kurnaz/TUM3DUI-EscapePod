using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Core.Scripts.SelectionAndManipulation
{
    public class Snappable : MonoBehaviour
    {
        public SnapType SnappableType => snappableType;
        [SerializeField] private Transform targetToSnap;
        [FormerlySerializedAs("snapType")] [SerializeField] private SnapType snappableType;

        private void Awake()
        {
            targetToSnap = transform.parent.parent;
        }

        public void SnapToPosition(Vector3 snapPosition)
        {
            targetToSnap.position = snapPosition;
        }
        
        public Transform GetTargetToSnap()
        {
            return targetToSnap;
        }
    }
}

public enum SnapType
{
    OxygenTank,
}
