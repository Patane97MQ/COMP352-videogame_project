using UnityEngine;

public abstract class Activating : MonoBehaviour {


    public bool inverted = false;
    public ColourHandler colourHandler;

    private bool active = false;

    protected void Start()
    {
        active = inverted;
        CheckForActivation(true);
    }
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

    public bool IsActive()
    {
        return active;
    }

    void OnActivated(Activator activator)
    {
        CheckForActivation(false);
    }
    void OnDeactivated(Activator activator)
    {
        CheckForActivation(false);
    }

    void CheckForActivation(bool firstRun)
    {
        bool currentState = active;

        active = ActiveColoursCheck();
        if (inverted)
            active = !active;
        // If the 'active' state has changed, update the object but running Activate() or Deactivate() respectively
        if (currentState != active || firstRun)
        {
            if (active)
                Activate();
            else
                DeActivate();
        }
    }

    
    private bool ActiveColoursCheck()
    {
        return colourHandler.CheckAllColours();
    }

    public abstract void Activate();
    public abstract void DeActivate();
}
public enum ActivatingType
{
    Once, Constant, Toggle
}