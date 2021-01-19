using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
    public abstract class DragEventHandler2D : EventHandler, ISerializationCallbackReceiver
    {
        [Header("Individually-set-from-the-scene draggables")]
        [SerializeField] protected List<Draggable2D> draggableObjects;

        [Header("Lets you pass the draggable and target into Fungus vars")]
        [VariableProperty(typeof(GameObjectVariable))]
        [SerializeField] protected GameObjectVariable draggableRef;

        [VariableProperty(typeof(GameObjectVariable))]
        [SerializeField] protected GameObjectVariable targetRef;


        protected virtual List<Draggable2D> AllDraggables
        {
            get { return dynamicDraggables.AllObjects; }
        }

        [Header("For dynamically-generated Draggables")]
        [SerializeField] protected Draggable2DManager dynamicDraggables = new Draggable2DManager();


        protected EventDispatcher eventDispatcher = null;

        protected virtual void Awake()
        {
            SetUpDynamicObjectHandlers();
            dynamicDraggables.Update();
            HandleAwakeBackwardsCompat();
        }

        protected virtual void SetUpDynamicObjectHandlers()
        {
            dynamicDraggables.IndividualObjects = draggableObjects;
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
                dynamicDraggables.Update();
            }

            draggableObject = null;
        }

        [Tooltip("Draggable object to listen for drag events on")]
        [HideInInspector]
        [SerializeField] protected Draggable2D draggableObject;

        protected virtual void OnEnable()
        {
            if (Application.isPlaying)
            {
                UpdateEventDispatcher();
                ListenForDragEvents();
                KeepTrackOfSceneObjects();
            }
        }

        protected virtual void UpdateEventDispatcher()
        {
            eventDispatcher = FungusManager.Instance.EventDispatcher;
        }

        protected abstract void ListenForDragEvents();

        protected abstract void KeepTrackOfSceneObjects();

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

            if (targetRef != null)
                targetRef.Value = target;
        }

    }


    public abstract class DragRelatedManager
    {
        public abstract void Update();
    }

    [System.Serializable]
    public abstract class DragRelatedManager<T> : DragRelatedManager
    {
        public List<T> IndividualObjects { get; set; }
        public List<T> AllObjects { get; set; } = new List<T>();
        public List<Transform> ObjectHolders
        {
            get { return objectHolders; }
        }
        [SerializeField] List<Transform> objectHolders;

        protected HashSet<T> noDuplicates = new HashSet<T>();

        public override void Update()
        {
            noDuplicates.Clear();

            noDuplicates.UnionWith(IndividualObjects);

            foreach (Transform holder in ObjectHolders)
            {
                var inHolder = holder.GetComponentsInChildren<T>();
                noDuplicates.UnionWith(inHolder);
            }

            AllObjects.Clear();
            AllObjects.AddRange(noDuplicates);
        }
    }

    [System.Serializable]
    public class Draggable2DManager : DragRelatedManager<Draggable2D> { }

    [System.Serializable]
    public class Collider2DManager : DragRelatedManager<Collider2D> { }

    public abstract class DragWithTargetEventHandler2D: DragEventHandler2D
    {

    }
}
