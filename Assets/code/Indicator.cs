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
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("objects/coloured/" + activator.colour + "/indicator/" + (activator.Activated() ? "on" : "off"), typeof(Sprite)) as Sprite;
    }
    void OnActivated(Activator activator)
    {
        if (activator.Equals(this.activator))
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("objects/coloured/" + activator.colour + "/indicator/" + (activator.Activated() ? "on" : "off"), typeof(Sprite)) as Sprite;

    }
    void OnDeactivated(Activator activator)
    {
        if (activator.Equals(this.activator))
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("objects/coloured/" + activator.colour + "/indicator/" + (activator.Activated() ? "on" : "off"), typeof(Sprite)) as Sprite;
    }
}
