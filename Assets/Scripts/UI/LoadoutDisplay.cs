using UnityEngine;

public abstract class LoadoutDisplay<TDisplayHub, TLoadout, TDisplayee> : MonoBehaviour
    where TDisplayHub: DisplayHub<TDisplayee>
{
    [Tooltip("What will hold the displays.")]
    [SerializeField] protected RectTransform displayHolder = null;
    [SerializeField] protected TDisplayHub hubPrefab = null;
    [SerializeField] protected TLoadout toDisplay;

    protected virtual void Start()
    {
        DisplayLoadout();
    }

    protected abstract void DisplayLoadout();
}
