using UnityEngine;
using Fungus;

[CommandInfo("Variable", 
    "Toggle Boolean",
    "Reverses the state of a Boolean variable. If its false, it becomes true. If its true, it becomes false.")]
public class ToggleBoolean : Command
{
    [VariableProperty(typeof(BooleanVariable))]
    [SerializeField] protected BooleanVariable boolVar = null;

    public override void OnEnter()
    {
        base.OnEnter();
        boolVar.Value = !boolVar.Value;
        Continue();
    }
}

