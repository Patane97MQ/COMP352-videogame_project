using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour {

    [HideInInspector]
    public bool interacting = false;

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

    private void Update()
    {
        if (!interactables.Contains(current) && current != null)
        {
            current.DeInteract(this);
            current = null;
        }
        if (interactables.Count > 0)
        {
            if (Input.GetKey("e"))
            {
                interacting = true;
                if (current == null)
                    current = interactables[0];
                current.Interact(this);
                return;
            }
            else
            {
                if (current != null)
                    current.DeInteract(this);
                current = null;
            }
        }
        interacting = false;
    }
}
