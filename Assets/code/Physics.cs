using System;
using System.Collections;
using UnityEngine;

public class Physics : MonoBehaviour
{

    //public Vector2 gravity = new Vector2(0, -9.81f);
    
    public float weight = 0f;
    public Vector2 drag = new Vector2(1f, 1f);
    public Vector2 stepHeight = new Vector2(0.1f, 0.1f);

    protected Transform t;
    protected Rigidbody2D rb2D;
    protected BoxCollider2D c2D;
    protected Vector2 c2Dcenter;

    [HideInInspector]
    public float rotation;
    
    public bool up;
    public bool down;
    public bool left;
    public bool right;


    [HideInInspector]
    public bool airborne;
    [HideInInspector]
    public bool pushing;
    [HideInInspector]
    public bool pulling;

    [HideInInspector]
    public Vector2 velocity;
    [HideInInspector]
    public Vector2 acceleration = new Vector2(0, 0);
    

    protected float edgeCut = 0.02f;

    private float speedCap = float.MaxValue;

    public Transform GetTransform()
    {
        return t;
    }

    // Use this for initialization
    protected void Start()
    {
        t = transform;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        c2D = gameObject.GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    protected void FixedUpdate()
    {

        pushing = false;
        // Crouching hotfix. Have no idea why, but 0.030f is the magic number here. Not going to question it, its working for now xD
        c2Dcenter = new Vector2(transform.position.x + c2D.offset.x - (Math.Sign(c2D.offset.x) * 0.030f), transform.position.y + (Math.Sign(Physics2D.gravity.y) * (-c2D.offset.y +  (Math.Sign(c2D.offset.y) * 0.030f))));
        // Velocity constants are always applied!
        AddPositionY(velocity.y);
        AddPositionX(velocity.x);
        
        acceleration = (Physics2D.gravity + (Physics2D.gravity.normalized * weight)) / 1000;
        // accelerationY should never/rarely be changed. This is the constant downwards force of 'gravity'
        CheckRotation();

        // Velocity is always accelerated. This is exclusively used for gravity
        AddVelocity(acceleration);

        CalculateDrag();
        if (!down)
            StartCoroutine(CheckAirborne());
        else
            airborne = false;
    }
    IEnumerator CheckAirborne()
    {
        yield return new WaitForSeconds(0.05f);
        if (!down)
            airborne = true;
    }
    // ====================================================================

    private void CalculateDrag()
    {
        velocity = new Vector2((drag.x > 0 ? Mathf.MoveTowards(velocity.x, 0, drag.x / 100) : velocity.x), (drag.y > 0 ? Mathf.MoveTowards(velocity.y, 0, drag.y / 1000) : velocity.y));
        //velocity = Vector2.MoveTowards(velocity, new Vector2(0, velocity.y), drag.x / 100);
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
        }
        float x = CheckNextMoveX(bx);
        transform.position = new Vector3(Utilities.ClosestTo(transform.position.x + x, speedCap, 0), transform.position.y, 0);
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
    public Vector2 AddVelocity(Vector2 bv)
    {
        velocity += bv;
        return velocity;
    }
    public float AddRawForceX(Physics other, float x)
    {
        return AddVelocity(new Vector2(x, 0)).x;
    }
    public float AddRawForceY(Physics other, float y)
    {
        return AddVelocity(new Vector2(0, y)).y;
    }
    public float AddForceX(Physics other, float x)
    {
        float force = x / weight;
        //float force = (x * (other.weight / weight)) / weight;
        return AddRawForceX(other, force);
    }
    public float AddForceY(Physics other, float y)
    {
        //float force = y / weight;
        float force = CalculateForce(other, y);
        return AddRawForceY(other, force);
    }
    public float CalculateForce(Physics other, float a)
    {
        return a / weight;
    }
    // ====================================================================
    
    public void SetSpeedCap(float cap)
    {
        speedCap = cap;
    }
    public void ResetSpeedCap()
    {
        speedCap = float.MaxValue;
    }
    // Ensures object is correctly rotated for the direction of gravity.
    private void CheckRotation()
    {
        if (Physics2D.gravity.normalized == Vector2.down)
            rotation = 0;
        else if (Physics2D.gravity.normalized == Vector2.up)
            rotation = 180;
        else if (Physics2D.gravity.normalized == Vector2.left)
            rotation = -90;
        else if (Physics2D.gravity.normalized == Vector2.right)
            rotation = 90;

    }
    private void DrawBoxCast(Vector2 origin, Vector2 size, Vector2 direction, float distance)
    {
        Utilities.DrawBox(origin, size, Color.green);
        Utilities.DrawBox(origin + direction * distance, size, Color.black);
    }


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

    private float CalculateXCollision(float x, Vector2 direction)
    {
        float current = x;
        foreach (Physics physics in gameObject.GetComponentsInChildren<Physics>())
        {
            float temp = physics.RunAllBoxCastsForX(x, direction);
            if (Math.Abs(temp) < Math.Abs(current))
                current = temp;
        }
        return current;
    }
    
    private float RunAllBoxCastsForX(float x, Vector2 direction)
    {

        // Drawing a BoxCast of the below (for debugging)
        DrawBoxCast(c2Dcenter, new Vector2(0.01f, c2D.bounds.size.y - edgeCut * 2), direction, Math.Abs(x) + c2D.bounds.extents.x - 0.005f);
        Utilities.DrawBox(new Vector2(transform.position.x + c2D.offset.x - (Math.Sign(c2D.offset.x) * 0.030f), transform.position.y + c2D.offset.y - (Math.Sign(c2D.offset.y) * 0.030f)), c2D.bounds.size, Color.red);
        // BoxCast of this object is shot in the direction it wants to move. This will basically check if the objects 'next move' will hit anything
        RaycastHit2D nextCheck = Utilities.BoxCastHandler(gameObject, c2Dcenter, new Vector2(0.01f, c2D.bounds.size.y - edgeCut * 2), transform.rotation.z, direction, Math.Abs(x) + c2D.bounds.extents.x - 0.005f);

        // If an object is hit.
        if (nextCheck)
        {
            // Distance is saved as this object. The reason that its saved here is because it might be changed later (within stepCheck)
            float raycastDistance = nextCheck.distance;
            RaycastHit2D stepCheck = Utilities.BoxCastHandler(gameObject, c2Dcenter + new Vector2(0, (-Physics2D.gravity.normalized.y) * stepHeight.x), new Vector2(0.01f, c2D.bounds.size.y - edgeCut * 2), 0, direction, Math.Abs(x) + c2D.bounds.extents.x - 0.005f);
            RaycastHit2D stepCheckGround = Utilities.BoxCastHandler(gameObject, c2Dcenter, new Vector2(c2D.bounds.size.x - edgeCut * 2, 0.01f), 0, Physics2D.gravity.normalized, edgeCut + c2D.bounds.extents.y - 0.005f);

            DrawBoxCast(c2Dcenter, new Vector2(c2D.bounds.size.x - edgeCut * 2, 0.01f), Vector2.down, edgeCut + c2D.bounds.extents.y - 0.005f);
            // In this scenario, we bump into an object which is below our stepheight threshhold (such as a button). If so, we will try to step on top of it.
            // We also only pass this if statement when the object we are colliding is NOT the originally collided object.
            if (stepCheckGround && (!stepCheck || stepCheck.collider.gameObject != nextCheck.collider.gameObject && HasPhysics(stepCheck.collider.gameObject)))
            {
                // If we are able to step onto the object without glitching, do so!
                if (Math.Abs(x) >= edgeCut)
                {
                    // If we did hit an object AND that object has any sort of physics
                    if (stepCheck && HasPhysics(stepCheck.collider.gameObject))
                    {
                        pushing = true;
                        // Add force to that object
                        GrabPhysics(stepCheck.collider.gameObject).AddForceX(this, x);
                        // Move the distance from this object to the physics object
                        raycastDistance = stepCheck.distance;
                    }
                    else
                        // Move the Y value up to the current stepHeight (works in both Gravity directions)
                        transform.position += (new Vector3(0, -Physics2D.gravity.normalized.y)) * stepHeight.x;
                }
                // Otherwise, clip slightly into the object. This way we dont lose any momentum or movement on any objects!
                else
                    return x;
            }
            else
            {
                // If we are colliding into an object with physics, push it.
                if (HasPhysics(nextCheck.collider.gameObject))
                {
                    pushing = true;
                    GrabPhysics(nextCheck.collider.gameObject).AddForceX(this, x);
                }

                SetTouching(direction, true);
                SetVelocity(new Vector2(0, velocity.y));
                return Math.Sign(direction.x) * (raycastDistance - c2D.bounds.extents.x + 0.005f);
            }
        }
        return x;
    }
    private float RunAllBoxCastsForY(float y, Vector2 direction)
    {

        // Drawing a BoxCast of the below (for debugging)
        DrawBoxCast(c2Dcenter, new Vector2(c2D.bounds.size.x - edgeCut * 2, 0.01f), direction, Math.Abs(y) + c2D.bounds.extents.y - 0.005f);
        // BoxCast of this object is shot in the direction it wants to move. This will basically check if the objects 'next move' will hit anything
        RaycastHit2D nextCheck = Utilities.BoxCastHandler(gameObject, c2Dcenter, new Vector2(c2D.bounds.size.x - edgeCut * 2, 0.01f), transform.rotation.z, direction, Math.Abs(y) + c2D.bounds.extents.y - 0.005f);

        // If an object is hit.
        if (nextCheck)
        {
            // Distance is saved as this object. The reason that its saved here is because it might be changed later (within stepCheck)
            float raycastDistance = nextCheck.distance;

            // Checks that the object is next to a wall to the right and can fall to the left of an object its about to hit (such as a vertical button on a right wall)
            RaycastHit2D stepCheckRightWall = Utilities.BoxCastHandler(gameObject, c2Dcenter, new Vector2(0.01f, c2D.bounds.size.y - edgeCut * 2), 0, Vector2.right, stepHeight.y + c2D.bounds.extents.x - 0.005f);
            //Utilities.DrawBox(new Vector2(transform.position.x + stepHeight.y + c2D.bounds.extents.x - 0.025f, transform.position.y), new Vector2(0.05f, c2D.bounds.size.y - edgeCut * 2), Color.blue);
            RaycastHit2D stepCheckLeft  = Utilities.BoxCastHandler(gameObject, c2Dcenter - new Vector2(stepHeight.y, 0), new Vector2(c2D.bounds.size.x - edgeCut * 2, 0.01f), 0, direction, Math.Abs(y) + c2D.bounds.extents.y - 0.005f);
            //Utilities.DrawBox(new Vector2(transform.position.x - stepHeight.y, transform.position.y - Math.Abs(y) - c2D.bounds.extents.y + 0.025f), new Vector2(c2D.bounds.size.x - edgeCut * 2, 0.05f), Color.blue);

            // Checks that the object is next to a wall to the left and can fall to the right of an object its about to hit (such as a vertical button on a left wall)
            RaycastHit2D stepCheckLeftWall = Utilities.BoxCastHandler(gameObject, c2Dcenter, new Vector2(0.01f, c2D.bounds.size.y - edgeCut * 2), 0, Vector2.left, stepHeight.y + c2D.bounds.extents.x - 0.005f);
            //Utilities.DrawBox(new Vector2(transform.position.x - stepHeight.y - c2D.bounds.extents.x + 0.025f, transform.position.y), new Vector2(0.05f, c2D.bounds.size.y - edgeCut * 2), Color.blue);
            RaycastHit2D stepCheckRight = Utilities.BoxCastHandler(gameObject, c2Dcenter + new Vector2(stepHeight.y, 0), new Vector2(c2D.bounds.size.x - edgeCut * 2, 0.01f), 0, direction, Math.Abs(y) + c2D.bounds.extents.y - 0.005f);
            
            // In this scenario, we bump into an object which is below our stepheight threshhold (such as a button). If so, we will try to step on top of it.
            // We also only pass this if statement when the object we are colliding is NOT the originally collided object.
            if ((stepCheckRightWall && stepCheckRightWall.collider.gameObject != nextCheck.collider.gameObject && !HasPhysics(stepCheckRightWall.collider.gameObject) && !stepCheckLeft)
                || (stepCheckLeftWall && stepCheckLeftWall.collider.gameObject != nextCheck.collider.gameObject && !HasPhysics(stepCheckLeftWall.collider.gameObject) && !stepCheckRight))
            {

                // If we are able to step onto the object without glitching, do so!
                if (Math.Abs(y) >= edgeCut)
                {
                    // LEFT
                    // If we did hit an object AND that object has any sort of physics
                    if (stepCheckLeft && HasPhysics(stepCheckLeft.collider.gameObject))
                    {
                        // Add force to that object
                        GrabPhysics(stepCheckLeft.collider.gameObject).AddForceY(this, y);
                        // Move the distance from this object to the physics object
                        raycastDistance = stepCheckLeft.distance;
                    }
                    // RIGHT
                    else if (stepCheckRight && HasPhysics(stepCheckRight.collider.gameObject))
                    {
                        // Add force to that object
                        GrabPhysics(stepCheckRight.collider.gameObject).AddForceY(this, y);
                        // Move the distance from this object to the physics object
                        raycastDistance = stepCheckRight.distance;
                    }
                    // This is when the player actually gets pushed as they cannot stand on the thin object
                    else
                    {
                        int dir = !stepCheckLeft ? -1 : (!stepCheckRight ? 1 : 0);
                        // Move the Y value up to the current stepHeight (works in both Gravity directions)
                        transform.position += (new Vector3(dir, 0)) * stepHeight.y;
                        
                    }
                }
                // Otherwise, clip slightly into the object. This way we dont lose any momentum or movement on any objects!
                else
                    return y;
            }
            else
            {
                // If we are colliding into an object with physics, push it.
                if (HasPhysics(nextCheck.collider.gameObject))
                    GrabPhysics(nextCheck.collider.gameObject).AddForceY(this, y);
                
                SetTouching(direction, true);
                SetVelocity(new Vector2(velocity.x, 0));
                return Math.Sign(direction.y) * (nextCheck.distance - c2D.bounds.extents.y + 0.005f);
            }
        }
        return y;
    }
    private float CheckNextMoveX(float x)
    {
        SetTouching(Vector2.left, false);
        SetTouching(Vector2.right, false);
        if (x < 0)
            return CalculateXCollision(x, Vector2.left);
        else if (x > 0)
            return CalculateXCollision(x, Vector2.right);
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

        if (actualDirection == Physics2D.gravity.normalized)
            down = touching;
        else if (actualDirection == -Physics2D.gravity.normalized)
            up = touching;
        else if (actualDirection == Vector2.Perpendicular(-Physics2D.gravity.normalized))
            left = touching;
        else if (actualDirection == Vector2.Perpendicular(Physics2D.gravity.normalized))
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
