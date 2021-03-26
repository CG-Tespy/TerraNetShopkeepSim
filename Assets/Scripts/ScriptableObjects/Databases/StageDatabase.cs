using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStageDatabase", menuName = "Shopkeep/StageDatabase")]
public class StageDatabase : CollectionSO<Stage>
{
    /// <summary>
    /// Alias for Contents
    /// </summary>
    public virtual IList<Stage> Stages { get { return Contents; } }
}
