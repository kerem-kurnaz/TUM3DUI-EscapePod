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
        [SerializeField] private string keypadGameText = "INTRUDER DETECTED<br> <- ENTER PASSCODE TO KEYPAD <-";
        
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
            GameFlowManager.OnOxygenGameEnd += OxygenGameEnd;
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
        }

        private void OxygenGameEnd()
        {
            _canFlash = false;
            topText.enabled = true;
            bottomText.enabled = false;
            topText.text = keypadGameText;
        }
    }
}
