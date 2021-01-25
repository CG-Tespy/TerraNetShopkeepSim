// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using System.Collections.Generic;
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
    public class DragEntered : DragEventHandler2D, ISerializationCallbackReceiver
    {
        public class DragEnteredEvent
        {
            public Draggable2D DraggableObject;
            public Collider2D TargetCollider;

            public DragEnteredEvent(Draggable2D draggableObject, Collider2D targetCollider)
            {
                DraggableObject = draggableObject;
                TargetCollider = targetCollider;
            }
        }

        [Tooltip("Drag target object to listen for drag events on")]
        [HideInInspector]
        [SerializeField] protected Collider2D targetObject;

        [SerializeField] protected List<Collider2D> targetObjects;


        public virtual List<Collider2D> AllTargets
        {
            get { return dynamicTargets.AllObjects; }
        }

        [Header("For dynamically-generated targets")]
        [SerializeField] protected Collider2DManager dynamicTargets = new Collider2DManager();

        protected override void ListenForDragEvents()
        {
            eventDispatcher = FungusManager.Instance.EventDispatcher;
            eventDispatcher.AddListener<DragEnteredEvent>(OnDragEnteredEvent);
        }

        /// <summary>
        /// You'll mainly want to use this to keep the draggables list updated with the 
        /// programmatically-generated stuff
        /// </summary>
        protected virtual void RefreshDraggableObjectList()
        {
            HashSet<Draggable2D> noDuplicates = new HashSet<Draggable2D>();
            var draggablesFromHolders = GetObjectsFrom<Draggable2D>(dynamicDraggables.ObjectHolders);

            var outdatedDraggables = AllDraggables;
            noDuplicates.UnionWith(outdatedDraggables); // So we keep the ones that were unparented from a valid holder
            noDuplicates.UnionWith(draggableObjects);
            noDuplicates.UnionWith(draggablesFromHolders);

            AllDraggables.Clear();
            AllDraggables.AddRange(noDuplicates);
        }

        protected virtual List<TObj> GetObjectsFrom<TObj>(IList<Transform> holders)
        {
            List<TObj> fromHolders = new List<TObj>();

            foreach (Transform holder in holders)
            {
                IList<TObj> foundInHolder = holder.GetComponentsInChildren<TObj>();
                fromHolders.AddRange(foundInHolder);
            }

            return fromHolders;
        }

        protected virtual void RefreshTargetObjectList()
        {
            HashSet<Collider2D> noDuplicates = new HashSet<Collider2D>();
            var targetsFromHolders = GetObjectsFrom<Collider2D>(dynamicTargets.ObjectHolders);
            var outdatedTargets = AllTargets;

            noDuplicates.UnionWith(outdatedTargets);
            noDuplicates.UnionWith(targetObjects);
            noDuplicates.UnionWith(targetsFromHolders);

            AllTargets.Clear();
            AllTargets.AddRange(noDuplicates);
        }


        protected override void UnlistenForDragEvents()
        {
            eventDispatcher.RemoveListener<DragEnteredEvent>(OnDragEnteredEvent);
            eventDispatcher = null;
        }

        private void OnDragEnteredEvent(DragEnteredEvent evt)
        {
            RefreshDraggableObjectList();
            RefreshTargetObjectList();
            OnDragEntered(evt.DraggableObject, evt.TargetCollider);
        }

        #region Compatibility

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        protected override void Awake()
        {
            // Add any draggable object already present to list for backwards compatability
            if (draggableObject != null)
            {
                if (!AllDraggables.Contains(draggableObject))
                {
                    AllDraggables.Add(draggableObject);
                }
            }

            if (targetObject != null)
            {
                if (!AllTargets.Contains(targetObject))
                {
                    AllTargets.Add(targetObject);
                }
            }
            draggableObject = null;
            targetObject = null;

            base.Awake();
        }

        #endregion Compatibility

        #region Public members

        /// <summary>
        /// Called by the Draggable2D object when the the drag enters the drag target.
        /// </summary>
        public virtual void OnDragEntered(Draggable2D draggableObject, Collider2D targetObject)
        {
            bool acceptDraggable = draggableOptional || this.AllDraggables.Contains(draggableObject);
            bool acceptTarget = targetOptional || this.AllTargets.Contains(targetObject);
            bool differentObjects = draggableObject.gameObject != targetObject.gameObject;
            bool shouldRespond = acceptDraggable && acceptTarget && differentObjects;

            if (shouldRespond)
            {
                Debug.Log("Responding to drag enter: " + this.name);
                if (draggableRef != null)
                    draggableRef.Value = draggableObject.gameObject;
               
                if (targetRef != null)
                    targetRef.Value = targetObject.gameObject;
                
                ExecuteBlock();
            }
        }

        public override string GetSummary()
        {
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

            if (summary.Length == 0)
            {
                return "None";
            }

            return summary;
        }

        protected override void KeepTrackOfSceneObjects()
        {
            RefreshDraggableObjectList();
            RefreshTargetObjectList();
        }

        #endregion Public members
    }

}