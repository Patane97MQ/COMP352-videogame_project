using System;
using UnityEngine;


public class PlayerMovement : Physics {

    public float moveStrength = 1f;
    public float jumpStrength = 1.5f;

    public PlayerSounds sounds = new PlayerSounds();

    private bool waitJump;

    private bool facingRight = true, facingDown = true;
    private bool crouching = false;

    private float capMovement = float.MaxValue;
    private float axis;

    private AudioSource source;

    [HideInInspector]
    public bool flipSpriteX = true, flipSpriteY = true;

    new void Start()
    {
        base.Start();
        Utilities.initialGravity = Physics2D.gravity;
        source = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update() {
        JumpInput();

        CrouchInput();

        MovementInput();

        SpriteFlipX();

        SpriteFlipY();
    }

    private void JumpInput()
    {
        // If player is on the ground and "Jump" button is pressed,
        // They will jump the opposite direction of gravity
        if (down && Input.GetButton("Jump") && !crouching)
        {
            if (!waitJump)
            {
                SetVelocity((-Physics2D.gravity.normalized) * jumpStrength / weight);
                source.PlayOneShot(sounds.jump);
                waitJump = true;
            }
        }
        if (Input.GetButtonUp("Jump"))
            waitJump = false;
    }

    private void CrouchInput()
    {
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            if (!crouching)
            {
                crouching = true;
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2, 0);
                transform.position = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y / 2), 0);
            }
        }
        else
        {
            if (crouching)
            {
                // This section checks if the player is allowed to un-crouch (If there is something above them whilst crouching)
                RaycastHit2D hit;
                if (Physics2D.gravity.normalized.y < 0)
                    hit = Utilities.BoxCastHandler(gameObject, new Vector2(transform.position.x, transform.position.y - c2D.bounds.extents.y), new Vector2(c2D.bounds.size.x - edgeCut * 2, 0.01f), 0, Vector2.up, c2D.bounds.size.y * 2);
                else
                    hit = Utilities.BoxCastHandler(gameObject, new Vector2(transform.position.x, transform.position.y + c2D.bounds.extents.y), new Vector2(c2D.bounds.size.x - edgeCut * 2, 0.01f), 0, Vector2.down, c2D.bounds.size.y * 2);
                if (!hit)
                {
                    crouching = false;
                    Debug.Log(transform.localScale.y / 2);
                    transform.position = new Vector3(transform.position.x, transform.position.y + (transform.localScale.y / 2), 0);
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2, 0);
                }
            }
        }
    }

    private void MovementInput()
    {
        // Changes the movement to different axes depending on the direction of gravity

        axis = Input.GetAxis("Horizontal");
        //SetVelocity(new Vector2(movementSpeed / 10 * axis, movementSpeed / 10 * axis) * Vector2.Perpendicular(gravityDirection));
        if (Physics2D.gravity.normalized.x == 0)
            SetVelocity(new Vector2(Utilities.ClosestTo(moveStrength / weight * axis, capMovement, 0), velocity.y));
        else if (Physics2D.gravity.normalized.y == 0)
            SetVelocity(new Vector2(velocity.x, Utilities.ClosestTo(moveStrength / weight * axis, capMovement, 0)));

        capMovement = float.MaxValue;
    }

    private void SpriteFlipX()
    {
        // Flips objects spriteX depending on moving left or right
        if (flipSpriteX)
        {
            if (axis < 0 && facingRight || axis > 0 && !facingRight)
            {
                facingRight = !facingRight;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 0);
            }
        }
        else
            flipSpriteX = true;
    }

    private void SpriteFlipY()
    {
        // Flips objects spriteY depending on the direction of Y Gravity
        if (flipSpriteY)
        {
            if (Physics2D.gravity.normalized.y == 1 && facingDown || Physics2D.gravity.normalized.y == -1 && !facingDown)
            {
                facingDown = !facingDown;
                //transform.localRotation = new Quaternion(0, 0, 180, 0);
                transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, 0);
            }
        }
        else
            flipSpriteY = true;
    }

    public float CapMovement(float x)
    {
        Debug.Log("Cap=" + x);
        capMovement = x;
        return capMovement;
    }

    [System.Serializable]
    public class PlayerSounds
    {
        public AudioClip jump;
    }
}
