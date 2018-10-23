using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    public abstract void Interact(Interactor interactor);
    public abstract void DeInteract(Interactor interactor);

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Interactor interactor = collision.gameObject.GetComponent<Interactor>();
        if (interactor == null)
            return;
        interactor.AddInteractable(this);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Interactor interactor = collision.gameObject.GetComponent<Interactor>();
        if (interactor == null)
            return;
        interactor.RemoveInteractable(this);
    }
}
