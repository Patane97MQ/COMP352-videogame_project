using UnityEngine;

public class Button : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "red_clone")
        {
            Debug.Log("check");
        }
    }
}