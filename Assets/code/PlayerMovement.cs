using UnityEngine;


public class PlayerMovement : Physics {

    public float moveStrength = 1f;
    public float jumpStrength = 1.5f;

    public PlayerSounds sounds = new PlayerSounds();

    private bool facingRight = true;
    private bool crouching = false;
    
    private float axis;
    private float collOffset = 0.065f;

    private AudioSource source;

    new void Start()
    {
        base.Start();
        Utilities.initialGravity = Physics2D.gravity;
    }
    void Awake (){
        source = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    new void FixedUpdate () {

        base.FixedUpdate();
        // If player is on the ground and "Jump" button is pressed,
        // They will jump the opposite direction of gravity
        if (down && Input.GetButton("Jump") && !crouching){
            SetVelocity((-Physics2D.gravity.normalized) * jumpStrength / weight);
            source.PlayOneShot(sounds.jump);
            }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            if(!crouching)
            {
                crouching = true;
                transform.position = new Vector3(transform.position.x, transform.position.y + (Physics2D.gravity.normalized.y * (transform.localScale.y / 2)), 0);
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2, 0);
            }
        }
        else
        {
            if (crouching)
            {
                RaycastHit2D hit;
                if (Physics2D.gravity.normalized.y < 0)
                    hit = Utilities.BoxCastHandler(gameObject, new Vector2(transform.position.x, transform.position.y - c2D.bounds.extents.y), new Vector2(c2D.bounds.size.x, 0.05f), 0, Vector2.up, c2D.bounds.size.y * 2);
                else
                    hit = Utilities.BoxCastHandler(gameObject, new Vector2(transform.position.x, transform.position.y + c2D.bounds.extents.y), new Vector2(c2D.bounds.size.x, 0.05f), 0, Vector2.down, c2D.bounds.size.y * 2);
                if (!hit)
                {
                    crouching = false;
                    transform.position = new Vector3(transform.position.x, transform.position.y + (-Physics2D.gravity.normalized.y * (transform.localScale.y / 2)), 0);
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2, 0);
                }
            }
        }

        // Changes the movement to different axes depending on the direction of gravity

        axis = Input.GetAxis("Horizontal");
        //SetVelocity(new Vector2(movementSpeed / 10 * axis, movementSpeed / 10 * axis) * Vector2.Perpendicular(gravityDirection));
        if (Physics2D.gravity.normalized.x == 0)
            SetVelocity(new Vector2(moveStrength / weight * axis, velocity.y));
        else if (Physics2D.gravity.normalized.y == 0)
            SetVelocity(new Vector2(velocity.x, moveStrength / weight * axis));
        
        // Determines which way the object should face when moving in specified gravity
        if ((Physics2D.gravity.normalized == Vector2.down || Physics2D.gravity.normalized == Vector2.right) 
         && (axis < 0 && facingRight || axis > 0 && !facingRight)
         || (Physics2D.gravity.normalized == Vector2.up || Physics2D.gravity.normalized == Vector2.left)
         && (axis > 0 && facingRight || axis < 0 && !facingRight))
        {
            facingRight = !facingRight;
            transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, 0);
        }

    }

    [System.Serializable]
    public class PlayerSounds
    {
        public AudioClip jump;
    }
}
