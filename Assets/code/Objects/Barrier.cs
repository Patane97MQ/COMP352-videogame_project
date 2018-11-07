using System.Collections;
using UnityEngine;

public class Barrier : Activating
{

    public DoorSounds sounds = new DoorSounds();
    private AudioSource source;

    private Animator animator;

    void Awake(){
        source = GetComponent<AudioSource>();
        animator = gameObject.GetComponent<Animator>();
    }
    public override void Activate()
    {
        animator.SetBool("Active", true);
    }

    public override void DeActivate()
    {
        animator.SetBool("Active", false);

    }

    public void SoundOpen()
    {
        source.PlayOneShot(sounds.open);
    }
    public void SoundBoom()
    {
        source.PlayOneShot(sounds.boom);
    }

       [System.Serializable]
    public class DoorSounds {
        public AudioClip open;
        public AudioClip boom;
    }
}
