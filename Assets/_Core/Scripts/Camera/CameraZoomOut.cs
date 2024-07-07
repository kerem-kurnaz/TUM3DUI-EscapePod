using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace _Core.Scripts.Utility
{
    public class CameraZoomOut : MonoBehaviour
    {
        [SerializeField] private Transform zoomOutLocation;
        [SerializeField] private float zoomOutTime = 1.0f;
        [SerializeField] private Ease ease = Ease.OutQuad;
        private Transform _cameraTransform;

        private void Awake()
        {
            if (Camera.main != null) _cameraTransform = Camera.main.transform;
        }

        private void Start()
        {
            GameFlowManager.OnGameEnd += ZoomOut;
        }
        
        private void ZoomOut()
        {
            if (_cameraTransform == null) return;
            _cameraTransform.DOMove(zoomOutLocation.position, zoomOutTime).SetEase(ease);
        }
    }
}
