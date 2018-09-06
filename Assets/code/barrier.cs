using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrier : MonoBehaviour {
    
    public List<GameObject> activatorsList;
    // Use this for initialization
     
    public bool allActive;


    void Start() {
        allActive = false;
        

}
	// Update is called once per frame
	void Update () {

        foreach(GameObject activator in activatorsList){
            buttonScript temp = (buttonScript) activator.GetComponent(typeof(buttonScript));
            Debug.Log(temp.amountActive());
            if (temp.active())
            {
                if (temp.amountActive() == activatorsList.Count)
                    allActive = true;
            }                    
                if(allActive){
                    Destroy(gameObject);
            }
                

        }
       
    }

}
