using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
