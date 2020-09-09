using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfNotUniqueInName : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        var otherUniquesArr = GameObject.FindObjectsOfType<DestroyIfNotUniqueInName>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
