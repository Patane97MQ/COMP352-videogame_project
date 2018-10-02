using UnityEngine;


public class Button : Tagable {
    bool pressed = false;

    private void Start()
    {
        ChangeSprite();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   
        if(activateTags.Count == 0 || activateTags.Count != 0 && activateTags.Contains(collision.gameObject.tag))
            SetActivated(true);
        pressed = true;
        ChangeSprite();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (activateTags.Count == 0  || activateTags.Count != 0 && activateTags.Contains(collision.gameObject.tag))
            SetActivated(false);
        pressed = false;
        ChangeSprite();
        }
    void ChangeSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("objects/coloured/" + colour + "/button_" + (activated ? "on" : "off") + (pressed ? "_pressed" : ""), typeof(Sprite)) as Sprite;
    }
}
