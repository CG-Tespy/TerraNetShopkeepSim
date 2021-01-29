using UnityEngine;

namespace Fungus
{
    // Base class for all types of drag events
    public abstract class DragEvent2D
    {
        public Draggable2D DraggableObject;
        public Collider2D TargetCollider;

        public DragEvent2D(Draggable2D draggableObject = null, Collider2D targetCollider = null)
        {
            DraggableObject = draggableObject;
            TargetCollider = targetCollider;
        }
    }
}