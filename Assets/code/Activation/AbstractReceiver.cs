using System;
using UnityEngine;

public abstract class AbstractReceiver : MonoBehaviour {

    public StartingState startingState = StartingState.AUTO;
    public bool inverted = false;

    private bool active = false;

    protected void Start()
    {
        switch (startingState)
        {
            case StartingState.AUTO:
                active = inverted;
                CheckForActivation();
                break;
            case StartingState.ACTIVE:
                active = true;
                UpdateActivation();
                break;
            case StartingState.DEACTIVE:
                active = false;
                UpdateActivation();
                break;
        }
    }
    void OnEnable()
    {
        AbstractActivator.OnActivated += OnActivated;
        AbstractActivator.OnDeactivated += OnDeactivated;
    }
    void OnDisable()
    {
        AbstractActivator.OnActivated -= OnActivated;
        AbstractActivator.OnDeactivated -= OnDeactivated;
    }

    public bool IsActive()
    {
        return active;
    }

    protected abstract void OnActivated(AbstractActivator activator);
    protected abstract void OnDeactivated(AbstractActivator activator);
    
    void UpdateActivation()
    {
        if (active)
            Activate();
        else
            DeActivate();
    }

    void CheckForActivation(ColourEnum colour)
    {
        bool currentState = active;

        try
        {
            active = ColourCheck();
        if (inverted)
            active = !active;

        // If the 'active' state has changed, update the object but running Activate() or Deactivate() respectively
        if (currentState != active)
            UpdateActivation();
        }
        catch (Exception e)
        {
            Debug.Log("Colour Exception 1: " + e.Message);
            
        }
    }


    // Checks ALL colours. Generally used in the 'AUTO' selection for StartingState
    void CheckForActivation()
    {
        try
        {
            active = ColourCheck();
            if (inverted)
                active = !active;

            UpdateActivation();
        } catch (InvalidOperationException)
        {
            Debug.Log("Colour Exception 2");
        }
    }

    
    private bool ColourCheck()
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
public enum StartingState
{
    AUTO, ACTIVE, DEACTIVE
}