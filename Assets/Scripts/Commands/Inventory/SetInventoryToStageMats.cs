using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Inventory",
                 "Set Inventory to Stage Mats",
                 @"Sets the inventory contents to that of the stage's mats.")]
[AddComponentMenu("")]
public class SetInventoryToStageMats : Command
{
    [VariableProperty(typeof(ShopInventoryVariable))]
    [SerializeField] ShopInventoryVariable inventory = null;
    [VariableProperty("<Value>", typeof(StageVariable))]
    [SerializeField] StageVariable stageWithMats = null;

    public override void OnEnter()
    {
        base.OnEnter();

        inventory.Value.Items.Clear();
        PopulateInventory();

        Continue();
    }

    protected virtual void PopulateInventory()
    {
        Stage stage = stageWithMats.Value;
        var items = inventory.Value.Items;

        foreach (var item in stage.MatsGatherable)
        {
            items.Add(item);
        }
    }
}
