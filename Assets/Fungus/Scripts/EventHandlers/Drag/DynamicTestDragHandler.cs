using UnityEngine;

namespace Fungus
{
    [EventHandlerInfo("Sprite",
                      "Some other drag handler",
                      @"Like normal Drag Entered, but for runtime-generated objects.")]
    [AddComponentMenu("")]
    public class DynamicTestDragHandler : DynamicDragEventHandler2D<DragStartedEvent>
    {
        protected override void OnMainDragEvent(DragStartedEvent evt)
        {
            throw new System.NotImplementedException();
        }
    }
}