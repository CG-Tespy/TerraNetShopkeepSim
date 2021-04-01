using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using Action = System.Action;

[CommandInfo("Shopkeep/Property", "Item Property", "Helps assign properties from items to vars")]
public class ItemProperty : BaseVariableProperty
{
    public enum Property
    {
        Price,
    }

    [SerializeField] protected Property property = Property.Price;

    [Tooltip("The item to get or set a property of")]
    [SerializeField] protected ItemData itemData;

    [SerializeField]
    [VariableProperty(typeof(IntegerVariable), 
                    typeof(ItemVariable))]
    protected Variable inOutVar;

    protected virtual void Awake()
    {
        PrepareDicts();
    }

    protected virtual void PrepareDicts()
    {
        PrepareOperationDict();
        PrepareGetOperations();
        PrepareSetOperations();
    }

    protected virtual void PrepareOperationDict()
    {
        operations[GetSet.Get] = getOperations;
        operations[GetSet.Set] = setOperations;
    }

    protected Dictionary<GetSet, Dictionary<Property, Action>> operations = new Dictionary<GetSet, Dictionary<Property, Action>>();
    protected Dictionary<Property, Action> getOperations = new Dictionary<Property, Action>();
    protected Dictionary<Property, Action> setOperations = new Dictionary<Property, Action>();

    protected virtual void PrepareGetOperations()
    {
        // Where things are gotten from the item, and set to the inOutVar
        getOperations[Property.Price] = GetPrice;
    }

    protected virtual void GetPrice()
    {
        int priceToGet = itemData.Value.Price;
        var whereToApplyPrice = inOutInt;
        whereToApplyPrice.Value = priceToGet;
    }

    protected IntegerVariable inOutInt;

    protected virtual void PrepareSetOperations()
    {
        // Where what's in the inOutVar gets applied to the item
        setOperations[Property.Price] = SetPrice;
    }

    protected virtual void SetPrice()
    {
        string message = "Cannot Set price of an Item directly!";
        throw new System.InvalidOperationException(message);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        UpdateVariables();
        ApplyOperation();
        Continue();
    }

    protected virtual void UpdateVariables()
    {
        inOutInt = inOutVar as IntegerVariable;
        inOutItem = inOutVar as ItemVariable;
    }

    
    protected ItemVariable inOutItem;

    protected virtual void ApplyOperation()
    {
        var hasWhatWeWant = operations[getOrSet];
        var operationToApply = hasWhatWeWant[property];
        operationToApply();
    }
}
