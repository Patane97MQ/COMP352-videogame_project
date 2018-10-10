using UnityEngine;

public abstract class Interactable : MonoBehaviour {

    //public float radius = 1f;

    public abstract void Interact(Interactor interactor);
    public abstract void DeInteract(Interactor interactor);

    //public void Update()
    //{
    //    RaycastHit2D hit = Utilities.BoxCastHandler(gameObject, gameObject.transform.position, gameObject.GetComponent<Collider2D>().bounds.size + new Vector3(radius, radius), 0f, Vector2.up, 1f);

    //    if (hit)
    //    {
    //        Interactor interactor = hit.collider.gameObject.GetComponent<Interactor>();
    //        if (interactor == null)
    //        {
    //            Debug.Log("null");
    //            return;
    //        }
    //        interactor.AddInteractable(this);
    //    }
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireCube(gameObject.transform.position, gameObject.GetComponent<Collider2D>().bounds.size + new Vector3(radius, radius));
    //}
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Interactor interactor = collision.gameObject.GetComponent<Interactor>();
        if (interactor == null)
            return;
        interactor.AddInteractable(this);
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        Interactor interactor = collision.gameObject.GetComponent<Interactor>();
        if (interactor == null)
            return;
        interactor.RemoveInteractable(this);
    }
}
