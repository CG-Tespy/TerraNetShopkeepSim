using System.Collections.Generic;
using UnityEngine;
using Fungus;
using Action = System.Action;
using PropertyAction = System.Action<Draggable2DProperty.Property>;

// <summary>
/// Get or Set a property of a Transform component
/// </summary>
[CommandInfo("Property",
             "Draggable2D",
             "Get or Set a property of a Draggable2D component through a GameObject variable")]
public class Draggable2DProperty : BaseVariableProperty
{
    public enum Property
    {
        InScreenSpace,
        ReturnPosition
    }

    [SerializeField]
    protected Property property = Property.InScreenSpace;

    [SerializeField]
    protected GameObjectData gameObjectData;

    [SerializeField]
    [VariableProperty(typeof(Vector3Variable),
                          typeof(BooleanVariable))]
    protected Variable inOutVar;

    protected virtual void Awake()
    {
        PrepareOperationDict(); // We execute the operations with a dict instead of if statements, all for better performance
    }

    protected virtual void PrepareOperationDict()
    {
        applyOperations[GetSet.Get] = getOperations.Apply;
        applyOperations[GetSet.Set] = setOperations.Apply;
    }

    protected Dictionary<GetSet, PropertyAction> applyOperations = new Dictionary<GetSet, PropertyAction>();

    protected class GetOperations
    {
        public GetOperations()
        {
            LinkOperationsToProperties();
        }

        protected virtual void LinkOperationsToProperties()
        {
            operations[Property.InScreenSpace] = GetInScreenSpace;
            operations[Property.ReturnPosition] = GetReturnPosition;
        }

        protected Dictionary<Property, Action> operations = new Dictionary<Property, Action>();

        protected virtual void GetInScreenSpace()
        {
            inOutBool.Value = Target.InScreenSpace;
        }

        protected BooleanVariable inOutBool;
        public Draggable2D Target { get; set; }

        protected virtual void GetReturnPosition()
        {
            inOutVec3.Value = Target.ReturnPosition;
        }

        protected Vector3Variable inOutVec3;

        public virtual Variable InOutVar
        {
            set
            {
                inOutBool = value as BooleanVariable;
                inOutVec3 = value as Vector3Variable;
            }
        }

        public virtual void Apply(Property property)
        {
            operations[property]();
        }

    }

    protected GetOperations getOperations = new GetOperations();

    protected class SetOperations
    {
        public SetOperations()
        {
            LinkOperationsToProperties();
        }

        protected virtual void LinkOperationsToProperties()
        {
            operations[Property.InScreenSpace] = SetInScreenSpace;
            operations[Property.ReturnPosition] = SetReturnPosition;
        }

        protected Dictionary<Property, Action> operations = new Dictionary<Property, Action>();

        protected virtual void SetInScreenSpace()
        {
            Target.InScreenSpace = inOutBool.Value;
        }

        protected BooleanVariable inOutBool;
        public Draggable2D Target { get; set; }

        protected virtual void SetReturnPosition()
        {
            Target.ReturnPosition = inOutVec3.Value;
        }

        protected Vector3Variable inOutVec3;

        public virtual Variable InOutVar
        {
            set
            {
                inOutBool = value as BooleanVariable;
                inOutVec3 = value as Vector3Variable;
            }
        }

        public virtual void Apply(Property property)
        {
            operations[property]();
        }
    }

    protected SetOperations setOperations = new SetOperations();

    public override void OnEnter()
    {
        base.OnEnter();

        GetDraggable();
        UpdateOperationDependencies();
        applyOperations[getOrSet](property);
        Continue();

    }

    protected virtual void GetDraggable()
    {
        draggable = gameObjectData.Value.GetComponent<Draggable2D>();
    }

    protected Draggable2D draggable = null;

    protected virtual void UpdateOperationDependencies()
    {
        getOperations.Target = setOperations.Target = draggable;
        getOperations.InOutVar = setOperations.InOutVar = inOutVar;
    }
        
    public override string GetSummary()
    {
        var invalidGameObject = gameObjectData.Value == null || gameObjectData.Value.GetComponent<Draggable2D>() == null;

        if (invalidGameObject)
        {
            return "Error: no Game Object with Draggable2D Set";
        }

        if (inOutVar == null)
        {
            return "Error: no variable set to push or pull data to or from";
        }

        return getOrSet.ToString() + " " + property.ToString();
    }


}