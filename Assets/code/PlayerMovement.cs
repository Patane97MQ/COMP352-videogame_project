using UnityEngine;


public class PlayerMovement : Physics {

    public float movementSpeed = 1f;
    public float jumpStrength = 1.5f;

    private bool facingRight = true;
    private bool crouching = false;
    
    private float axis;
    private float collOffset = 0.065f;


    // Update is called once per frame
    new void FixedUpdate () {

        // If player is on the ground and "Jump" button is pressed,
        // They will jump the opposite direction of gravity
        if (down && Input.GetButton("Jump") && !crouching)
            SetVelocity((-gravityDirection)*jumpStrength / 10);

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
        
        // Determines which way the object should face when moving in specified gravity
        if ((gravityDirection == Vector2.down || gravityDirection == Vector2.right) 
         && (axis < 0 && facingRight || axis > 0 && !facingRight)
         || (gravityDirection == Vector2.up || gravityDirection == Vector2.left)
         && (axis > 0 && facingRight || axis < 0 && !facingRight))
        {
            facingRight = !facingRight;
            transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, 0);
        }

        base.FixedUpdate();
    }

    


    //[Obsolete("Method is obsolete. Use 'CheckNextMoveX' instead.")]
    //private float OLDCheckNextMoveX(float x)
    //{
    //    SetTouching(Vector2.left, false);
    //    SetTouching(Vector2.right, false);
    //    if (x < 0)
    //    {
    //        Debug.DrawRay(TopLeft() + new Vector2(x, -collOffset), Vector2.down * (c2D.bounds.size.y - collOffset * 2), Color.yellow);

    //        RaycastHit2D hit = Physics2D.Raycast(TopLeft() + new Vector2(x, -collOffset), Vector2.down, c2D.bounds.size.y - collOffset * 2, ~(1 << 8));

    //        if (hit)
    //        {
    //            Debug.Log("Left has been hit! Repositioning...");
    //            SetTouching(Vector2.left, true);
    //            SetVelocity(new Vector2(0, velocity.y));
    //            return (hit.collider.bounds.max.x) - c2D.bounds.min.x;
    //        }
    //    }
    //    else if (x > 0)
    //    {
    //        Debug.DrawRay(BotRight() + new Vector2(x, collOffset), Vector2.up * (c2D.bounds.size.y - collOffset * 2), Color.yellow);

    //        RaycastHit2D hit = Physics2D.Raycast(BotRight() + new Vector2(x, collOffset), Vector2.up, c2D.bounds.size.y - collOffset * 2, ~(1 << 8));
    //        if (hit)
    //        {
    //            Debug.Log("Right has been hit! Repositioning...");
    //            SetTouching(Vector2.right, true);
    //            SetVelocity(new Vector2(0, velocity.y));
    //            return (hit.collider.bounds.min.x) - c2D.bounds.max.x;
    //        }
    //    }
    //    return x;

    //}
    //[Obsolete("Method is obsolete. Use 'CheckNextMoveY' instead.")]
    //private float OLDCheckNextMoveY(float y)
    //{
    //    SetTouching(Vector2.down, false);
    //    SetTouching(Vector2.up, false);
    //    if (y < 0)
    //    {
    //        SetTouching(Vector2.up, false);
    //        Debug.DrawRay(BotRight() + new Vector2(-collOffset, y), Vector2.left * (c2D.bounds.size.x - collOffset*2), Color.yellow);

    //        RaycastHit2D hit = Physics2D.Raycast(BotRight() + new Vector2(-collOffset, y), Vector2.left, c2D.bounds.size.x - collOffset*2, ~(1 << 8));
    //        if (hit && hit.distance == 0)
    //            hit = Physics2D.Raycast(BotLeft() + new Vector2(collOffset, y), Vector2.right, c2D.bounds.size.x - collOffset * 2, ~(1 << 8));
    //        if (hit)
    //        {
    //            Debug.Log(hit.normal);
    //            Debug.Log("Down has been hit! Repositioning...");
    //            SetTouching(Vector2.down, true);
    //            SetVelocity(new Vector2(velocity.x, 0));
    //            return (hit.collider.bounds.max.y) - c2D.bounds.min.y;
    //        }
    //    }
    //    else if (y > 0)
    //    {
    //        SetTouching(Vector2.down, false);
    //        Debug.DrawRay(TopLeft() + new Vector2(collOffset, y), Vector2.right * (c2D.bounds.size.x - collOffset * 2), Color.yellow);

    //        RaycastHit2D hit = Physics2D.Raycast(TopLeft() + new Vector2(collOffset, y), Vector2.right, c2D.bounds.size.x - collOffset*2, ~(1 << 8));
    //        if (hit)
    //        {
    //            Debug.Log("Up has been hit! Repositioning...");
    //            SetTouching(Vector2.up, true);
    //            SetVelocity(new Vector2(velocity.x, 0));
    //            return (hit.collider.bounds.min.y) - c2D.bounds.max.y;
    //        }
    //    }
    //    return y;

    //}
}
