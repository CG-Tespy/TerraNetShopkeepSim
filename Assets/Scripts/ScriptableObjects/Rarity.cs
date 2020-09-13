using UnityEngine;

[CreateAssetMenu(fileName = "NewRarity", menuName= "Shopkeep/Rarity")]
public class Rarity : ScriptableObject, EnumSO
{
    [SerializeField] Sprite icon;

    public string Name { get { return name; } }
    public Sprite Icon { get { return icon; } }

}
