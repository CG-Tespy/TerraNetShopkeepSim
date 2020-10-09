using UnityEngine;
using Fungus;

[CommandInfo("Variable", "Set Object Variable", "Sets the value of one ObjectVariable to another.")]
public class SetObjectVariable : Command
{
    [VariableProperty(typeof(ObjectVariable))]
    [SerializeField] ObjectVariable source = null;

    [VariableProperty(typeof(ObjectVariable))]
    [SerializeField] ObjectVariable target = null;

    public override void OnEnter()
    {
        base.OnEnter();
        target.Value = source.Value;
        Continue();
    }
}
