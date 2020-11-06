using UnityEngine;

public abstract class DisplayComponent : MonoBehaviour
{
    /// <summary>
    /// What is to be displayed in some way.
    /// </summary>
    public virtual object DisplayBase
    {
        get => displayBase;
        set
        {
            displayBase = value;
            UpdateDisplay();
        }
    }

    object displayBase;

    /// <summary>
    /// Updates what this component displays, based on the display base.
    /// </summary>
    protected abstract void UpdateDisplay();
}

public abstract class DisplayComponent<T> : DisplayComponent
{
    /// <summary>
    /// What is to be displayed in some way.
    /// </summary>
    public new virtual T DisplayBase
    {
        get => (T) base.DisplayBase;
        set { base.DisplayBase = value; }
    }

}
