using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerClose : MonoBehaviour {

    public GameObject whichBarrier;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Renderer>().enabled = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.GetComponent<Collider2D>().enabled = false;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
     
        whichBarrier.GetComponent<SpriteRenderer>().enabled = true;
        whichBarrier.GetComponent<Collider2D>().enabled = true;
        disableButtons();

    }

    void disableButtons()
    {
        whichBarrier.GetComponent<Barrier>().enabled = false;

    }


}
