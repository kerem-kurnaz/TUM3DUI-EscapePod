
using UnityEngine;

namespace _Core.Scripts.Utility
{
    public static class HelperFunctions 
    {
        public static float MapValue(float x, float sourceMin, float sourceMax, float targetMin, float targetMax)
        {
            return targetMin + (x - sourceMin) * (targetMax - targetMin) / (sourceMax - sourceMin);
        }
        
        public static float PercentageInRange(float percentage, float sourceMin, float sourceMax)
        {
            return sourceMin + percentage * (sourceMax - sourceMin);
        }
        
        public static void RotateObjectY(Transform t, float rotationSpeed)
        {
            var rotationAmount = rotationSpeed * Time.deltaTime;
            t.Rotate(0, rotationAmount, 0);
        }
        
        public static void RotateObjectX(Transform t, float rotationSpeed)
        {
            var rotationAmount = rotationSpeed * Time.deltaTime;
            t.Rotate(rotationAmount, 0, 0);
        }
        
        public static void RotateObjectZ(Transform t, float rotationSpeed)
        {
            var rotationAmount = rotationSpeed * Time.deltaTime;
            t.Rotate(0, 0, rotationAmount);
        }
        
        public static bool IsForwardTowardsRight(Transform source, Transform target)
        {
            var targetRight = target.right;

            var dotProduct = Vector3.Dot(source.forward, targetRight);

            return dotProduct > 0;
        }
        
        public static bool IsForwardTowardsUp(Transform source, Transform target)
        {
            var targetUp = target.up;

            var dotProduct = Vector3.Dot(source.forward, targetUp);

            return dotProduct > 0;
        }
    }
}
