using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep",
                 "GetName",
                 @"Assigns the name of the specified value to the specified variable. This assumes the value has a Name property.")]
[AddComponentMenu("")]
public class GetName : Command
{
    [SerializeField] ObjectData withName;
    [VariableProperty(typeof(StringVariable))]
    [SerializeField] StringVariable stringVar = null;

    public override void OnEnter()
    {
        base.OnEnter();

        var withName = this.withName.Value;

        if (!HasName(withName))
            throw new System.ArgumentException("The value of what the variable " + withName.name + " has, has no Name property!");

        stringVar.Value = ((dynamic)withName).Name;

        Continue();
    }

    bool HasName(object val)
    {
        return ((dynamic)val).Name != null;
    }
}
