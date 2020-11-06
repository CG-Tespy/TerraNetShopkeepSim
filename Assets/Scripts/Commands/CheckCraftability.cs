using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Variable", "Check Craftability", 
    "Sets a Boolean Variable based on whether a Crafting System can craft a certain recipe.)")]
public class CheckCraftability : Command
{
    [SerializeField] protected CraftingSystem craftingSystem;
    [SerializeField] ItemData recipe;
    [VariableProperty(typeof(BooleanVariable))]
    [SerializeField] BooleanVariable boolVar;

    public override void OnEnter()
    {
        base.OnEnter();

        boolVar.Value = craftingSystem.CanCraft(recipe.Value as Recipe);

        Continue();
    }
}
