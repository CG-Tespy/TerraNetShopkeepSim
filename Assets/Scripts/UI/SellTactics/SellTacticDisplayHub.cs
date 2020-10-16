public class SellTacticDisplayHub : DisplayHub<SellTactic>
{
    /// <summary>
    /// Alias for the DisplayBase.
    /// </summary>
    public virtual SellTactic Tactic
    {
        get { return this.DisplayBase; }
        set { this.DisplayBase = value; }
    }
}
