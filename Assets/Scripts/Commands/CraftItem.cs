using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Crafting", "Craft Item", "Crafts an item, setting the result to a variable.")]
public class CraftItem : Command
{
    [SerializeField] protected ItemData recipe;
    [VariableProperty(typeof(ItemVariable))]
    [SerializeField] protected ItemVariable itemVar = null;
    [Tooltip("If this is empty, it will be set to one in the scene (if it exists).")]
    [SerializeField] protected CraftingSystem craftingSystem = null;

    public override void OnEnter()
    {
        base.OnEnter();

        if (craftingSystem == null)
            craftingSystem = FindObjectOfType<CraftingSystem>();

        itemVar.Value = craftingSystem.Craft(recipe.Value as Recipe);

        Continue();
    }
}
