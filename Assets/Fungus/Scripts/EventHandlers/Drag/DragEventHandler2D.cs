using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

namespace Fungus
{
    public abstract class DragEventHandler2D<TDragEvent> : EventHandler, 
        ISerializationCallbackReceiver, IDragEventHandler2D
        where TDragEvent: DragEvent2D
    {
        [VariableProperty(typeof(GameObjectVariable))]
        [SerializeField] protected GameObjectVariable draggableRef;

        [Tooltip("Whether or not this has to respond only to the draggables specified.")]
        [SerializeField] protected bool draggableOptional = false;

        protected EventDispatcher eventDispatcher = null;

        public abstract IList<Draggable2D> AllDraggables { get; }

        protected virtual void Awake() { }

        protected virtual void OnEnable()
        {
            Debug.Log("Drag event handler on enable!");
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

        protected virtual void ListenForDragEvents()
        {
            eventDispatcher.AddListener<TDragEvent>(OnMainDragEvent);
        }

        /// <summary>
        /// The base response to the event this handler mainly listens for. 
        /// Delegates the work to another func based on what it was passed.
        /// </summary>
        protected abstract void OnMainDragEvent(TDragEvent evt);


        protected virtual void OnDisable()
        {
            if (Application.isPlaying)
            {
                UnlistenForDragEvents();
            }
        }

        protected virtual void UnlistenForDragEvents()
        {
            eventDispatcher.RemoveListener<TDragEvent>(OnMainDragEvent);
        }

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

        public override string GetSummary()
        {
            summary.Clear();

            bool noValidDraggables = AllDraggables.Count(NotCountingNulls) == 0;
            bool reportNoValidDraggables = !draggableOptional && noValidDraggables;

            if (reportNoValidDraggables)
            {
                summary.Append("Error: no valid draggable objects assigned.");
            }
            else
            {
                summary.Append("Draggable(s): ");
                summary.Append(GetNamesCommaSeparated(AllDraggables as IList<Object>));
            }

            return summary.ToString();
        }

        protected StringBuilder summary = new StringBuilder(); // Performance

        protected virtual bool NotCountingNulls(object obj) { return obj != null; }

        protected virtual string GetNamesCommaSeparated(IList<Object> objsWithNames)
        {
            commaSeparatedNames.Clear();

            for (int i = 0; i < objsWithNames.Count; i++)
            {
                var objEl = objsWithNames[i];

                if (objEl != null)
                {
                    commaSeparatedNames.Append(objEl.name);
                    commaSeparatedNames.Append(", ");
                }
            }

            return commaSeparatedNames.ToString();
        }

        protected StringBuilder commaSeparatedNames = new StringBuilder();

    }

    public abstract class DragEventHandler2DWithTarget<TDragEvent> : DragEventHandler2D<TDragEvent>, IDragAndTargetEventHandler2D
        where TDragEvent: DragEvent2D
    {
        [VariableProperty(typeof(GameObjectVariable))]
        [SerializeField] protected GameObjectVariable targetRef;
        [SerializeField] protected bool targetOptional = false;

        public abstract IList<Collider2D> AllTargets { get; }

        public override string GetSummary()
        {
            summary.Clear();

            bool noValidTargets = AllTargets.Count(NotCountingNulls) == 0;
            bool reportNoValidTargets = !targetOptional && noValidTargets;

            if (reportNoValidTargets)
            {
                summary.Append("Error: no valid target objects assigned.");
            }
            else
            {
                summary.Append(base.GetSummary());
                summary.Append("Target(s): ");
                summary.Append(GetNamesCommaSeparated(AllTargets as IList<Object>));
            }

            return summary.ToString();
        }
    }
}
