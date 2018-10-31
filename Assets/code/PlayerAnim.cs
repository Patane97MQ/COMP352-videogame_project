using UnityEngine;

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
        if(Input.GetAxisRaw("Horizontal") != 0)
            animator.SetTrigger("Moving");
        else
            animator.SetTrigger("Idle");
        if (pMovement.jumping)
            animator.SetTrigger("Jump");
	}
}
