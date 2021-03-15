using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Customer", "Add Customers To Collection",
    "Adds the Customers under the target holder to the target ObjectCollection.")]
public class AddCustomersToCollection : Command
{
    [SerializeField] CustomerManager customerManager = null;
    [SerializeField] CollectionData collection;

    public override void OnEnter()
    {
        base.OnEnter();

        IList<Customer> customers = customerManager.CustomersInShop;

        for (int i = 0; i < customers.Count; i++)
        {
            Customer customerEl = customers[i];
            collection.Value.Add(customerEl);
        }

        Continue();
    }
}
