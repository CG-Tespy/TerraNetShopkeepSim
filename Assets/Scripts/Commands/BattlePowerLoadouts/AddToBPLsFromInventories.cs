using System.Collections.Generic;
using UnityEngine;
using Fungus;


[CommandInfo("Shopkeep/Inventory",
    "Add to BPLs From Inventories",
    @"Adds all Battle Powers from the specified inventories to the specified Battle Power Loadouts.")]
[AddComponentMenu("")]
public class AddToBPLsFromInventories : Command
{
    [SerializeField] protected ShopInventoryData[] inventories = { };
    [SerializeField] protected ObjectData[] loadouts = { };

    public override void OnEnter()
    {
        base.OnEnter();

        foreach (ShopInventory sourceInv in inventories)
        {
            foreach (Object loadoutObj in loadouts)
            {
                BattlePowerLoadout actualLoadout = loadoutObj as BattlePowerLoadout;
                CopyPowersToInventory(sourceInv, actualLoadout);
            }
        }

        Continue();
    }

    /// <summary>
    /// Well, more like add references to the originals to the target inv.
    /// </summary>
    protected virtual void CopyPowersToInventory(ShopInventory sourceInv, BattlePowerLoadout targetLoadout)
    {
        IList<BattlePower> powersInSource = CollectionSOUtil.GetAllOfSubtype<Item, BattlePower>(sourceInv.Items);

        foreach (var power in powersInSource)
        {
            targetLoadout.Add(power);
        }
    }

}
