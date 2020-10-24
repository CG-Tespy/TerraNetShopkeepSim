using UnityEngine;
using System.Collections.Generic;
using Fungus;

[CommandInfo("Shopkeep/Customer", 
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
            IList<Item> potentialBuys = ItemsAppealingTo(customerEl);
            Item toBuy = FirstAffordableItemIn(potentialBuys, customerEl);
            customerEl.WantedItem = toBuy;
            availableToBuy.Remove(toBuy); // So no two Customers go after the same item
        }
    }

    protected virtual IList<Item> ItemsAppealingTo(Customer customer)
    {
        List<Item> appealingToCust = new List<Item>();

        ObjectCollection customerTastes = customer.ItemPrefs as ObjectCollection;

        for (int i = 0; i < availableToBuy.Count; i++)
        {
            Item onSale = availableToBuy[i];

            if (ItemFitsCustomerTastes(onSale, customerTastes))
                appealingToCust.Add(onSale);
        }

        return appealingToCust;
    }

    protected virtual bool ItemFitsCustomerTastes(Item item, ObjectCollection customerTastes)
    {
        for (int i = 0; i < customerTastes.Count; i++)
        {
            var taste = (EnumScriptableObject) customerTastes[i];
            if (item.Classes.Contains(taste))
                return true;
        }

        return false;
    }
    
    protected virtual Item FirstAffordableItemIn(IList<Item> options, Customer customer)
    {
        for (int i = 0; i < options.Count; i++)
        {
            Item item = options[i];
            if (customer.CanAfford(item))
                return item;
        }

        return null;
    }
    
}
