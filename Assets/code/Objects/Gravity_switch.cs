using UnityEngine;

public class Gravity_switch : Activating {

    public Vector2 original;
    public Direction direction = Direction.up;

    private bool toggle = false;

    public override void Activate()
    {
        if (toggle)
        {
            Debug.Log("Deactive");
            if (original != Vector2.zero)
                Physics2D.gravity = original;
            ChangeSprite();
            toggle = false;
            return;
        }
        toggle = true;
        Debug.Log("Active");
        original = Physics2D.gravity;
        switch (direction)
        {
            case Direction.up:
                float mag = Physics2D.gravity.magnitude;
                Physics2D.gravity = Vector2.up*mag;
                ChangeSprite();
                break;
        }
    }

    public override void DeActivate()
    {
    }

    private string GravityDirectionString()
    {
        Vector2 normalizedGrav = Physics2D.gravity.normalized;
        if (normalizedGrav == Vector2.up)
            return "up";
        if (normalizedGrav == Vector2.down)
            return "down";
        if (normalizedGrav == Vector2.left)
            return "left";
        if (normalizedGrav == Vector2.right)
            return "right";
        return "unknown";
    }

    void ChangeSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("objects/gravity_switcher_" + GravityDirectionString(), typeof(Sprite)) as Sprite;
    }
    public enum Direction
    {
        up, down, left, right
    }
}
