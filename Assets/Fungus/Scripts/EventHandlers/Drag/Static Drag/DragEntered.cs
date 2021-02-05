// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)
using System.Linq;
using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// The block will execute when the player is dragging an object which starts touching the target object.
    /// 
    /// ExecuteAlways used to get the Compatibility that we need, use of ISerializationCallbackReceiver is error prone
    /// when used on Unity controlled objects as it runs on threads other than main thread.
    /// </summary>
    [EventHandlerInfo("Sprite",
                      "Drag Entered",
                      "The block will execute when the player is dragging an object which starts touching the target object.")]
    [AddComponentMenu("")]
    [ExecuteInEditMode]
    public class DragEntered : StaticDragEventHandler2DWithTarget<DragEnteredEvent>, ISerializationCallbackReceiver
    {
        protected override void OnMainDragEvent(DragEnteredEvent evt)
        {
            OnDragEntered(evt.DraggableObject, evt.TargetCollider);
        }


        #region Compatibility

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        protected override void HandleAwakeBackwardsCompat()
        {
            base.HandleAwakeBackwardsCompat();

            if (targetObject != null)
            {
                if (!targetObjects.Contains(targetObject))
                {
                    targetObjects.Add(targetObject);
                }
            }

            targetObject = null;
        }

        #endregion Compatibility

        #region Public members

        /// <summary>
        /// Called by the Draggable2D object when the the drag enters the drag target.
        /// </summary>
        public virtual void OnDragEntered(Draggable2D draggableObject, Collider2D targetObject)
        {
            bool validDraggable = draggableOptional || AllDraggables.Contains(draggableObject);
            bool validTarget = targetOptional || this.targetObjects.Contains(targetObject);

            if (validDraggable & validTarget)
            {
                UpdateVarRefs(draggableObject.gameObject, targetObject.gameObject);
                ExecuteBlock();
            }
        }


        #endregion Public members
    }
}