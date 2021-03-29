using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Battle", 
    "Enemy Drop Items", 
    "Creates drops based on the enemy details, adding them to the appropriate inventories.")]
public class DropItems : Command
{
    [SerializeField] protected ObjectData enemyType;
    [SerializeField] protected ShopInventoryData[] toAddDropsTo;

    public override void OnEnter()
    {
        base.OnEnter();
        this.Reset();
        GenerateDrops();
        AddDropsToInventories();
        Continue();
    }

    protected virtual void Reset()
    {
        drops.Clear(); // So we won't add drops from a previous run of this command
    }

    protected IList<Item> drops = new List<Item>();

    protected virtual void GenerateDrops()
    {
        EnemyType enemyType = this.enemyType.Value as EnemyType;
        IList<Item> potentialDrops = enemyType.MatsDropped;
        IList<float> dropChances = enemyType.DropChances;

        // We assume that dropChances has as many elements as potentialDrops, since
        // in an index-based basis, the drops should have a chance associated with them
        for (int i = 0; i < potentialDrops.Count; i++)
        {
            Item currentDrop = potentialDrops[i];
            float currentChance = dropChances[i];

            float randNum = Random.Range(noChance, guaranteed);
            if (currentChance >= randNum)
                drops.Add(currentDrop);
        }
    }

    protected static int noChance = 0, guaranteed = 100;

    protected virtual void AddDropsToInventories()
    {
        foreach (ShopInventory inv in toAddDropsTo)
        {
            inv.AddRange(drops);
        }
    }


}
