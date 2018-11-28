using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public ColourEnum colour;

    private bool pressed = false;

    private Animator animator;
    private AbstractActivator activator;
    private Tags tags;
    private List<GameObject> pressing = new List<GameObject>();

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        activator = gameObject.GetComponent<AbstractActivator>();
        colour = (activator && activator is ColourActivator ? ((ColourActivator)activator).colour : colour);
        tags = gameObject.GetComponent<Tags>();
        UpdateSprite();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!pressing.Contains(collider.gameObject))
        {
            if (!tags || tags.CheckTag(collider.gameObject.tag))
            {
                pressing.Add(collider.gameObject);
                if (collider.gameObject.tag.Contains("crate"))
                    collider.gameObject.GetComponent<Crate>().LightUp(true);
            }
            else
            {
                animator.SetTrigger("failed");
            }
        }
        if(!animator.GetBool("pressed") && pressing.Count > 0 && activator.SetActivated(true) && activator.Activated())
        {
            pressed = true;
            animator.SetTrigger("pressed");
            UpdateSprite();
        }

    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(!tags || tags.CheckTag(collider.gameObject.tag))
        {
            pressing.Remove(collider.gameObject);
            if(collider.gameObject.tag.Contains("crate"))
                collider.gameObject.GetComponent<Crate>().LightUp(false);
        }
        if(animator.GetBool("pressed") && pressing.Count <= 0 && activator.SetActivated(false) && !activator.Activated())
        {
            pressed = false;
            UpdateSprite();
        }
            
    }
    void UpdateSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("objects/coloured/" + colour + "/button_" + (activator.Activated() ? "on" : "off") + (pressed ? "_pressed" : "") + ("_" + activator.activatorType.ToString()), typeof(Sprite)) as Sprite;
    }
}
