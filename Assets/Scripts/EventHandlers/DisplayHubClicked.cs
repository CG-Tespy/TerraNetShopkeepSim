using UnityEngine;
using Fungus;

public abstract class DisplayHubClicked<THub, TDisplayBase> : EventHandler where THub: DisplayHub<TDisplayBase>
{
    [Tooltip("The variable what will hold the value of what the display hub was displaying for.")]
    [VariableProperty("<Value>", typeof(ObjectVariable))]
    [SerializeField] protected ObjectVariable objVar = null;

    [Tooltip("This event only fires if the hub clicked is directly parented to any of these holders. If this array is empty, this responds to any and all hubs.")]
    [SerializeField] protected Transform[] hubHolders = null;

    protected virtual void Awake()
    {
        DisplayHub.AnyClicked.AddListener(OnAnyDisplayHubClicked);
    }

    protected virtual void OnDestroy()
    {
        DisplayHub.AnyClicked.RemoveListener(OnAnyDisplayHubClicked);
    }

    protected virtual void OnAnyDisplayHubClicked(IDisplayHub hub)
    {
        THub powerHub = hub as THub;

        if (powerHub == null || !ShouldRespondToHub(powerHub))
            return;

        if (objVar != null)
            objVar.Value = (dynamic)powerHub.DisplayBase;

        ExecuteBlock();
    }

    bool ShouldRespondToHub(THub hub)
    {
        if (RespondToAllHubs)
            return true;

        foreach (var holder in hubHolders)
        {
            var holderChildren = holder.GetChildren();
            foreach (var holderChild in holderChildren)
                if (holderChild == hub.transform)
                    return true;
        }

        return false;
    }

    bool RespondToAllHubs { get { return this.hubHolders.Length == 0; } }
}
