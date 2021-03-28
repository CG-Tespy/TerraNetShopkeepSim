using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
    public class DragEnterBase : DragEventHandler2D<DragEnteredEvent>
    {
        public override IList<Draggable2D> AllDraggables => throw new System.NotImplementedException();

        protected override void ListenForDragEvents()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnMainDragEvent(DragEnteredEvent evt)
        {
            throw new System.NotImplementedException();
        }

        protected override void UnlistenForDragEvents()
        {
            throw new System.NotImplementedException();
        }
    }
}