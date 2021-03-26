using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Crafting", "Craft Item", "Crafts an item, setting the result to a variable.")]
public class CraftItem : Command
{
    [SerializeField] protected ItemData recipe;

    [VariableProperty(typeof(ItemVariable))]
    [SerializeField] protected ItemVariable itemVar = null;

    [Tooltip("If what's crafted is a Battle Power, it will be assigned to this var. Otherwise, the var will be set to null.")]
    [VariableProperty(typeof(ObjectVariable))]
    [SerializeField] protected ObjectVariable battlePowerVar;

    [Tooltip("Is set to true or false based on whether or not the crafted item is a battle power.")]
    [VariableProperty(typeof(BooleanVariable))]
    [SerializeField] protected BooleanVariable isBattlePower;

    [Tooltip("If this is empty, it will be set to one in the scene (if it exists).")]
    [SerializeField] protected CraftingSystem craftingSystem = null;

    public override void OnEnter()
    {
        base.OnEnter();

        FetchCraftingSystemAsNeeded();
        DoTheCrafting();
        AssignToVarsAsNeeded();

        Continue();
    }

    protected virtual void FetchCraftingSystemAsNeeded()
    {
        if (craftingSystem == null)
            craftingSystem = FindObjectOfType<CraftingSystem>();
    }

    protected virtual void DoTheCrafting()
    {
        craftResult = craftingSystem.Craft(recipe.Value as Recipe);
    }

    protected Item craftResult = null;

    protected virtual void AssignToVarsAsNeeded()
    {
        if (itemVar != null)
            itemVar.Value = craftResult;

        if (battlePowerVar != null)
            battlePowerVar.Value = craftResult as BattlePower;

        if (isBattlePower != null)
            isBattlePower.Value = battlePowerVar.Value != null;
    }
}
