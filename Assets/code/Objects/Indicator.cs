using UnityEngine;

public class Indicator : MonoBehaviour {

    //public Activator activator;
    public ColourEnum colour;
    public int amount = 1;

    void OnEnable()
    {
        Activator.OnActivated += OnActivated;
        Activator.OnDeactivated += OnDeactivated;
    }
    void OnDisable()
    {
        Activator.OnActivated -= OnActivated;
        Activator.OnDeactivated -= OnDeactivated;
    }
    private void Start()
    {
        ChangeSprite();
    }
    void OnActivated(Activator activator)
    {
        if (activator.colour == colour)
            ChangeSprite();

    }
    void OnDeactivated(Activator activator)
    {
        if (activator.colour == colour)
            ChangeSprite();
    }
    void ChangeSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("objects/coloured/" + colour + "/indicator_" + (ColourHandler.CountColour(colour) >= amount ? "on" : "off"), typeof(Sprite)) as Sprite;
    }
}
