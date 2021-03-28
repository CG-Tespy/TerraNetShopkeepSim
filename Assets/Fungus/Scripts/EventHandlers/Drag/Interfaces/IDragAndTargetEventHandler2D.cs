using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
    public interface IDragAndTargetEventHandler2D : IDragEventHandler2D
    {
        IList<Collider2D> AllTargets { get; }

        /// <summary>
        /// Whether or not this can respond to any target, even if its not among those specifically
        /// set for the event to respond to
        /// </summary>
        bool TargetOptional { get; }
    }
}