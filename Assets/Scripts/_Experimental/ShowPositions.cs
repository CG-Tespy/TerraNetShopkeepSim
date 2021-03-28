using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPositions : MonoBehaviour
{
    public Vector3 worldPos, localPos, worldToLocal, localToWorld, mousePos;


    // Update is called once per frame
    void Update()
    {
        worldPos = transform.position;
        localPos = transform.localPosition;
        worldToLocal = transform.InverseTransformPoint(worldPos);
        localToWorld = transform.TransformPoint(localPos);
        mousePos = Input.mousePosition;
    }
}
