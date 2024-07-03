using UnityEngine;
using Random = UnityEngine.Random;

namespace _Core.Scripts.Selection
{
    public class Selectable : MonoBehaviour
    {
        public Material highlightMaterial;
        private Material _defaultMaterial;

        private bool _activeState = false;
        private Renderer _renderer;

        private void Awake()
        {
            _defaultMaterial = new Material(GetComponent<Renderer>().material);
            _renderer = GetComponent<Renderer>();
        }

        private void Update () {
            if( _activeState && Input.GetKeyDown(KeyCode.Mouse0))
            {
                // remove using System; entry that is auto generated
                _defaultMaterial.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            }
        }

        public void Select()
        {
            _activeState = true;
            _renderer.material = highlightMaterial;
        }

        public void DeSelect()
        {
            _activeState = false;
            _renderer.material = _defaultMaterial ;
        }

        public bool IsSelected()
        {
            return _activeState;
        }
    }
}
