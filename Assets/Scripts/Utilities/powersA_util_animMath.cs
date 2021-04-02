using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class powersA_util_animMath
{
    public static float Lerp(float min, float max, float p, bool allowExtrapolation)
    {
        if(!allowExtrapolation)
        {
            if (p < 0) return min;
            if (p > 1) return max;
        }

        return (max - min) * p + min;
    }

    public static Vector3 Lerp(Vector3 min, Vector3 max, float p)
    {
        return (max - min) * p + min;
    }

    public static Vector3 Lerp(Vector3 min, Vector3 max, float p, bool allowExtrapolation)
    {
        if (!allowExtrapolation)
        {
            if (p < 0) p = 0;
            if (p > 1) p = 1;
        }

        return (max - min) * p + min;
    }

    public static Vector2 Lerp(Vector2 min, Vector2 max, float p)
    {
        return (max - min) * p + min;
    }
    public static Vector2 Lerp(Vector2 min, Vector2 max, float p, bool allowExtrapolation)
    {
        if (!allowExtrapolation)
        {
            if (p < 0) return min;
            if (p > 1) return max;
        }

        return (max - min) * p + min;
    }

    public static Quaternion Lerp(Quaternion min, Quaternion max, float p, bool allowExtrapolation = true)
    {
        if (!allowExtrapolation)
        {
            if (p < 0) p = 0;
            if (p > 1) p = 1;
        }
        return Quaternion.Lerp(min, max, p);
    }

    public static float Slide(float current, float target, float percentLeftAfter1Second)
    {
        float p = 1 - Mathf.Pow(percentLeftAfter1Second, Time.deltaTime);
        return powersA_util_animMath.Lerp(current, target, p, true);
    }

    public static Vector3 Slide(Vector3 current, Vector3 target, float percentLeftAfter1Second)
    {
        float p = 1 - Mathf.Pow(percentLeftAfter1Second, Time.deltaTime);
        return powersA_util_animMath.Lerp(current, target, p, true);
    }
    public static Vector2 Slide(Vector2 current, Vector2 target, float percentLeftAfter1Second)
    {
        float p = 1 - Mathf.Pow(percentLeftAfter1Second, Time.deltaTime);
        return powersA_util_animMath.Lerp(current, target, p);
    }

    public static Quaternion Slide(Quaternion current, Quaternion target, float percentLeftAfter1Second)
    {
        float p = 1 - Mathf.Pow(percentLeftAfter1Second, Time.deltaTime);
        return powersA_util_animMath.Lerp(current, target, p, true);
    }

    public static Vector3 SpotOnCircleXZ(float radius, float currentAngle)
    {
        Vector3 offset = new Vector3();

        offset.x = Mathf.Sin(currentAngle) * radius;
        offset.z = Mathf.Cos(currentAngle) * radius;

        return offset;
    }
}
