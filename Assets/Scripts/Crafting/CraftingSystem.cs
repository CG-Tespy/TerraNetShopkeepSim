using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField] protected ShopInventory[] haveUsableMats = null;
    [SerializeField] protected BattlePowerLoadout[] battlePowerMats = null;
    
    public Item Craft(Recipe recipe)
    {
        if (!CanCraft(recipe))
            return null;

        UseUpMatsRequiredBy(recipe);
        return recipe.ItemCreated;
    }

    /// <summary>
    /// Returns true if the usable mat pool has the right stuff for the recipe.
    /// False otherwise.
    /// </summary>
    public virtual bool CanCraft(Recipe recipe)
    {
        if (recipe == null)
            return false;

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
        PlayerMats.RemoveRange(recipe.Materials);
    }

    public int DoThing(int thing)
    {
        return 0;
    }

    public Recipe OtherThing()
    {
        return null;
    }
}
