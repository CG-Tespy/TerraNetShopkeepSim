using UnityEngine;

[CreateAssetMenu(fileName = "NewSellTactic", menuName = "Shopkeep/Sell Tactic")]
public class SellTactic : EnumScriptableObject
{
    [Tooltip("How much the effective sale price is cut when this displeases the customer. This is a percentage.")]
    [SerializeField] float failurePenalty = 10f;
    [Tooltip("How much the effective sale price is boosted when this succeeds. This is a percentage.")]
    [SerializeField] float successBoost = 10f;

    public float FailurePenalty
    {
        get { return failurePenalty; }
    }

    public float SuccessBoost
    {
        get { return successBoost; }
    }
}
