using UnityEngine;

namespace _Core.Scripts.Selection
{
    public class Selector : MonoBehaviour
    {
        [SerializeField] private Transform selectorStartPoint;

        private Selectable _lastSelection = null;
        private void FixedUpdate()
        {
            // position offset avoids self collision with non centered colliders
            var intersects = Physics.Raycast(selectorStartPoint.position + selectorStartPoint.up * 0.1f, selectorStartPoint.up, out var hitInfo);
            if (intersects)
            {
                var newSelection = hitInfo.transform.gameObject.GetComponent<Selectable>();
                // Only activate new selectable if previous one was deselected
                if(!_lastSelection && newSelection != _lastSelection)
                {
                    _lastSelection.DeSelect();
                    _lastSelection = null;
                }
                if (!newSelection)
                {
                    newSelection.Select();
                    _lastSelection = newSelection;
                }
            }
            else
            {
                if (!_lastSelection)
                {
                    _lastSelection.DeSelect();
                    _lastSelection = null;
                }   

            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(selectorStartPoint.position + selectorStartPoint.up * 0.1f, selectorStartPoint.up);
        }
    }
}
