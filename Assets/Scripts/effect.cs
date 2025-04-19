using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect : MonoBehaviour
{
    // Start is called before the first frame update
    public float destroyTime;
    void Start()
    {
        // destroyTime = 2;
        Destroy(gameObject,destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
