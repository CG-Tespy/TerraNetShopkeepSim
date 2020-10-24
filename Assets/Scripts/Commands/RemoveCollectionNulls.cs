using UnityEngine;
using Fungus;

[CommandInfo("Collection", "Remove Collection Nulls", "Removes all null elements from the passed collection.")]
public class RemoveCollectionNulls : Command
{
    [SerializeField] CollectionData collection;

    public override void OnEnter()
    {
        base.OnEnter();

        var collection = this.collection.Value;

        for (int i = 0; i < collection.Count; i++)
        {
            var element = collection.Get(i);
            if (element == null)
            {
                collection.RemoveAt(i);
                i--;
            }
        }

        Continue();
    }
}
