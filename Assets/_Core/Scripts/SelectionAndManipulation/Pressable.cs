using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Core.Scripts.SelectionAndManipulation
{
    public class Pressable : MonoBehaviour
    {
        public Action<bool> OnPress;
        
        public Transform PressableTransform => _pressableTransform;
        [SerializeField] private Vector3 pressMoveVector = Vector3.down; // Vector to move the button when pressed
        [SerializeField] private float pressMoveFactor = 0.05f; // Scale factor when the button is pressed
        [SerializeField] private float pressDuration = 0.1f;    // Duration of the press animation
        [SerializeField] private float releaseDuration = 0.1f;  // Duration of the release animation
        [SerializeField] private Ease pressEase = Ease.OutQuad; // Easing function for press animation
        [SerializeField] private Ease releaseEase = Ease.OutBounce; // Easing function for release animation
        
        private Selectable _selectable;
        private Transform _pressableTransform;
        private bool _readyToPress = false;
        private bool _isActive = false;
        private bool _moveOnPress = true;
        
        private void Awake()
        {
            _pressableTransform = transform.parent.parent;
            
            _selectable = _pressableTransform.GetComponentInChildren<Selectable>();
        }

        private void Start()
        {
            _selectable.OnSelect += ReadyToPress;
            _selectable.OnDeselect += NotReadyToPress;
        }

        private void Update()
        {
            if (_readyToPress && Input.GetKeyDown(KeyCode.Space))
            {
                if (_isActive)
                {
                    _isActive = false;
                }
                else
                {
                    _isActive = true;
                }
                
                Press(_isActive);
            }
        }

        private void ReadyToPress()
        {
            _readyToPress = true;
        }

        private void NotReadyToPress()
        {
            _readyToPress = false;
        }

        private void Press(bool activeState)
        {
            OnPress?.Invoke(_isActive);

            if (!_moveOnPress) return;
            var originalPosition = _pressableTransform.localPosition;
            var pressedPosition = originalPosition + pressMoveVector * pressMoveFactor;

            // Animate the move down (press effect)
            _pressableTransform.DOLocalMove(pressedPosition, pressDuration).SetEase(pressEase)
                .OnComplete(() =>
                {
                    // Animate the move back to the original position (release effect)
                    _pressableTransform.DOLocalMove(originalPosition, releaseDuration).SetEase(releaseEase);
                });
        }
        
        public void SetMoveOnPress(bool moveOnPress)
        {
            _moveOnPress = moveOnPress;
        }
    }
}
