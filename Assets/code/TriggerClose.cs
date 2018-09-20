using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerClose : MonoBehaviour {

    public GameObject whichBarrier;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Renderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    
     void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Player")
        {
            Debug.Log("collison exited");
            whichBarrier.GetComponent<SpriteRenderer>().enabled = true;
            whichBarrier.GetComponent<Collider2D>().enabled = true;

            gameObject.GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    
    void OnCollisionExit2D(Collision2D collision)
    {
   

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }


}
