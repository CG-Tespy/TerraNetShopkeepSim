using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public interface IDisplayHub
{
    UnityEvent Clicked { get; }
    void UpdateDisplayComponents();
}

/// <summary>
/// Provides functionality all display hubs have, regardless of what they're
/// set to display things for.
/// </summary>
public abstract class DisplayHub : Selectable, IDisplayHub, IPointerClickHandler
{
    public UnityEvent Clicked { get; } = new UnityEvent();
    public static DisplayHubEvent AnyClicked { get; } = new DisplayHubEvent();

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!this.interactable)
            return;

        AnyClicked.Invoke(this);
        this.Clicked.Invoke();
    }

    public abstract void UpdateDisplayComponents();
}

public abstract class DisplayHub<TToDisplay> : DisplayHub, IDisplayHub, IPointerClickHandler
{
    /// <summary>
    /// The object that gets displayed in some way by this hub's subcomponents.
    /// </summary>
    public virtual TToDisplay DisplayBase
    {
        get { return displayBase; }
        set
        {
            displayBase = value;
            UpdateDisplayComponents();
        }
    }

    TToDisplay displayBase;

    public override void UpdateDisplayComponents()
    {
        // We let the subcomponents do their own thing, after giving them
        // whatever holds the data they want to work with.
        foreach (var component in displayComponents)
            component.DisplayBase = DisplayBase;
    }

    private List<DisplayComponent<TToDisplay>> displayComponents = new List<DisplayComponent<TToDisplay>>();

    protected override void Awake()
    {
        base.Awake();
        FetchComponentHolder();
    }

    protected virtual void FetchComponentHolder()
    {
        if (componentHolder == null)
            componentHolder = transform;
    }

    [SerializeField] Transform componentHolder = null;

    protected override void Start()
    {
        base.Start();
        // By the time this func is called, all the display components should be there
        FetchDisplayComponents();
        UpdateDisplayComponents();
    }

    protected virtual void FetchDisplayComponents()
    {
        var componentArr = componentHolder.GetComponentsInChildren<DisplayComponent<TToDisplay>>();
        displayComponents.AddRange(componentArr);
    }

}

public class DisplayHubEvent: UnityEvent<IDisplayHub> { }
