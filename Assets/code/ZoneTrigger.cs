using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTrigger : Activator {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("triggered!");
        SetActivated(false);

    }
}
