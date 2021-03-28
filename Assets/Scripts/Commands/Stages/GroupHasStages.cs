using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Stages", 
    "Group Has Stages", 
    "Checks if a group has the given stages, assigning the result to a Boolean Variable.")]
public class GroupHasStages : Command
{
    [SerializeField] protected StageGroup group;
    [SerializeField] protected Stage[] stages;
    [VariableProperty(typeof(BooleanVariable))]
    [SerializeField] protected BooleanVariable result;

    public override void OnEnter()
    {
        base.OnEnter();
        result.Value = group.ContainsRange(stages);
        Continue();
    }
}
