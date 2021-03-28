using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField] protected ShopInventory[] regularMats = null;
    [SerializeField] protected BattlePowerLoadout[] battlePowerMats = null;
    
    public Item Craft(Recipe recipe)
    {
        if (!CanCraft(recipe))
            return null;

        UseUpMatsRequiredBy(recipe);
        return recipe.ItemCreated;
    }


    /// <summary>
    /// So you can pass in a recipe from an Object variable in a Flowchart.
    /// </summary>
    public virtual bool CanCraft(Object hasRecipe)
    {
        if (hasRecipe is Recipe)
            return CanCraft(hasRecipe as Recipe);

        return false;
    }

    /// <summary>
    /// Returns true if the usable mat pool has the right stuff for the recipe.
    /// False otherwise.
    /// </summary>
    public virtual bool CanCraft(Recipe recipe)
    {
        if (recipe == null)
            return false;

        UpdatePlayerMats();

        // As the recipe can demand more than one of the same material, we need a separate
        // list so we can see if the mat pool has enough of said material.
        IList<Item> usableMats = new List<Item>(this.PlayerMats);

        foreach (Item recipeMat in recipe.Materials)
        {
            if (!usableMats.Contains(recipeMat))
                return false;

            usableMats.Remove(recipeMat);
        }

        return true;
    }

    protected virtual void UpdatePlayerMats()
    {
        playerMats.Clear();

        foreach (ShopInventory shopInventory in regularMats)
        {
            playerMats.AddRange(shopInventory.Items);
        }

        foreach (BattlePowerLoadout bpLoadout in battlePowerMats)
        {
            foreach (BattlePower power in bpLoadout.Contents)
            {
                playerMats.Add(power);
            }
        }
    }

    /// <summary>
    /// So you can pass in a recipe from an Object variable in a Flowchart.
    /// </summary>
    public virtual bool CanCraft(Item hasRecipe)
    {
        if (hasRecipe is Recipe)
            return CanCraft(hasRecipe as Recipe);

        return false;
    }

    protected IList<Item> PlayerMats
    {
        get { return playerMats; }
    }

    protected IList<Item> playerMats = new List<Item>();

    protected virtual void UseUpMatsRequiredBy(Recipe recipe)
    {
        IList<Item> materialsRemaining = new List<Item>(recipe.Materials);

        // Remove them from the individual inventories and loadouts
        foreach (ShopInventory shopInv in regularMats)
        {
            for (int i = 0; i < materialsRemaining.Count; i++)
            {
                Item materialToUse = materialsRemaining[i];
                bool successfulRemove = shopInv.Items.Remove(materialToUse);

                if (successfulRemove)
                {
                    materialsRemaining.Remove(materialToUse);
                    i--; // So we properly check the next ingredient
                }
            }
        }

        foreach (BattlePowerLoadout bpLoadout in battlePowerMats)
        {
            for (int i = 0; i < materialsRemaining.Count; i++)
            {
                Item materialToUse = materialsRemaining[i];
                bool successfulRemove = bpLoadout.Remove(materialToUse as BattlePower);

                if (successfulRemove)
                {
                    materialsRemaining.Remove(materialToUse);
                    i--;
                }
            }
        }

    }

}
