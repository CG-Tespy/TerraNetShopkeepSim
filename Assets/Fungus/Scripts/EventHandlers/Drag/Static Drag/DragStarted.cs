// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using System.Linq;
using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// The block will execute when the player starts dragging an object.
    /// </summary>
    [EventHandlerInfo("Sprite",
                      "Drag Started",
                      "The block will execute when the player starts dragging an object.")]
    [AddComponentMenu("")]
    public class DragStarted : StaticDragEventHandler2D<DragStartedEvent>, ISerializationCallbackReceiver
    {

        protected override void OnMainDragEvent(DragStartedEvent evt)
        {
            OnDragStarted(evt.DraggableObject);
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

        /// <summary>
        /// Called by the Draggable2D object when the drag starts.
        /// </summary>
        public virtual void OnDragStarted(Draggable2D draggableObject)
        {
            if (draggableOptional || AllDraggables.Contains(draggableObject))
            {
                UpdateVarRefs(draggableObject.gameObject, null);
                ExecuteBlock();
            }
        }

        public override string GetSummary()
        {
            if (draggableObjects.Count(x=> x != null) == 0)
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