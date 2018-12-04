using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Button : Tagable
{
    bool pressed = false;
    public float deactivateDelay = 0f;
    List<GameObject> pressing = new List<GameObject>();
    List<GameObject> delayed = new List<GameObject>();
    public ButtonSounds sounds = new ButtonSounds();
    private AudioSource source;

    private void Start()
    {
        ChangeSprite();
    }
    
    void Awake (){
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (delayed.Remove(collider.gameObject))
            return;
        if (!pressing.Contains(collider.gameObject))
        {
            if(activateTags.Count == 0 || (activateTags.Count != 0 && activateTags.Contains(collider.gameObject.tag)))
            {
                pressing.Add(collider.gameObject);
                if (collider.gameObject.tag.Contains("crate"))
                {
                    string colour = collider.gameObject.tag.Replace("_crate", "");
                    collider.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("objects/crates/" + colour + "_on", typeof(Sprite)) as Sprite;
                }
            }
            else
            {
                source.PlayOneShot(sounds.incorrectActive);
            }
        }
        if (!pressed && pressing.Count > 0)
        {
            if (SetActivated(true) && activated)
                source.PlayOneShot(sounds.correctActive);
            pressed = true;
            ChangeSprite();
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        delayed.Add(collider.gameObject);
        StartCoroutine(CheckDeactive(collider));
    }
    IEnumerator CheckDeactive(Collider2D collider)
    {
        yield return new WaitForSeconds(deactivateDelay);
        if (!delayed.Remove(collider.gameObject))
            yield break;
        if (activateTags.Count == 0 || activateTags.Count != 0 && activateTags.Contains(collider.gameObject.tag) && pressing.Contains(collider.gameObject))
        {
            pressing.Remove(collider.gameObject);
            if (collider.gameObject.tag.Contains("crate"))
            {
                string colour = collider.gameObject.tag.Replace("_crate", "");
                collider.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("objects/crates/" + colour, typeof(Sprite)) as Sprite;
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
        if(activatorType == ActivatorType.Once)
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("objects/coloured/" + colour + "/button_" + (activated ? "on" : "off") + (activated ? "_pressed" : "") + ("_" + activatorType.ToString()), typeof(Sprite)) as Sprite;
        else
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("objects/coloured/" + colour + "/button_" + (activated ? "on" : "off") + (pressed ? "_pressed" : "") + ("_"+activatorType.ToString()), typeof(Sprite)) as Sprite;
    }

    [System.Serializable]
    public class ButtonSounds {
        public AudioClip correctActive;
        public AudioClip incorrectActive;
    }
}
