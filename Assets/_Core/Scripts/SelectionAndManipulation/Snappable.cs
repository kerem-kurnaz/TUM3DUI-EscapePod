using System;
using UnityEngine;

namespace _Core.Scripts.SelectionAndManipulation
{
    public class Snappable : MonoBehaviour
    {
        [SerializeField] private Transform targetToSnap;

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
