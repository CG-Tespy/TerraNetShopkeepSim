/// <summary>
/// Displays a Battle Power in the UI.
/// </summary>
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
}
