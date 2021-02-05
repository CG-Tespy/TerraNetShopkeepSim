using System.Collections.Generic;

namespace Fungus
{
    public interface IDragEventHandler2D
    {
        IList<Draggable2D> AllDraggables { get; }
    }
}