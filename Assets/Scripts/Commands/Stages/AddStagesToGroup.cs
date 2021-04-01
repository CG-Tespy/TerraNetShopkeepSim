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

}
