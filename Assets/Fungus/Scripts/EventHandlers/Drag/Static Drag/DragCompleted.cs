// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// The block will execute when the player drags an object and successfully drops it on a target object.
    ///
    /// ExecuteAlways used to get the Compatibility that we need, use of ISerializationCallbackReceiver is error prone
    /// when used on Unity controlled objects as it runs on threads other than main thread.
    /// </summary>
    [EventHandlerInfo("Sprite",
                      "Drag Completed",
                      "The block will execute when the player drags an object and successfully drops it on a target object.")]
    [AddComponentMenu("")]
    [ExecuteInEditMode]
    public class DragCompleted: StaticDragEventHandler2DWithTarget<DragCompletedEvent>, 
        ISerializationCallbackReceiver,
        IDragCompleted
    {
        // There's no way to poll if an object is touching another object, so
        // we have to listen to the callbacks and track the touching state ourselves.
        protected bool overTarget = false;

        protected Collider2D targetCollider = null;

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

        private void OnDragEnteredEvent(DragEnteredEvent evt)
        {
            OnDragEntered(evt.DraggableObject, evt.TargetCollider);
        }

        private void OnDragExitedEvent(DragExitedEvent evt)
        {
            OnDragExited(evt.DraggableObject, evt.TargetCollider);
        }

        #region Compatibility

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            //presentl using awake due to errors on non main thread access of targetCollider
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        #endregion Compatibility

        #region Public members

        public virtual IList<Draggable2D> DraggableObjects { get { return draggableObjects; } }

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
            if (this.targetObjects != null && this.draggableObjects != null &&
                AllDraggables.Contains(draggableObject) &&
                this.targetObjects.Contains(targetObject))
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
            if (this.targetObjects != null && this.draggableObjects != null &&
                this.draggableObjects.Contains(draggableObject) &&
                this.targetObjects.Contains(targetObject))
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
            bool validDraggable = draggableOptional || draggableObjects.Contains(draggableObject);

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

        public override string GetSummary()
        {
            if (draggableObjects.Count(x => x != null) == 0)
            {
                return "Error: no draggable objects assigned.";
            }
            if (targetObjects.Count(x => x != null) == 0)
            {
                return "Error: no target objects assigned.";
            }

            string summary = "Draggable: ";
            if (this.draggableObjects != null && this.draggableObjects.Count != 0)
            {
                for (int i = 0; i < this.draggableObjects.Count; i++)
                {
                    if (draggableObjects[i] != null)
                    {
                        summary += draggableObjects[i].name + ",";
                    }
                }
            }

            summary += "\nTarget: ";
            if (this.targetObjects != null && this.targetObjects.Count != 0)
            {
                for (int i = 0; i < this.targetObjects.Count; i++)
                {
                    if (targetObjects[i] != null)
                    {
                        summary += targetObjects[i].name + ",";
                    }
                }
            }

            return summary;
        }

        #endregion Public members
    }

    public interface IDragCompleted
    {
        IList<Draggable2D> AllDraggables { get; }
        bool IsOverTarget();
    }
}