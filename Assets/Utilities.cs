using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static float EPSILON = .01f;
    public static bool ApproximatelyEqual(float a, float b)
    {
        return Mathf.Abs(a - b) < EPSILON;
    }

    public static float Clamp(float val, float min, float max)
    {
        if (val<min)
        {
            val = min;
        }
        if(val > max)
        {
            val = max;
        }
        return val;
    }

    public static float AndgleDiffPosNeg(float a, float b)
    {
        float diff = a - b;
        if (diff > 180)
        {
            return diff - 360;
        }
        if (diff < -180)
        {
            return diff + 360;
        }
        return diff;
    }

    public static float Degrees360(float angleDegrees)
    {
        while (angleDegrees >= 360)
        {
            angleDegrees -= 360;
        }
        while (angleDegrees < 0)
        {
            angleDegrees += 360;
        }
        return angleDegrees;
    }

    public static float ToDegrees(float radians)
    {
        float degrees = radians * (180 / Mathf.PI);
        //degrees = degrees + 180;
        return degrees;
    }

    public static Bounds GetViewportBounds(Camera camera, Vector3 screenPosition1, Vector3 screenPosition2)
    {
        Vector3 v1 = Camera.main.ScreenToViewportPoint(screenPosition1);
        Vector3 v2 = Camera.main.ScreenToViewportPoint(screenPosition2);
        Vector3 min = Vector3.Min(v1, v2);
        Vector3 max = Vector3.Max(v1, v2);
        min.z = camera.nearClipPlane;
        max.z = camera.farClipPlane;

        Bounds bounds = new Bounds();
        bounds.SetMinMax(min, max);
        return bounds;
    }

    public static Rect GetScreenRect(Vector3 pos1, Vector3 pos2)
    {
        pos1.y = Screen.height - pos1.y;
        pos2.y = Screen.height - pos2.y;
        Vector3 topLeft = Vector3.Min(pos1, pos2);
        Vector3 bottomRight = Vector3.Max(pos1, pos2);
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }

    public static void DrawScreenRectBorder(Rect rect, float thickness, Color color)
    {
        //Top
        Utilities.DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
        //Left
        Utilities.DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
        //Right
        Utilities.DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, thickness), color);
        //Bottom
        Utilities.DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
    }

    public static void DrawScreenRect(Rect rect, Color color)
    {
        GUI.color = color;
        GUI.DrawTexture(rect, WhiteTexture);
        GUI.color = Color.white;
    }

    static Texture2D whiteTexture;
    public static Texture2D WhiteTexture
    {
        get
        {
            if (whiteTexture == null)
            {
                whiteTexture = new Texture2D(1, 1);
                whiteTexture.SetPixel(0, 0, Color.white);
                whiteTexture.Apply();
            }
            return whiteTexture;
        }
    }

}
