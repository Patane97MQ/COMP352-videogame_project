using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour {

    private List<Interactable> interactables = new List<Interactable>();
    private Interactable current;

    public void AddInteractable(Interactable interactable)
    {
        if(!interactables.Contains(interactable))
            interactables.Add(interactable);
    }

    public void RemoveInteractable(Interactable interactable)
    {
        interactables.Remove(interactable);
    }

    public Interactable GetCurrentInteractable()
    {
        return interactables[0];
    }
	
    void OnGUI()
    {
        if (interactables.Count > 0)
        {
            Rect position = new Rect(interactables[0].gameObject.transform.position + new Vector3(0, 10f), new Vector2(5f, 5f));
            GUI.Label(position, "E");
        }
    }
    private void Update()
    {
        if (!interactables.Contains(current))
            current = null;
        if (interactables.Count > 0)
        {
            Utilities.DrawBox(interactables[0].GetComponent<Transform>().position, new Vector2(1f, 1f), Color.magenta);
            if (Input.GetKey("e"))
            {
                if (current == null)
                    current = interactables[0];
                current.Interact(this);
            }
            else
            {
                if (current != null)
                    current.DeInteract(this);
                current = null;
            }
        }
    }
}
