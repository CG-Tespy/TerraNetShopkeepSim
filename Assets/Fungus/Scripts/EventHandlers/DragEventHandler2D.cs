using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
    public abstract class DragEventHandler2D : EventHandler, ISerializationCallbackReceiver
    {
        [SerializeField] protected bool isOnPrefab;

        [VariableProperty(typeof(GameObjectVariable))]
        [SerializeField] protected GameObjectVariable draggableRef;

        [Tooltip("Whether or not this has to respond only to the draggables specified.")]
        [SerializeField] protected bool draggableOptional = false;

        protected EventDispatcher eventDispatcher = null;

        protected virtual void Awake()
        {
            if (isOnPrefab) 
                // ^OnEnable doesn't trigger for prefabs right when they're instantiated, even if
                // they're set to be enabled by default
                OnEnable();
        }

        protected virtual void OnEnable()
        {
            if (Application.isPlaying)
            {
                UpdateEventDispatcher();
                ListenForDragEvents();
            }
        }

        protected virtual void UpdateEventDispatcher()
        {
            eventDispatcher = FungusManager.Instance.EventDispatcher;
        }

        protected abstract void ListenForDragEvents();

        protected virtual void OnDisable()
        {
            if (Application.isPlaying)
            {
                UnlistenForDragEvents();
            }
        }

        protected abstract void UnlistenForDragEvents();

        #region Compatibility

        public virtual void OnAfterDeserialize()
        {
        }

        public virtual void OnBeforeSerialize()
        {
        }

        #endregion

        protected virtual void UpdateVarRefs(GameObject draggable, GameObject target)
        {
            if (draggableRef != null)
                draggableRef.Value = draggable;
        }

    }

    public abstract class DragEventHandler2DWithTarget: DragEventHandler2D
    {
        [VariableProperty(typeof(GameObjectVariable))]
        [SerializeField] protected GameObjectVariable targetRef;
        [SerializeField] protected bool targetOptional = false;

        protected override void UpdateVarRefs(GameObject draggable, GameObject target)
        {
            base.UpdateVarRefs(draggable, target);

            if (targetRef != null)
                targetRef.Value = target;
        }
    }

    /// <summary>
    /// For drag event handlers that require you to set objects from the scene, as opposed to
    /// letting you work with runtime-generated objects
    /// </summary>
    public abstract class StaticDragEventHandler2D : DragEventHandler2D
    {
        [SerializeField] protected List<Draggable2D> draggableObjects;

        protected override void Awake()
        {
            HandleAwakeBackwardsCompat();
            base.Awake(); // We want OnEnable triggered AFTER all the other setup
        }

        protected virtual void HandleAwakeBackwardsCompat()
        {
            RegisterAlreadyPresentDraggable();
        }

        protected virtual void RegisterAlreadyPresentDraggable()
        {
            if (draggableObject != null && draggableObjects.Contains(draggableObject))
            {
                draggableObjects.Add(draggableObject);
            }

            draggableObject = null;
        }

        [Tooltip("Draggable object to listen for drag events on")]
        [HideInInspector]
        [SerializeField] protected Draggable2D draggableObject;
    }

    /// <summary>
    /// For static drag event handlers that consider both draggables and targets.
    /// </summary>
    public abstract class StaticDragEventHandler2DWithTarget: DragEventHandler2DWithTarget
    {
        [Tooltip("Drag target object to listen for drag events on")]
        [HideInInspector]
        [SerializeField] protected Collider2D targetObject;

        [SerializeField] protected List<Collider2D> targetObjects;
    }

    public abstract class DynamicDragEventHandler2D : DragEventHandler2D
    {
        [Header("For dynamically-generated Draggables")]
        [SerializeField] protected Draggable2DManager dynamicDraggables = new Draggable2DManager();

        protected abstract void KeepTrackOfSceneObjects();

        protected override void Awake()
        {
            dynamicDraggables.Update();
            base.Awake();
        }
    }


  
}
