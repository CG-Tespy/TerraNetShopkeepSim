using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
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
    public abstract class StaticDragEventHandler2DWithTarget : StaticDragEventHandler2D
    {
        [VariableProperty(typeof(GameObjectVariable))]
        [SerializeField] protected GameObjectVariable targetRef;
        [SerializeField] protected bool targetOptional = false;

        [Tooltip("Drag target object to listen for drag events on")]
        [HideInInspector]
        [SerializeField] protected Collider2D targetObject;

        [SerializeField] protected List<Collider2D> targetObjects;

        protected override void UpdateVarRefs(GameObject draggable, GameObject target)
        {
            base.UpdateVarRefs(draggable, target);

            if (targetRef != null)
                targetRef.Value = target;
        }
    }
}