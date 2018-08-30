using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_press : MonoBehaviour {

void OnCollisionEnter (Collision col){
	Debug.Log("check collider");
	if (col.gameObject.name == "Clone1"){
		Debug.Log("check");
		Destroy(col.gameObject);
	}
}


}



// using UnityEngine;
// using System.Collections;

// public class DestroyCubes : MonoBehaviour
// {
//     void OnCollisionEnter (Collision col)
//     {
//         if(col.gameObject.name == "prop_powerCube")
//         {
//             Destroy(col.gameObject);
//         }
//     }
// }