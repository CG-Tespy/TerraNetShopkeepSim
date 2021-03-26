using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNaviDatabase", menuName = "Shopkeep/NaviDatabase")]
public class NaviDatabase : CollectionSO<Navi>
{
    /// <summary>
    /// Alias for the Contents
    /// </summary>
    public virtual IList<Navi> Navis { get { return Contents; } }
}
