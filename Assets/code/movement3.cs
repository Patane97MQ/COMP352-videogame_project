using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class movement3 : MonoBehaviour {

    public float jumpStrength = 1f;
    public Vector2 gravityDirection = Vector2.down;
    public float gravityConstant = 0.4f;

    private Transform t;
    private Rigidbody2D rb2D;
    private Collider2D c2D;

    private bool facingRight = true;

    private float rotation;
    private bool up;
    private bool down;
    private bool left;
    private bool right;

    private Vector2 velocity;
    private Vector2 acceleration;

    private float axis;
    private float axisLimiter;
    private float collOffset = 0.065f;

    // Use this for initialization
    void Start () {
        t = transform;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        c2D = gameObject.GetComponent<Collider2D>();

        // accelerationY should never/rarely be changed. This is the constant downwards force of 'gravity'
        acceleration = gravityDirection * (gravityConstant / 100);
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        CheckRotation();
        // If player is on the ground and "Jump" button is pressed,
        // They will jump the opposite direction of gravity
        if (down && Input.GetButton("Jump"))
            SetVelocity((-gravityDirection)*jumpStrength / 10);

        // Changes the movement to different axes depending on the direction of gravity
        axis = Input.GetAxis("Horizontal");
        Debug.Log(axis);
        if (gravityDirection.x == 0)
            SetVelocity(new Vector2(0.1f * axis, velocity.y));
        else if (gravityDirection.y == 0)
            SetVelocity(new Vector2(velocity.x, 0.1f * axis));

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

        Debug.Log("Collisions: " + (down ? "down " : "")
            + (up ? "up " : "")
            + (left ? "left " : "")
            + (right ? "right " : ""));

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

    private float CheckNextMoveX(float x)
    {
        SetTouching(Vector2.left, false);
        SetTouching(Vector2.right, false);
        if (x < 0)
        {
            Debug.DrawRay(TopLeft() + new Vector2(x, -collOffset), Vector2.down * (c2D.bounds.size.y - collOffset * 2), Color.yellow);

            RaycastHit2D hit = Physics2D.Raycast(TopLeft() + new Vector2(x, -collOffset), Vector2.down, c2D.bounds.size.y - collOffset*2, ~(1 << 8));
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

            RaycastHit2D hit = Physics2D.Raycast(BotRight() + new Vector2(x, collOffset), Vector2.up, c2D.bounds.size.y - collOffset*2, ~(1 << 8));
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
    private float CheckNextMoveY(float y)
    {
        SetTouching(Vector2.down, false);
        SetTouching(Vector2.up, false);
        if (y < 0)
        {
            SetTouching(Vector2.up, false);
            Debug.DrawRay(BotRight() + new Vector2(-collOffset, y), Vector2.left * (c2D.bounds.size.x - collOffset*2), Color.yellow);

            RaycastHit2D hit = Physics2D.Raycast(BotRight() + new Vector2(-collOffset, y), Vector2.left, c2D.bounds.size.x - collOffset*2, ~(1 << 8));
            if (hit)
            {
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
}
