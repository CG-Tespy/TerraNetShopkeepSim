using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemDisplayHub : DisplayHub<Item>
{
    [SerializeField] ItemPriceDisplay showsPrice;

    public static List<ItemDisplayHub> InScene { get; } = new List<ItemDisplayHub>();

    protected override void Awake()
    {
        base.Awake();
        InScene.Add(this);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        InScene.Remove(this);
    }

    /// <summary>
    /// Alias for the display base.
    /// </summary>
    public virtual Item Item
    {
        get { return this.DisplayBase; }
        set
        {
            this.DisplayBase = value;
        }
    }

    /// <summary>
    /// The price as shown by the price display.
    /// </summary>
    public virtual int Price
    {
        get { return showsPrice.PriceDisplayed; }
    }
}
