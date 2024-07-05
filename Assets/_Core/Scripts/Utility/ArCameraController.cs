using System;
using UnityEngine;

namespace _Core.Scripts
{
    public class ArCameraController : MonoBehaviour
    {
        private float _distanceBetweenMainCamera;
        private Transform _mainCameraTransform;

        private void Start()
        {
            _mainCameraTransform = GameManager.Instance.MainCameraTransform;
            _distanceBetweenMainCamera = Vector3.Distance(_mainCameraTransform.position, transform.position);
        }
        
        private void SetArCameraInFrontOfMainCamera()
        {
            transform.position = _mainCameraTransform.position + _mainCameraTransform.forward * _distanceBetweenMainCamera;
            transform.LookAt(_mainCameraTransform);
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SetArCameraInFrontOfMainCamera();
            }
        }
    }
}
