using System;
using UnityEngine;

public abstract class Activating : MonoBehaviour {

    public StartingState startingState = StartingState.AUTO;
    public bool inverted = false;
    public ColourHandler colourHandler;

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
        CheckForActivation(activator.colour);
    }
    void OnDeactivated(Activator activator)
    {
        CheckForActivation(activator.colour);
    }

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
        bool currentState = active;
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