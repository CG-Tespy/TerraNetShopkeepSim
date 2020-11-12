using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep", "Get Recipe Mats", "Adds the recipes' mats to the specified Collections and ShopInventories.")]
public class GetRecipeMats : Command
{
    [SerializeField] ItemData[] recipes;
    [SerializeField] CollectionData[] collections;
    [SerializeField] ShopInventoryData[] inventories;

    protected IList<Item> recipeMats = new List<Item>();
    protected IList<Object> collectionRecipeMats = new List<Object>();
    // ^ Can't AddRange an Item list into an Object Collection normally, hence this
    // workaround.

    public override void OnEnter()
    {
        base.OnEnter();

        GetAllRecipeMats();

        AddToCollections();
        AddToInventories();

        Continue();
    }

    protected virtual void GetAllRecipeMats()
    {
        recipeMats.Clear();
        collectionRecipeMats.Clear();
        // ^The same copy of this command may be run twice, and perhaps for testing, the user
        // may change the recipes' materials. Thus, we need to start with a clean slate every
        // time this command is run.

        foreach (Recipe recipeEl in recipes)
        {
            recipeMats.AddRange(recipeEl.Materials);

            foreach (Item material in recipeEl.Materials)
            {
                collectionRecipeMats.Add(material);
            }
        }
    }

    protected virtual void AddToCollections()
    {
        foreach (ObjectCollection collectionEl in collections)
            collectionEl.AddRange(collectionRecipeMats);
    }

    protected virtual void AddToInventories()
    {
        foreach (ShopInventory inventoryEl in inventories)
            inventoryEl.AddRange(recipeMats);
    }


}
