using System;
using UnityEngine;


public class movement3 : MonoBehaviour {

    public float movementSpeed = 1f;
    public float jumpStrength = 1f;
    public Vector2 gravityDirection = Vector2.down;
    public float gravityConstant = 0.4f;
    public float stepHeight = 0.1f;

    private Transform t;
    private Rigidbody2D rb2D;
    private Collider2D c2D;

    private bool facingRight = true;
    private bool crouching = false;

    private float rotation;
    private bool up;
    private bool down;
    private bool left;
    private bool right;

    private Vector2 velocity;
    private Vector2 acceleration;
    
    private float axis;
    private float collOffset = 0.065f;

    private Vector2 xAxisMovement;

    // Use this for initialization
    void Start()
    {
        t = transform;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        c2D = gameObject.GetComponent<Collider2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        // accelerationY should never/rarely be changed. This is the constant downwards force of 'gravity'
        Vector2 normGravDir = new Vector2(gravityDirection.x, gravityDirection.y);
        normGravDir.Normalize();
        acceleration = normGravDir * (gravityConstant / 100);
        CheckRotation();
        // If player is on the ground and "Jump" button is pressed,
        // They will jump the opposite direction of gravity
        Vector2 jumpVel = new Vector2();
        if (down && Input.GetButton("Jump") && !crouching)
            //jumpVel = (-gravityDirection) * jumpStrength / 10;
            AddVelocity((-gravityDirection) * jumpStrength / 10);
        if (Input.GetButton("Vertical"))
        {
            if(!crouching)
            {
                crouching = true;
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2, 0);
                transform.position = new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2, 0);
            }
        }
        else
        {
            if (crouching)
            {
                crouching = false;
                transform.position = new Vector3(transform.position.x, transform.position.y + transform.localScale.y / 2, 0);
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2, 0);
            }
        }

        // Changes the movement to different axes depending on the direction of gravity

        axis = Input.GetAxis("Horizontal");
        //SetVelocity(new Vector2(movementSpeed / 10 * axis, movementSpeed / 10 * axis) * Vector2.Perpendicular(gravityDirection));
        if (gravityDirection.x == 0)
            SetVelocity(new Vector2(movementSpeed / 10 * axis, velocity.y));
        else if (gravityDirection.y == 0)
            SetVelocity(new Vector2(velocity.x, movementSpeed / 10 * axis));
        AddVelocity(-xAxisMovement);
        xAxisMovement = Vector2.Perpendicular(gravityDirection) * (movementSpeed / 10 * axis);
        AddVelocity(xAxisMovement);
        
        // Determines which way the object should face when moving in specified gravity
        if ((gravityDirection == Vector2.down || gravityDirection == Vector2.right) 
         && (axis < 0 && facingRight || axis > 0 && !facingRight)
         || (gravityDirection == Vector2.up || gravityDirection == Vector2.left)
         && (axis > 0 && facingRight || axis < 0 && !facingRight))
        {
            facingRight = !facingRight;
            transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, 0);
        }

        // Velocity constants are always applied!
        AddPositionX(velocity.x);
        AddPositionY(velocity.y);

        //AddVelocity(-xAxisMovement);
        //Debug.Log("Collisions: " + (down ? "down " : "")
        //    + (up ? "up " : "")
        //    + (left ? "left " : "")
        //    + (right ? "right " : ""));

        //Debug.Log("Velocity(" + velocity.x + "," + velocity.y + ")");
        //Debug.Log("Down(" + down + ")");

        // Velocity is always accelerated. This is exclusively used for gravity
        AddVelocity(acceleration);
    }

    // ====================================================================

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

    // ====================================================================

    // Ensures object is correctly rotated for the direction of gravity.
    private void CheckRotation()
    {
        //float previousRotation = rotation;
        //if (gravityDirection == Vector2.down)
        //    rotation = 0;
        //else if (gravityDirection == Vector2.up)
        //    rotation = 180;
        //else if (gravityDirection == Vector2.left)
        //    rotation = -90;
        //else if (gravityDirection == Vector2.right)
        //    rotation = 90;
        //if (previousRotation != rotation)
        //    transform.Rotate(new Vector3(0, 0, rotation));
        transform.Rotate(Vector2.)
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

    private float edgeCut = 0.01f;

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
                RaycastHit2D stepCheck = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y + stepHeight), new Vector2(0.01f, c2D.bounds.size.y - edgeCut * 2), transform.rotation.z, Vector2.left, Math.Abs(x) + c2D.bounds.extents.x - 0.005f, ~(1 << 8));

                // If no objects were hit when checking the stepheight (meaning we can step on top of this object) 
                // AND If we make the downwards edge cut (accounting for the edge cut of the bottom collider. If this check wasnt here, then we can clip into objects which is BAD!)
                if (!stepCheck && Math.Abs(x) >= edgeCut)
                    transform.position = new Vector3(transform.position.x, transform.position.y + stepHeight, transform.position.z);
                else
                {
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

                if (!stepCheck && Math.Abs(x) >= edgeCut)
                    transform.position = new Vector3(transform.position.x, transform.position.y + stepHeight, transform.position.z);
                else
                {
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

            RaycastHit2D hit = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y), new Vector2(c2D.bounds.size.x - edgeCut * 2, 0.01f), transform.rotation.z, Vector2.down, Math.Abs(y) + c2D.bounds.extents.y - 0.005f, ~(1 << 8));
            if (hit)
            {
                SetTouching(Vector2.down, true);
                SetVelocity(new Vector2(velocity.x, 0));
                return -(hit.distance - c2D.bounds.extents.y + 0.005f);
            }
        }
        else if (y > 0)
        {
            DrawBoxCast(new Vector2(transform.position.x, transform.position.y), new Vector2(c2D.bounds.size.x - edgeCut * 2, 0.01f), Vector2.up, Math.Abs(y) + c2D.bounds.extents.y - 0.005f);

            RaycastHit2D hit = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y), new Vector2(c2D.bounds.size.x - edgeCut * 2, 0.01f), transform.rotation.z, Vector2.up, Math.Abs(y) + c2D.bounds.extents.y - 0.005f, ~(1 << 8));
            if (hit)
            {
                SetTouching(Vector2.up, true);
                SetVelocity(new Vector2(velocity.x, 0));
                return hit.distance - c2D.bounds.extents.y + 0.005f;
            }
        }
        return y;
    }

    [Obsolete("Method is obsolete. Use 'CheckNextMoveX' instead.")]
    private float OLDCheckNextMoveX(float x)
    {
        SetTouching(Vector2.left, false);
        SetTouching(Vector2.right, false);
        if (x < 0)
        {
            Debug.DrawRay(TopLeft() + new Vector2(x, -collOffset), Vector2.down * (c2D.bounds.size.y - collOffset * 2), Color.yellow);

            RaycastHit2D hit = Physics2D.Raycast(TopLeft() + new Vector2(x, -collOffset), Vector2.down, c2D.bounds.size.y - collOffset * 2, ~(1 << 8));

            if (hit)
            {
                Debug.Log("Left has been hit! Repositioning...");
                SetTouching(Vector2.left, true);
                SetVelocity(new Vector2(0, velocity.y));
                return (hit.collider.bounds.max.x) - c2D.bounds.min.x;
            }
        }
        else if (x > 0)
        {
            Debug.DrawRay(BotRight() + new Vector2(x, collOffset), Vector2.up * (c2D.bounds.size.y - collOffset * 2), Color.yellow);

            RaycastHit2D hit = Physics2D.Raycast(BotRight() + new Vector2(x, collOffset), Vector2.up, c2D.bounds.size.y - collOffset * 2, ~(1 << 8));
            if (hit)
            {
                Debug.Log("Right has been hit! Repositioning...");
                SetTouching(Vector2.right, true);
                SetVelocity(new Vector2(0, velocity.y));
                return (hit.collider.bounds.min.x) - c2D.bounds.max.x;
            }
        }
        return x;

    }
    [Obsolete("Method is obsolete. Use 'CheckNextMoveY' instead.")]
    private float OLDCheckNextMoveY(float y)
    {
        SetTouching(Vector2.down, false);
        SetTouching(Vector2.up, false);
        if (y < 0)
        {
            SetTouching(Vector2.up, false);
            Debug.DrawRay(BotRight() + new Vector2(-collOffset, y), Vector2.left * (c2D.bounds.size.x - collOffset*2), Color.yellow);

            RaycastHit2D hit = Physics2D.Raycast(BotRight() + new Vector2(-collOffset, y), Vector2.left, c2D.bounds.size.x - collOffset*2, ~(1 << 8));
            if (hit && hit.distance == 0)
                hit = Physics2D.Raycast(BotLeft() + new Vector2(collOffset, y), Vector2.right, c2D.bounds.size.x - collOffset * 2, ~(1 << 8));
            if (hit)
            {
                Debug.Log(hit.normal);
                Debug.Log("Down has been hit! Repositioning...");
                SetTouching(Vector2.down, true);
                SetVelocity(new Vector2(velocity.x, 0));
                return (hit.collider.bounds.max.y) - c2D.bounds.min.y;
            }
        }
        else if (y > 0)
        {
            SetTouching(Vector2.down, false);
            Debug.DrawRay(TopLeft() + new Vector2(collOffset, y), Vector2.right * (c2D.bounds.size.x - collOffset * 2), Color.yellow);

            RaycastHit2D hit = Physics2D.Raycast(TopLeft() + new Vector2(collOffset, y), Vector2.right, c2D.bounds.size.x - collOffset*2, ~(1 << 8));
            if (hit)
            {
                Debug.Log("Up has been hit! Repositioning...");
                SetTouching(Vector2.up, true);
                SetVelocity(new Vector2(velocity.x, 0));
                return (hit.collider.bounds.min.y) - c2D.bounds.max.y;
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
