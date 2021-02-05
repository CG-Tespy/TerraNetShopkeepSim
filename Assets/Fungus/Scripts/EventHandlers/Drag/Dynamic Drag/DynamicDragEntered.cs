using UnityEngine;

namespace Fungus
{
    [EventHandlerInfo("Sprite",
                      "Drag Entered (Dynamic)",
                      @"Like normal Drag Entered, but for runtime-generated objects.")]
    [AddComponentMenu("")]
    public class DynamicDragEntered : DynamicDragEventHandler2DWithTarget<DragEnteredEvent>
    {
        protected override void OnMainDragEvent(DragEnteredEvent evt)
        {
            base.OnMainDragEvent(evt);
            OnDragEntered(evt.DraggableObject, evt.TargetCollider);
        }

        /// <summary>
        /// Called by the Draggable2D object when the the drag enters the drag target.
        /// </summary>
        public virtual void OnDragEntered(Draggable2D draggableObject, Collider2D targetObject)
        {
            bool validDraggable = draggableOptional || AllDraggables.Contains(draggableObject);
            bool validTarget = targetOptional || AllTargets.Contains(targetObject);

            if (validDraggable & validTarget)
            {
                UpdateVarRefs(draggableObject.gameObject, targetObject.gameObject);
                ExecuteBlock();
            }
        }
    }
}