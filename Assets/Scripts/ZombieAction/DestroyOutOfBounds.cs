using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private int limitZ = 100;

    void Start()
    {

    }

    void Update()
    {
        if (transform.position.z > limitZ)
        {
            Destroy(gameObject);
        }
    }
}