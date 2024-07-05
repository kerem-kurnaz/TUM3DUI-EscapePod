using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Core.Scripts.SelectionAndManipulation
{
    public class Snapper : MonoBehaviour
    {
        [SerializeField] private Transform snapperObject;
        [SerializeField] private SnapType snapperType;
        private Snappable _currentSnappable;
        private GameObject _snapHighlight;

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
                _currentSnappable.GetTargetToSnap().GetComponentInChildren<Movable>().OnMoveInputUp -= SnapObject;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            var snappable = other.GetComponentInChildren<Snappable>();
            if (snappable != null)
            {
                if (snapperType != snappable.SnappableType) return;
                
                _currentSnappable = snappable;
                _currentSnappable.GetTargetToSnap().GetComponentInChildren<Movable>().OnMoveInputUp += SnapObject;
                CreateHighlightClone(_currentSnappable.GetTargetToSnap().gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var snappable = other.GetComponentInChildren<Snappable>();
            if (snappable != null && snappable == _currentSnappable)
            {
                _currentSnappable.GetTargetToSnap().GetComponentInChildren<Movable>().OnMoveInputUp -= SnapObject;
                _currentSnappable = null;
                DestroyHighlightClone();
            }
        }
        
        public void SnapObject()
        {
            if (_currentSnappable != null)
            {
                DestroyHighlightClone();
                _currentSnappable.SnapToPosition(GetSnapPosition(snapperObject));
            }
        }

        private static Vector3 GetSnapPosition(Transform snapper)
        {
            var snapperCollider = snapper.GetComponent<Collider>();
            return snapper.position + Vector3.up * (snapperCollider.bounds.size.y);
        }
        
        private void CreateHighlightClone(GameObject snappableObject)
        {
            DestroyHighlightClone();

            _snapHighlight = Instantiate(snappableObject, GetSnapPosition(snapperObject), snappableObject.transform.rotation);
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
            foreach (var col in colliders)
            {
                Destroy(col);
            }
            
            // Remove any other components that shouldn't be in the highlight
            var scripts = obj.GetComponentsInChildren<MonoBehaviour>();
            foreach (var script in scripts)
            {
                Destroy(script);
            }

            // If the original has Rigidbody, remove it
            var rigidbodies = obj.GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rigidbodies)
            {
                Destroy(rb);
            }
        }

        private void DestroyHighlightClone()
        {
            if (_snapHighlight != null)
            {
                Destroy(_snapHighlight);
            }
        }

    }
}
