using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStageGroup", menuName = "Shopkeep/StageGroup")]
public class StageGroup : CollectionSO<Stage>
{
    public virtual IList<Stage> Stages { get { return Contents; } }
}
