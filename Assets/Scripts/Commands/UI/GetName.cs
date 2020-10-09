using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Battle",
                 "GetName",
                 @"Assigns the name of the specified value to the specified variable. This assumes the value has a Name property.")]
[AddComponentMenu("")]
public class GetName : Command
{
    [VariableProperty(typeof(ObjectVariable))]
    [SerializeField] ObjectVariable withName = null;
    [VariableProperty(typeof(StringVariable))]
    [SerializeField] StringVariable stringVar = null;

    public override void OnEnter()
    {
        base.OnEnter();

        if (!HasName(withName.Value))
            throw new System.ArgumentException("The value of what the variable " + withName.name + " has, has no Name property!");

        stringVar.Value = ((dynamic)withName.Value).Name;

        Continue();
    }

    bool HasName(object val)
    {
        return ((dynamic)val).Name != null;
    }
}
