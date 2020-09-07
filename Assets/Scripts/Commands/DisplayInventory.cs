using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep",
                 "Display Inventory",
                 @"Displays the specified inventory with the specified UI elements. 
If the inventory is already being displayed,
this refreshes the display.")]
[AddComponentMenu("")]
public class DisplayInventory : Command
{
    [SerializeField] ShopInventory inventory = null;
    [SerializeField] ItemDisplayHub displayPrefab = null;
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
        foreach (var item in inventory.Items)
        {
            var newDisplay = Instantiate(displayPrefab, displayHolder);
            newDisplay.DisplayBase = item;
        }
    }
}
