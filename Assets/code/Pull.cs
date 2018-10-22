using UnityEngine;

[RequireComponent(typeof(Physics))]
public class Pull : Interactable{

    public override void Interact(Interactor interactor)
    {
        GameObject iGameObject = interactor.gameObject;
        PlayerMovement pMovement = iGameObject.GetComponent<PlayerMovement>();
        if (pMovement == null)
            return;

        Collider2D collider = gameObject.GetComponent<Collider2D>();
        Collider2D iCollider = iGameObject.GetComponent<Collider2D>();
        
        if (iGameObject.transform.position.x + iCollider.bounds.extents.x < gameObject.transform.position.x - collider.bounds.extents.x
            || iGameObject.transform.position.x - iCollider.bounds.extents.x > gameObject.transform.position.x + collider.bounds.extents.x)
        {
            
            Physics thisPhysics = gameObject.GetComponent<Physics>();

            float velocityx = pMovement.velocity.x;
            //if()
            pMovement.SetVelocity(new Vector2(0, pMovement.velocity.y));
            pMovement.AddVelocity(new Vector2(thisPhysics.CalculateForce(pMovement, velocityx), 0));
            
            thisPhysics.AddRawForceX(pMovement, pMovement.velocity.x);
        }
    }
    public override void DeInteract(Interactor interactor)
    {
        //Physics thisPhysics = gameObject.GetComponent<Physics>();

        //thisPhysics.ResetParent();
        //thisPhysics.useParentX = false;
        //GameObject iGameObject = interactor.gameObject;
        //if (iGameObject.transform.Equals(gameObject.transform.parent))
        //    gameObject.transform.SetParent(null);
    }
}
