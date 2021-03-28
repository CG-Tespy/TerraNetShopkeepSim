using System.Collections.Generic;
using UnityEngine;

public class StageGroupSaver : TerraNetDataSaver<StageGroupSaveData, StageGroup>
{
    [SerializeField] protected StageGroupDatabase savableStageGroups;
    [SerializeField] protected StageDatabase savableStages;

    public override IList<StageGroup> ToSave
    {
        get { return savableStageGroups.Contents; }
    }


    public override StageGroupSaveData CreateSave(StageGroup stageGroup)
    {
        return StageGroupSaveDataFactory.CreateFrom(stageGroup, savableStages, savableStageGroups);
    }

}
