using UnityEngine;

public abstract class Activating : MonoBehaviour {

    public bool active = false;

    public ColourHandler colourHandler;

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

    public bool isActive()
    {
        return active;
    }


    void OnActivated(Activator activator)
    {
        CheckForActivation();
    }
    void OnDeactivated(Activator activator)
    {
        CheckForActivation();
    }

    void CheckForActivation()
    {
        bool currentState = active;

        active = ActiveColoursCheck();

        // If the 'active' state has changed, update the object but running Activate() or Deactivate() respectively
        if (currentState != active)
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

    protected abstract void Activate();
    protected abstract void DeActivate();
}
public enum ActivatingType
{
    Once, Constant, Toggle
}