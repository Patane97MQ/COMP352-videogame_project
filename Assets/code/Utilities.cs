using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities {
    public static Vector2 initialGravity;

    public static void Destroy(GameObject gameObject)
    {
        if (gameObject.tag == "Player")
            ReloadScene();
        else
        {
            // Maybe play a destroy animation?
            UnityEngine.Object.Destroy(gameObject);
        }
    }

    public static void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
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
    public static void DrawBox(Vector2 centre, Vector2 size, Color color)
    {
        Debug.DrawLine(new Vector2(centre.x - size.x / 2, centre.y + size.y / 2), new Vector2(centre.x + size.x / 2, centre.y + size.y / 2), color);
        Debug.DrawLine(new Vector2(centre.x + size.x / 2, centre.y + size.y / 2), new Vector2(centre.x + size.x / 2, centre.y - size.y / 2), color);
        Debug.DrawLine(new Vector2(centre.x + size.x / 2, centre.y - size.y / 2), new Vector2(centre.x - size.x / 2, centre.y - size.y / 2), color);
        Debug.DrawLine(new Vector2(centre.x - size.x / 2, centre.y - size.y / 2), new Vector2(centre.x - size.x / 2, centre.y + size.y / 2), color);
    }
}
public enum Direction
{
    Up, Down, Left, Right
}
