using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour {
    
    private SpriteRenderer sRender;
    private BoxCollider2D bCollider;
    public Sprite myFirstImage;
    public Sprite mySecondImage;

    private void Start()
    {
        sRender = GetComponent<SpriteRenderer>();
        bCollider = GetComponent<BoxCollider2D>();
    }

    public void SetImage1()
    {
        sRender.sprite = myFirstImage;
    }

    public void SetImage2()
    {
        sRender.sprite = mySecondImage;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "red_clone")
        {
            sRender.sprite = mySecondImage;
            bCollider.size = new Vector2(0.5f, 0.05f);
     
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        sRender.sprite = myFirstImage;
        //bCollider.size = new Vector2(0.5f, 0.15f);
    }
}