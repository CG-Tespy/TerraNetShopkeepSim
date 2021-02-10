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
                    return draggableObjects;
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
            UpdateAllDraggablesInScene();
            RegisterDraggableHandlers();
            OnDragCompleted(evt.DraggableObject);
        }

        private void OnDragEnteredEvent(DragEnteredEvent evt)
        {
            UpdateAllDraggablesInScene();
            RegisterDraggableHandlers();
            OnDragEntered(evt.DraggableObject, evt.TargetCollider);
        }

        private void OnDragExitedEvent(DragExitedEvent evt)
        {
            UpdateAllDraggablesInScene();
            RegisterDraggableHandlers();
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
            bool validDraggable = draggableOptional || AllDraggables.Contains(draggableObject);
            bool validTarget = (targetOptional || AllTargets.Contains(targetObject));

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
            bool validTarget = targetObject == targetCollider;

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
                Debug.Log("Drag complete draggable: " + draggableObject.name + " Target: " + targetCollider.name);
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