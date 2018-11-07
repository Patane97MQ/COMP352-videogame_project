using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Button : Tagable
{
    bool pressed = false;
    public float deactivateDelay = 0f;
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
        {
            pressing.Add(collision.gameObject);
            if (collision.gameObject.tag.Contains("crate"))
            {
                string colour = collision.gameObject.tag.Replace("_crate", "");
                collision.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("objects/crates/" + colour + "_on", typeof(Sprite)) as Sprite;
            }
        }
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
        StartCoroutine(CheckDeactive(collision));
    }
    IEnumerator CheckDeactive(Collision2D collision)
    {
        yield return new WaitForSeconds(deactivateDelay);
        if (activateTags.Count == 0 || activateTags.Count != 0 && activateTags.Contains(collision.gameObject.tag) && pressing.Contains(collision.gameObject))
        {
            pressing.Remove(collision.gameObject);
            if (collision.gameObject.tag.Contains("crate"))
            {
                string colour = collision.gameObject.tag.Replace("_crate", "");
                collision.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("objects/crates/" + colour, typeof(Sprite)) as Sprite;
            }
        }
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
