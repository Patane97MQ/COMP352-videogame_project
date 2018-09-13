using System.Collections.Generic;
using UnityEngine;

public abstract class Activating : MonoBehaviour {

    public bool active = false;

    public ActivatingType activateType = ActivatingType.Once;
    public bool activeTogether = false;


    public List<Activator> activatorsList;

    void OnEnable()
    {
        Activator.OnActivated += OnActivated;
        Activator.OnDeactivated += OnDeactivated;
        try
        {
            GetActives();
        }
        catch
        {
            Debug.Log("Activating.cs disabled for '" + gameObject.name + "' object.\nFailed to build Activators List as some elements are null!");
            this.enabled = false;
        }
    }
    void OnDisable()
    {
        Activator.OnActivated -= OnActivated;
        Activator.OnDeactivated += OnDeactivated;
    }

    void OnActivated(Activator activator)
    {
        if (activatorsList.Contains(activator))
            CheckActives();
    }
    void OnDeactivated(Activator activator)
    {
        if (activatorsList.Contains(activator))
            CheckActives();
    }
    void CheckActives()
    {
        bool currentState = active;

        active = ActiveHandler();
        // If the 'active' state has changed, update the object but running Activate() or Deactivate() respectively
        if (currentState != active)
        {
            if (active)
                Activate();
            else
                DeActivate();
        }
    }
    private bool ActiveHandler()
    {
        List<bool> actives = GetActives();
        switch (activateType)
        {
            case ActivatingType.Once:

                if (!activeTogether && actives.Contains(true))
                    return true;
                else if (activeTogether && !actives.Contains(false))
                    return true;
                break;

            case ActivatingType.Constant:
                if (!activeTogether && actives.Contains(true))
                    return true;
                else if (activeTogether && !actives.Contains(false))
                    return true;
                else
                    return false;

            case ActivatingType.Toggle:
                if (!activeTogether && actives.Contains(true))
                    return !active;
                else if (activeTogether && !actives.Contains(false))
                    return !active;
                Debug.Log("toggle1");
                break;
        }
        return active;
    }
    private List<bool> GetActives()
    {
        List<bool> list = new List<bool>();
        foreach (Activator activator in activatorsList)
            list.Add(activator.Activated());

        return list;
    }
    protected abstract void Activate();
    protected abstract void DeActivate();
}
public enum ActivatingType
{
    Once, Constant, Toggle
}