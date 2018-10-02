using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Physics : MonoBehaviour {

    public Vector2 gravity = new Vector2(0, -9.81f);

    public float horizontalDrag = 0.0001f;
    public float weight = 0f;
    public float stepHeight = 0.1f;

    protected Transform t;
    protected Rigidbody2D rb2D;
    protected Collider2D c2D;

    protected float rotation;
    protected bool up;
    protected bool down;
    protected bool left;
    protected bool right;

    protected Vector2 velocity;
    protected Vector2 acceleration;

    // Use this for initialization
    void Start()
    {
        t = transform;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        c2D = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    protected void FixedUpdate ()
    {
        acceleration = gravity / 1000;
        // accelerationY should never/rarely be changed. This is the constant downwards force of 'gravity'
        CheckRotation();

        // Velocity constants are always applied!
        AddPositionX(velocity.x);
        AddPositionY(velocity.y);

        //Debug.Log("Collisions: " + (down ? "down " : "")
        //    + (up ? "up " : "")
        //    + (left ? "left " : "")
        //    + (right ? "right " : ""));

        //Debug.Log("Velocity(" + velocity.x + "," + velocity.y + ")");
        //Debug.Log("Down(" + down + ")");

        // Velocity is always accelerated. This is exclusively used for gravity
        AddVelocity(acceleration);
        HorizontalDrag();
    }
    // ====================================================================

    private void HorizontalDrag()
    {
        velocity = Vector2.MoveTowards(velocity, new Vector2(0, velocity.y), 1);
    }
    //private void ApplyFriction()
    //{
    //    velocity = new Vector2(Mathf.MoveTowards(velocity.x, 0, horizontalDrag), Mathf.MoveTowards(velocity.y, 0, horizontalDrag));
    //}
    // Try not to use this function on its own. Use SetVelocity or AddVelocity.
    public void AddPositionX(float bx)
    {
        if (bx == 0)
        {
            SetTouching(Vector2.left, false);
            SetTouching(Vector2.right, false);
            return;
        }
        float x = CheckNextMoveX(bx);
        transform.position = new Vector3(transform.position.x + x, transform.position.y, 0);
    }
    // Try not to use this function on its own. Use SetVelocity or AddVelocity.
    public void AddPositionY(float by)
    {
        if (by == 0)
        {
            SetTouching(Vector2.down, false);
            SetTouching(Vector2.up, false);
            return;
        }
        float y = CheckNextMoveY(by);
        transform.position = new Vector3(transform.position.x, transform.position.y + y, 0);
    }
    // Sets the current velocity as a whole.
    public void SetVelocity(Vector2 bv)
    {
        velocity = bv;
    }
    // Adds to the current velocity.
    public void AddVelocity(Vector2 bv)
    {
        velocity += bv;
    }

    public float AddForceX(float x)
    {
        float force = x / weight;
        AddVelocity(new Vector2(force, 0));
        return force;
    }
    public float AddForceY(float y)
    {
        float force = y / weight;
        AddVelocity(new Vector2(0, force));
        return force;
    }
    // ====================================================================

    // Ensures object is correctly rotated for the direction of gravity.
    private void CheckRotation()
    {
        if (gravity.normalized == Vector2.down)
            rotation = 0;
        else if (gravity.normalized == Vector2.up)
            rotation = 180;
        else if (gravity.normalized == Vector2.left)
            rotation = -90;
        else if (gravity.normalized == Vector2.right)
            rotation = 90;
        if(transform.rotation.z != rotation)
            transform.Rotate(new Vector3(0, 0, rotation));

    }
    private void DrawBox(Vector2 centre, Vector2 size, Color color)
    {
        Debug.DrawLine(new Vector2(centre.x - size.x / 2, centre.y + size.y / 2), new Vector2(centre.x + size.x / 2, centre.y + size.y / 2), color);
        Debug.DrawLine(new Vector2(centre.x + size.x / 2, centre.y + size.y / 2), new Vector2(centre.x + size.x / 2, centre.y - size.y / 2), color);
        Debug.DrawLine(new Vector2(centre.x + size.x / 2, centre.y - size.y / 2), new Vector2(centre.x - size.x / 2, centre.y - size.y / 2), color);
        Debug.DrawLine(new Vector2(centre.x - size.x / 2, centre.y - size.y / 2), new Vector2(centre.x - size.x / 2, centre.y + size.y / 2), color);
    }
    private void DrawBoxCast(Vector2 origin, Vector2 size, Vector2 direction, float distance)
    {
        DrawBox(origin, size, Color.green);
        DrawBox(origin + direction * distance, size, Color.red);
    }

    private float edgeCut = 0.02f;
    
    private static Boolean HasPhysics(GameObject obj)
    {
        if (obj == null)
            return false;
        return (obj.GetComponent<Physics>() != null ? true : false);
    }
    private static Physics GrabPhysics(GameObject obj)
    {
        if (obj == null)
            return null;
        return obj.GetComponent<Physics>();
    }

    // Currently returns the first RaycastHit2D that:
    // 1. Isnt the object this script is attached to.
    // 2. Isnt a trigger.
    private RaycastHit2D BoxCastHandler(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance)
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(origin, size, angle, direction, distance);
        //if(hits.Count() > 0)
        //{
        //    if (hits[0] && hits[0].collider.gameObject == gameObject)
        //        return hits[1];
        //    return hits[0];
        //}

        foreach (RaycastHit2D hit in hits)
        {
            if (hit && (hit.collider.gameObject == gameObject || hit.collider.isTrigger))
                continue;
            return hit;
        }
        return new RaycastHit2D();
    }

    private float RunAllBoxCastsForX(float x, Vector2 direction)
    {
        // Drawing a BoxCast of the below (for debugging)
        DrawBoxCast(new Vector2(transform.position.x, transform.position.y), new Vector2(0.01f, c2D.bounds.size.y - edgeCut * 2), direction, Math.Abs(x) + c2D.bounds.extents.x - 0.005f);

        // BoxCast of this object is shot in the direction it wants to move. This will basically check if the objects 'next move' will hit anything
        RaycastHit2D nextCheck = BoxCastHandler(new Vector2(transform.position.x, transform.position.y), new Vector2(0.01f, c2D.bounds.size.y - edgeCut * 2), transform.rotation.z, direction, Math.Abs(x) + c2D.bounds.extents.x - 0.005f);

        // ************************************************************************************************************************************************************************************************
        // *** When Stephen gets time, he will change this 'nextCheck' and 'stepCheck' to list an array of all objects to be hit and determine everything after looping through all.
        // ************************************************************************************************************************************************************************************************

        // If an object is hit.
        if (nextCheck)
        {
            // Distance is saved as this object. The reason that its saved here is because it might be changed later (within stepCheck)
            float raycastDistance = nextCheck.distance;
            RaycastHit2D stepCheck = BoxCastHandler(new Vector2(transform.position.x, transform.position.y + stepHeight), new Vector2(0.01f, c2D.bounds.size.y - (edgeCut * 2)), transform.rotation.z, direction, Math.Abs(x) + c2D.bounds.extents.x - 0.005f);
            
            // In this scenario, we bump into an object which is below our stepheight threshhold (such as a button). If so, we will try to step on top of it.
            // We also only pass this if statement when the object we are colliding is NOT the originally collided object.
            if (!stepCheck || stepCheck.collider.gameObject != nextCheck.collider.gameObject && (HasPhysics(stepCheck.collider.gameObject)))
            {
                // If we are able to step onto the object without glitching, do so!
                if (Math.Abs(x) >= edgeCut)
                {
                    // If we did hit an object AND that object has any sort of physics
                    if (stepCheck && HasPhysics(stepCheck.collider.gameObject))
                    {
                        // Add force to that object
                        GrabPhysics(stepCheck.collider.gameObject).AddForceX(x);
                        // Move the distance from this object to the physics object
                        raycastDistance = stepCheck.distance;
                    }
                    else
                        // Move the Y value up to the current stepHeight
                        transform.position += Vector3.up * stepHeight;
                }
                // Otherwise, clip slightly into the object. This way we dont lose any momentum or movement on any objects!
                else
                    return x;
            }
            // If we are colliding into an object with physics, push it.
            if (HasPhysics(nextCheck.collider.gameObject))
                GrabPhysics(nextCheck.collider.gameObject).AddForceX(x);
            
            SetTouching(direction, true);
            SetVelocity(new Vector2(0, velocity.y));
            return Math.Sign(direction.x)*(raycastDistance - c2D.bounds.extents.x + 0.005f);
        }
        return x;
    }
    private float RunAllBoxCastsForY(float y, Vector2 direction)
    {
        // Drawing a BoxCast of the below (for debugging)
        DrawBoxCast(new Vector2(transform.position.x, transform.position.y), new Vector2(c2D.bounds.size.x - edgeCut * 2, 0.01f), direction, Math.Abs(y) + c2D.bounds.extents.y - 0.005f);

        // BoxCast of this object is shot in the direction it wants to move. This will basically check if the objects 'next move' will hit anything
        RaycastHit2D nextCheck = BoxCastHandler(new Vector2(transform.position.x, transform.position.y), new Vector2(c2D.bounds.size.x - edgeCut * 2, 0.01f), transform.rotation.z, direction, Math.Abs(y) + c2D.bounds.extents.y - 0.005f);

        // ************************************************************************************************************************************************************************************************
        // *** When Stephen gets time, he will change this 'nextCheck' and 'stepCheck' to list an array of all objects to be hit and determine everything after looping through all.
        // ************************************************************************************************************************************************************************************************

        // If an object is hit.
        if (nextCheck)
        {
            // If we are colliding into an object with physics, push it.
            if (HasPhysics(nextCheck.collider.gameObject))
                GrabPhysics(nextCheck.collider.gameObject).AddForceY(y);

            SetTouching(direction, true);
            SetVelocity(new Vector2(velocity.x, 0));
            return Math.Sign(direction.y) * (nextCheck.distance - c2D.bounds.extents.y + 0.005f);
        }
        return y;
    }
    private float CheckNextMoveX(float x)
    {
        SetTouching(Vector2.left, false);
        SetTouching(Vector2.right, false);
        if (x < 0)
            return RunAllBoxCastsForX(x, Vector2.left);
        else if (x > 0)
            return RunAllBoxCastsForX(x, Vector2.right);
        return x;
    }
    private float CheckNextMoveY(float y)
    {
        SetTouching(Vector2.down, false);
        SetTouching(Vector2.up, false);
        if (y < 0)
            return RunAllBoxCastsForY(y, Vector2.down);
        else if (y > 0)
            return RunAllBoxCastsForY(y, Vector2.up);
        return y;
    }
    private void SetTouching(Vector2 actualDirection, bool touching)
    {

        if (actualDirection == gravity.normalized)
            down = touching;
        else if (actualDirection == -gravity.normalized)
            up = touching;
        else if (actualDirection == Vector2.Perpendicular(-gravity.normalized))
            left = touching;
        else if (actualDirection == Vector2.Perpendicular(gravity.normalized))
            right = touching;
    }
    private Vector2 TopLeft()
    {
        return new Vector2(transform.position.x - c2D.bounds.extents.x, transform.position.y + c2D.bounds.extents.y);
    }
    private Vector2 BotRight()
    {
        return new Vector2(transform.position.x + c2D.bounds.extents.x, transform.position.y - c2D.bounds.extents.y);
    }
    private Vector2 TopRight()
    {
        return new Vector2(transform.position.x + c2D.bounds.extents.x, transform.position.y + c2D.bounds.extents.y);
    }
    private Vector2 BotLeft()
    {
        return new Vector2(transform.position.x - c2D.bounds.extents.x, transform.position.y - c2D.bounds.extents.y);
    }
}
