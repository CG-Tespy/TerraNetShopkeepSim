using UnityEngine;

namespace Fungus
{
    [EventHandlerInfo("Sprite",
                      "Drag Cancelled (Dynamic)",
                      @"Like normal Drag Cancelled, but for runtime-generated objects.")]
    [AddComponentMenu("")]
    public class DynamicDragCancelled : DynamicDragEventHandler2D<DragCancelledEvent>
    {
        protected override void OnMainDragEvent(DragCancelledEvent evt)
        {
            OnDragCancelled(evt.DraggableObject);
        }

        public virtual void OnDragCancelled(Draggable2D draggableObject)
        {
            bool validDraggable = draggableOptional || AllDraggables.Contains(draggableObject);

            if (validDraggable)
            {
                UpdateVarRefs(draggableObject.gameObject, null);
                ExecuteBlock();
            }
        }
    }
}