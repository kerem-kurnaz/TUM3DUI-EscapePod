using System;
using UnityEngine;
using _Core.Scripts.Utility;
using DG.Tweening;

namespace _Core.Scripts.Level
{
    public class Indicator : MonoBehaviour
    {
        [SerializeField] private float oxygenLevel = 0f;
        
        [SerializeField] private Vector2 minMaxRotation = new Vector2(-150, -25);
        [SerializeField] private float duration = 1.0f; 
        [SerializeField] private Ease rotationEase = Ease.OutQuad;

        private void Awake()
        {
            TurnIndicator(oxygenLevel);
        }

        private void TurnIndicator(float targetValue)
        {
            var targetZValueInRotation = HelperFunctions.MapValue(targetValue, minMaxRotation.x, minMaxRotation.y, 0f, 1f);
            var targetValueInRotation = new Vector3(0, 0, targetZValueInRotation);
            
            transform.DOLocalRotate(targetValueInRotation, duration).SetEase(rotationEase);
        }
    }
}
