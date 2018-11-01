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
        Debug.Log(Input.GetAxisRaw("Horizontal"));
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (!animator.GetBool("Walk"))
                animator.SetBool("Walk", true);
        }
        else
        {
            if (animator.GetBool("Walk"))
                animator.SetBool("Walk", false);
        }
        if (pMovement.jumping)
            animator.SetTrigger("Jump");
	}
}
