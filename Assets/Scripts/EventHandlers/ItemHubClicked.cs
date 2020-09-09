using UnityEngine;
using Fungus;

[EventHandlerInfo("Shopkeep", "Item Hub Clicked",
    @"An event for when an Item Display Hub component is clicked.
The itemVar property will be holding the item that the clicked hub was displaying for.")]
public class ItemHubClicked : EventHandler
{
    [VariableProperty("<Value>", typeof(ItemVariable))]
    [SerializeField] ItemVariable itemVar = null;

    [Tooltip("This event only fires if the hub clicked is directly parented to any of these holders. If this array is empty, this responds to any and all hubs.")]
    [SerializeField] Transform[] hubHolders = null;

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
        ItemDisplayHub itemHub = hub as ItemDisplayHub;

        if (itemHub == null || !ShouldRespondToHub(itemHub))
            return;

        if (itemVar != null)
            itemVar.Value = itemHub.DisplayBase;

        ExecuteBlock();
    }

    bool ShouldRespondToHub(ItemDisplayHub hub)
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

    bool RespondToAllHubs {  get { return this.hubHolders.Length == 0; } }
}
