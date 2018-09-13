using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrier : MonoBehaviour {

    public ActivateType activateType;
    public bool allActive;

    public List<GameObject> activatorsList;
    // Use this for initialization


    void Start() {
        allActive = false;
        

}
	// Update is called once per frame
	void Update () {

        foreach(GameObject activator in activatorsList){
            buttonScript temp = (buttonScript) activator.GetComponent(typeof(buttonScript));
            // INCOMPLETE!!!
            if (temp.Active() == true)
            {
                Destroy(gameObject);
            } 
            
        }
       
    }

}
public enum ActivateType
{
    Once, Constant, Toggle
}
