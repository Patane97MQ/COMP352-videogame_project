﻿using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Animator))]
public class PlayerAnim : MonoBehaviour {

    Animator animator;
    PlayerMovement pMovement;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        pMovement = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetBool("Walk", pMovement.moving);

        animator.SetBool("Crouch", pMovement.crouching);

        animator.SetBool("Jump", pMovement.jumping);

        animator.SetBool("Airborne", !pMovement.down);
        
        animator.SetBool("Push", pMovement.pushing || pMovement.pulling ? true : false);
	}
}
