
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
    }
}
