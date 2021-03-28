using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
    [EventHandlerInfo("Sprite",
                      "Drag Exited (Dynamic)",
                      @"Like normal Drag Exited, but for runtime-generated objects.")]
    [AddComponentMenu("")]
    public class DynamicDragExited : DynamicDragEventHandler2D<DragExitedEvent>
    {
        protected override void OnMainDragEvent(DragExitedEvent evt)
        {
            base.OnMainDragEvent(evt);
            OnDragExited(evt.DraggableObject);
        }

        public virtual void OnDragExited(Draggable2D draggableObject)
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