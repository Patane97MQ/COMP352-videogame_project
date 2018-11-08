using System;
using UnityEngine;

public class Crate : Physics {

    Animator animator;
    AudioSource source;

    public new void Start()
    {
        base.Start();
        animator = gameObject.GetComponent<Animator>();
        source = gameObject.GetComponent<AudioSource>();
    }
    public new void FixedUpdate()
    {
        if (velocity.x != 0)
        {
            source.volume = Math.Abs(velocity.x * 10);
            animator.SetBool("Pull", true);
        }
        else
        {
            source.volume = 1;
            animator.SetBool("Pull", false);
        }
        base.FixedUpdate();
    }
    public void StopCurrentSound()
    {
        source.Stop();
    }
    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
