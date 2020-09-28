using UnityEngine;
using System.Collections.Generic;

/**
 * For defining the various enemy types in the game, through
 * the Unity editor. The actual enemy instances are created from these..
 */
[CreateAssetMenu(fileName = "NewEnemy", menuName = "Shopkeep/Battle/Enemy")]
public class EnemyType : FighterType
{
    [SerializeField] Item[] matsDropped = null;
    [Tooltip("Drop chances for the mats. The chance for the first mat is the first element of this.")]
    [SerializeField] float[] dropChances = null;


    public IList<Item> MatsDropped { get { return matsDropped; } }
    public float[] DropChances { get { return dropChances; } }

}
