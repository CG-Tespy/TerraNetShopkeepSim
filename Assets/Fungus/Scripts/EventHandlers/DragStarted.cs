// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using System.Collections.Generic;
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
    public class DragStarted : EventHandler, ISerializationCallbackReceiver
    {
        public class DragStartedEvent
        {
            public Draggable2D DraggableObject;

            public DragStartedEvent(Draggable2D draggableObject)
            {
                DraggableObject = draggableObject;
            }
        }

        [VariableProperty(typeof(GameObjectVariable))]
        [SerializeField] protected GameObjectVariable draggableRef;

        // The ones you set from the scene
        [SerializeField] protected List<Draggable2D> draggableObjects;

        [Tooltip("These have Draggables parented to them. You'll want to use this for programmatically-generated draggables.")]
        [SerializeField] protected List<Transform> draggableObjectHolders;

        // Includes everything from both the holders, and what's set from the scene.
        // We're using a Hash Set to avoid duplicates
        protected HashSet<Draggable2D> allDraggables = new HashSet<Draggable2D>();

        [HideInInspector]
        [SerializeField] protected Draggable2D draggableObject;

        protected EventDispatcher eventDispatcher;

        protected virtual void OnEnable()
        {
            eventDispatcher = FungusManager.Instance.EventDispatcher;
            RefreshDraggableObjectList();

            eventDispatcher.AddListener<DragStartedEvent>(OnDragStartedEvent);
        }


        /// <summary>
        /// You'll mainly want to use this to keep the draggables list updated with the 
        /// programmatically-generated stuff
        /// </summary>
        protected virtual void RefreshDraggableObjectList()
        {
            allDraggables.Clear();
            allDraggables.UnionWith(draggableObjects);
            var draggablesFromHolders = GetDraggablesFromHolders();
            allDraggables.UnionWith(draggablesFromHolders);

        }

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

        protected virtual void OnDisable()
        {
            eventDispatcher.RemoveListener<DragStartedEvent>(OnDragStartedEvent);

            eventDispatcher = null;
        }

        private void OnDragStartedEvent(DragStartedEvent evt)
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

            if (summary.Length == 0)
            {
                return "None";
            }

            return summary;
        }

        #endregion Public members
    }
}