using UnityEngine;


public class Button : Activator {

    

    void OnCollisionEnter2D(Collision2D collision)
    {   
        if((activateTags.Count == 0 && collision.gameObject.tag != "environment") || activateTags.Count != 0 && activateTags.Contains(collision.gameObject.tag))
            SetActivated(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((activateTags.Count == 0 && collision.gameObject.tag != "environment") || activateTags.Count != 0 && activateTags.Contains(collision.gameObject.tag))
            SetActivated(false);
    }
}
