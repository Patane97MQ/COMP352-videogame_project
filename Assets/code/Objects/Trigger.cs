using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    public Activating triggering;
    public List<string> activateTags;

    // Use this for initialization
    void Start ()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (activateTags.Count == 0 || activateTags.Count != 0 && activateTags.Contains(collision.gameObject.tag))
        {
            triggering.GetComponent<Activating>().DeActivate();

            // Disabling activating script
            triggering.GetComponent<Activating>().enabled = false;
        }
    }
}
