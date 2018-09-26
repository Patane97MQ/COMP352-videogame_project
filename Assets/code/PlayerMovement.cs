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
            SetVelocity((-gravity.normalized) * jumpStrength / 10);

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
        if (gravity.normalized.x == 0)
            SetVelocity(new Vector2(movementSpeed / 10 * axis, velocity.y));
        else if (gravity.normalized.y == 0)
            SetVelocity(new Vector2(velocity.x, movementSpeed / 10 * axis));
        
        // Determines which way the object should face when moving in specified gravity
        if ((gravity.normalized == Vector2.down || gravity.normalized == Vector2.right) 
         && (axis < 0 && facingRight || axis > 0 && !facingRight)
         || (gravity.normalized == Vector2.up || gravity.normalized == Vector2.left)
         && (axis > 0 && facingRight || axis < 0 && !facingRight))
        {
            facingRight = !facingRight;
            transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, 0);
        }

        base.FixedUpdate();
    }
}
