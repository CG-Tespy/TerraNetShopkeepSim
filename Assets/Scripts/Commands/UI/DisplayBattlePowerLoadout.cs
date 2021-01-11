using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/UI",
    "Display Battle Power Loadout",
    @"Displays the specified battle power loadout with the specified UI elements. 
If the loadout is already being displayed,
this refreshes the display.")]
[AddComponentMenu("")]
public class DisplayBattlePowerLoadout : Command
{
    [SerializeField] ObjectData loadout;
    [SerializeField] BattlePowerDisplayHub displayPrefab = null;
    [Tooltip("The part of the UI that will hold instances of the display prefab.")]
    [SerializeField] RectTransform displayHolder = null;

    public override void OnEnter()
    {
        base.OnEnter();

        ClearContents();
        PopulateContents();

        Continue();
    }

    protected virtual void ClearContents()
    {
        Transform[] contents = displayHolder.GetChildren();

        displayHolder.DetachChildren();

        foreach (var child in contents)
            Destroy(child.gameObject);
    }

    protected virtual void PopulateContents()
    {
        BattlePowerLoadout loadout = this.loadout.Value as BattlePowerLoadout;

        foreach (var power in loadout.Contents)
        {
            var newDisplay = Instantiate(displayPrefab, displayHolder);
            newDisplay.DisplayBase = power;
        }
    }
}
