using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep",
                 "Set Inventory to Stage Mats",
                 @"Sets the inventory contents to that of the stage's mats.")]
[AddComponentMenu("")]
public class SetInventoryToStageMats : Command
{
    [SerializeField] ShopInventory inventory = null;
    [VariableProperty("<Value>", typeof(StageVariable))]
    [SerializeField] StageVariable stageVar = null;

    public override void OnEnter()
    {
        base.OnEnter();

        inventory.Items.Clear();
        PopulateInventory();

        Continue();
    }

    protected virtual void PopulateInventory()
    {
        Stage stage = stageVar.Value;

        foreach (var itemDesign in stage.MatsGatherable)
        {
            var newItem = Item.From(itemDesign);
            inventory.Items.Add(newItem);
        }
    }
}
