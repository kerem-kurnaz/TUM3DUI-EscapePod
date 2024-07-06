using System;
using UnityEngine;

namespace _Core.Scripts.Utility
{
    public class ArCameraLookDirectionController : MonoBehaviour
    {
        private Quaternion _initialRotation;

        private void Awake()
        {
            _initialRotation = transform.rotation;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Q))
            {
                var isTowardsLeft = IsForwardTowardsRight(GameManager.Instance.Selector.GetSelectorStartPoint(), transform);
                if (isTowardsLeft)
                {
                    HelperFunctions.RotateObjectY(transform, -90f);
                }
                else
                {
                    HelperFunctions.RotateObjectY(transform, 90f);
                }
            }

            if (Input.GetKey(KeyCode.E))
            {
                var isTowardsUp = IsForwardTowardsUp(GameManager.Instance.Selector.GetSelectorStartPoint(), transform);
                if (isTowardsUp)
                {
                    HelperFunctions.RotateObjectX(transform, 90f);
                }
                else
                {
                    HelperFunctions.RotateObjectX(transform, -90f);
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                transform.rotation = _initialRotation;
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
