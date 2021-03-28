using UnityEngine;

/// <summary>
/// Displays a Battle Power in the UI.
/// </summary>
[RequireComponent(typeof(BattlePowerController))]
public class BattlePowerDisplayHub : DisplayHub<BattlePower>
{
    /// <summary>
    /// Alias for the DisplayBase.
    /// </summary>
    public virtual BattlePower BattlePower
    {
        get { return this.DisplayBase; }
        set { this.DisplayBase = value; }
    }

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<BattlePowerController>();
    }

    protected BattlePowerController controller = null;

    /// <summary>
    /// BattlePowerLoadout the BP this is displaying belongs to.
    /// </summary>
    public virtual BattlePowerLoadout Loadout
    {
        get { return controller.Loadout; }
        set { controller.Loadout = value; }
    }
}
