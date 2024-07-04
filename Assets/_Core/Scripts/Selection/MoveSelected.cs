using UnityEngine;

namespace _Core.Scripts.Selection
{
    public class MoveSelected : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;
        private Selectable _selectable;
        private Selector _selector;
    
        private bool _monitoringSelectionInput = false;
        private bool _canMove = false;
        private Rigidbody _rb;
        private void Awake()
        {
            _selectable = GetComponent<Selectable>();
            _rb = GetComponentInParent<Rigidbody>();
        }

        private void Start()
        {
            _selectable.OnSelect += WaitForSelectionInput;
            _selectable.OnDeselect += DisableInputWaiting;
            _selector = GameManager.Instance.Selector;
        }

        private void Update()
        {
            if (_monitoringSelectionInput)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    _canMove = true;
                }
            }
            if (_canMove && Input.GetKeyUp(KeyCode.Space))
            {
                _canMove = false;
                _monitoringSelectionInput = false;
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
                _rb.velocity = Vector3.zero;
                return;
            }

            var transformToBeMoved = transform.parent;
            var currentPosition = transformToBeMoved.position;
            var targetDirection = _selector.GetSelectorStartPoint().forward;

            var movePoint = GetTargetPoint(targetDirection, _selector.GetSelectorStartPoint().position, 
                currentPosition);
        
            _rb.velocity = (movePoint - currentPosition) * moveSpeed;
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
