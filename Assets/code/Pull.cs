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

            float x = thisPhysics.AddRawForceX(pMovement, pMovement.velocity.x*1.5f);

            pMovement.SetVelocity(new Vector2(pMovement.velocity.x, pMovement.velocity.y));
            //pMovement.AddRawForceX(thisPhysics, -Mathf.Sign(pMovement.velocity.x)*(x / thisPhysics.weight));

            //thisPhysics.SetParent(pMovement);
            //thisPhysics.useParentX = true;

            //thisPhysics.AddRawForceX(iPhysics, iPhysics.velocity.x);
            //float xOffset = gameObject.transform.position.x - iGameObject.transform.position.x;
            //gameObject.transform.position = new Vector2(iGameObject.transform.position.x + xOffset, gameObject.transform.position.y);
            //gameObject.transform.SetParent(iGameObject.transform);
            //Debug.Log("ThisPhysics: X="+thisPhysics.velocity.x + ", Y="+thisPhysics.velocity.y);
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
