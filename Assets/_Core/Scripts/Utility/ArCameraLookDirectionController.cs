using UnityEngine;

namespace _Core.Scripts.Utility
{
    public class ArCameraLookDirectionController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKey(KeyCode.Q))
            {
                var isTowardsLeft = IsForwardTowardsRight(GameManager.Instance.Selector.GetSelectorStartPoint(), transform);
                if (isTowardsLeft)
                {
                    RotateObjectY(transform, -90f);
                }
                else
                {
                    RotateObjectY(transform, 90f);
                }
            }
        }

        private static bool IsForwardTowardsRight(Transform source, Transform target)
        {
            var targetRight = target.right;

            var dotProduct = Vector3.Dot(source.forward, targetRight);

            return dotProduct > 0;
        }

        private static void RotateObjectY(Transform t, float rotationSpeed)
        {
            var rotationAmount = rotationSpeed * Time.deltaTime;
            t.Rotate(0, rotationAmount, 0);
        }
    }
}
