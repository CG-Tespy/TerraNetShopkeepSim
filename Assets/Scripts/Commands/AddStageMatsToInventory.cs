using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep",
                 "Add Stage Mats to Inventory",
                 @"Adds a copy of the specified stage's mats to the specified inventory.")]
[AddComponentMenu("")]
public class AddStageMatsToInventory : Command
{
    [SerializeField] StageData stage;

    [SerializeField] ShopInventoryData inventory;

    public override void OnEnter()
    {
        base.OnEnter();
        var stageMats = stage.Value.MatsGatherable;
        var inventoryItems = inventory.Value.Items;

        foreach (var mat in stageMats)
        {
            inventoryItems.Add(Item.From(mat));
        }

        Continue();
    }
}
