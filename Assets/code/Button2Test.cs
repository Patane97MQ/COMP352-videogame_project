using System.Collections.Generic;
using UnityEngine;


public class Button2Test : Tagable
{
    bool pressed = false;
    bool colStay = false;
    bool aOnce = false;
    List<GameObject> pressing = new List<GameObject>();
    public Activating triggering;
    public Activating triggering2;

    private void Start()
    {
        ChangeSprite();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //if (activateTags.Count == 0 || activateTags.Count != 0 && activateTags.Contains(collision.gameObject.tag))
        //    SetActivated(true);

        //if (aOnce == false)
        //{
        //    triggering.GetComponent<Activating>().Activate();
        //    triggering2.GetComponent<Activating>().Activate();
        //    aOnce = true;
        //}
        //pressed = true;
        //ChangeSprite();

        if (activateTags.Count == 0 || activateTags.Count != 0 && activateTags.Contains(collision.gameObject.tag))
            pressing.Add(collision.gameObject);
        if(pressed = false && pressing.Count > 0)
        {
            SetActivated(true);
            pressed = true;
            ChangeSprite();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (activateTags.Count == 0 || activateTags.Count != 0 && activateTags.Contains(collision.gameObject.tag))
        //    SetActivated(false);
        //pressed = false;
        //ChangeSprite();
        if (activateTags.Count == 0 || activateTags.Count != 0 && activateTags.Contains(collision.gameObject.tag))
            pressing.Add(collision.gameObject);
        if (pressed = true && pressing.Count <= 0)
        {
            SetActivated(false);
            pressed = false;
            ChangeSprite();
        }
    }
    void ChangeSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("objects/coloured/" + colour + "/button_" + (activated ? "on" : "off") + (pressed ? "_pressed" : ""), typeof(Sprite)) as Sprite;
    }
}
