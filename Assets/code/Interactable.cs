using UnityEngine;

public abstract class Interactable : MonoBehaviour {

    public abstract void Interact(Interactor interactor);

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Interactor interactor = collision.gameObject.GetComponent<Interactor>();
        if (interactor == null)
            return;

    }
}
