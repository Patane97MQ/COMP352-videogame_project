﻿using UnityEngine;

public abstract class AbstractActivator : MonoBehaviour {

    public ActivatorType activatorType = ActivatorType.Constant;

    public delegate void ActivateAction(AbstractActivator activator);
    public static event ActivateAction OnActivated;
    public static event ActivateAction OnDeactivated;

    protected bool activated = false;
    protected bool activatedOnce = false;

    // Returns true if the activation state has changed, false otherwise.
    public bool SetActivated(bool activated)
    {
        bool before = this.activated;
        if (activated)
            this.activated = ActivateHandler(true);
        else
            this.activated = ActivateHandler(false);

        if (!before && this.activated)
        {
            BeforeActivatedSent();
            try { OnActivated(this); }
            catch { }
            return true;
        }
        else if (before && !this.activated)
        {
            BeforeDeactivatedSent();
            try { OnDeactivated(this); }
            catch { }
            return true;
        }
        return false;
    }

    protected abstract void BeforeActivatedSent();

    protected abstract void BeforeDeactivatedSent();

    // Decides how the object should be activated depending on the type. Eg. Toggle chooses to either Toggle On (active) or Off (not active)
    protected bool ActivateHandler(bool activated)
    {
        switch (activatorType)
        {
            case ActivatorType.Constant:
                return activated;

            case ActivatorType.Toggle:
                if (activated)
                    return !this.activated;
                return this.activated;

            case ActivatorType.Once:
                if (activatedOnce)
                    return this.activated;
                activatedOnce = true;
                return activated;
        }
        return activated;
    }
    public bool Activated()
    {
        return activated;
    }
}
//public enum ActivatorType
//{
//    Constant, Toggle, Once
//}
