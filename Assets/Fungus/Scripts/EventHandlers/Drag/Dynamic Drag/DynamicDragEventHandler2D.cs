using UnityEngine;
using System.Collections.Generic;

namespace Fungus
{
    public abstract class DynamicDragEventHandler2D<TDragEvent> : DragEventHandler2D<TDragEvent>
        where TDragEvent: DragEvent2D
    {
        protected override void Awake()
        {
            base.Awake();
            KeepTrackOfDynamicObjects();
        }

        protected virtual void KeepTrackOfDynamicObjects()
        {
            dynamicDraggables.Update();
        }

        [SerializeField] protected Draggable2DManager dynamicDraggables;

        public override IList<Draggable2D> AllDraggables
        {
            get { return dynamicDraggables.AllObjects; }
        }

        protected override void OnMainDragEvent(TDragEvent evt)
        {
            KeepTrackOfDynamicObjects(); 
            // The response to the actual event should happen in subclasses, after
            // the objects are kept track of at the time
        }


    }

    public abstract class DynamicDragEventHandler2DWithTarget<TDragEvent> : DynamicDragEventHandler2D<TDragEvent>
        where TDragEvent : DragEvent2D
    {
        [VariableProperty(typeof(GameObjectVariable))]
        [SerializeField] protected GameObjectVariable targetRef;
        [SerializeField] protected bool targetOptional = false;

        protected override void KeepTrackOfDynamicObjects()
        {
            base.KeepTrackOfDynamicObjects();
            dynamicTargets.Update();
        }

        [SerializeField] protected Collider2DManager dynamicTargets;

        public virtual IList<Collider2D> AllTargets
        {
            get { return dynamicTargets.AllObjects; }
        }

        protected override void UpdateVarRefs(GameObject draggable, GameObject target)
        {
            base.UpdateVarRefs(draggable, target);
            if (targetRef != null)
                targetRef.Value = target;
        }

    }
}