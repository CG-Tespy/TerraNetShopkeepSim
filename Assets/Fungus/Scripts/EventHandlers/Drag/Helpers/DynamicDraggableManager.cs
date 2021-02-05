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

        public IList<T> AllObjects
        {
            get { return allObjects; }
        }

        [Tooltip("All draggables parented to the object holders. Updated on Awake and when the appropriate drag events occur.")]
        [SerializeField] List<T> allObjects = new List<T>();

        public override void Update()
        {
            RetrieveAllValidHolders();
            RegisterAllValidHolders();
        }

        protected virtual void RetrieveAllValidHolders()
        {
            noDuplicates.Clear();

            foreach (Transform holder in ObjectHolders)
            {
                var inHolder = holder.GetComponentsInChildren<T>();
                noDuplicates.UnionWith(inHolder);
            }
        }

        protected HashSet<T> noDuplicates = new HashSet<T>();

        protected virtual void RegisterAllValidHolders()
        {
            allObjects.Clear();
            allObjects.AddRange(noDuplicates);
        }
    }

    [System.Serializable]
    public class Draggable2DManager : DynamicDraggableManager<Draggable2D> { }

    [System.Serializable]
    public class Collider2DManager : DynamicDraggableManager<Collider2D> { }
}
