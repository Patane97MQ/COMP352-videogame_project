using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Rigidbody2D))]
public class newMovement : MonoBehaviour {
    public float maxSpeed = 5f;
    public float acceleration = 4f;
    public float jumpSpeed = 4f;
    public Boolean usingCollider = true;

    private Vector2 gravityVector;
    private Vector2 jumpVector;
    //private float gravityAcceleration = 0.48f;

    private Rigidbody2D _rigidbody2D;
    private Bounds objectBounds;

    private bool facingRight = true;

    private float axis;
    private float border = 0.05f;

    private float topY;
    private float botY;
    private float leftX;
    private float rightX;

    // Current collisions
    private bool top = false;
    private bool bottom = false;
    private bool left = false;
    private bool right = false;


    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        objectBounds = GetComponent<Collider2D>().bounds;
        ResetGravity();
        topY = transform.position.y + objectBounds.extents.y + border;
        botY = transform.position.y - objectBounds.extents.y - border;
        leftX = transform.position.x - objectBounds.extents.x - border;
        rightX = transform.position.x + objectBounds.extents.x + border;
    }

    void ResetGravity()
    {
        SetGravity(new Vector2(0, 0.48f));
    }
    void SetGravity(Vector2 vector)
    {
        gravityVector = vector;
        jumpVector = new Vector2((vector.x != 0 ? 1 : 0) * jumpSpeed, (vector.y != 0 ? 1 : 0) * jumpSpeed);
    }
    void Update()
    {
        if (usingCollider)
            CollisionCheck();
        //NewCollisionCheck();
        
        //Vector2 velocity = _rigidbody2D.velocity;
        axis = Input.GetAxis("Horizontal");
        HorizontalMovement();

        if (!usingCollider)
        {
            if (Input.GetButton("Jump"))
                //velocity = jumpVector;
                setVelocityY(jumpVector.y);
        }
        else
        {
            if (bottom && Input.GetButton("Jump"))
            {
                //velocity = jumpVector;
                setVelocityY(jumpVector.y);
            }

            //else if (bottom)
            //    velocity.y = 0;
            //else if (top && velocity.y > 0)
            //    velocity.y = 0;
            //else
            //    velocity = velocity - gravityVector;
            if(!bottom)
                addVelocityY(-gravityVector.y);
        }


        //_rigidbody2D.velocity = velocity;

        if (_rigidbody2D.velocity.x < 0 && facingRight || _rigidbody2D.velocity.x > 0 && !facingRight)
        {
            facingRight = !facingRight;
            Vector3 s = transform.localScale;
            s.x = -s.x;
            transform.localScale = s;
        }
    }
    void addVelocityY(float y)
    {
        Vector2 velocity = _rigidbody2D.velocity;
        velocity.y += y;
        _rigidbody2D.velocity = velocity;
    }
    void setVelocityX(float x)
    {
        Debug.Log("X=" + x);
        Vector2 velocity = _rigidbody2D.velocity;
        if (x != 0)
        {
            x = checkVertical(x);
        }
        velocity.x = x;
        _rigidbody2D.velocity = velocity;
    }
    void setVelocityY(float y)
    {
        Debug.Log("Y=" + y);
        Vector2 velocity = _rigidbody2D.velocity;
        if (y != 0)
        {
            y = checkHorizontal(y);
        }
        velocity.y = y;
        _rigidbody2D.velocity = velocity;
    }
    void setVelocity(float x, float y)
    {
        setVelocityX(x);
        setVelocityY(y);
    }

    private float checkVertical(float x)
    {
        if (x < 0)
        {
            Debug.Log("=================LEFT====================");
            Debug.DrawRay(topLeft() + new Vector2(x, 0), Vector2.down * objectBounds.size.y, Color.yellow);
            RaycastHit2D hit = Physics2D.Raycast(topLeft() + new Vector2(x, 0), Vector2.down, objectBounds.size.y, ~(1 << 8));
            if (hit)
            {
                Debug.Log("================HIT HIT HIT=====================");
                float distance = Math.Abs((hit.transform.position.x - hit.collider.bounds.extents.x) - leftX);
                //transform.position = new Vector3(transform.position.x - distance, transform.position.y, 0);
                return x;
            }
        }
        else if (x > 0)
        {
            Debug.Log("=================RIGHT====================");
            Debug.DrawRay(botRight() + new Vector2(x, 0), Vector2.up * objectBounds.size.y, Color.yellow);
            RaycastHit2D hit = Physics2D.Raycast(botRight() + new Vector2(x, 0), Vector2.up, objectBounds.size.y, ~(1 << 8));
            if (hit)
            {
                Debug.Log("================HIT HIT HIT=====================");
                float distance = Math.Abs((hit.transform.position.x + hit.collider.bounds.extents.x) - rightX);
                //transform.position = new Vector3(transform.position.x + distance, transform.position.y, 0);
                return x;
            }
        }
        return x;
    }

    private float checkHorizontal(float y)
    {
        if (y > 0)
        {
            Debug.Log("================UP=====================");
            Debug.DrawRay(topLeft() + new Vector2(0, y), Vector2.right * objectBounds.size.x, Color.yellow);
            RaycastHit2D hit = Physics2D.Raycast(topLeft() + new Vector2(0, y), Vector2.right, objectBounds.size.x, ~(1 << 8));
            if (hit)
            {
                Debug.Log("================HIT HIT HIT=====================");
                float distance = Math.Abs((hit.transform.position.y - hit.collider.bounds.extents.y) - topY);
                //transform.position = new Vector3(transform.position.x, transform.position.y + distance, 0);
                return y;
            }
        }
        else if(y < 0)
        {
            Debug.Log("================DOWN=====================");
            Debug.DrawRay(botRight() + new Vector2(0, y), Vector2.left * objectBounds.size.x, Color.yellow);
            RaycastHit2D hit = Physics2D.Raycast(botRight() + new Vector2(0, y), Vector2.left, objectBounds.size.x, ~(1 << 8));
            if (hit)
            {
                Debug.Log("================HIT HIT HIT=====================");
                float distance = Math.Abs((hit.transform.position.y + hit.collider.bounds.extents.y) - botY);
                //transform.position = new Vector3(transform.position.x, transform.position.y - distance, 0); 
                return y;
            }
        }
        return y;

    }
    void HorizontalMovement()
    {
        float tempSpeed = Math.Abs(_rigidbody2D.velocity.x) + acceleration;

        //if (left && axis < 0)
        //    axis = 0;
        //if (right && axis > 0)
        //    axis = 0;
        if(axis != 0)
            setVelocityX(Math.Min(tempSpeed, maxSpeed) * axis);
    }
    Vector2 topLeft()
    {
        return new Vector2(transform.position.x - objectBounds.extents.x, transform.position.y + objectBounds.extents.y);
    }
    Vector2 botRight()
    {
        return new Vector2(transform.position.x + objectBounds.extents.x, transform.position.y - objectBounds.extents.y);
    }
    void CollisionCheck()
    {
        float height = objectBounds.size.y;
        float width = objectBounds.size.x;

        // Try not to go lower than 0.01f
        float border = 0.05f;

        Debug.DrawRay(topLeft() + new Vector2(0, border), Vector2.right * width, Color.white);
        Debug.DrawRay(botRight() + new Vector2(0, -border), Vector2.left * width, Color.red);
        Debug.DrawRay(topLeft() + new Vector2(-border, 0), Vector2.down * height, Color.green);
        Debug.DrawRay(botRight() + new Vector2(border, 0), Vector2.up * height, Color.blue);

        // Raycasts ignore the layer '1'
        int layerMask = ~(1 << 8);

        float newX = transform.position.x;
        float newY = transform.position.y;
        
        RaycastHit2D hit;

        // Checking Top collider
        hit = Physics2D.Raycast(topLeft() + new Vector2(0, border), Vector2.right, width, layerMask);
        if (hit)
        {
            top = true;
            // Moves the object to sit right on top of the object below it (such as the ground);
            newY = hit.transform.position.y - hit.collider.bounds.extents.y - objectBounds.extents.y - border;
            Debug.Log("Top Collision");
        }
        else
            top = false;

        transform.position = new Vector3(newX, newY, 0);
        // Checking Bottom collider
        hit = Physics2D.Raycast(botRight() + new Vector2(0, -border), Vector2.left, width, layerMask);
        if (hit)
        {
            bottom = true;
            // Moves the object to sit right on top of the object below it (such as the ground);
            newY = hit.transform.position.y + hit.collider.bounds.extents.y + objectBounds.extents.y + border;
            Debug.Log("Bottom Collision");
        }
        else
            bottom = false;

        transform.position = new Vector3(newX, newY, 0);
        // Checking Left collider
        hit = Physics2D.Raycast(topLeft() + new Vector2(-border, 0), Vector2.down, height, layerMask);
        if (hit)
        {
            left = true;
            newX = hit.transform.position.x + hit.collider.bounds.extents.x + objectBounds.extents.x + border;
            Debug.Log("Left Collision");
        }
        else
            left = false;

        transform.position = new Vector3(newX, newY, 0);
        // Checking Right collider
        hit = Physics2D.Raycast(botRight() + new Vector2(border, 0), Vector2.up, height, layerMask);
        if (hit)
        {
            right = true;
            newX = hit.transform.position.x - hit.collider.bounds.extents.x - objectBounds.extents.x - border;
            Debug.Log("Right Collision");
        }
        else
            right = false;
        

        transform.position = new Vector3(newX, newY, 0);

    }

    void NewCollisionCheck()
    {
        float height = objectBounds.size.y;
        float width = objectBounds.size.x;

        // Try not to go lower than 0.01f
        float border = 0.05f;

        Debug.DrawRay(topLeft() + new Vector2(0, border), Vector2.right * width, Color.white);
        Debug.DrawRay(botRight() + new Vector2(0, -border), Vector2.left * width, Color.red);
        Debug.DrawRay(topLeft() + new Vector2(-border, 0), Vector2.down * height, Color.green);
        Debug.DrawRay(botRight() + new Vector2(border, 0), Vector2.up * height, Color.blue);

        // Raycasts ignore the layer '1'
        int layerMask = ~(1 << 8);

        List<RaycastHit2D> hits = new List<RaycastHit2D>();

        // Checking Top collider
        hits.AddRange(Physics2D.RaycastAll(topLeft() + new Vector2(0, border), Vector2.right, width, layerMask));
        // Checking Bottom collider
        hits.AddRange(Physics2D.RaycastAll(botRight() + new Vector2(0, -border), Vector2.left, width, layerMask));
        // Checking Left collider
        hits.AddRange(Physics2D.RaycastAll(topLeft() + new Vector2(-border, 0), Vector2.down, height, layerMask));
        // Checking Right collider
        hits.AddRange(Physics2D.RaycastAll(botRight() + new Vector2(border, 0), Vector2.up, height, layerMask));

        //Debug.Log("BEFORE: "+ allHits.Count);
        //List<RaycastHit2D> hits = allHits.Distinct().ToList();
        //Debug.Log("AFTER: "+hits.Count);
        float currentX = transform.position.x;
        float currentY = transform.position.y;

        float distance = float.MaxValue;
        float value = 0;
        Boolean XAxis = true;

        float tempAxis;

        foreach(RaycastHit2D hit in hits)
        {
            // Check Up
            // ColliderY - half Colliders Height (YSize/2) - half objects Height (YSize/2)
            tempAxis = hit.transform.position.y + hit.collider.bounds.extents.y + objectBounds.extents.y + border;
            float cDistance = Math.Abs(tempAxis - currentY);
            if (cDistance < distance)
            {
                value = tempAxis;
                distance = cDistance;
                XAxis = false;
            }


            // Check Down
            tempAxis = hit.transform.position.y - hit.collider.bounds.extents.y - objectBounds.extents.y - border;
            cDistance = Math.Abs(tempAxis - currentY);
            if (cDistance < distance)
            {
                value = tempAxis;
                distance = cDistance;
                XAxis = false;
            }


            // Check Left
            tempAxis = hit.transform.position.x - hit.collider.bounds.extents.x - objectBounds.extents.x - border;
            cDistance = Math.Abs(tempAxis - currentX);
            if (cDistance < distance)
            {
                value = tempAxis;
                distance = cDistance;
                XAxis = true;
            }


            // Check Right
            tempAxis = hit.transform.position.x + hit.collider.bounds.extents.x + objectBounds.extents.x + border;
            cDistance = Math.Abs(tempAxis - currentX);
            if (cDistance < distance)
            {
                value = tempAxis;
                distance = cDistance;
                XAxis = true;
            }
        }
        
        Debug.Log("XAXIS=" + XAxis + " | value=" + value);
        if(hits.Count > 0)
            transform.position = new Vector3((XAxis ? value : currentX), (!XAxis ? value : currentY), 0);
    }
}