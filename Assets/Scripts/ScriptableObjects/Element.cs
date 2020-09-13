using UnityEngine;

[CreateAssetMenu(fileName = "NewElement", menuName= "Shopkeep/Element")]
public class Element : ScriptableObject, EnumSO
{
    [SerializeField] Sprite icon = null;

    public string Name { get { return name; } }
    public Sprite Icon { get { return icon; } }
}