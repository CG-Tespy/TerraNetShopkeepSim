using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Stages", "Add Stages To Group", "Adds a stage to a group")]
public class AddStagesToGroup : Command
{
    [SerializeField] protected Stage[] stages;
    [SerializeField] protected StageGroup group;

    public override void OnEnter()
    {
        base.OnEnter();

        foreach (Stage stageEl in stages)
        {
            group.Add(stageEl);
        }

        Continue();
    }

    protected virtual void AlertFalseStage()
    {
        string message = "There is a non-stage in the Stages array in AddStagesToGroup!";
        throw new System.InvalidOperationException(message);
    }
}
