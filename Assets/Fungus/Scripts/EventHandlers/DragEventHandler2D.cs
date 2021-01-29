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

}
