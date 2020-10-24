using UnityEngine;

/// <summary>
/// Contains data about sell results in a way that Flowcharts can easily
/// interact with.
/// </summary>
public class SellResults : MonoBehaviour
{
    [SerializeField] int basePrice;
    [SerializeField] float customerResponse, endMultiplier;
    [SerializeField] int moneyMade;
    public int BasePrice
    {
        get { return basePrice; }
        set { basePrice = value; }
    }
    public float CustomerResponse
    {
        get { return customerResponse; }
        set 
        { 
            customerResponse = value;
            EndMultiplier = value;
        }
    }
    public float EndMultiplier
    {
        get { return endMultiplier; }
        set { endMultiplier = value; }
    }
    public int MoneyMade
    {
        get { return (int) (BasePrice * EndMultiplier); }
    }

    // Need these, since Invoke Method won't work with properties
    public void SetBasePrice(int val) { BasePrice = val; }
    public void SetCustomerResponse(float val) { CustomerResponse = val; }
    public void SetEndMultiplier(float val) { EndMultiplier = val; }
        
    public int GetBasePrice() { return BasePrice; }
    public float GetCustomerResponse() { return CustomerResponse; }
    public float GetEndMultiplier() { return EndMultiplier; }
    public int GetMoneyMade() { return MoneyMade; }
}
