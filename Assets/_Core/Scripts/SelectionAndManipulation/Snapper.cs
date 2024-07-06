using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Core.Scripts.SelectionAndManipulation
{
    public class Snapper : MonoBehaviour
    {
        public Action OnSnap;
        public Snappable CurrentSnappable => _currentSnappable;
        [SerializeField] private Transform snapperObject;
        [SerializeField] private SnapType snapperType;
        private Snappable _currentSnappable;
        private GameObject _snapHighlight;

        private bool _isSnapped = false;

        private void Awake()
        {
            if (snapperObject == null)
            {
                snapperObject = transform.parent;
            }
        }

        private void OnDisable()
        {
            if (_currentSnappable != null)
            {
                _currentSnappable.GetSnapperTransform().GetComponentInChildren<Movable>().OnMoveInputUp -= SnapObject;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            var snappable = other.GetComponentInChildren<Snappable>();
            if (snappable != null && !_isSnapped)
            {
                if (snapperType != snappable.SnappableType) return;
                
                _currentSnappable = snappable;
                _currentSnappable.GetSnapperTransform().GetComponentInChildren<Movable>().OnMoveInputUp += SnapObject;
                _currentSnappable.GetSnapperTransform().GetComponentInChildren<Movable>().OnStartMoving += SetIsSnappedFalse;
                CreateHighlightClone(_currentSnappable.GetSnapperParentTransform().gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var snappable = other.GetComponentInChildren<Snappable>();
            if (snappable != null && snappable == _currentSnappable)
            {
                _currentSnappable.GetSnapperTransform().GetComponentInChildren<Movable>().OnMoveInputUp -= SnapObject;
                _currentSnappable.GetSnapperTransform().GetComponentInChildren<Movable>().OnStartMoving -= SetIsSnappedFalse;
                _currentSnappable = null;
                DestroyHighlightClone();
            }
        }
        
        public void SnapObject()
        {
            if (_currentSnappable != null)
            {
                _isSnapped = true;
                DestroyHighlightClone();
                _currentSnappable.SnapToPosition(GetSnapPosition(snapperObject));
                OnSnap?.Invoke();
            }
        }

        private static Vector3 GetSnapPosition(Transform snapper)
        {
            var snapperCollider = snapper.GetComponent<Collider>();
            var snapperRenderer = snapper.GetComponent<Renderer>();
            return snapperRenderer.bounds.center;
            //return snapper.position + Vector3.forward * (snapperCollider.bounds.size.x);
        }
        
        private void CreateHighlightClone(GameObject snappableObject)
        {
            if (_isSnapped) return;
            
            DestroyHighlightClone();
            var snapPos = GetSnapPosition(snapperObject);
            
            _snapHighlight = Instantiate(snappableObject, snapPos, Quaternion.identity);
            
            var highlightCenter = _snapHighlight.GetComponentInChildren<Snappable>().GetCenter();
            var offset = snapPos - highlightCenter;
            _snapHighlight.transform.position += offset;
            
            _snapHighlight.transform.parent = snapperObject;

            RemoveUnwantedComponents(_snapHighlight);
            MakeTransparent(_snapHighlight);
        }

        private static void MakeTransparent(GameObject obj)
        {
            var renderers = obj.GetComponentsInChildren<Renderer>();
            foreach (var rend in renderers)
            {
                foreach (var mat in rend.materials)
                {
                    var originalColor = mat.color;
                    var transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f); // Adjust alpha for transparency
                    mat.color = transparentColor;

                    // Optionally, change the shader to support transparency if it's not already
                    if (mat.shader.name != "Transparent/Diffuse")
                    {
                        mat.shader = Shader.Find("Transparent/Diffuse");
                    }
                }
            }
        }
        
        private static void RemoveUnwantedComponents(GameObject obj)
        {
            var colliders = obj.GetComponentsInChildren<Collider>();
            if (colliders.Length != 0)
            {
                foreach (var col in colliders)
                {
                    Destroy(col);
                }
            }

            // Remove any other components that shouldn't be in the highlight
            var scripts = obj.GetComponentsInChildren<MonoBehaviour>();
            if (scripts.Length != 0)
            {
                foreach (var script in scripts)
                {
                    Destroy(script);
                }
            }

            // If the original has Rigidbody, remove it
            var rigidbodies = obj.GetComponentsInChildren<Rigidbody>();
            if (rigidbodies.Length != 0)
            {
                foreach (var rb in rigidbodies)
                {
                    Destroy(rb);
                }
            }
        }

        private void DestroyHighlightClone()
        {
            if (_snapHighlight != null)
            {
                Destroy(_snapHighlight);
            }
        }
        
        private void SetIsSnappedFalse()
        {
            _isSnapped = false;
        }
    }
}
