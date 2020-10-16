using UnityEngine;
using System.Collections.Generic;
using Fungus;

[CommandInfo("Shopkeep", 
    "Set Customer Target Items", 
    "Makes Customers in the passed transform decide what to buy from the passed catalog.")]
public class SetCustomerTargetItems : Command
{
    [SerializeField] protected Collection customers = null;
    [SerializeField] protected ShopInventory catalog = null;

    protected IList<Item> availableToBuy = new List<Item>();

    public override void OnEnter()
    {
        base.OnEnter();

        FetchAvailableItems();
        HaveCustomersPick();

        Continue();
    }


    protected virtual void FetchAvailableItems()
    {
        availableToBuy.AddRange(catalog.Items);
    }

    protected virtual void HaveCustomersPick()
    {
        for (int i = 0; i < customers.Count; i++)
        {
            Customer customerEl = (Customer) customers[i];
            Item itemDesired = GetItemFittingPrefsOf(customerEl);
            customerEl.WantedItem = itemDesired;
            availableToBuy.Remove(itemDesired); // So no two Customers go after the same item
        }
    }

    protected virtual Item GetItemFittingPrefsOf(Customer customer)
    {
        ObjectCollection customerPrefs = customer.ItemPrefs as ObjectCollection;
        
        for (int i = 0; i < availableToBuy.Count; i++)
        {
            Item onSale = availableToBuy[i];

            if (ItemFitsPrefs(onSale, customerPrefs))
                return onSale;
        }

        return null;
    }

    protected virtual bool ItemFitsPrefs(Item item, ObjectCollection customerPrefs)
    {
        for (int i = 0; i < customerPrefs.Count; i++)
        {
            var pref = (EnumScriptableObject) customerPrefs[i];
            if (item.Classes.Contains(pref))
                return true;
        }

        return false;
    }
}
