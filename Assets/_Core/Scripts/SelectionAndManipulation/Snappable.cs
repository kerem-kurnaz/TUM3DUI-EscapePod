using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Core.Scripts.SelectionAndManipulation
{
    public class Snappable : MonoBehaviour
    {
        public SnapType SnappableType => snappableType;
        [SerializeField] private Transform snapper;
        [SerializeField] private Transform snapperParent;
        [SerializeField] private SnapType snappableType;
        private Renderer _renderer;
        
        public bool _inSnappingRange { get; private set; } = false;

        private void Awake()
        {
            snapper = transform.parent.parent;
            snapperParent = snapper.parent;
            if (snapperParent == null)
            {
                snapperParent = snapper;
            }
            _renderer = snapper.GetComponent<Renderer>();
        }

        public void SnapToPosition(Vector3 snapPosition)
        {
            for (int i = 0; i < 2; i++)//weird fix idk
            {
                var offset = snapPosition - _renderer.bounds.center;
                snapperParent.position += offset;
                snapperParent.rotation = Quaternion.identity;
            }
        }
        
        public Transform GetSnapperTransform()
        {
            return snapper;
        }
        
        public Transform GetSnapperParentTransform()
        {
            return snapperParent;
        }
        
        public Vector3 GetCenter()
        {
            return _renderer.bounds.center;
        }
        
        public void SetSnappingRange(bool inSnappingRange)
        {
            _inSnappingRange = inSnappingRange;
        }
    }
}

public enum SnapType
{
    OxygenTank,
    Default,
    
}
