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
        [SerializeField] private float duration = 1.0f; 
        [SerializeField] private Ease ease = Ease.OutQuad;
        
        private Light _light;
        private bool _isActive = false;
        private bool _increasing = true; 
        private AudioSource _alarmSound;

        private void Awake()
        {
            _light = GetComponentInChildren<Light>();
            _alarmSound = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                SetAlarmLightState(!_isActive);
            }
        }

        private void LoopAlarmLightIntensity()
        {
            if (!_isActive)
            {
                _light.DOIntensity(minMaxIntensity.x, duration).SetEase(ease); 
                return; 
            }

            var targetIntensity = _increasing ? minMaxIntensity.y : minMaxIntensity.x;
            _light.DOIntensity(targetIntensity, duration)
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
            }
            else
            {
                _alarmSound.Stop();
            }
        }
    }
}
