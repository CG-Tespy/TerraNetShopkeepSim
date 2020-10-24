using System.Collections.Generic;
using UnityEngine;
using Fungus;
using System.Linq;

[CommandInfo("Shopkeep/Customer", 
    "Get Item Fitting Prefs", 
    "Randomly picks an item fitting the specified item prefs from the specified inventory, and assigns it to the specified object.")]
public class GetItemFittingPrefs : Command
{
    [SerializeField] ShopInventoryData inventory;
    [Tooltip("The item will be assigned to this.")]
    [SerializeField] ObjectData outValue;
    [Tooltip("Object Collection with the prefs.")]
    [SerializeField] CollectionData prefs;

    public override void OnEnter()
    {
        base.OnEnter();

        Item item = PickRandomItem();
        outValue.Value = item;

        Continue();
    }

    protected virtual Item PickRandomItem()
    {
        Item randItem = null;
        var prefs = this.prefs.Value as ObjectCollection;
        ISet<Item> items = GetItemsThatFitAny(prefs);

        if (items.Count > 0)
        {
            int randIndex = Random.Range(0, items.Count - 1);
            randItem = items.ElementAt(randIndex);
        }

        return randItem;
    }

    protected virtual ISet<Item> GetItemsThatFitAny(ObjectCollection prefs)
    {
        ISet<Item> items = new HashSet<Item>();

        for (int i = 0; i < prefs.Count; i++)
        {
            Object prefToCheckFor = prefs[i] as Object;
            ISet<Item> itemsFittingThisPref = ItemsFitting(prefToCheckFor);
            items.AddRange(itemsFittingThisPref);
        }

        return items;

    }

    /// <summary>
    /// Returns all items in the inventory that match the given pref.
    /// </summary>
    protected virtual ISet<Item> ItemsFitting(Object pref)
    {
        ISet<Item> result = new HashSet<Item>();
        var items = inventory.Value.Items;

        for (int i = 0; i < items.Count; i++)
        {
            var itemEl = items[i];
            if (ItemFitsPref(itemEl, pref))
                result.Add(itemEl);
        }

        return result;
    }

    protected virtual bool ItemFitsPref(Item item, Object prefToCheckFor)
    {
        var prefsItemFits = item.Classes;
        return prefsItemFits.Contains(prefToCheckFor);
    }
}
