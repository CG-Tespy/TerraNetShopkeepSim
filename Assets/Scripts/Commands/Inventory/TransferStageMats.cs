using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Inventory",
"Transfer Stage Mats",
"Lets you transfer stage mats to an inventory")]
public class TransferStageMats : Command
{
    [SerializeField] protected StageData[] stages = null;
    [SerializeField] protected ShopInventoryData inventoryData;

    public override void OnEnter()
    {
        base.OnEnter();

        ExecuteTransferral();

        Continue();
    }

    protected virtual void ExecuteTransferral()
    {
        var items = inventoryData.Value.Items;
        
        foreach (Stage stageEl in stages)
        {
            items.AddRange(stageEl.MatsGatherable);
        }
    }
}
