using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Core.Scripts.Selection
{
    public class Selectable : MonoBehaviour
    {
        public Action OnSelect;
        public Action OnDeselect;
        
        [SerializeField] private Material highlightMaterial;
        
        private Renderer _renderer;
        private Material _defaultMaterial;
        private Color _defaultHighlightColor;
        private bool _activeState = false;

        private void Awake()
        {
            _defaultHighlightColor = highlightMaterial.color;
            _renderer = transform.parent.GetComponent<Renderer>();
            _defaultMaterial = new Material(_renderer.material);
        }

        private void OnDisable()
        {
            highlightMaterial.color = _defaultHighlightColor;
        }

        private void Update () {
            if( _activeState && Input.GetKeyDown(KeyCode.Mouse0))
            {
                // remove using System; entry that is auto generated
                highlightMaterial.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            }
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
            _renderer.material = _defaultMaterial ;
            OnDeselect?.Invoke();
        }

        public bool IsSelected()
        {
            return _activeState;
        }
    }
}
