using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridShuffler : MonoBehaviour
{
    private const float bottomBound = -4.0f;
    private const float topBound = 11.0f;
    private const float speed = 0.33f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2( transform.position.x - speed*Time.deltaTime, transform.position.y - speed * Time.deltaTime);

        if (transform.position.y < bottomBound)
        {
            transform.position = new Vector2( 1, topBound);
        }
    }
}
