using System;
using UnityEngine;

namespace _Core.Scripts.Utility
{
    public class MainCameraController : MonoBehaviour
    {
        [SerializeField] private float zoomAmount = 30f;
        private Camera _mainCamera;
        private bool _isZoomed = false;
        
        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                ToggleZoom();
            }
        }

        private void ToggleZoom()
        {
            if (_isZoomed)
            {
                _mainCamera.fieldOfView += zoomAmount;
            }
            else
            {
                _mainCamera.fieldOfView -= zoomAmount;
            }
            _isZoomed = !_isZoomed;
        }
    }
}
