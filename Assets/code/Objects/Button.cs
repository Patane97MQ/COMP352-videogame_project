using System.Collections.Generic;
using UnityEngine;


public class Button : Tagable
{
    bool pressed = false;
    List<GameObject> pressing = new List<GameObject>();
    public ButtonSounds sounds = new ButtonSounds();
    private AudioSource source;

    private void Start()
    {
        ChangeSprite();
    }

    void Awake (){
        source = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (activateTags.Count == 0 || activateTags.Count != 0 && activateTags.Contains(collision.gameObject.tag) && !pressing.Contains(collision.gameObject))
            pressing.Add(collision.gameObject);
        if (pressed == false && pressing.Count > 0)
        {
            SetActivated(true);
            pressed = true;
            ChangeSprite();
            source.PlayOneShot(sounds.soundTrigger);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (activateTags.Count == 0 || activateTags.Count != 0 && activateTags.Contains(collision.gameObject.tag) && pressing.Contains(collision.gameObject))
            pressing.Remove(collision.gameObject);
        if (pressed == true && pressing.Count <= 0)
        {
            SetActivated(false);
            pressed = false;
            ChangeSprite();
        }
    }
    void ChangeSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("objects/coloured/" + colour + "/button_" + (activated ? "on" : "off") + (pressed ? "_pressed" : "") + ("_"+activatorType.ToString()), typeof(Sprite)) as Sprite;
    }

    [System.Serializable]
    public class ButtonSounds {
        public AudioClip soundTrigger;
    }
}
