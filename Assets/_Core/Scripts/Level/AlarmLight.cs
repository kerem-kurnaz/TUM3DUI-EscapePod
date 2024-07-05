using System;
using _Core.Scripts.Utility;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Core.Scripts.Level
{
    public class AlarmLight : MonoBehaviour
    {
        [SerializeField] private Vector2 minMaxIntensity = new Vector2(0, 3);
        [SerializeField] private float lightIncreaseDuration = 0.5f; 
        [SerializeField] private Ease ease = Ease.OutQuad;
        
        private Light _light;
        private bool _isActive = false;
        private bool _increasing = true; 
        private AudioSource _alarmSound;
        private AudioLowPassFilter _lowPassFilter;

        private void Awake()
        {
            _light = GetComponentInChildren<Light>();
            _alarmSound = GetComponent<AudioSource>();
            _lowPassFilter = GetComponent<AudioLowPassFilter>();
        }

        private void LoopAlarmLightIntensity()
        {
            if (!_isActive)
            {
                _light.DOIntensity(minMaxIntensity.x, lightIncreaseDuration).SetEase(ease); 
                return; 
            }

            var targetIntensity = _increasing ? minMaxIntensity.y : minMaxIntensity.x;
            _light.DOIntensity(targetIntensity, lightIncreaseDuration)
                .SetEase(ease)
                .OnComplete(() =>
                {
                    _increasing = !_increasing; 
                    LoopAlarmLightIntensity();  
                });
        }
        
        public void SetAlarmLightState(bool isActive)
        {
            _isActive = isActive;
            LoopAlarmLightIntensity();
            if (_isActive)
            {
                _alarmSound.Play();
                SlowlyIncreaseLowPassFilter();
            }
            else
            {
                _alarmSound.Stop();
            }
        }

        private void SlowlyIncreaseLowPassFilter()
        {
            DOTween.To(() => _lowPassFilter.cutoffFrequency, x => _lowPassFilter.cutoffFrequency = x, 5000, 5f)
                .SetEase(Ease.Linear); // Linear easing for a smooth increase
        }
    }
}
