using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeDisplayHub : ItemDisplayHub
{
    /// <summary>
    /// Alias for the display base.
    /// </summary>
    public virtual Recipe Recipe
    {
        get { return (Recipe) this.DisplayBase; }
        set { this.DisplayBase = value; }
    }
}
