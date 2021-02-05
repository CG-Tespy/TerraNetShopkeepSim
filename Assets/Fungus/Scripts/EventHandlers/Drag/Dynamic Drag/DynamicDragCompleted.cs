using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
    [EventHandlerInfo("Sprite",
                      "Drag Completed (Dynamic)",
                      @"Like normal Drag Completed, but for runtime-generated objects.")]
    [AddComponentMenu("")]
    public class DynamicDragCompleted : DynamicDragEventHandler2DWithTarget<DragCompletedEvent>, IDragCompleted
    {
        #region Hooks
        protected override void ListenForDragEvents()
        {
            eventDispatcher.AddListener<DragCompletedEvent>(OnMainDragEvent);
            eventDispatcher.AddListener<DragEnteredEvent>(OnDragEnteredEvent);
            eventDispatcher.AddListener<DragExitedEvent>(OnDragExitedEvent);

            foreach (Draggable2D dragObj in AllDraggables)
            {
                dragObj.RegisterHandler(this);
            }
        }

        protected override void UnlistenForDragEvents()
        {
            eventDispatcher.RemoveListener<DragCompletedEvent>(OnMainDragEvent);
            eventDispatcher.RemoveListener<DragEnteredEvent>(OnDragEnteredEvent);
            eventDispatcher.RemoveListener<DragExitedEvent>(OnDragExitedEvent);

            foreach (Draggable2D dragObj in AllDraggables)
            {
                dragObj.UnregisterHandler(this);
            }

            eventDispatcher = null;
        }

        protected override void OnMainDragEvent(DragCompletedEvent evt)
        {
            OnDragCompleted(evt.DraggableObject);
        }

        #endregion

        private void OnDragEnteredEvent(DragEnteredEvent evt)
        {
            OnDragEntered(evt.DraggableObject, evt.TargetCollider);
        }

        private void OnDragExitedEvent(DragExitedEvent evt)
        {
            OnDragExited(evt.DraggableObject, evt.TargetCollider);
        }


        // There's no way to poll if an object is touching another object, so
        // we have to listen to the callbacks and track the touching state ourselves.
        protected bool overTarget = false;

        protected Collider2D targetCollider = null;

        /// <summary>
        /// Returns true if the draggable object is over the drag target object.
        /// </summary>
        public virtual bool IsOverTarget()
        {
            return overTarget;
        }

        /// <summary>
        /// Called by the Draggable2D object when the it enters the drag target.
        /// </summary>
        public virtual void OnDragEntered(Draggable2D draggableObject, Collider2D targetObject)
        {
            bool validDraggable = draggableOptional || AllDraggables.Contains(draggableObject);
            bool validTarget = targetOptional || AllTargets.Contains(targetObject);

            if (validDraggable && validTarget)
            {
                overTarget = true;
                targetCollider = targetObject;
            }
        }

        /// <summary>
        /// Called by the Draggable2D object when the it exits the drag target.
        /// </summary>
        public virtual void OnDragExited(Draggable2D draggableObject, Collider2D targetObject)
        {
            bool validDraggable = draggableOptional || AllDraggables.Contains(draggableObject);
            bool validTarget = targetOptional || AllTargets.Contains(targetObject);

            if (validDraggable && validTarget)
            {
                overTarget = false;
                targetCollider = null;
            }
        }

        /// <summary>
        /// Called by the Draggable2D object when the the drag ends over the drag target.
        /// </summary>
        public virtual void OnDragCompleted(Draggable2D draggableObject)
        {
            bool validDraggable = draggableOptional || AllDraggables.Contains(draggableObject);

            if (validDraggable && overTarget)
            {
                // Assume that the player will have to do perform another drag and drop operation
                // to complete the drag again. This is necessary because we don't get an OnDragExited if the
                // draggable object is set to be inactive.
                UpdateVarRefs(draggableObject.gameObject, targetCollider.gameObject);

                overTarget = false;
                targetCollider = null;

                ExecuteBlock();
            }
        }
    }
}