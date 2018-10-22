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
        
        if (iGameObject.transform.position.x + iCollider.bounds.extents.x < gameObject.transform.position.x - collider.bounds.extents.x
            || iGameObject.transform.position.x - iCollider.bounds.extents.x > gameObject.transform.position.x + collider.bounds.extents.x)
        {
            
            Physics thisPhysics = gameObject.GetComponent<Physics>();

<<<<<<< HEAD:Assets/code/Interact/Pull.cs
            float pMovementVelX = pMovement.velocity.x;

            // Simply saves the X direction of the pMovement object to this object (eg. of this object is to the left, direction will be -1)
            int direction = Math.Sign(thisPhysics.GetTransform().position.x - pMovement.GetTransform().position.x);

            // If pMovement is moving towards the object, do nothing.
            if (Math.Sign(pMovementVelX) == direction)
                return;

            pMovement.flipSpriteX = false;

=======
            float velocityx = pMovement.velocity.x;
            //if()
>>>>>>> 769ff3ef155d02ff39c38748caffb8e7f654d20e:Assets/code/Pull.cs
            pMovement.SetVelocity(new Vector2(0, pMovement.velocity.y));
            pMovement.AddVelocity(new Vector2(thisPhysics.CalculateForce(pMovement, pMovementVelX), 0));
            
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
