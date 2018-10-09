using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities {
    public static Vector2 initialGravity;

    public static void LoadScene(int index)
    {
        Resetting();
        SceneManager.LoadScene(index);
    }
    public static void LoadScene(string name)
    {
        Resetting();
        SceneManager.LoadScene(name);
    }
    private static void Resetting()
    {
        if (initialGravity != Vector2.zero)
            Physics2D.gravity = initialGravity;
        ColourHandler.Reset();
    }
    public static float ClosestTo(float a, float b, float target)
    {
        if (Math.Abs(a - target) <= Math.Abs(b - target))
            return a;
        return b;
    }
}
