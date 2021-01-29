// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using System.Collections.Generic;
using Action = System.Action;
using IEnumerator = System.Collections.IEnumerator;

namespace Fungus
{
    /// <summary>
    /// Detects drag and drop interactions on a Game Object, and sends events to all Flowchart event handlers in the scene.
    /// The Game Object must have Collider2D & RigidBody components attached. 
    /// The Collider2D must have the Is Trigger property set to true.
    /// The RigidBody would typically have the Is Kinematic property set to true, unless you want the object to move around using physics.
    /// Use in conjunction with the Drag Started, Drag Completed, Drag Cancelled, Drag Entered & Drag Exited event handlers.
    /// </summary>
    public class Draggable2D : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Tooltip("Is object dragging enabled")]
        [SerializeField] protected bool dragEnabled = true;

        [Tooltip("Move object back to its starting position when drag is cancelled")]
        [FormerlySerializedAs("returnToStartPos")]
        [SerializeField] protected bool returnOnCancelled = true;

        [Tooltip("Move object back to its starting position when drag is completed")]
        [SerializeField] protected bool returnOnCompleted = true;

        [Tooltip("Time object takes to return to its starting position")]
        [SerializeField] protected float returnDuration = 1f;

        [Tooltip("Mouse texture to use when hovering mouse over object")]
        [SerializeField] protected Texture2D hoverCursor;

        [Tooltip("Use the UI Event System to check for drag events. Clicks that hit an overlapping UI object will be ignored. Camera must have a PhysicsRaycaster component, or a Physics2DRaycaster for 2D colliders.")]
        [SerializeField] protected bool useEventSystem;

        [Tooltip("Whether or not this exists in Screen Space, like UI elements usually are. Make sure this is correct, or you'll " +
            "get some funky behavior.")]
        [SerializeField] protected bool inScreenSpace = false;

        [Tooltip("For when you want the object to be rendered undraggable while returning to its original position.")]
        [SerializeField] protected bool disableDragDuringReturn = false;

        protected Vector3 startingPosition; // So we know where to move this if returnOnCompleted is true
        protected bool updatePosition = false;
        protected Vector3 newPosition;
        protected Vector3 mouseMovement = Vector3.zero;

        public virtual bool InScreenSpace
        {
            get { return inScreenSpace; }
            set { inScreenSpace = value; }
        }

        public virtual Vector3 StartingPosition
        {
            get { return startingPosition; }
        }

        protected virtual void Awake()
        {
            CacheMainCamera(); // Performance
            SetLocalPositionUpdaters();
            startingPosition = transform.position;
        }

        protected virtual void CacheMainCamera()
        {
            mainCamera = Camera.main;
        }

        protected Camera mainCamera;

        protected virtual void SetLocalPositionUpdaters()
        {
            // The boolean corresponds to inScreenSpace
            newPositionUpdaters[true] = UpdatePositionForUI;
            newPositionUpdaters[false] = UpdatePositionForSprites;
        }

        protected Dictionary<bool, Action> newPositionUpdaters = new Dictionary<bool, Action>();

        protected virtual void UpdatePositionForUI()
        {
            mouseMovement = Input.mousePosition - prevMousePosition;
            newPosition += mouseMovement;
        }

        protected virtual void UpdatePositionForSprites()
        {
            mouseMovement = mainCamera.ScreenToWorldPoint(Input.mousePosition) - mainCamera.ScreenToWorldPoint(prevMousePosition);
            newPosition += mouseMovement;
        }

        #region DragCompleted handlers
        protected List<DragCompleted> dragCompletedHandlers = new List<DragCompleted>();

        public void RegisterHandler(DragCompleted handler)
        {
            dragCompletedHandlers.Add(handler);
        }

        public void UnregisterHandler(DragCompleted handler)
        {
            if (dragCompletedHandlers.Contains(handler))
            {
                dragCompletedHandlers.Remove(handler);
            }
        }
        #endregion

        protected virtual void LateUpdate()
        {
            // iTween will sometimes override the object position even if it should only be affecting the scale, rotation, etc.
            // To make sure this doesn't happen, we force the position change to happen in LateUpdate.
            if (updatePosition)
            {
                transform.position = newPosition;
                updatePosition = false;
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (!dragEnabled)
            {
                return;
            }

            var eventDispatcher = FungusManager.Instance.EventDispatcher;

            eventDispatcher.Raise(new DragEntered.DragEnteredEvent(this, other));
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            if (!dragEnabled)
            {
                return;
            }

            var eventDispatcher = FungusManager.Instance.EventDispatcher;

            eventDispatcher.Raise(new DragExited.DragExitedEvent(this, other));
        }

        protected virtual void DoBeginDrag()
        {
            if (!dragEnabled) // For when you want drag to be disabled during return
                return;

            ResetCachedPositions();

            var eventDispatcher = FungusManager.Instance.EventDispatcher;
            eventDispatcher.Raise(new DragStarted.DragStartedEvent(this));
        }


        protected virtual void ResetCachedPositions()
        {
            startingPosition = transform.position;
            prevPosition = transform.position;
            newPosition = transform.position;
            prevMousePosition = Input.mousePosition;
        }

        Vector3 prevPosition = Vector3.zero, prevMousePosition = Vector3.zero;

        protected virtual void DoDrag()
        {
            if (!dragEnabled)
            {
                return;
            }

            UpdateNewPosition();

            prevMousePosition = Input.mousePosition; // For the next frame's drag operations
            prevPosition = transform.position;
            updatePosition = true;
        }

        protected virtual void UpdateNewPosition()
        {
            var updater = newPositionUpdaters[inScreenSpace];
            updater();
        }

        protected virtual void DoEndDrag()
        {
            if (!dragEnabled)
            {
                return;
            }

            var eventDispatcher = FungusManager.Instance.EventDispatcher;
            bool dragCompleted = false;

            for (int i = 0; i < dragCompletedHandlers.Count; i++)
            {
                var handler = dragCompletedHandlers[i];
                if (handler != null && handler.DraggableObjects.Contains(this))
                {
                    if (handler.IsOverTarget())
                    {
                        dragCompleted = true;

                        eventDispatcher.Raise(new DragCompleted.DragCompletedEvent(this));
                    }
                }
            }

            if (!dragCompleted)
            {
                eventDispatcher.Raise(new DragCancelled.DragCancelledEvent(this));

                if (returnOnCancelled)
                {
                    LeanTween.move(gameObject, startingPosition, returnDuration).setEase(LeanTweenType.easeOutExpo);
                    StartCoroutine(DisableDraggingFor(returnDuration));
                }
            }
            else if (returnOnCompleted)
            {
                LeanTween.move(gameObject, startingPosition, returnDuration).setEase(LeanTweenType.easeOutExpo);
                StartCoroutine(DisableDraggingFor(returnDuration));
            }
        }

        protected virtual IEnumerator DisableDraggingFor(float duration)
        {
            dragEnabled = false;
            yield return new WaitForSeconds(duration);
            dragEnabled = true;
        }

        protected virtual void DoPointerEnter()
        {
            ChangeCursor(hoverCursor);
        }

        protected virtual void DoPointerExit()
        {
            SetMouseCursor.ResetMouseCursor();
        }

        protected virtual void ChangeCursor(Texture2D cursorTexture)
        {
            if (!dragEnabled)
            {
                return;
            }

            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        }

        #region Legacy OnMouseX methods

        protected virtual void OnMouseDown()
        {
            if (!useEventSystem)
            {
                DoBeginDrag();
            }
        }

        protected virtual void OnMouseDrag()
        {
            if (!useEventSystem)
            {
                DoDrag();
            }
        }

        protected virtual void OnMouseUp()
        {
            if (!useEventSystem)
            {
                DoEndDrag();
            }
        }

        protected virtual void OnMouseEnter()
        {
            if (!useEventSystem)
            {
                DoPointerEnter();
            }
        }

        protected virtual void OnMouseExit()
        {
            if (!useEventSystem)
            {
                DoPointerExit();
            }
        }

        #endregion

        #region Public members

        /// <summary>
        /// Is object drag and drop enabled.
        /// </summary>
        /// <value><c>true</c> if drag enabled; otherwise, <c>false</c>.</value>
        public virtual bool DragEnabled { get { return dragEnabled; } set { dragEnabled = value; } }

        #endregion

        #region IBeginDragHandler implementation

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (useEventSystem && dragEnabled)
            {
                DoBeginDrag();
            }
        }

        #endregion

        #region IDragHandler implementation

        public void OnDrag(PointerEventData eventData)
        {
            if (useEventSystem)
            {
                DoDrag();
            }
        }

        #endregion

        #region IEndDragHandler implementation

        public void OnEndDrag(PointerEventData eventData)
        {
            if (useEventSystem)
            {
                DoEndDrag();
            }
        }

        #endregion

        #region IPointerEnterHandler implementation

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (useEventSystem)
            {
                DoPointerEnter();
            }
        }

        #endregion

        #region IPointerExitHandler implementation

        public void OnPointerExit(PointerEventData eventData)
        {
            if (useEventSystem)
            {
                DoPointerExit();
            }
        }

        #endregion
    }
} 