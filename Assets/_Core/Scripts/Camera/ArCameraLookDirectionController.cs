using System;
using System.Collections;
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

        private void Start()
        {
            GameFlowManager.OnGameEnd += ResetRotation;
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
                ResetRotation();
            }
        }

        private void ResetRotation()
        {
            transform.rotation = _initialRotation;
        }
    }
}
