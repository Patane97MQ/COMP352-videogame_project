using UnityEngine;

public class Indicator : MonoBehaviour {

    public Activator activator;

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
        if (activator.Equals(this.activator))
            ChangeSprite();

    }
    void OnDeactivated(Activator activator)
    {
        if (activator.Equals(this.activator))
            ChangeSprite();
    }
    void ChangeSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("objects/coloured/" + activator.colour + "/indicator_" + (activator.Activated() ? "on" : "off"), typeof(Sprite)) as Sprite;
    }
}
