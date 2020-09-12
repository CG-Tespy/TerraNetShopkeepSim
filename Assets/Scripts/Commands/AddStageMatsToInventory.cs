using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Inventory",
                 "Add Stage Mats to Inventory",
                 @"Adds a copy of the specified stages' mats to the specified inventory.")]
[AddComponentMenu("")]
public class AddStageMatsToInventory : Command
{
    [SerializeField] StageData[] stages;

    [SerializeField] ShopInventoryData inventory;

    public override void OnEnter()
    {
        base.OnEnter();

        AddTheMats();

        Continue();
    }

    protected virtual void AddTheMats()
    {
        var actualInventory = inventory.Value;
        var items = actualInventory.Items;

        foreach (Stage currentStage in stages)
            foreach (var mat in currentStage.MatsGatherable)
                items.Add(mat);
    }
}
