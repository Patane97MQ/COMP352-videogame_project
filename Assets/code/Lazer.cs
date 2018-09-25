using UnityEngine;

public class Lazer : MonoBehaviour {
    protected Collider2D c2D;

    void Start()
    {
        c2D = gameObject.GetComponent<Collider2D>();
    }
    private void Update()
    {
        RaycastHit2D laser = Physics2D.Raycast(transform.position + gameObject)
    }
}
