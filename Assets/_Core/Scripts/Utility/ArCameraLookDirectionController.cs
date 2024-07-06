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
                var isTowardsLeft = HelperFunctions.IsForwardTowardsRight(GameManager.Instance.Selector.GetSelectorStartPoint(), transform);
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
                var isTowardsUp = HelperFunctions.IsForwardTowardsUp(GameManager.Instance.Selector.GetSelectorStartPoint(), transform);
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
