using UnityEngine;

public class Barrier : Activating
{

    public DoorSounds sounds = new DoorSounds();
    private AudioSource source;

    private Color startColour;

    void Awake(){
        source = GetComponent<AudioSource>();
        startColour = gameObject.GetComponent<SpriteRenderer>().color;
    }
    public override void Activate()
    {
        // gameObject.GetComponent<SpriteRenderer>().enabled = false;
        ChangeSprite(true);
        gameObject.GetComponent<Collider2D>().enabled = false;
        source.PlayOneShot(sounds.soundTrigger);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(239, 240, 240);
    }

    public override void DeActivate()
    {
        // gameObject.GetComponent<SpriteRenderer>().enabled = true;
        ChangeSprite(false);
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().color = startColour;
    
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
