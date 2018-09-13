using UnityEngine;
using System.Collections.Generic;

public abstract class Activator : MonoBehaviour {

    public ActivatorType activatorType = ActivatorType.Constant;

    public List<string> activateTags;

    public Sprite deactivatedImage;
    public Sprite activatedImage;


    private SpriteRenderer sRender;
    public delegate void ActivateAction(Activator activator);
    public static event ActivateAction OnActivated;
    public static event ActivateAction OnDeactivated;

    protected bool activated = false;
    protected int counter = 0;


    // Use this for initialization
    void Start ()
    {
        sRender = GetComponent<SpriteRenderer>();
    }
    protected void SetActivated(bool activated)
    {

        if (activated)
        {
            this.activated = ActivateHandler(true);
            counter++;
            sRender.sprite = activatedImage;
            try { OnActivated(this); }
            catch { }
            
        }
        else
        {
            this.activated = ActivateHandler(false);
            sRender.sprite = deactivatedImage;
            try { OnDeactivated(this); }
            catch { }
        }

    }
    // Decides how the object should be activated depending on the type. Eg. Toggle chooses to either Toggle On (active) or Off (not active)
    private bool ActivateHandler(bool activated)
    {
        switch (activatorType)
        {
            case ActivatorType.Constant:
                return activated;

            case ActivatorType.Toggle:
                if (activated)
                    return !this.activated;
                return this.activated;
        }
        return activated;

    }
    public bool Activated()
    {
        return activated;
    }
    public int TimesPressed()
    {
        return counter;
    }
}
public enum ActivatorType
{
    Constant, Toggle
}
