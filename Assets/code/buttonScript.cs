using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class buttonScript : MonoBehaviour {
    public static bool buttonPressed;
    public int amountPressed; 
    private SpriteRenderer sRender;
    private BoxCollider2D bCollider;
    public Sprite myFirstImage;
    public Sprite mySecondImage;

    private void Start()
    {
        buttonPressed = false;
        amountPressed = 0;
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

    public bool active()
    {
        return buttonPressed;
    }

    public int amountActive()
    {
           
        return amountPressed;
    }



    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "red_clone") {

            buttonPressed = true;
            amountPressed++;
            Debug.Log(amountPressed++);
        }

        sRender.sprite = mySecondImage;
        bCollider.size = new Vector2(0.5f, 0.05f);
    }
        
 

    private void OnCollisionExit2D(Collision2D collision)
    {
        
        sRender.sprite = myFirstImage;
        //bCollider.size = new Vector2(0.5f, 0.15f);
    }
}