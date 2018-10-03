using UnityEngine;

public class NextLevelDoor : Activating {
    
    //changed methods from protected to public as we were receiving a compiler error. Change back if required. 
     public override void Activate()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

      public override void DeActivate()
    { }
}
