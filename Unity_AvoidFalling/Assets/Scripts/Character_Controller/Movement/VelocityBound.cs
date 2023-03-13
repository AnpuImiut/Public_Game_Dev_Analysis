using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class VelocityBound : MonoBehaviour
{
    [SerializeField] private float max_speed;
    private Rigidbody object_rb;
    // Start is called before the first frame update
    void Start()
    {
        object_rb = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        max_speed_bound();
    }

    void max_speed_bound()
    {
        float[] tmp_arr = new float[3];
        float old_y = object_rb.velocity.y;
        // upper bound for speed but only in x- and z-axis
        // y-axis is unbound for jumping speed
        for(int i = 0; i < 3; i++)
        {
            if(object_rb.velocity[i] >= 0)
            {
                tmp_arr[i] = Math.Min(object_rb.velocity[i], max_speed);
            }
            else
            {
                tmp_arr[i] = Math.Max(object_rb.velocity[i], -max_speed);
            }
        }
        object_rb.velocity = new Vector3(tmp_arr[0], old_y, tmp_arr[2]);
    }
}
