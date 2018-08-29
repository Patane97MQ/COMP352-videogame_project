﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class PlatformerMove : MonoBehaviour {

	public float walkSpeed = 4f;
	public float jumpSpeed = 4f;
	public bool walkOnAir = false;
	private Rigidbody2D _rigidbody2D;

	private bool facingRight = true;
	private bool onGround;
	public Rect groundRect = new Rect(-0.32f, -0.72f, 0.56f, 0.1f);
	public LayerMask groundLayerMask = -1;


	void Start() {
		_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
		CheckOnGround();

		Vector2 v = _rigidbody2D.velocity;

		if (onGround || walkOnAir) {
			v.x = walkSpeed * Input.GetAxis("Horizontal");
		}

		if (onGround && Input.GetButtonDown("Jump")) {
			v.y = jumpSpeed;
		}

		_rigidbody2D.velocity = v;

		if (v.x < 0 && facingRight || v.x > 0 && !facingRight) {
			facingRight = !facingRight;
			Vector3 s = transform.localScale;
			s.x = -s.x;
			transform.localScale = s;

			groundRect.x = - groundRect.max.x;

		}

	}

	private void CheckOnGround() {

		onGround = false;

		Vector2 min = new Vector2(transform.position.x, transform.position.y) + groundRect.min;
		Vector2 max = new Vector2(transform.position.x, transform.position.y) + groundRect.max;

		Collider2D collider = Physics2D.OverlapArea(min, max, groundLayerMask);

		onGround = collider != null;

	}

	void OnDrawGizmos() {
		Vector3 centre = transform.position;
		centre.x += groundRect.center.x;
		centre.y += groundRect.center.y;

		Vector3 size = Vector3.zero;
		size.x += groundRect.width;
		size.y += groundRect.height;

		if (onGround) {
			Gizmos.color = Color.red;
		}
		else {
			Gizmos.color = Color.white;
		}
		Gizmos.DrawWireCube(centre, size);

	}

}
