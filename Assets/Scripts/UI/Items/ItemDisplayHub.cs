using System.Collections.Generic;

public class ItemDisplayHub : DisplayHub<Item>
{
    public static List<ItemDisplayHub> InScene { get; } = new List<ItemDisplayHub>();

    protected override void Awake()
    {
        base.Awake();
        InScene.Add(this);
    }

    protected virtual void OnDestroy()
    {
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
}
