using System;
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

        if (iGameObject.transform.position.x + iCollider.bounds.extents.x < gameObject.transform.position.x - collider.bounds.extents.x && pMovement.facingRight)
        {
            PullObject(pMovement, true);
        }
        else if (iGameObject.transform.position.x - iCollider.bounds.extents.x > gameObject.transform.position.x + collider.bounds.extents.x && !pMovement.facingRight)
        {
            PullObject(pMovement, false);
        }
    }
    private void PullObject(PlayerMovement pMovement, bool pMovementToLeft)
    {
        pMovement.pulling = true;
        Physics thisPhysics = gameObject.GetComponent<Physics>();

        float pMovementVelX = pMovement.velocity.x;
        pMovement.allowFlipX = false;

        // Simply saves the X direction of the pMovement object to this object (eg. of this object is to the left, direction will be -1)
        int direction = Math.Sign(thisPhysics.GetTransform().position.x - pMovement.GetTransform().position.x);


        // If pMovement is moving towards the object, do nothing.
        if (Math.Sign(pMovementVelX) == direction)
            return;

        //pMovement.SetVelocity(new Vector2(0, pMovement.velocity.y));
        float x = thisPhysics.CalculateForce(pMovement, pMovementVelX);
        //pMovement.AddVelocity(new Vector2(x, 0));
        pMovement.SetVelocity(new Vector2(x, pMovement.velocity.y));

        thisPhysics.SetVelocity(new Vector2(x, thisPhysics.velocity.y));
        Debug.Log(thisPhysics.velocity.x);
        //thisPhysics.AddRawForceX(pMovement, pMovement.velocity.x);
    }
    public override void DeInteract(Interactor interactor)
    {
        GameObject iGameObject = interactor.gameObject;
        PlayerMovement pMovement = iGameObject.GetComponent<PlayerMovement>();
        if (pMovement == null)
            return;
        pMovement.pulling = false;
    }
}
