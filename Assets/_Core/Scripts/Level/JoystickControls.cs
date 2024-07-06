using System;
using _Core.Scripts.SelectionAndManipulation;
using _Core.Scripts.Utility;
using UnityEngine;

namespace _Core.Scripts.Level
{
    public class JoystickControls : MonoBehaviour
    {
        [SerializeField] private Transform craneArmTransform;
        [SerializeField] private Transform joystickArmTransform;
        private Pressable _pressable;
        private bool _isJoystickActive = false;

        private void Awake()
        {
            _pressable = GetComponentInChildren<Pressable>();
            _pressable.SetMoveOnPress(false);
        }

        private void Start()
        {
            _pressable.OnPress += StartJoystickControls;
        }
        
        private void OnDisable()
        {
            _pressable.OnPress -= StartJoystickControls;
        }

        private void Update()
        {
            if (_isJoystickActive)
            {
                var isTowardsLeft = HelperFunctions.IsForwardTowardsRight(GameManager.Instance.Selector.GetSelectorStartPoint(), transform);
                if (isTowardsLeft)
                {
                    HelperFunctions.RotateObjectZ(craneArmTransform, -90f);
                    RotateJoystickArm(-30f);
                }
                else
                {
                    HelperFunctions.RotateObjectZ(craneArmTransform, 90f);
                    RotateJoystickArm(30f);
                }
            }
        }

        private void StartJoystickControls(bool isActive)
        {
            _isJoystickActive = isActive;
        }
        
        private void RotateJoystickArm(float rotationAngle)
        {
            joystickArmTransform.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        }
    }
}
