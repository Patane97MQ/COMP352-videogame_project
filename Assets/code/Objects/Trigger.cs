using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    public Activating triggering;
    public List<string> activateTags;
    public TriggerAction action = TriggerAction.PermDeactivate;
    public TriggerDirection direction = new TriggerDirection();

    // Use this for initialization
    void Start ()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (activateTags.Count == 0 || activateTags.Count != 0 && activateTags.Contains(collision.gameObject.tag))
        {
            if(direction.TriggerCheck(gameObject, collision.gameObject)) {
                if (triggering == null)
                    return;
                switch (action)
                {
                    case TriggerAction.PermActivate:
                        triggering.Activate();
                        triggering.enabled = false;
                        break;
                    case TriggerAction.PermDeactivate:
                        triggering.DeActivate();
                        triggering.enabled = false;
                        break;
                    case TriggerAction.Activate:
                        triggering.Activate();
                        break;
                    case TriggerAction.Deactivate:
                        triggering.DeActivate();
                        break;
                }
            }
        }
    }
    public enum TriggerAction
    {
        PermActivate, PermDeactivate, Activate, Deactivate
    }
    [System.Serializable]
    public class TriggerDirection
    {
        public bool up = true, down = true, left = true, right = true;

        public bool TriggerCheck(GameObject originalObject, GameObject otherObject)
        {
            if (up && otherObject.transform.position.y - otherObject.GetComponent<Collider2D>().bounds.extents.y >= originalObject.transform.position.y + originalObject.GetComponent<Collider2D>().bounds.extents.y)
            {
                return true;
            }
            if (down && otherObject.transform.position.y + otherObject.GetComponent<Collider2D>().bounds.extents.y <= originalObject.transform.position.y - originalObject.GetComponent<Collider2D>().bounds.extents.y)
            {
                return true;
            }
            if (left && otherObject.transform.position.x + otherObject.GetComponent<Collider2D>().bounds.extents.x <= originalObject.transform.position.x - originalObject.GetComponent<Collider2D>().bounds.extents.x)
            {
                return true;
            }
            if (right && otherObject.transform.position.x - otherObject.GetComponent<Collider2D>().bounds.extents.x >= originalObject.transform.position.x + originalObject.GetComponent<Collider2D>().bounds.extents.x)
            {
                return true;
            }
            return false;
        }
    }
}
