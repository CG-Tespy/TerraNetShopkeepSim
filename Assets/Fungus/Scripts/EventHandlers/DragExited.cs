// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using System.Collections.Generic;
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
    public class DragExited : DragEventHandler2D, ISerializationCallbackReceiver
    {
        public class DragExitedEvent
        {
            public Draggable2D DraggableObject;
            public Collider2D TargetCollider;

            public DragExitedEvent(Draggable2D draggableObject, Collider2D targetCollider)
            {
                DraggableObject = draggableObject;
                TargetCollider = targetCollider;
            }
        }

        /*
        [VariableProperty(typeof(GameObjectVariable))]
        [SerializeField] protected GameObjectVariable draggableRef;

        [VariableProperty(typeof(GameObjectVariable))]
        [SerializeField] protected GameObjectVariable targetRef;

        [Tooltip("Draggable object to listen for drag events on")]
        [HideInInspector]
        [SerializeField] protected Draggable2D draggableObject;

        [SerializeField] protected List<Draggable2D> draggableObjects;

        */

        protected override void KeepTrackOfSceneObjects()
        {
            dynamicDraggables.Update();
            dynamicTargets.Update();
        }

        [Tooltip("Drag target object to listen for drag events on")]
        [HideInInspector]
        [SerializeField] protected Collider2D targetObject;

        [Header("Individually-set-from-the-scene targets")]
        [SerializeField] protected List<Collider2D> targetObjects;

        [Header("For dynamically-generated targets")]
        [SerializeField] protected Collider2DManager dynamicTargets = new Collider2DManager();

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
            KeepTrackOfSceneObjects();
            OnDragExited(evt.DraggableObject, evt.TargetCollider);
        }

        #region Compatibility

        protected override void HandleAwakeBackwardsCompat()
        {
            base.HandleAwakeBackwardsCompat();
            RegisterAlreadyPresentTarget();
        }

        protected virtual void RegisterAlreadyPresentTarget()
        {
            if (targetObject != null && !targetObjects.Contains(targetObject))
            {
                targetObjects.Add(targetObject);
                dynamicTargets.Update();
            }

            targetObject = null;
        }

        #endregion Compatibility

        protected override void SetUpDynamicObjectHandlers()
        {
            base.SetUpDynamicObjectHandlers();
            dynamicTargets.IndividualObjects = targetObjects;
        }

        #region Public members

        /// <summary>
        /// Called by the Draggable2D object when the drag exits from the targetObject.
        /// </summary>
        public virtual void OnDragExited(Draggable2D draggableObject, Collider2D targetObject)
        {

            if (AllDraggables.Contains(draggableObject) &&
                AllTargets.Contains(targetObject))
            {
                UpdateVarRefs(draggableObject.gameObject, targetObject.gameObject);
                ExecuteBlock();
            }
        }

        public override string GetSummary()
        {
            string summary = "Draggable: ";

            if (this.AllDraggables.Count > 0)
            {
                for (int i = 0; i < this.AllDraggables.Count; i++)
                {
                    var currentDraggable = AllDraggables[i];
                    if (currentDraggable != null)
                    {
                        summary += currentDraggable.name + ",";
                    }
                }
            }

            summary += "\nTarget: ";

            if (AllTargets.Count > 0)
            {
                for (int i = 0; i < AllTargets.Count; i++)
                {
                    var currentTarget = AllTargets[i];
                    if (currentTarget != null)
                    {
                        summary += currentTarget.name + ",";
                    }
                }
            }

            if (summary.Length == 0)
            {
                return "None";
            }

            return summary;
        }

        public virtual List<Collider2D> AllTargets
        {
            get { return dynamicTargets.AllObjects; }
        }

        #endregion Public members
    }

    
}