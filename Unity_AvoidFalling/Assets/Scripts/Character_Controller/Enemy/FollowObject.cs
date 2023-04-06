using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private GameObject follow_target = null;
    // smart tracking improves how the NPC follows the target,
    // especially handling sharp turns better
    [SerializeField] private bool smart_follow;
    [SerializeField] private float smart_follow_control;
    [SerializeField] private float speed;
    private Rigidbody object_rb;
    
    void Start()
    {
        object_rb = transform.GetComponent<Rigidbody>();
        // This script is only used by enemies, therefore the player is a
        // viable follow target at all time
        if(follow_target == null)
        {
            follow_target = GameObject.FindWithTag("Player");
        }
        EventManager.register("PlayerDestroyed", follow_object_destroyed);
    }

    void Update()
    {
        if(smart_follow)
        {
            adjust_targeted_moving();
        }
        move_towards_target();
    }

    void OnDestroy()
    {
        EventManager.unregister("PlayerDestroyed", follow_object_destroyed);
    }

    void move_towards_target()
    {
        // if no follow target exist, not moving is the default behavior
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
            is a helpful to make the following more smooth */
        float angle = Vector3.Angle(object_rb.velocity, follow_target.transform.position - transform.position);
        object_rb.velocity *= (1 - angle/smart_follow_control);
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
