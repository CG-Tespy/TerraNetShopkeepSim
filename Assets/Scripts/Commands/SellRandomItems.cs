using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;


[CommandInfo("Shopkeep",
                 "Sell Random Items",
                 @"Sells random items in the specified inventory")]
[AddComponentMenu("")]
public class SellRandomItems : Command
{
    [SerializeField] ShopInventory inventory = null;
    [Tooltip("This value will be raised based on the sold items' prices.")]
    [SerializeField] IntegerData money;
    [SerializeField] int minAmountToSell = 1;
    [SerializeField] int maxAmountToSell = 2;
    IList<Item> Items { get { return inventory.Items; } }

    public override void OnEnter()
    {
        base.OnEnter();
        SellRandomly();

        Continue();
    }

    protected virtual void SellRandomly()
    {
        int amountToSell = DecideAmountToSell();

        for (int i = 0; i < amountToSell; i++)
        {
            var item = PickRandomItem();
            Sell(item);
        }
    }

    protected virtual int DecideAmountToSell()
    {
        // Need to make sure not to sell more than the inventory has
        int effMinAmount = Mathf.Min(minAmountToSell, Items.Count);
        int effMaxAmount = Mathf.Min(maxAmountToSell, Items.Count);
        int amount = Random.Range(effMinAmount, effMaxAmount);

        return amount;
    }

    protected virtual Item PickRandomItem()
    {
        int itemIndex = Random.Range(0, Items.Count);
        var item = Items[itemIndex];
        return item;
    }

    protected virtual void Sell(Item itemToSell)
    {
        Items.Remove(itemToSell);
        money.Value += itemToSell.Price;
    }
}
