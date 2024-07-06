using System;
using _Core.Scripts.Utility;
using DG.Tweening;
using UnityEngine;

namespace _Core.Scripts.Level
{
    public class OxygenTankLight : MonoBehaviour
    {
        [SerializeField] private Color emissionColor = Color.red;
        
        private Renderer _rend;
        private Color _originalColor;
        private Tweener _tweener;
        private void Awake()
        {
            _rend = GetComponent<Renderer>();
            _originalColor = _rend.materials[0].GetColor("_EmissionColor");
        }

        private void Start()
        {
            GameFlowManager.OnStartGame += LoopHighlightColor;
            GameFlowManager.OnOxygenGameEnd += StopHighlightColor;
        }

        private void LoopHighlightColor()
        {
            _rend.materials[0].EnableKeyword("_EMISSION");
            _tweener = _rend.materials[0].DOColor(emissionColor, "_EmissionColor", 1.0f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
        
        private void StopHighlightColor()
        {
            _tweener.Kill();
            _rend.materials[0].SetColor("_EmissionColor", _originalColor);
        }
    }
}
