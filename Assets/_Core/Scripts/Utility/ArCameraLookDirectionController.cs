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

            if (Input.GetKey(KeyCode.E))
            {
                var isTowardsUp = IsForwardTowardsUp(GameManager.Instance.Selector.GetSelectorStartPoint(), transform);
                if (isTowardsUp)
                {
                    RotateObjectX(transform, 90f);
                }
                else
                {
                    RotateObjectX(transform, -90f);
                }
            }
        }

        private static bool IsForwardTowardsRight(Transform source, Transform target)
        {
            var targetRight = target.right;

            var dotProduct = Vector3.Dot(source.forward, targetRight);

            return dotProduct > 0;
        }
        
        //check if the forward direction of the source is towards the up direction of the target
        private static bool IsForwardTowardsUp(Transform source, Transform target)
        {
            var targetUp = target.up;

            var dotProduct = Vector3.Dot(source.forward, targetUp);

            return dotProduct > 0;
        }

        private static void RotateObjectY(Transform t, float rotationSpeed)
        {
            var rotationAmount = rotationSpeed * Time.deltaTime;
            t.Rotate(0, rotationAmount, 0);
        }
        
        private static void RotateObjectX(Transform t, float rotationSpeed)
        {
            var rotationAmount = rotationSpeed * Time.deltaTime;
            t.Rotate(rotationAmount, 0, 0);
        }
        
        private static void RotateObjectZ(Transform t, float rotationSpeed)
        {
            var rotationAmount = rotationSpeed * Time.deltaTime;
            t.Rotate(0, 0, rotationAmount);
        }
        
        void RotateTowardsTargetDirection(Vector3 targetDirection, float rotationSpeed)
        {
            // Normalize the target direction to ensure it's a unit vector
            Vector3 normalizedDirection = targetDirection.normalized;

            // Calculate the target rotation based on the desired direction
            Quaternion targetRotation = Quaternion.LookRotation(normalizedDirection);

            // Smoothly rotate towards the target rotation using Slerp for smooth interpolation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
