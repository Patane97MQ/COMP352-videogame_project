using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrier : MonoBehaviour {

    // Use this for initialization
    void Start() {
      

    }
	// Update is called once per frame
	void Update () {

            if (Globals.buttonPressed == true)
            {
            Destroy(gameObject);
        }
        }

    }
