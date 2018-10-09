using UnityEngine;
using System.Collections.Generic;

//public class Gravity_switch : Activator {

//    public Vector2 original;
//    public Direction direction = Direction.up;

//    public override void Activate()
//    {
//        switch (direction)
//        {
//            case Direction.up:
//                float mag = Physics2D.gravity.magnitude;
//                Physics2D.gravity = Vector2.up*mag;
//                ChangeSprite();
//                break;
//        }
//    }

//    public override void DeActivate()
//    {
//    }

//    private string GravityDirectionString()
//    {
//        Vector2 normalizedGrav = Physics2D.gravity.normalized;
//        if (normalizedGrav == Vector2.up)
//            return "up";
//        if (normalizedGrav == Vector2.down)
//            return "down";
//        if (normalizedGrav == Vector2.left)
//            return "left";
//        if (normalizedGrav == Vector2.right)
//            return "right";
//        return "unknown";
//    }

//    void ChangeSprite()
//    {
//        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("objects/gravity_switcher_" + GravityDirectionString(), typeof(Sprite)) as Sprite;
//    }
//    public enum Direction
//    {
//        up, down, left, right
//    }
//}
public class Gravity_switch : MonoBehaviour
{
    bool pressed = false;
    List<GameObject> pressing = new List<GameObject>();

    public Direction direction = Direction.Up;
    public ButtonSounds sounds = new ButtonSounds();
    private AudioSource source;

    private void Start()
    {
        ChangeSprite();
    }

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!pressing.Contains(collision.gameObject))
            pressing.Add(collision.gameObject);
        if (pressed == false && pressing.Count > 0)
        {
            switch (direction)
            {
                case Direction.Up:
                    Physics2D.gravity = Vector2.up * Physics2D.gravity.magnitude;
                    break;
                case Direction.Down:
                    Physics2D.gravity = Vector2.down * Physics2D.gravity.magnitude;
                    break;
            }
            pressed = true;
            ChangeSprite();
            source.PlayOneShot(sounds.soundTrigger);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (pressing.Contains(collision.gameObject))
            pressing.Remove(collision.gameObject);
        if (pressed == true && pressing.Count <= 0)
        {
            pressed = false;
            ChangeSprite();
        }
    }
    void ChangeSprite()
    {
        //gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("objects/" + colour + "/button_" + (activated ? "on" : "off") + (pressed ? "_pressed" : "") + ("_" + activatorType.ToString()), typeof(Sprite)) as Sprite;
    }

    [System.Serializable]
    public class ButtonSounds
    {
        public AudioClip soundTrigger;
    }
}