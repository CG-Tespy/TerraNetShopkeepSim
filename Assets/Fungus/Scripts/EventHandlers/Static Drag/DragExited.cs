// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using System.Linq;
using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// The block will execute when the player is dragging an object which stops touching the target object.
    ///
    /// ExecuteAlways used to get the Compatibility that we need, use of ISerializationCallbackReceiver is error prone
    /// when used on Unity controlled objects as it runs on threads other than main thread.
    /// </summary>
    [EventHandlerInfo("Sprite",
                      "Drag Exited",
                      "The block will execute when the player is dragging an object which stops touching the target object.")]
    [AddComponentMenu("")]
    [ExecuteInEditMode]
    public class DragExited : StaticDragEventHandler2DWithTarget, ISerializationCallbackReceiver
    {
        public class DragExitedEvent : DragEvent2D
        {
            public DragExitedEvent(Draggable2D draggableObject, 
                Collider2D targetCollider) : 
                base(draggableObject, targetCollider)
            {
            }
        }

        protected override void ListenForDragEvents()
        {
            eventDispatcher.AddListener<DragExitedEvent>(OnDragEnteredEvent);
        }

        protected override void UnlistenForDragEvents()
        {
            eventDispatcher.RemoveListener<DragExitedEvent>(OnDragEnteredEvent);
            eventDispatcher = null;
        }

        private void OnDragEnteredEvent(DragExitedEvent evt)
        {
            OnDragExited(evt.DraggableObject, evt.TargetCollider);
        }

        #region Compatibility

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }


        #endregion Compatibility

        #region Public members

        /// <summary>
        /// Called by the Draggable2D object when the drag exits from the targetObject.
        /// </summary>
        public virtual void OnDragExited(Draggable2D draggableObject, Collider2D targetObject)
        {
            if (this.targetObjects != null && this.draggableObjects != null &&
                this.draggableObjects.Contains(draggableObject) &&
                this.targetObjects.Contains(targetObject))
            {
                UpdateVarRefs(draggableObject.gameObject, targetObject.gameObject);
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
}