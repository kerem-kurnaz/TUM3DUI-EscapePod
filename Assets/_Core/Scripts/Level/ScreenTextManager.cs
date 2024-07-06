using System;
using System.Collections;
using _Core.Scripts.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Core.Scripts.Level
{
    public class ScreenTextManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI topText;
        [SerializeField] private TextMeshProUGUI bottomText;

        private bool _canFlash = true;
        private void Awake()
        {
            if (!topText || !bottomText)
            {
                Debug.LogError("ScreenTextManager: TextMeshPro references are missing!");
            }
        }

        private void Start()
        {
            GameFlowManager.OnStartGame += StartText;
            GameFlowManager.OnOxygenGameEnd += () => _canFlash = false;
        }

        private void StartText()
        {
            StartCoroutine(StartTextFlash(bottomText, 0.5f));
        }

        private IEnumerator StartTextFlash(TextMeshProUGUI text, float flashDuration)
        {
            while (_canFlash)
            {
                text.enabled = !text.enabled;
                yield return new WaitForSeconds(flashDuration);
            }

            text.enabled = false;
            topText.enabled = true;
            topText.text = "next game";
        }
    }
}
