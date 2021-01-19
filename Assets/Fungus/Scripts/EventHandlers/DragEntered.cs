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
    public class DragEntered : EventHandler, ISerializationCallbackReceiver
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

        [VariableProperty(typeof(GameObjectVariable))]
        [SerializeField] protected GameObjectVariable draggableRef;

        [VariableProperty(typeof(GameObjectVariable))]
        [SerializeField] protected GameObjectVariable targetRef;

        [Tooltip("Draggable object to listen for drag events on")]
        [HideInInspector]
        [SerializeField] protected Draggable2D draggableObject;

        [SerializeField] protected List<Draggable2D> draggableObjects;

        [Tooltip("Drag target object to listen for drag events on")]
        [HideInInspector]
        [SerializeField] protected Collider2D targetObject;

        [SerializeField] protected List<Collider2D> targetObjects;

        protected EventDispatcher eventDispatcher;

        [Tooltip("These have Draggables parented to them. You'll want to use this for programmatically-generated draggables.")]
        [SerializeField] protected List<Transform> draggableObjectHolders;

        [Tooltip("These have Draggables parented to them. You'll want to use this for programmatically-generated draggables.")]
        [SerializeField] protected List<Transform> targetObjectHolders;

        

        protected virtual void OnEnable()
        {
            if (Application.isPlaying)
            {
                ListenForDragEvents();
                RefreshDraggableObjectList();
                RefreshTargetObjectList();
            }
        }

        protected virtual void ListenForDragEvents()
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
            var draggablesFromHolders = GetObjectsFrom<Draggable2D>(draggableObjectHolders);

            var outdatedDraggables = allDraggables;
            noDuplicates.UnionWith(outdatedDraggables); // So we keep the ones that were unparented from a valid holder
            noDuplicates.UnionWith(draggableObjects);
            noDuplicates.UnionWith(draggablesFromHolders);

            allDraggables.Clear();
            allDraggables.AddRange(noDuplicates);
        }

        // Includes everything from both the holders, and what's set from the scene.

        protected List<Draggable2D> allDraggables = new List<Draggable2D>();

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
            var targetsFromHolders = GetObjectsFrom<Collider2D>(targetObjectHolders);
            var outdatedTargets = allTargets;

            noDuplicates.UnionWith(outdatedTargets);
            noDuplicates.UnionWith(targetObjects);
            noDuplicates.UnionWith(targetsFromHolders);

            allTargets.Clear();
            allTargets.AddRange(noDuplicates);
        }


        protected List<Collider2D> allTargets = new List<Collider2D>();

        protected virtual void OnDisable()
        {
            if (Application.isPlaying)
            {
                UnlistenForDragEvents();
            }
        }

        protected virtual void UnlistenForDragEvents()
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

        private void Awake()
        {
            //add any dragableobject already present to list for backwards compatability
            if (draggableObject != null)
            {
                if (!allDraggables.Contains(draggableObject))
                {
                    allDraggables.Add(draggableObject);
                }
            }

            if (targetObject != null)
            {
                if (!allTargets.Contains(targetObject))
                {
                    allTargets.Add(targetObject);
                }
            }
            draggableObject = null;
            targetObject = null;
        }

        #endregion Compatibility

        #region Public members

        /// <summary>
        /// Called by the Draggable2D object when the the drag enters the drag target.
        /// </summary>
        public virtual void OnDragEntered(Draggable2D draggableObject, Collider2D targetObject)
        {
            if (this.allTargets != null && this.allDraggables != null &&
                this.allDraggables.Contains(draggableObject) &&
                this.allTargets.Contains(targetObject) && draggableObject.gameObject != targetObject.gameObject)
            {
                if (draggableRef != null)
                {
                    draggableRef.Value = draggableObject.gameObject;
                }
                if (targetRef != null)
                {
                    targetRef.Value = targetObject.gameObject;
                }
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

        #endregion Public members
    }

   
}