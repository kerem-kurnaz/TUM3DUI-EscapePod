using System;
using UnityEngine;
using _Core.Scripts.Utility;
using DG.Tweening;
using UnityEngine.Serialization;

namespace _Core.Scripts.Level
{
    public class Indicator : MonoBehaviour
    {
        [SerializeField] private OxygenTankLight oxygenTankLight;
        
        [SerializeField] private float startOxygenLevel = 0f;
        
        [SerializeField] private Vector2 minMaxRotation = new Vector2(-150, -25);
        [SerializeField] private Ease rotationEase = Ease.OutQuad;
        
        [SerializeField] private bool cantChange = false;
        

        private void Awake()
        {
            InitializeIndicator(startOxygenLevel, 0.1f);
        }

        private void Start()
        {
            if (oxygenTankLight)
            {
                oxygenTankLight.OnOxygenConnected += TurnIndicatorFull;
            }
        }

        private void OnDisable()
        {
            if (oxygenTankLight)
                oxygenTankLight.OnOxygenConnected -= TurnIndicatorFull;
        }

        private void TurnIndicatorFull()
        {
            TurnIndicator(1f, 1f);
        }

        private void TurnIndicator(float targetValue, float duration)
        {
            if (cantChange) return;
            
            var targetZValueInRotation = HelperFunctions.PercentageInRange(targetValue, minMaxRotation.x, minMaxRotation.y);
            var targetValueInRotation = new Vector3(0, 0, targetZValueInRotation);
            
            transform.DOLocalRotate(targetValueInRotation, duration).SetEase(rotationEase);
        }

        private void InitializeIndicator(float targetValue, float duration)
        {
            var targetZValueInRotation = HelperFunctions.PercentageInRange(targetValue, minMaxRotation.x, minMaxRotation.y);
            var targetValueInRotation = new Vector3(0, 0, targetZValueInRotation);
            
            transform.DOLocalRotate(targetValueInRotation, duration).SetEase(rotationEase);
        }
    }
}
