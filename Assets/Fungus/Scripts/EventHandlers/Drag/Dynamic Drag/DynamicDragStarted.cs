using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
    [EventHandlerInfo("Sprite",
                      "Drag Started (Dynamic)",
                      @"Like normal Drag Started, but for runtime-generated objects.")]
    [AddComponentMenu("")]
    public class DynamicDragStarted : DynamicDragEventHandler2D<DragStartedEvent>
    {
        protected override void OnMainDragEvent(DragStartedEvent evt)
        {
            base.OnMainDragEvent(evt);
            OnDragStarted(evt.DraggableObject);
        }

        #region Public members

        /// <summary>
        /// Called by the Draggable2D object when the drag starts.
        /// </summary>
        public virtual void OnDragStarted(Draggable2D draggableObject)
        {
            bool validDraggable = draggableOptional || AllDraggables.Contains(draggableObject);

            if (validDraggable)
            {
                UpdateVarRefs(draggableObject.gameObject, null);
                ExecuteBlock();
            }
        }

        #endregion Public members
    }
}