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
        if (pMovement.velocity.x != 0)
            animator.SetBool("Walk", true);
        else
            animator.SetBool("Walk", false);
        if (pMovement.jumping)
            animator.SetTrigger("Jump");
	}
}
