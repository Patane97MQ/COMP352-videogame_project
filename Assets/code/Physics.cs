using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Physics : MonoBehaviour {

    public Vector2 gravityDirection = Vector2.down;
    public float gravityConstant = 0.98f;
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
        acceleration = gravityDirection * (gravityConstant / 100);
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

    public void AddForceX(float x)
    {
        AddVelocity(new Vector2(x/weight, 0));
    }
    public void AddForceY(float y)
    {
        AddVelocity(new Vector2(0, y/weight));
    }
    // ====================================================================

    // Ensures object is correctly rotated for the direction of gravity.
    private void CheckRotation()
    {
        float previousRotation = rotation;
        if (gravityDirection == Vector2.down)
            rotation = 0;
        else if (gravityDirection == Vector2.up)
            rotation = 180;
        else if (gravityDirection == Vector2.left)
            rotation = -90;
        else if (gravityDirection == Vector2.right)
            rotation = 90;
        if (previousRotation != rotation)
            transform.Rotate(new Vector3(0, 0, rotation));

    }
    private void DrawBox(Vector2 centre, Vector2 size, Color color)
    {
        Debug.DrawLine(new Vector2(centre.x - size.x / 2, centre.y + size.y / 2), new Vector2(centre.x + size.x / 2, centre.y + size.y / 2), color);
        Debug.DrawLine(new Vector2(centre.x + size.x / 2, centre.y + size.y / 2), new Vector2(centre.x + size.x / 2, centre.y - size.y / 2), color);
        Debug.DrawLine(new Vector2(centre.x + size.x / 2, centre.y - size.y / 2), new Vector2(centre.x - size.x / 2, centre.y - size.y / 2), color);
        Debug.DrawLine(new Vector2(centre.x - size.x / 2, centre.y - size.y / 2), new Vector2(centre.x - size.x / 2, centre.y + size.y / 2), color);
        //Debug.DrawRay(new Vector2(centre.x - size.x / 2, centre.y + size.y / 2), Vector2.right * size.x / 2, color);
        //Debug.DrawRay(new Vector2(centre.x + size.x / 2, centre.y + size.y / 2), Vector2.down * size.y / 2, color);
        //Debug.DrawRay(new Vector2(centre.x + size.x / 2, centre.y - size.y / 2), Vector2.left * size.x / 2, color);
        //Debug.DrawRay(new Vector2(centre.x - size.x / 2, centre.y - size.y / 2), Vector2.up * size.y / 2, color);
    }
    private void DrawBoxCast(Vector2 origin, Vector2 size, Vector2 direction, float distance)
    {
        DrawBox(origin, size, Color.green);
        DrawBox(origin + direction * distance, size, Color.red);
    }

    private float edgeCut = 0.02f;

    //private float BoxCheckingX(Vector2 direction, float x)
    //{
    //    // Drawing a BoxCast of the below (for debugging)
    //    DrawBoxCast(new Vector2(transform.position.x, transform.position.y), new Vector2(0.01f, c2D.bounds.size.y - edgeCut * 2), direction, Math.Abs(x) + c2D.bounds.extents.x - 0.005f);

    //    // BoxCast of this object is shot in the direction it wants to move. This will basically check if the objects 'next move' will hit anything
    //    RaycastHit2D nextCheck = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y), new Vector2(0.01f, c2D.bounds.size.y - edgeCut * 2), transform.rotation.z, direction, Math.Abs(x) + c2D.bounds.extents.x - 0.005f, ~(1 << 8));

    //    // If the object WILL hit something on its next move
    //    if (nextCheck)
    //    {
    //        // BoxCast of this object is shot the same as above, however the y position is +stepHeight.
    //        // If we hit nothing, then we know the originally hit object is below our step height and the object should be placed on top of it
    //        List<RaycastHit2D> stepChecks = new List<RaycastHit2D>(Physics2D.BoxCastAll(new Vector2(transform.position.x, transform.position.y + stepHeight), new Vector2(0.01f, c2D.bounds.size.y - (edgeCut * 2)), transform.rotation.z, direction, Math.Abs(x) + c2D.bounds.extents.x - 0.005f, ~(1 << 8)));

    //        foreach (RaycastHit2D step in stepChecks)
    //        {
    //            if (step.collider.gameObject.GetComponent("Physics") as Physics == null)
    //                Debug.Log(step.collider.name);
    //        }
    //        // If no objects were hit when checking the stepheight (meaning we can step on top of this object) 
    //        // AND If we make the downwards edge cut (accounting for the edge cut of the bottom collider. If this check wasnt here, then we can clip into objects which is BAD!)
    //        if (stepChecks.Where(stepCheck => !(stepCheck.collider is Physics)).Count() == 0)
    //        {
    //            if (Math.Abs(x) >= edgeCut)
    //                transform.position += Vector3.up * stepHeight;
    //            else
    //                return x;
    //        }

    //        else
    //        {
    //            if ((nextCheck.collider.GetComponent("Physics") as Physics) != null)
    //                (nextCheck.collider.GetComponent("Physics") as Physics).AddForceX(x);
    //            // Setting the object to be touching the left
    //            SetTouching(Vector2.left, true);

    //            // Setting the object X velocity to 0
    //            SetVelocity(new Vector2(0, velocity.y));

    //            // Returning the distance the object is from the hit object. This ensures that it moves as close as it can to that object before stopping
    //            // If we didnt do this, then there would be large gaps between an object and its collider
    //            return -(nextCheck.distance - c2D.bounds.extents.x + 0.005f);
    //        }
    //    }
    //}

    private float CheckNextMoveX(float x)
    {
        SetTouching(Vector2.left, false);
        SetTouching(Vector2.right, false);
        if (x < 0)
        {
            // Drawing a BoxCast of the below (for debugging)
            DrawBoxCast(new Vector2(transform.position.x, transform.position.y), new Vector2(0.01f, c2D.bounds.size.y - edgeCut * 2), Vector2.left, Math.Abs(x) + c2D.bounds.extents.x - 0.005f);

            // BoxCast of this object is shot in the direction it wants to move. This will basically check if the objects 'next move' will hit anything
            RaycastHit2D nextCheck = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y), new Vector2(0.01f, c2D.bounds.size.y - edgeCut * 2), transform.rotation.z, Vector2.left, Math.Abs(x) + c2D.bounds.extents.x - 0.005f, ~(1 << 8));

            // If the object WILL hit something on its next move
            if (nextCheck)
            {
                // BoxCast of this object is shot the same as above, however the y position is +stepHeight.
                // If we hit nothing, then we know the originally hit object is below our step height and the object should be placed on top of it
                List<RaycastHit2D> stepChecks = new List<RaycastHit2D>(Physics2D.BoxCastAll(new Vector2(transform.position.x, transform.position.y + stepHeight), new Vector2(0.01f, c2D.bounds.size.y - (edgeCut * 2)), transform.rotation.z, Vector2.left, Math.Abs(x) + c2D.bounds.extents.x - 0.005f, ~(1 << 8)));

                foreach(RaycastHit2D step in stepChecks)
                {
                    if (step.collider.gameObject.GetComponent("Physics") as Physics == null)
                        Debug.Log(step.collider.name);
                }
                // If no objects were hit when checking the stepheight (meaning we can step on top of this object) 
                // AND If we make the downwards edge cut (accounting for the edge cut of the bottom collider. If this check wasnt here, then we can clip into objects which is BAD!)
                if (stepChecks.Where(stepCheck => !(stepCheck.collider is Physics)).Count() == 0)
                {
                    if (Math.Abs(x) >= edgeCut)
                        transform.position += Vector3.up * stepHeight;
                    else
                        return x;
                }
                
                else
                {
                    if ((nextCheck.collider.GetComponent("Physics") as Physics) != null)
                        (nextCheck.collider.GetComponent("Physics") as Physics).AddForceX(x);
                    // Setting the object to be touching the left
                    SetTouching(Vector2.left, true);

                    // Setting the object X velocity to 0
                    SetVelocity(new Vector2(0, velocity.y));

                    // Returning the distance the object is from the hit object. This ensures that it moves as close as it can to that object before stopping
                    // If we didnt do this, then there would be large gaps between an object and its collider
                    return -(nextCheck.distance - c2D.bounds.extents.x + 0.005f);
                }
            }
        }
        // This entire section is identical to the above, only it is checking the right side of the object (x > 0)
        // Use above comments for reference.
        else if (x > 0)
        {
            DrawBoxCast(new Vector2(transform.position.x, transform.position.y), new Vector2(0.01f, c2D.bounds.size.y - edgeCut * 2), Vector2.right, Math.Abs(x) + c2D.bounds.extents.x - 0.005f);

            RaycastHit2D nextCheck = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y), new Vector2(0.01f, c2D.bounds.size.y - edgeCut * 2), transform.rotation.z, Vector2.right, Math.Abs(x) + c2D.bounds.extents.x - 0.005f, ~(1 << 8));
            if (nextCheck)
            {
                RaycastHit2D stepCheck = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y + stepHeight), new Vector2(0.01f, c2D.bounds.size.y - edgeCut * 2), transform.rotation.z, Vector2.right, Math.Abs(x) + c2D.bounds.extents.x - 0.005f, ~(1 << 8));

                if (!stepCheck)
                {
                    if (Math.Abs(x) >= edgeCut)
                        transform.position += Vector3.up * stepHeight;
                    else
                        return x;
                }
                else
                {
                    if ((nextCheck.collider.GetComponent("Physics") as Physics) != null)
                        (nextCheck.collider.GetComponent("Physics") as Physics).AddForceX(x);
                    SetTouching(Vector2.right, true);
                    SetVelocity(new Vector2(0, velocity.y));
                    return nextCheck.distance - c2D.bounds.extents.x + 0.005f;
                }
            }
        }
        return x;
    }
    private float CheckNextMoveY(float y)
    {
        SetTouching(Vector2.down, false);
        SetTouching(Vector2.up, false);
        if (y < 0)
        {
            DrawBoxCast(new Vector2(transform.position.x, transform.position.y), new Vector2(c2D.bounds.size.x - edgeCut * 2, 0.01f), Vector2.down, Math.Abs(y) + c2D.bounds.extents.y - 0.005f);

            RaycastHit2D nextCheck = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y), new Vector2(c2D.bounds.size.x - edgeCut * 2, 0.01f), transform.rotation.z, Vector2.down, Math.Abs(y) + c2D.bounds.extents.y - 0.005f, ~(1 << 8));
            if (nextCheck)
            {
                if ((nextCheck.collider.GetComponent("Physics") as Physics) != null)
                    (nextCheck.collider.GetComponent("Physics") as Physics).AddForceY(y);
                SetTouching(Vector2.down, true);
                SetVelocity(new Vector2(velocity.x, 0));
                return -(nextCheck.distance - c2D.bounds.extents.y + 0.005f);
            }
        }
        else if (y > 0)
        {
            DrawBoxCast(new Vector2(transform.position.x, transform.position.y), new Vector2(c2D.bounds.size.x - edgeCut * 2, 0.01f), Vector2.up, Math.Abs(y) + c2D.bounds.extents.y - 0.005f);

            RaycastHit2D nextCheck = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y), new Vector2(c2D.bounds.size.x - edgeCut * 2, 0.01f), transform.rotation.z, Vector2.up, Math.Abs(y) + c2D.bounds.extents.y - 0.005f, ~(1 << 8));
            if (nextCheck)
            {
                if ((nextCheck.collider.GetComponent("Physics") as Physics) != null)
                    (nextCheck.collider.GetComponent("Physics") as Physics).AddForceY(y);
                SetTouching(Vector2.up, true);
                SetVelocity(new Vector2(velocity.x, 0));
                return nextCheck.distance - c2D.bounds.extents.y + 0.005f;
            }
        }
        return y;
    }
    private void SetTouching(Vector2 actualDirection, bool touching)
    {

        if (actualDirection == gravityDirection)
            down = touching;
        else if (actualDirection == -gravityDirection)
            up = touching;
        else if (actualDirection == Vector2.Perpendicular(-gravityDirection))
            left = touching;
        else if (actualDirection == Vector2.Perpendicular(gravityDirection))
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
