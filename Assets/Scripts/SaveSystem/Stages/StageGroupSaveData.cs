using System.Collections.Generic;
using UnityEngine;

public class StageGroupSaveData : IndexSaveData, ICollectionSOData
{
    [SerializeField] protected List<int> stageIndexes = new List<int>();

    public IList<int> ContentIndexes
    {
        get { return stageIndexes; }
        set
        {
            stageIndexes.Clear();
            stageIndexes.AddRange(value);
        }
    }
        
    public string CollectionName
    {
        get { return name; }
        set { name = value; }
    }
    public virtual int CollectionIndex
    {
        get { return index; }
        set { index = value; }
    }
}
        
        
