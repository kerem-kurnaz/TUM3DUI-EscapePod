using System;
using _Core.Scripts.SelectionAndManipulation;
using _Core.Scripts.Utility;
using DG.Tweening;
using UnityEngine;

namespace _Core.Scripts.Level
{
    public class OxygenTankLight : MonoBehaviour
    {
        public Action OnOxygenConnected;
        [SerializeField] private Color emissionColor = Color.red;
        
        private Renderer _rend;
        private Tweener _tweener;
        private Snapper _snapper;
        
        private Color _originalColor;
        
        private void Awake()
        {
            _snapper = GetComponentInChildren<Snapper>();
            _rend = GetComponent<Renderer>();
            _originalColor = _rend.materials[0].GetColor("_EmissionColor");
        }

        private void Start()
        {
            GameFlowManager.OnStartGame += LoopHighlightColor;
            _snapper.OnSnap += StopHighlightColor;
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
            
            _snapper.CurrentSnappable.transform.parent.GetComponentInChildren<Selectable>().SetCanSelect(false);
            OnOxygenConnected?.Invoke();
        }
    }
}
