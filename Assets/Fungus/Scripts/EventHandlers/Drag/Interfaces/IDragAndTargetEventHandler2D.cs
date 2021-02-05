using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
    public interface IDragAndTargetEventHandler2D : IDragEventHandler2D
    {
        IList<Collider2D> AllTargets { get; }
    }
}