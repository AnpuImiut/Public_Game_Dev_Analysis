using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    private bool player_control;
    private float push_strength;
    private SphereCollider sphere_collider;
    // size of the force-field expansion
    private float[] collider_sizes;
    [SerializeField] private float expand_factor;
    
    /* 
        ForceField is an invisible attack that pushes the enemy
        (player or enemy) with force, possibly of the platform.
     */

    void Start()
    {
        // Without a parent class this is a invisible attack
        // and some control variables are not set.
        player_control = transform.parent.GetComponent<SmashAttack>().is_player_controlled();
        push_strength = transform.parent.GetComponent<SmashAttack>().get_push_strength();
        collider_sizes = transform.parent.GetComponent<SmashAttack>().get_collider_sizes();
        // set collider radius to initial value
        sphere_collider = transform.GetComponent<SphereCollider>();
        sphere_collider.radius = collider_sizes[0];
    }

    void FixedUpdate()
    {
        // expand collider until it reaches the maximum
        if(sphere_collider.radius < collider_sizes[1])
        {
            sphere_collider.radius *= expand_factor;
        }
        else{
            // destroy on maximum collider size
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // player used smash attack and hits enemy
        if(other.gameObject.CompareTag("Enemy") && player_control)
        {
            Vector3 dir = (other.transform.position - transform.position).normalized;
            other.transform.GetComponent<Rigidbody>().AddForce(dir * push_strength, ForceMode.VelocityChange);
        }
        // enemy used smash attack and hits player
        else if(other.gameObject.CompareTag("Player") && !player_control)
        {
            Vector3 dir = (other.transform.position - transform.position).normalized;
            other.transform.GetComponent<Rigidbody>().AddForce(dir * push_strength, ForceMode.VelocityChange);
            other.gameObject.GetComponent<PlayerController>().loose_full_control();
        }
    }

    public bool is_player_controlled() {return player_control;}
}
