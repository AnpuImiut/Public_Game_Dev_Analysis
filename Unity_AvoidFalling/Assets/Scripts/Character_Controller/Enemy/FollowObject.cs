using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private GameObject follow_target = null;
    [SerializeField] private bool intelligent_follow;
    [SerializeField] private float speed;
    private Rigidbody object_rb;
    // Start is called before the first frame update
    void Start()
    {
        object_rb = transform.GetComponent<Rigidbody>();
        if(follow_target == null)
        {
            follow_target = GameObject.FindWithTag("Player");
        }
        EventManager.register("DestroyPlayer", follow_object_destroyed);
    }

    void OnDestroy()
    {
        EventManager.unregister("DestroyPlayer", follow_object_destroyed);
    }

    // Update is called once per frame
    void Update()
    {
        if(intelligent_follow)
        {
            adjust_targeted_moving();
        }
        move_towards_target();
    }

    void move_towards_target()
    {
        if(follow_target == null)
        {
            follow_target = transform.gameObject;
        }
        Vector3 dir = (follow_target.transform.position - transform.position).normalized;
        object_rb.AddForce(dir * speed);
    }
    void adjust_targeted_moving()
    {
        /*  The angle between current velocity and vector pointing at the target
            is a helpful measure to make the following more smooth */
        float angle = Vector3.Angle(object_rb.velocity, follow_target.transform.position - transform.position);
        object_rb.velocity *= (1 - angle/3000f);
    }

    void follow_object_destroyed()
    {
        follow_target = transform.gameObject;
    }

    public GameObject get_follow_target()
    {
        return follow_target;
    }
}
