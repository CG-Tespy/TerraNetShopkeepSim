using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class StageGroupLoader : TerraNetSaveLoader<StageGroupSaveData>
{
    [SerializeField] protected StageGroupDatabase loadableStageGroups;
    [SerializeField] protected StageDatabase loadableStages;

    protected override bool CanLoad(StageGroupSaveData saveData)
    {
        bool groupIndexNonNegative = saveData.CollectionIndex >= 0;
        bool groupIndexInRightRange = saveData.CollectionIndex < loadableStageGroups.Count();
        bool validGroupIndex = groupIndexNonNegative && groupIndexInRightRange;

        bool groupIsEmpty = saveData.ContentIndexes.Count == 0;

        bool noNegativeIndex = groupIsEmpty || saveData.ContentIndexes.Min() >= 0;
        bool noIndexTooHigh = groupIsEmpty || saveData.ContentIndexes.Max() < loadableStages.Count();
        bool validContentIndexes = groupIsEmpty || (noNegativeIndex && noIndexTooHigh);

        return validGroupIndex && validContentIndexes;
    }

    protected override void AlertCannotLoad(StageGroupSaveData saveData)
    {
        string message = string.Format(loadFailMessageFormat, saveData.CollectionName);

        throw new System.InvalidOperationException(message);
    }

    protected static readonly string loadFailMessageFormat = "Cannot load Stage Group {0}";

    protected override void RestoreStateWith(StageGroupSaveData saveData)
    {
        StageGroup group = loadableStageGroups.Contents[saveData.CollectionIndex];
        group.Clear();
        AddStagesIntoGroup(group, saveData.ContentIndexes);
    }

    protected virtual void AddStagesIntoGroup(StageGroup group, IList<int> stageIndexes)
    {
        foreach (int index in stageIndexes)
        {
            Stage stage = loadableStages.Contents[index];
            group.Add(stage);
        }

    }
}
