﻿using UnityEngine;

public class Barrier : Activating
{

    public DoorSounds sounds = new DoorSounds();
    private AudioSource source;

    void Awake(){
        source = GetComponent<AudioSource>();
    }
    public override void Activate()
    {
        //gameObject.GetComponent<SpriteRenderer>().enabled = false;
        ChangeSprite(true);
        gameObject.GetComponent<Collider2D>().enabled = false;
        source.PlayOneShot(sounds.soundTrigger);
    }

    public override void DeActivate()
    {
        //gameObject.GetComponent<SpriteRenderer>().enabled = true;
        ChangeSprite(false);
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
    void ChangeSprite(bool active)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("objects/barrier_4" + (active ? "_off" : ""), typeof(Sprite)) as Sprite;
    }
       [System.Serializable]
    public class DoorSounds {
        public AudioClip soundTrigger;
    }
}
