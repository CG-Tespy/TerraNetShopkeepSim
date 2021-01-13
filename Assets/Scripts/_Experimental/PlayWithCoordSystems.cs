using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWithCoordSystems : MonoBehaviour
{
    public Vector3 mouseCoords;
    public Vector3 worldCoords;

    private void Update()
    {
        worldCoords = Camera.main.ScreenToWorldPoint(mouseCoords);
        
    }
}
