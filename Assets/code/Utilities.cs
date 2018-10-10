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
    
    // Currently returns the first RaycastHit2D that:
    // 1. Isnt the object this script is attached to.
    // 2. Isnt a trigger.
    public static RaycastHit2D BoxCastHandler(GameObject caster, Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance)
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(origin, size, angle, direction, distance);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit && (hit.collider.gameObject == caster || hit.collider.isTrigger))
                continue;
            return hit;
        }
        return new RaycastHit2D();
    }
}
public enum Direction
{
    Up, Down, Left, Right
}
