﻿using UnityEngine;
using Fungus;

[EventHandlerInfo("Shopkeep", "Item Hub Clicked",
    @"An event for when an Item Display Hub component is clicked.
The itemVar property will be holding the item that the clicked hub was displaying for.")]
public class ItemHubClicked : EventHandler
{
    [VariableProperty("<Value>", typeof(ItemVariable))]
    [SerializeField] protected ItemVariable itemVar = null;

    [Tooltip("If the item is a Battle Power, it will be assigned to this variable.")]
    [VariableProperty("<Value>", typeof(ObjectVariable))]
    [SerializeField] protected ObjectVariable battlePower = null;

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
        ItemDisplayHub itemHub = hub as ItemDisplayHub;

        if (itemHub == null || !ShouldRespondToHub(itemHub))
            return;

        if (itemVar != null)
            itemVar.Value = itemHub.DisplayBase;
        if (battlePower != null)
            battlePower.Value = itemHub.DisplayBase as BattlePower;

        ExecuteBlock();
    }

    protected virtual bool ShouldRespondToHub(ItemDisplayHub hub)
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

    protected virtual bool RespondToAllHubs {  get { return this.hubHolders.Length == 0; } }
}
