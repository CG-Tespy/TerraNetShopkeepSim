using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Battle", 
    "Set Fighter To Object Variable", 
    "Getting around a limitation in the Set Variable command")]
public class SetFighterToObjectVariable : Command
{
    [VariableProperty(typeof(ObjectVariable))]
    [SerializeField] ObjectVariable objVar = null;

    [SerializeField] FighterControllerData otherVar;

    public override void OnEnter()
    {
        base.OnEnter();
        objVar.Value = otherVar.Value as Object;
        Continue();
    }
}
