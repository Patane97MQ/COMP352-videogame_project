using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    public Activating triggering;
    public List<string> activateTags;
    public TriggerAction action = TriggerAction.PermDeactivate;

    // Use this for initialization
    void Start ()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (activateTags.Count == 0 || activateTags.Count != 0 && activateTags.Contains(collision.gameObject.tag))
        {
            switch (action)
            {
                case TriggerAction.PermActivate:
                    triggering.GetComponent<Activating>().Activate();
                    triggering.GetComponent<Activating>().enabled = false;
                    break;
                case TriggerAction.PermDeactivate:
                    triggering.GetComponent<Activating>().DeActivate();
                    triggering.GetComponent<Activating>().enabled = false;
                    break;
                case TriggerAction.Activate:
                    triggering.GetComponent<Activating>().Activate();
                    break;
                case TriggerAction.Deactivate:
                    triggering.GetComponent<Activating>().DeActivate();
                    break;
            }
        }
    }
    public enum TriggerAction
    {
        PermActivate, PermDeactivate, Activate, Deactivate
    }
}
