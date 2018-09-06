using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class movement3 : MonoBehaviour {

    private Transform t;
    private Rigidbody2D rb2D;
    private Collider2D c2D;

    // Use this for initialization
    void Start () {
        t = transform;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        c2D = gameObject.GetComponent<Collider2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //Gravity
        AddPositionY(-0.048f);
	}

    void AddPositionX(float x)
    {
        transform.position = new Vector3(transform.position.x + x, transform.position.y, 0);
    }
    void AddPositionY(float by)
    {
        float y = CheckNextMoveY(by);
        transform.position = new Vector3(transform.position.x, transform.position.y + y, 0);
    }

    float CheckNextMoveY(float y)
    {
        if (y < 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(botRight() + new Vector2(0, y), Vector2.left, c2D.bounds.size.x, ~(1 << 8));
            if (hit)
            {
                Debug.Log("Down has been hit! Repositioning...");
                return -Math.Abs((hit.collider.bounds.max.y) - c2D.bounds.min.y);
            }
        }
        else if (y > 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(topLeft() + new Vector2(0, y), Vector2.right, c2D.bounds.size.x, ~(1 << 8));
            if (hit)
            {
                Debug.Log("Up has been hit! Repositioning...");
                return Math.Abs((hit.collider.bounds.min.y) - c2D.bounds.max.y);
            }
        }
        return y;

    }
    Vector2 topLeft()
    {
        return new Vector2(transform.position.x - c2D.bounds.extents.x, transform.position.y + c2D.bounds.extents.y);
    }
    Vector2 botRight()
    {
        return new Vector2(transform.position.x + c2D.bounds.extents.x, transform.position.y - c2D.bounds.extents.y);
    }
}
