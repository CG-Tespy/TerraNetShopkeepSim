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

    // The subclasses shared between both the dynamic and static event handlers
    public class DragEnteredEvent : DragEvent2D
    {
        public DragEnteredEvent(Draggable2D draggableObject, Collider2D targetCollider) : base(draggableObject, targetCollider)
        {
        }
    }

    public class DragCancelledEvent : DragEvent2D
    {
        public DragCancelledEvent(Draggable2D draggableObject) : base(draggableObject) { }
    }

    public class DragCompletedEvent : DragEvent2D
    {
        public DragCompletedEvent(Draggable2D draggableObject) : base(draggableObject)
        {
        }
    }

    public class DragExitedEvent : DragEvent2D
    {
        public DragExitedEvent(Draggable2D draggableObject,
            Collider2D targetCollider) :
            base(draggableObject, targetCollider)
        {
        }
    }

    public class DragStartedEvent : DragEvent2D
    {
        public DragStartedEvent(Draggable2D draggableObject) : base(draggableObject) { }
    }
}