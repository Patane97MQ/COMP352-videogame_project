using UnityEngine;

public class Pickup : Interactable {

    Transform originalParent;

    public override void Interact(Interactor interactor)
    {
        Debug.Log("Interact");
        if (Utilities.ToLeft(gameObject, interactor.gameObject) || Utilities.ToRight(gameObject, interactor.gameObject))
        {
            originalParent = gameObject.transform.parent;
            gameObject.transform.SetParent(interactor.gameObject.transform);
            gameObject.GetComponent<Physics>().enabled = false;
            //gameObject.GetComponent<Physics>().gravity = false;
            gameObject.transform.localPosition = new Vector2(.64f, 0);
        }
    }
    public override void DeInteract(Interactor interactor)
    {
        Debug.Log("Deinteract");
        gameObject.transform.SetParent(originalParent);
        gameObject.GetComponent<Physics>().enabled = true;
        //gameObject.GetComponent<Physics>().gravity = true;
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
