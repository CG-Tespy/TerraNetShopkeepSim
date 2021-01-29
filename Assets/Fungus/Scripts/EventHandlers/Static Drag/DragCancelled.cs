// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// The block will execute when the player drags an object and releases it without dropping it on a target object.
    /// </summary>
    [EventHandlerInfo("Sprite",
                      "Drag Cancelled",
                      "The block will execute when the player drags an object and releases it without dropping it on a target object.")]
    [AddComponentMenu("")]
    public class DragCancelled : StaticDragEventHandler2D, ISerializationCallbackReceiver
    {
        public class DragCancelledEvent : DragEvent2D
        {
            public DragCancelledEvent(Draggable2D draggableObject) : base(draggableObject) { }
        }

        protected override void ListenForDragEvents()
        {
            eventDispatcher.AddListener<DragCancelledEvent>(OnDragCancelledEvent);
        }

        protected override void UnlistenForDragEvents()
        {
            eventDispatcher.RemoveListener<DragCancelledEvent>(OnDragCancelledEvent);
            eventDispatcher = null;
        }

        protected virtual void OnDragCancelledEvent(DragCancelledEvent evt)
        {
            OnDragCancelled(evt.DraggableObject);
        }

        #region Compatibility

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            //add any dragableobject already present to list for backwards compatability
            if (draggableObject != null)
            {
                if (!draggableObjects.Contains(draggableObject))
                {
                    draggableObjects.Add(draggableObject);
                }
                draggableObject = null;
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        #endregion Compatibility

        #region Public members

        public virtual void OnDragCancelled(Draggable2D draggableObject)
        {
            if (draggableObjects.Contains(draggableObject))
            {
                UpdateVarRefs(draggableObject.gameObject, null);
                ExecuteBlock();
            }
        }

        public override string GetSummary()
        {
            if (draggableObjects.Count(x => x != null) == 0)
            {
                return "Error: no draggable objects assigned.";
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
            return summary;
        }

        #endregion Public members
    }
}