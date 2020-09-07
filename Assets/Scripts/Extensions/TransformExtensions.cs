using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static Transform[] GetChildren(this Transform trans)
    {
        Transform[] children = new Transform[trans.childCount];

        for (int i = 0; i < trans.childCount; i++)
        {
            children[i] = trans.GetChild(i);
        }

        return children;
    }
}
