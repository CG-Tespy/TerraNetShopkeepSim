using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Inventory", 
    "AddBattlePowersToInventory", 
    "Copies all the battle powers in the source inventories to the target ones.")]
[AddComponentMenu("")]
public class AddBattlePowersToInventory : Command
{
    [SerializeField] protected ShopInventoryData[] sources = { };
    [SerializeField] protected ShopInventoryData[] targets = { };

    public override void OnEnter()
    {
        base.OnEnter();
        AddFromSourcesToTargets();
        Continue();
    }

    protected virtual void AddFromSourcesToTargets()
    {
        foreach (ShopInventory sourceInv in sources)
        {
            foreach (ShopInventory targetInv in targets)
            {
                CopyPowersToInventory(sourceInv, targetInv);
            }
        }
    }

    /// <summary>
    /// Well, more like add references to the originals to the target inv.
    /// </summary>
    protected virtual void CopyPowersToInventory(ShopInventory sourceInv, ShopInventory targetInv)
    {
        IList<BattlePower> powersInSource = CollectionSOUtil.GetAllOfSubtype<Item, BattlePower>(sourceInv.Items);

        foreach (var power in powersInSource)
        {
            targetInv.Add(power);
        }
    }

    
}
