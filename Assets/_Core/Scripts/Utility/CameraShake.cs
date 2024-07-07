using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Core.Scripts.Utility
{
    public class CameraShake : MonoBehaviour
    {
        public Vector2 minMaxIntensity = new Vector2(-1, 1);
        public float shakeDuration = 0.5f;
        public float shakeMagnitude = 0.2f;
        public float dampingSpeed = 1.0f;
        
        private Vector3 _initialPosition;

        private void OnEnable()
        {
            _initialPosition = transform.localPosition;
        }

        public void TriggerShake()
        {
            StartCoroutine(Shake());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                TriggerShake();
            }
        }

        private IEnumerator Shake()
        {
            var elapsed = 0.0f;

            while (elapsed < shakeDuration)
            {
                // Calculate a random offset within the shakeMagnitude range
                var x = Random.Range(minMaxIntensity.x, minMaxIntensity.y) * shakeMagnitude;
                var y = Random.Range(minMaxIntensity.x, minMaxIntensity.y) * shakeMagnitude;

                // Apply the offset to the camera's position
                transform.localPosition = new Vector3(x, y, _initialPosition.z);

                // Increase the elapsed time
                elapsed += Time.deltaTime;

                // Wait for the next frame before continuing
                yield return null;
            }

            // Restore the camera to its original position
            transform.localPosition = _initialPosition;
        }
    }
}