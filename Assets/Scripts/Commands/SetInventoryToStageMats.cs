using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep",
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

        foreach (var itemDesign in stage.MatsGatherable)
        {
            var newItem = Item.From(itemDesign);
            inventory.Value.Items.Add(newItem);
        }
    }
}
