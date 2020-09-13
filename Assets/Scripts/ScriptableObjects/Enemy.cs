using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Shopkeep/Enemy")]
public class Enemy : ScriptableObject
{
    [SerializeField] int hp = 10, atk = 10, spd = 10;
    [TextArea(5, 10)]
    [SerializeField] string description = "";
    [SerializeField] Item[] matsDropped = null;
    [Tooltip("Drop chances for the mats. The chance for the first mat is the first element of this.")]
    [SerializeField] float[] dropChances = null;
    public string Name { get { return name; } }
    public IList<Item> MatsDropped { get { return matsDropped; } }
    public float[] DropChances { get { return dropChances; } }

}