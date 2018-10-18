using UnityEngine;
using UnityEngine.SceneManagement;

public class Laser : Activating {
    public Direction direction = Direction.Up;
    public Color colour = Color.red;

    protected Collider2D c2D;
    protected LineRenderer lr;
    protected RaycastHit2D laserRay;
    protected Vector2 startPosition;
    protected Vector2 vectorDirection;
    GameObject hit;

    public override void Activate()
    {
        lr.enabled = true;
    }

    public override void DeActivate()
    {
        lr.enabled = false;
    }

    new void Start()
    {
        c2D = gameObject.GetComponent<Collider2D>();
        lr = gameObject.GetComponent<LineRenderer>();
        base.Start();
    }
    private void Update()
    {
        if (IsActive())
        {
            startPosition = (Vector2)transform.position + ((new Vector2(c2D.bounds.extents.x, c2D.bounds.extents.y) + new Vector2(0.001f, 0.001f)) * vectorDirection);
            switch (direction)
            {
                case Direction.Up:
                    vectorDirection = Vector2.up;
                    break;
                case Direction.Down:
                    vectorDirection = Vector2.down;
                    break;
                case Direction.Left:
                    vectorDirection = Vector2.left;
                    break;
                case Direction.Right:
                    vectorDirection = Vector2.right;
                    break;
                default:
                    break;
            }
            laserRay = Physics2D.Raycast(startPosition, vectorDirection);

            hit = laserRay.collider.gameObject;

            if(hit.tag != "environment") { 
}               Utilities.Destroy(hit);

            lr.SetPosition(0, startPosition);
            lr.SetPosition(1, startPosition + laserRay.distance * vectorDirection);
        }
    }
}
