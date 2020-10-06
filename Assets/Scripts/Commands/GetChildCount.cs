using UnityEngine;
using Fungus;

[CommandInfo("GameObject", "Get Child Count", "Assigns the child count of one Game Object and assigns it to a variable.")]
public class GetChildCount : Command
{
    [SerializeField] GameObjectData mayHaveChildren;
    [VariableProperty(typeof(IntegerVariable))]
    [SerializeField] IntegerVariable intVar = null;

    public override void OnEnter()
    {
        base.OnEnter();

        if (mayHaveChildren.Value != null && intVar != null)
            intVar.Value = mayHaveChildren.Value.transform.childCount;

        Continue();
    }
}
