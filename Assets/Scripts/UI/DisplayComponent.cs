using UnityEngine;

public abstract class DisplayComponent<T> : MonoBehaviour
{
    /// <summary>
    /// What is to be displayed in some way.
    /// </summary>
    public virtual T DisplayBase
    {
        get => displayBase;
        set
        {
            displayBase = value;
            UpdateDisplay();
        }
    }

    T displayBase;

    /// <summary>
    /// Updates what this component displays, based on the display base.
    /// </summary>
    protected abstract void UpdateDisplay();
}
