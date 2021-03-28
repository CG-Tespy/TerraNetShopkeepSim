using UnityEngine;
using Fungus;

[CommandInfo("Variable", "Find Object of Type", "Assigns an object of the right type to an ObjectVariable. This can set it to null.")]
public class FindObjectOfType : Command
{
    [SerializeField] ObjectData objectWithType;
    [VariableProperty(typeof(ObjectVariable))]
    [SerializeField] ObjectVariable toAssignTo = null;

    public override void OnEnter()
    {
        base.OnEnter();
        System.Type objType = objectWithType.Value.GetType();
        Debug.Log("Obj type is: " + objType);
        toAssignTo.Value = FindObjectOfType(objType);
        Continue();
    }
}
