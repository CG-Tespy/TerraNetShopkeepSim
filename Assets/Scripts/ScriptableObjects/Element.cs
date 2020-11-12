using UnityEngine;

[CreateAssetMenu(fileName = "NewElement", menuName= "Shopkeep/Element")]
public class Element : EnumScriptableObject, EnumSO
{
    public override string ToString()
    {
        return Name;
    }
}