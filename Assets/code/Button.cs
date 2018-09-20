using UnityEngine;


public class Button : Tagable {
    bool pressed = false;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("objects/coloured/" + colour + "/button/" + (pressed ? "pressed_" : "") + (activated ? "active" : "inactive"), typeof(Sprite)) as Sprite;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   
        if(activateTags.Count == 0 || activateTags.Count != 0 && activateTags.Contains(collision.gameObject.tag))
            SetActivated(true);
        pressed = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("objects/coloured/" + colour + "/button/" + (pressed ? "pressed_" : "") + (activated ? "active" : "inactive"), typeof(Sprite)) as Sprite;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (activateTags.Count == 0  || activateTags.Count != 0 && activateTags.Contains(collision.gameObject.tag))
            SetActivated(false);
        pressed = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("objects/coloured/" + colour + "/button/" + (pressed ? "pressed_" : "") + (activated ? "active" : "inactive"), typeof(Sprite)) as Sprite;
    }
}
