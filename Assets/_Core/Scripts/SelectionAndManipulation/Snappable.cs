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
        private Renderer _renderer;

        private void Awake()
        {
            targetToSnap = transform.parent.parent;
            _renderer = targetToSnap.GetComponent<Renderer>();
        }

        public void SnapToPosition(Vector3 snapPosition)
        {
            var offset = snapPosition - _renderer.bounds.center;
            targetToSnap.position += offset;
            //targetToSnap.position = snapPosition;
            targetToSnap.rotation = Quaternion.identity;
        }
        
        public Transform GetTargetToSnap()
        {
            return targetToSnap;
        }
        
        public Vector3 GetCenter()
        {
            return _renderer.bounds.center;
        }
    }
}

public enum SnapType
{
    OxygenTank,
    Default,
    
}
