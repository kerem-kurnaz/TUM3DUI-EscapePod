using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Core.Scripts.SelectionAndManipulation
{
    public class Selectable : MonoBehaviour
    {
        public Action OnSelect;
        public Action OnDeselect;
        public Transform SelectableTransform => _selectableTransform;
        
        [SerializeField] private Material highlightMaterial;
        
        private Transform _selectableTransform;
        private Renderer _renderer;
        private Material _defaultMaterial;
        private Movable _movable;
        private Color _defaultHighlightColor;
        private bool _activeState = false;
        
        private bool _isBeingMoved = false;

        private void Awake()
        {
            _selectableTransform = transform.parent.parent;
            _defaultHighlightColor = highlightMaterial.color;
            _renderer = _selectableTransform.GetComponent<Renderer>();
            _defaultMaterial = new Material(_renderer.material);
        }

        private void Start()
        {
            _movable.OnStartMoving += SetIsBeingMovedTrue;
            _movable.OnStopMoving += SetIsBeingMovedFalse;
            _movable.OnStopMoving += SetMaterialToDefault;
        }

        private void OnDisable()
        {
            highlightMaterial.color = _defaultHighlightColor;
        }

        public void Select()
        {
            _activeState = true;
            _renderer.material = highlightMaterial;
            OnSelect?.Invoke();
        }

        public void DeSelect()
        {
            _activeState = false;
            if (!_isBeingMoved)
            {
                SetMaterialToDefault();
            }
            OnDeselect?.Invoke();
        }

        public bool IsSelected()
        {
            return _activeState;
        }
        
        public void SetMovable(Movable movable)
        {
            _movable = movable;
        }
        
        private void SetIsBeingMovedTrue()
        {
            _isBeingMoved = true;
        }
        private void SetIsBeingMovedFalse()
        {
            _isBeingMoved = false;
        }
        private void SetMaterialToDefault()
        {
            if (!_activeState)
            {
                _renderer.material = _defaultMaterial;
            }
        }
    }
}
