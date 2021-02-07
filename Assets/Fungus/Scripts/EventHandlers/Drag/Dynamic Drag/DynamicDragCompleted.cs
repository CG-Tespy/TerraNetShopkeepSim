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

            UpdateAllDraggablesInScene();
            RegisterDraggableHandlers();
        }

        protected virtual void RegisterDraggableHandlers()
        {
            bool beHandlerForAllDraggablesInScene = draggableOptional;

            if (beHandlerForAllDraggablesInScene)
            {
                var allInScene = GameObject.FindObjectsOfType<Draggable2D>();
                foreach (Draggable2D dragObj in allInScene)
                {
                    dragObj.RegisterHandler(this);
                }
            }
            else
            {
                foreach (Draggable2D dragObj in AllDraggables)
                {
                    dragObj.RegisterHandler(this);
                }
            }
        }

        public override IList<Draggable2D> AllDraggables
        {
            get
            {
                if (draggableOptional)
                    return allDraggablesInScene;
                else
                    return dynamicDraggables.AllObjects;
            }
        }

        protected virtual void UpdateAllDraggablesInScene()
        {
            if (!draggableOptional)
                return;
            allDraggablesInScene.Clear();
            allDraggablesInScene.AddRange(GameObject.FindObjectsOfType<Draggable2D>());
        }

        protected List<Draggable2D> allDraggablesInScene = new List<Draggable2D>();

        protected override void UnlistenForDragEvents()
        {
            eventDispatcher.RemoveListener<DragCompletedEvent>(OnMainDragEvent);
            eventDispatcher.RemoveListener<DragEnteredEvent>(OnDragEnteredEvent);
            eventDispatcher.RemoveListener<DragExitedEvent>(OnDragExitedEvent);

            UpdateAllDraggablesInScene();
            UnregisterDraggableHandlers();
            eventDispatcher = null;
        }

        protected virtual void UnregisterDraggableHandlers()
        {
            bool unregisterForAllDraggablesInScene = draggableOptional;

            if (unregisterForAllDraggablesInScene)
            {
                var allInScene = GameObject.FindObjectsOfType<Draggable2D>();
                foreach (Draggable2D dragObj in allInScene)
                {
                    dragObj.UnregisterHandler(this);
                }
            }
            else
            {
                foreach (Draggable2D dragObj in AllDraggables)
                {
                    dragObj.UnregisterHandler(this);
                }
            }
        }


        protected override void OnMainDragEvent(DragCompletedEvent evt)
        {
            base.OnMainDragEvent(evt);
            UpdateAllDraggablesInScene();
            RegisterDraggableHandlers();
            OnDragCompleted(evt.DraggableObject);
        }

        #endregion

        private void OnDragEnteredEvent(DragEnteredEvent evt)
        {
            KeepTrackOfDynamicObjects();
            UpdateAllDraggablesInScene();
            RegisterDraggableHandlers();
            OnDragEntered(evt.DraggableObject, evt.TargetCollider);
        }

        private void OnDragExitedEvent(DragExitedEvent evt)
        {
            KeepTrackOfDynamicObjects();
            UpdateAllDraggablesInScene();
            RegisterDraggableHandlers();
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
                Debug.Log("Target collider for " + draggableObject.name + ": " + targetObject.name);
            }
        }

        /// <summary>
        /// Called by the Draggable2D object when the it exits the drag target.
        /// </summary>
        public virtual void OnDragExited(Draggable2D draggableObject, Collider2D targetObject)
        {
            bool validDraggable = draggableOptional || AllDraggables.Contains(draggableObject);
            bool validTarget = (targetOptional || AllTargets.Contains(targetObject));

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