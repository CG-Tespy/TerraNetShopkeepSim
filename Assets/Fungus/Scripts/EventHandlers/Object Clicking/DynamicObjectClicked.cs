using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
    [EventHandlerInfo("Sprite", 
        "Object Clicked (Dynamic)",
        "Like regular Object Clicked, but can apply to programmatically-generated Clickables.")]
    public class DynamicObjectClicked : EventHandler
    {
        [Tooltip("Clickables parented to these will have their clicks responded to by this Block")]
        [SerializeField] protected Transform[] clickableHolders;

        [Tooltip("Wait for a number of frames before executing the block.")]
        [SerializeField] protected int waitFrames = 1;

        [Tooltip("Game Object Variable that the clicked object gets assigned to")]
        [VariableProperty(typeof(GameObjectVariable))]
        [SerializeField] protected GameObjectVariable clickedObject;


        protected EventDispatcher eventDispatcher;

        protected virtual void OnEnable()
        {
            eventDispatcher = FungusManager.Instance.EventDispatcher;

            eventDispatcher.AddListener<ObjectClickedEvent>(OnObjectClickedEvent);
        }

        protected virtual void OnDisable()
        {
            eventDispatcher.RemoveListener<ObjectClickedEvent>(OnObjectClickedEvent);

            eventDispatcher = null;
        }

        void OnObjectClickedEvent(ObjectClickedEvent evt)
        {
            OnObjectClicked(evt.ClickableObject);
        }

        /// <summary>
        /// Called by the Clickable2D object when it is clicked.
        /// </summary>
        public virtual void OnObjectClicked(Clickable2D clickableObject)
        {
            UpdateAllClickables();

            if (allClickables.Contains(clickableObject))
            {
                clickedObject.Value = clickableObject.gameObject;
                StartCoroutine(DoExecuteBlock(waitFrames));
            }
        }

        protected virtual void UpdateAllClickables()
        {
            allClickables.Clear();

            foreach (Transform holder in clickableHolders)
            {
                IList<Clickable2D> clickables = holder.GetComponentsInChildren<Clickable2D>();
                allClickables.UnionWith(clickables);
            }
        }

        protected HashSet<Clickable2D> allClickables = new HashSet<Clickable2D>();

        /// <summary>
        /// Executing a block on the same frame that the object is clicked can cause
        /// input problems (e.g. auto completing Say Dialog text). A single frame delay 
        /// fixes the problem.
        /// </summary>
        protected virtual IEnumerator DoExecuteBlock(int numFrames)
        {
            if (numFrames == 0)
            {
                ExecuteBlock();
                yield break;
            }

            int count = Mathf.Max(waitFrames, 1);
            while (count > 0)
            {
                count--;
                yield return new WaitForEndOfFrame();
            }

            ExecuteBlock();
        }


        
    }
}