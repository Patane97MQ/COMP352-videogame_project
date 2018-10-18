using UnityEngine;

public class Acid : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Utilities.Destroy(collision.collider.gameObject);
    }
}
