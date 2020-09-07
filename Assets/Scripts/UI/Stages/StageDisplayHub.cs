using System.Collections.Generic;

public class StageDisplayHub : DisplayHub<Stage>
{
    public static List<StageDisplayHub> InScene { get; } = new List<StageDisplayHub>();

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
    public virtual Stage Stage
    {
        get { return this.DisplayBase; }
        set
        {
            this.DisplayBase = value;
        }
    }
}
