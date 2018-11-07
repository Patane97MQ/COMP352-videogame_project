using System.Collections;
using UnityEngine;

public class Barrier : Activating
{

    public DoorSounds sounds = new DoorSounds();
    private AudioSource source;
    private string spriteName;

    private Animator animator;

    private Color startColour;

    void Awake(){
        source = GetComponent<AudioSource>();
        startColour = gameObject.GetComponent<SpriteRenderer>().color;
        spriteName = gameObject.GetComponent<SpriteRenderer>().sprite.name;
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
