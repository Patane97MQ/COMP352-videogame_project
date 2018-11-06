using UnityEngine;


public class PlayerMovement : Physics {

    public float moveStrength = 1f;
    public float jumpStrength = 1.5f;

    public float moveAcceleration = 0.25f;


    public PlayerSounds sounds = new PlayerSounds();

    [HideInInspector]
    public bool facingRight = true, facingDown = true;

    private float axis;
    private float accelAdd = 0f;
    private float capMovement = float.MaxValue;

    [HideInInspector]
    public bool moving;
    [HideInInspector]
    public bool crouching;
    private Vector3 standingBounds;
    private Vector2 standingOffset;
    [HideInInspector]
    public bool jumping;
    private bool jumpRefresh;

    private AudioSource source;

    [HideInInspector]
    public bool flipSpriteX = true, flipSpriteY = true;


    new void Start()
    {
        base.Start();
        Utilities.initialGravity = Physics2D.gravity;
        source = GetComponent<AudioSource>();
        standingBounds = c2D.size;
        standingOffset = c2D.offset;
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
        if (down && jumping)
            jumping = false;
        if (down 
            && Input.GetButton("Jump") 
            && !crouching 
            && !jumpRefresh)
        {
            jumping = true;
            jumpRefresh = true;
            SetVelocity((-Physics2D.gravity.normalized) * jumpStrength / weight);
            source.PlayOneShot(sounds.jump);
        }
        if (Input.GetButtonUp("Jump"))
            jumpRefresh = false;
    }

    private void CrouchInput()
    {
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            if (!crouching)
            {
                crouching = true;
                c2D.offset = new Vector2(standingOffset.x, standingOffset.y - standingBounds.y / 4);
                c2D.size = new Vector3(standingBounds.x + 0.17f, standingBounds.y / 2, standingBounds.z);

                //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2, 0);
                //transform.position = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y / 2), 0);
            }
        }
        else
        {
            if (crouching)
            {
                // This section checks if the player is allowed to un-crouch (If there is something above them whilst crouching)
                RaycastHit2D hit;
                if (Physics2D.gravity.normalized.y < 0)
                    hit = Utilities.BoxCastHandler(gameObject, new Vector2(transform.position.x + c2D.offset.x, transform.position.y + c2D.offset.y - c2D.bounds.extents.y), new Vector2(c2D.bounds.size.x - edgeCut * 2, 0.01f), 0, Vector2.up, c2D.bounds.size.y * 2);
                else
                    hit = Utilities.BoxCastHandler(gameObject, new Vector2(transform.position.x + c2D.offset.x, transform.position.y - c2D.offset.y + c2D.bounds.extents.y), new Vector2(c2D.bounds.size.x - edgeCut * 2, 0.01f), 0, Vector2.down, c2D.bounds.size.y * 2);
                if (!hit)
                {
                    crouching = false;
                    c2D.offset = standingOffset;
                    c2D.size = standingBounds;
                    //transform.position = new Vector3(transform.position.x, transform.position.y + (transform.localScale.y / 2), 0);
                    //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2, 0);
                }
            }
        }
    }
    private void MovementInput()
    {
        axis = Input.GetAxisRaw("Horizontal");

        moving = (axis == 0 ? false : true);

        accelAdd = Mathf.MoveTowards(accelAdd, axis, moveAcceleration);
        
        SetVelocity(new Vector2(Utilities.ClosestTo(moveStrength / weight * accelAdd, capMovement, 0), velocity.y));

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
