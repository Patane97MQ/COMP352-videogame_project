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

    public void selectLevel()
    {
        int y = 0;
        string x = gameObject.name;
        if (!Int32.TryParse(x, out y))
        {
            y = -1;
        }
        debug.log(y);
        return y;

        SceneManager.loadScene(y);
    }
}
