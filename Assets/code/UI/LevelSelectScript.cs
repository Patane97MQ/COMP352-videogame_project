using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour {

    public int index;

	// Update is called once per frame
	void Update () {
		
	}

    public void SelectLevel(int x)
    {
      
        Utilities.LoadScene(x);

        
    }
}
