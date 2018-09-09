using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrier : MonoBehaviour {
    
    public List<GameObject> activatorsList;
    // Use this for initialization
     
    public bool allActive;
    public int counter = 0;


    void Start() {
        allActive = false;
        

}
	// Update is called once per frame
	void Update () {

        foreach(GameObject activator in activatorsList){
            buttonScript temp = (buttonScript) activator.GetComponent(typeof(buttonScript));
            if (temp.active() == true)
            {
                counter++;
                if (counter == 2)
                {
                    Destroy(gameObject);
                }
            } 
            
        }
       
    }

}
