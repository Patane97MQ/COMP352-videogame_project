using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    Collider2D triggerCol;

    public abstract void Interact(Interactor interactor);
    public abstract void DeInteract(Interactor interactor);

    public void Awake()
    {
        foreach(Collider2D col in gameObject.GetComponents<Collider2D>())
        {
            if (col.isTrigger)
            {
                triggerCol = col;
                break;
            }
        }
    }

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
    public void OnTriggerStay2D(Collider2D collision)
    {
        Interactor interactor = collision.gameObject.GetComponent<Interactor>();
        if (interactor == null)
            return;
        Color interactingColor = (interactor.interacting ? Color.cyan : Color.magenta);
        Utilities.DrawBox(gameObject.GetComponent<Transform>().position, new Vector2(triggerCol.bounds.size.x, triggerCol.bounds.size.y), interactingColor);
    }
}
