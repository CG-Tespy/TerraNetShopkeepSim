using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Property", "Item Property", "Helps assign properties from items to vars")]
public class ItemProperty : BaseVariableProperty
{
    public enum Property
    {
        ChildCount,
        EulerAngles,
        Forward,
        HasChanged,
        HierarchyCapacity,
        HierarchyCount,
        LocalEulerAngles,
        LocalPosition,
        LocalScale,
        LossyScale,
        Parent,
        Position,
        Right,
        Root,
        Up,
        Rotation,
        LocalRotation,
        WorldToLocalMatrix,
        LocalToWorldMatrix,

        SiblingIndex,
    }

    [Tooltip("The item to get or set a property of")]
    [SerializeField] protected ItemData itemData;


    [SerializeField]
    [VariableProperty(typeof(Vector3Variable),
                          typeof(QuaternionVariable),
                          typeof(TransformVariable),
                          typeof(Matrix4x4Variable),
                          typeof(IntegerVariable),
                          typeof(BooleanVariable))]
    protected Variable inOutVar;


}
