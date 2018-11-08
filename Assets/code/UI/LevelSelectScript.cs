using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SelectLevel()
    {
        int y;

        string x = gameObject.name;

        Debug.Log(x);

        if (!Int32.TryParse(x, out y))
        {
          Debug.Log("Failed to load level of index: " + x);

            return; 
        }

        Debug.Log(y);

        Utilities.LoadScene(y);

        
    }
}
