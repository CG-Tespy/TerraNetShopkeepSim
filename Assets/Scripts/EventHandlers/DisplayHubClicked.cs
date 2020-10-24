using UnityEngine;
using Fungus;
using System.Linq;

public enum HubClickResponse
{
    hubHolder,
    individual,
    hybrid
}

public abstract class DisplayHubClicked<THub, TDisplayBase> : EventHandler where THub: DisplayHub<TDisplayBase>
{
    [Tooltip("The variable what will hold the value of what the display hub was displaying for.")]
    [VariableProperty("<Value>", typeof(ObjectVariable))]
    [SerializeField] protected ObjectVariable objVar = null;

    [Tooltip("Decide what decides whether this event fires.")]
    [SerializeField] protected HubClickResponse responseType = HubClickResponse.hubHolder;

    [Tooltip("Which holders' hubs this can respond to the clicks of. If this array is empty, this responds to any and all hubs.")]
    [SerializeField] protected Transform[] hubHolders = null;

    [Tooltip("Which individual hubs this can respond to.")]
    [SerializeField] protected DisplayHub[] individuals;

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
        THub correctHub = hub as THub;

        if (correctHub == null || !ShouldRespondToHub(correctHub))
            return;

        AssignValuesToVarsFrom(correctHub);

        ExecuteBlock();
    }

    bool ShouldRespondToHub(THub hub)
    {
        if (RespondToAllHubs)
            return true;

        bool respondToHubsOnly = responseType == HubClickResponse.hubHolder;
        bool respondToIndivsOnly = responseType == HubClickResponse.individual;

        if (respondToHubsOnly)
            return HubIsInHolder(hub);
        else if (respondToIndivsOnly)
            return HubIsAmongIndividuals(hub);
        else
            return HubIsInHolder(hub) || HubIsAmongIndividuals(hub);
    }

    bool RespondToAllHubs 
    { 
        get 
        { 
            return (responseType == HubClickResponse.hubHolder || responseType == HubClickResponse.hybrid)
                && this.hubHolders.Length == 0;
        } 
    }

    bool HubIsInHolder(THub hub)
    {
        foreach (var holder in hubHolders)
        {
            var holderChildren = holder.GetChildren();
            foreach (var holderChild in holderChildren)
                if (holderChild == hub.transform)
                    return true;
        }

        return false;
    }

    bool HubIsAmongIndividuals(THub hub)
    {
        return individuals.Contains(hub);
    }

    protected virtual void AssignValuesToVarsFrom(THub hub)
    {
        if (objVar != null)
            objVar.Value = (dynamic)hub.DisplayBase;
    }
}
