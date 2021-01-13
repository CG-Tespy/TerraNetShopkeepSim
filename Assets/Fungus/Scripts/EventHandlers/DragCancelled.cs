// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using System.Collections.Generic;
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
    public class DragCancelled : EventHandler, ISerializationCallbackReceiver
    {
        public class DragCancelledEvent
        {
            public Draggable2D DraggableObject;

            public DragCancelledEvent(Draggable2D draggableObject)
            {
                DraggableObject = draggableObject;
            }
        }

        [VariableProperty(typeof(GameObjectVariable))]
        [SerializeField] protected GameObjectVariable draggableRef;

        [Tooltip("Draggable object to listen for drag events on")]
        [SerializeField] protected List<Draggable2D> draggableObjects;

        [HideInInspector]
        [SerializeField] protected Draggable2D draggableObject;

        protected EventDispatcher eventDispatcher;

        [Tooltip("These have Draggables parented to them. You'll want to use this for programmatically-generated draggables.")]
        [SerializeField] protected List<Transform> draggableObjectHolders;


        protected virtual void OnEnable()
        {
            eventDispatcher = FungusManager.Instance.EventDispatcher;
            eventDispatcher.AddListener<DragCancelledEvent>(OnDragCancelledEvent);
            RefreshDraggableObjectList();
        }

        protected virtual void OnDisable()
        {
            eventDispatcher.RemoveListener<DragCancelledEvent>(OnDragCancelledEvent);
            eventDispatcher = null;
        }

        protected virtual void OnDragCancelledEvent(DragCancelledEvent evt)
        {
            OnDragCancelled(evt.DraggableObject);
        }

        /// <summary>
        /// You'll mainly want to use this to keep the draggables list updated with the 
        /// programmatically-generated stuff
        /// </summary>
        protected virtual void RefreshDraggableObjectList()
        {
            HashSet<Draggable2D> noDuplicates = new HashSet<Draggable2D>();
            var draggablesFromHolders = GetDraggablesFromHolders();

            var outdatedDraggables = allDraggables;
            noDuplicates.UnionWith(outdatedDraggables); // So we keep the ones that were unparented from a valid holder
            noDuplicates.UnionWith(draggableObjects);
            noDuplicates.UnionWith(draggablesFromHolders);

            allDraggables.Clear();
            allDraggables.AddRange(noDuplicates);
        }

        // Includes everything from both the holders, and what's set from the scene.

        protected List<Draggable2D> allDraggables = new List<Draggable2D>();

        protected virtual List<Draggable2D> GetDraggablesFromHolders()
        {
            List<Draggable2D> fromHolders = new List<Draggable2D>();

            foreach (Transform holder in draggableObjectHolders)
            {
                IList<Draggable2D> foundInHolder = holder.GetComponentsInChildren<Draggable2D>();
                fromHolders.AddRange(foundInHolder);
            }

            return fromHolders;
        }


        #region Compatibility

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            //add any dragableobject already present to list for backwards compatability
            if (draggableObject != null)
            {
                if (!allDraggables.Contains(draggableObject))
                {
                    allDraggables.Add(draggableObject);
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
            RefreshDraggableObjectList();

            if (allDraggables.Contains(draggableObject))
            {
                if (draggableRef != null)
                {
                    draggableRef.Value = draggableObject.gameObject;
                }
                ExecuteBlock();
            }
        }

        public override string GetSummary()
        {
            string summary = "Draggable: ";
            if (this.allDraggables != null && this.allDraggables.Count != 0)
            {
                for (int i = 0; i < this.allDraggables.Count; i++)
                {
                    if (allDraggables[i] != null)
                    {
                        summary += allDraggables[i].name + ",";
                    }
                }
                return summary;
            }
            else
            {
                return "None";
            }
        }

        #endregion Public members
    }
}