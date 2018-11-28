﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities {
    public static Vector2 initialGravity;

    public static void Destroy(PlayerHandler.DeathType type, GameObject gameObject)
    {
        // If it is a player or clone
        PlayerHandler pHandler = gameObject.GetComponent<PlayerHandler>();
        if (pHandler)
        {
            pHandler.ResetAnim();
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            pHandler.Death(type);
        }
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
    public static void LoadNextScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

    public static float FurthestFrom(float a, float b, float target)
    {
        if (Math.Abs(a - target) >= Math.Abs(b - target))
            return a;
        return b;
    }
    // Is 'other' to the left of 'origin'
    public static bool ToLeft(GameObject origin, GameObject other)
    {
        if (origin.transform.position.x - origin.GetComponent<Collider2D>().bounds.extents.x > other.transform.position.x + other.GetComponent<Collider2D>().bounds.extents.x)
            return true;
        return false;
    }
    public static bool ToRight(GameObject origin, GameObject other)
    {
        if (origin.transform.position.x + origin.GetComponent<Collider2D>().bounds.extents.x < other.transform.position.x - other.GetComponent<Collider2D>().bounds.extents.x)
            return true;
        return false;
    }

    public static T[] OnlyChildrenComponents<T>(GameObject target)
    {
        T[] initialArray = target.GetComponentsInChildren<T>();
        List<T> targetTypeList = new List<T>(target.GetComponents<T>());
        List<T> list = new List<T>();
        foreach(T type in initialArray)
        {
            if (targetTypeList.Contains(type))
                continue;
            list.Add(type);
        }
        return list.ToArray();
    }

    // Currently returns the first RaycastHit2D that:
    // 1. Isnt the object this script is attached to.
    // 2. Isnt a trigger.
    public static RaycastHit2D BoxCastHandler(GameObject caster, Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance)
    {
        RaycastHit2D[] hits = BoxCastHandlerAll(caster, origin, size, angle, direction, distance);
        if (hits.Length > 0)
            return hits[0];
        else
            return new RaycastHit2D();
    }

    public static RaycastHit2D[] BoxCastHandlerAll(GameObject caster, Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance)
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(origin, size, angle, direction, distance);
        List<RaycastHit2D> returnedHits = new List<RaycastHit2D>();
        foreach (RaycastHit2D hit in hits)
        {
            if (hit && (hit.collider.gameObject == caster || hit.collider.gameObject.transform.parent == caster.transform || hit.collider.isTrigger))
                continue;
            returnedHits.Add(hit);
        }
        return returnedHits.ToArray();
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
