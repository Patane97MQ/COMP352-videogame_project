using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class buttonScript : MonoBehaviour {
    private SpriteRenderer sRender;
    private BoxCollider2D bCollider;
    public Sprite myFirstImage;
    public Sprite mySecondImage;

    public List<string> activateTags;

    private bool buttonPressed = false;
    private int counter = 0;

    private void Start()
    {
        sRender = GetComponent<SpriteRenderer>();
        bCollider = GetComponent<BoxCollider2D>();
    }

    public bool Active()
    {
        return buttonPressed;
    }

    public int TimesPressed()
    {
        return counter;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   
        if((activateTags.Count == 0 && collision.gameObject.tag != "environment")
            || activateTags.Count != 0 && activateTags.Contains(collision.gameObject.tag))
        {
            buttonPressed = true;
            counter++;
            Debug.Log(counter);
        }

        sRender.sprite = mySecondImage;
        bCollider.size = new Vector2(0.5f, 0.05f);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "red_clone")
        {
            buttonPressed = false;
        }
        sRender.sprite = myFirstImage;
        //bCollider.size = new Vector2(0.5f, 0.15f);
    }
}
