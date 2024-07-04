using System;
using UnityEngine;

namespace _Core.Scripts
{
    // ReSharper disable once Unity.InefficientPropertyAccess
    public class LaserPointer : MonoBehaviour
    {
        [SerializeField] private float laserLength = 10f; 
        [SerializeField] private LayerMask laserMask;
        
        private Transform _laserOrigin;
        private LineRenderer _lineRenderer;
        
        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = 2;
            _lineRenderer.material.color = Color.red;
        }

        private void Start()
        {
            _laserOrigin = GameManager.Instance.Selector.GetSelectorStartPoint();
        }

        private void Update()
        {
            ShootLaser();
        }
        
        private void ShootLaser()
        {
            _lineRenderer.SetPosition(0, _laserOrigin.position);
            var ray = new Ray(_laserOrigin.position, _laserOrigin.forward);

            if (Physics.Raycast(ray, out var hit, laserLength, laserMask))
            {
                _lineRenderer.SetPosition(1, hit.point);
            }
            else
            {
                _lineRenderer.SetPosition(1, _laserOrigin.position + _laserOrigin.forward * laserLength);
            }
        }
    }
}
