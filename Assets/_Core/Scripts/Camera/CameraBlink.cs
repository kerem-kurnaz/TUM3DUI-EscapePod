using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _Core.Scripts.Utility
{
    public class CameraBlink : MonoBehaviour
    {
        public Image blinkImage; // Reference to the black Image UI
        public float blinkDuration = 1.0f; // Duration of one blink cycle (close and open)
        public int blinkCount = 2; // Number of blinks
        public float delayBetweenBlinks = 0.5f; // Delay between each blink

        private void Start()
        {
            GameFlowManager.OnStartGame += StartBlinking;
        }

        private void OnDisable()
        {
            GameFlowManager.OnStartGame -= StartBlinking;
        }

        private void StartBlinking()
        {
            StartCoroutine(BlinkRoutine());
        }

        private IEnumerator BlinkRoutine()
        {
            yield return new WaitForSeconds(delayBetweenBlinks);
            yield return StartCoroutine(FadeImage(0.0f, 1f));
            yield return StartCoroutine(FadeImage(1.0f, .2f));
            yield return new WaitForSeconds(delayBetweenBlinks);
            yield return StartCoroutine(FadeImage(0.0f, 0.6f));
            yield return StartCoroutine(FadeImage(1.0f, .3f));
            yield return new WaitForSeconds(delayBetweenBlinks);
            yield return StartCoroutine(FadeImage(0.0f, .3f));
            
            // for (var i = 0; i < blinkCount; i++)
            // {
            //     // Fade to black (close eyelids)
            //     yield return StartCoroutine(FadeImage(1.0f, blinkDuration * 0.7f));
            //
            //     // Fade to transparent (open eyelids)
            //     yield return StartCoroutine(FadeImage(0.0f, blinkDuration * 0.3f));
            //
            //     // Wait for a moment before the next blink
            //     yield return new WaitForSeconds(delayBetweenBlinks);
            // }
        }

        private IEnumerator FadeImage(float targetAlpha, float duration)
        {
            // Get the current color and alpha of the image
            var currentColor = blinkImage.color;
            var startAlpha = currentColor.a;

            // Perform the fade over the specified duration
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                var blend = Mathf.Clamp01(t / duration);
                currentColor.a = Mathf.Lerp(startAlpha, targetAlpha, blend);
                blinkImage.color = currentColor;
                yield return null;
            }

            // Ensure the final alpha value is set
            currentColor.a = targetAlpha;
            blinkImage.color = currentColor;
        }
    }
}