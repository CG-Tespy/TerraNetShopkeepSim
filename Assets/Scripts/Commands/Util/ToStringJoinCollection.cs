using UnityEngine;
using Fungus;

[CommandInfo("Collection", 
    "ToString Join Collection", 
    "Combines everything in a collection in string form, assigning the result to a StringVariable.")]
public class ToStringJoinCollection : Command
{
    [SerializeField] protected CollectionData collection;
    [VariableProperty(typeof(StringVariable))]
    [SerializeField] protected StringVariable strVar = null;
    [SerializeField] protected StringData separator = new StringData(", ");

    protected string result = "";

    public override void OnEnter()
    {
        base.OnEnter();
        Reset();

        bool inputsAreThere = collection.Value != null && strVar != null;
        bool collectionHasAnything = collection.Value.Count > 0;

        if (inputsAreThere && collectionHasAnything)
        {
            strVar.Value = CombineCollectionIntoString();
        }

        Continue();
    }

    protected virtual void Reset()
    {
        result = "";
    }

    protected virtual string CombineCollectionIntoString()
    {
        RegisterCollectionContents();
        RemoveExtraSeparator();

        return result;
    }

    protected virtual void RegisterCollectionContents()
    {
        Collection collection = this.collection.Value;

        for (int i = 0; i < collection.Count; i++)
        {
            var element = collection.Get(i);

            result = string.Concat(result, element, separator.Value);
        }
    }

    protected virtual void RemoveExtraSeparator()
    {
        result = result.Substring(0, result.Length - separator.Value.Length);
    }
}
