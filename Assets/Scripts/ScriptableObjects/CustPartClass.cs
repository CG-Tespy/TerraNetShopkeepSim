using UnityEngine;

[CreateAssetMenu(fileName = "NewCustPartClass", menuName= "Shopkeep/Cust Part Class")]
public class CustPartClass : ScriptableObject, EnumSO
{
    [SerializeField] Sprite icon = null;

    public string Name { get { return name; } }
    public Sprite Icon { get { return icon; } }
}