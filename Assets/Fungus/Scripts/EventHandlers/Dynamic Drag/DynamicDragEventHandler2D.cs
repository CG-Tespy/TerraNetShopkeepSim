using UnityEngine;

namespace Fungus
{
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