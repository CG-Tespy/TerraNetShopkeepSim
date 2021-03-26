using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStageGroupDatabase", menuName = "Shopkeep/StageGroupDatabase")]
public class StageGroupDatabase : CollectionSO<StageGroup>
{
    /// <summary>
    /// Alias for Contents
    /// </summary>
    public virtual IList<StageGroup> StageGroups
    {
        get { return Contents; }
    }

}
