using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{

    public abstract class DynamicDraggableManager
    {
        public abstract void Update();
    }

    [System.Serializable]
    public abstract class DynamicDraggableManager<T> : DynamicDraggableManager
    {
        public List<Transform> ObjectHolders
        {
            get { return objectHolders; }
        }

        [Tooltip("Draggables parented to any of these will be responded to.")]
        [SerializeField] List<Transform> objectHolders;

        public ICollection<T> AllObjects
        {
            get { return allObjects; }
            set
            {
                // Set the contents, rather than the list itself
                allObjects.Clear();
                allObjects.AddRange(value);
            }
        }

        [Tooltip("All draggables parented to the object holders. Updated on Awake and when the appropriate drag events occur.")]
        [SerializeField] List<T> allObjects = new List<T>();

        public override void Update()
        {
            noDuplicates.Clear();

            foreach (Transform holder in ObjectHolders)
            {
                var inHolder = holder.GetComponentsInChildren<T>();
                noDuplicates.UnionWith(inHolder);
            }

            AllObjects = noDuplicates;
        }

        protected HashSet<T> noDuplicates = new HashSet<T>();
    }

    [System.Serializable]
    public class Draggable2DManager : DynamicDraggableManager<Draggable2D> { }

    [System.Serializable]
    public class Collider2DManager : DynamicDraggableManager<Collider2D> { }
}
