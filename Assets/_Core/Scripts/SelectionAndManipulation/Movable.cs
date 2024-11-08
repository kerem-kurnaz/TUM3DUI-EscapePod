using System;
using _Core.Scripts.Utility;
using Unity.VisualScripting;
using UnityEngine;

namespace _Core.Scripts.SelectionAndManipulation
{
    public class Movable : MonoBehaviour
    {
        public Action OnMoveInputUp;
        public Action OnStartMoving;
        public Action OnStopMoving;
        public bool IsMoving => _isMoving;
        
        [SerializeField] private float moveSpeed = 5f;
        
        private Selectable _selectable;
        private Selector _selector;
        private Rigidbody _rb;

        private bool _monitoringSelectionInput = false;
        private bool _canMove = false;
        private bool _isMoving = false;
        private float _forwardMovement = 0f;
        private void Awake()
        {
            _selectable = transform.parent.GetComponentInChildren<Selectable>();
            _rb = GetComponentInParent<Rigidbody>();
            _selectable.SetMovable(this);
        }

        private void Start()
        {
            _selectable.OnSelect += WaitForSelectionInput;
            _selectable.OnDeselect += DisableInputWaiting;
            _selector = GameManager.Instance.Selector;
        }

        private void OnDisable()
        {
            _selectable.OnSelect -= WaitForSelectionInput;
            _selectable.OnDeselect -= DisableInputWaiting;
        }

        private void Update()
        {
            if (_monitoringSelectionInput)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    _canMove = true;
                    OnStartMoving?.Invoke();
                    // Check for forward movement
                    if (Input.GetKey(KeyCode.W))
                    {
                        _forwardMovement = 1f;
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        _forwardMovement = -1f;
                    }
                    else
                    {
                        _forwardMovement = 0f;
                    }
                }
            }
            if (_canMove && Input.GetKeyUp(KeyCode.Space))
            {
                _canMove = false;
                _monitoringSelectionInput = false;
                OnMoveInputUp?.Invoke();
                OnStopMoving?.Invoke();
            }
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void WaitForSelectionInput()
        {
            if (!_canMove)
            {
                _monitoringSelectionInput = true;
            }
        }

        private void DisableInputWaiting()
        {
            if (!_canMove)
            {
                _monitoringSelectionInput = false;
            }
        }

        private void Move()
        {
            if (!_canMove)
            {
                _isMoving = false;
                _rb.velocity = Vector3.zero;
                return;
            }
            
            _isMoving = true;
            var transformToBeMoved = _selectable.SelectableTransform;
            var currentPosition = transformToBeMoved.position;
            var targetDirection = _selector.GetSelectorStartPoint().forward;

            var movePoint = GetTargetPoint(targetDirection, _selector.GetSelectorStartPoint().position, 
                currentPosition);
        
            _rb.velocity = (movePoint - currentPosition) * moveSpeed;
            //add the forward movement
            _rb.velocity += _selector.GetSelectorStartPoint().forward * (_forwardMovement * moveSpeed);
        }

        private static Vector3 GetTargetPoint(Vector3 selectorForward, Vector3 selectorPosition, Vector3 currentPos)
        {
            var directionToCurrentPosFromSelectorPos = currentPos - selectorPosition;

            // Using t as a parameter to find the position of C
            var directionToSelectorPosFromCurrentPos = selectorPosition - currentPos;
            var t = -Vector3.Dot(directionToSelectorPosFromCurrentPos, 
                directionToCurrentPosFromSelectorPos) / Vector3.Dot(selectorForward, directionToCurrentPosFromSelectorPos);
            var targetPos = selectorPosition + t * selectorForward.normalized;

            return targetPos;
        }
    }
}
