using System.Collections.Generic;

namespace Fungus
{
    public interface IDragEventHandler2D
    {
        /// <summary>
        /// All the draggables the event handler keeps track of.
        /// </summary>
        IList<Draggable2D> AllDraggables { get; }

        /// <summary>
        /// Whether or not this can respond to any draggable, even if its not among those specifically
        /// set for the event to respond to
        /// </summary>
        bool DraggableOptional { get; }
    }

}