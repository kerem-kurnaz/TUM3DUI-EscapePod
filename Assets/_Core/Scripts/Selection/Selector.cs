using System;
using UnityEngine;

namespace _Core.Scripts.Selection
{
    public class Selector : MonoBehaviour
    {
        [SerializeField] private Transform selectorStartPoint;
        [SerializeField] private LayerMask selectableLayers;

        private bool _canSelect = false;
        
        private Selectable _lastSelection = null;
        private Vector3 _selectionDirection;

        private void Awake()
        {
            GameManager.Instance.SetSelector(this);
        }

        private void Start()
        {
            GameManager.Instance.OnArControllerAvailable += EnableCanSelect;
            GameManager.Instance.OnArControllerUnavailable += DisableCanSelect;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnArControllerAvailable -= EnableCanSelect;
            GameManager.Instance.OnArControllerUnavailable -= DisableCanSelect;
        }

        private void FixedUpdate()
        {
            if (!_canSelect)
                return;
            
            _selectionDirection = selectorStartPoint.forward;
            var intersects = Physics.Raycast(selectorStartPoint.position, _selectionDirection, out var hitInfo, Mathf.Infinity, selectableLayers);
            if (intersects)
            {
                var newSelection = hitInfo.transform.gameObject.GetComponentInChildren<Selectable>();
                // Only activate new selectable if previous one was deselected
                if(_lastSelection && newSelection != _lastSelection)
                {
                    _lastSelection.DeSelect();
                    _lastSelection = null;
                }
                if (newSelection)
                {
                    newSelection.Select();
                    _lastSelection = newSelection;
                }
            }
            else
            {
                if (_lastSelection)
                {
                    _lastSelection.DeSelect();
                    _lastSelection = null;
                }   

            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(selectorStartPoint.position, _selectionDirection * 999f);
        }
        
        private void EnableCanSelect()
        {
            _canSelect = true;
        }
        private void DisableCanSelect()
        {
            _canSelect = false;
        }
        
        public Transform GetSelectorStartPoint()
        {
            return selectorStartPoint;
        }
    }
}
