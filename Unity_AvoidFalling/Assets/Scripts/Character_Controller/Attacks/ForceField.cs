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
    // Start is called before the first frame update
    void Start()
    {
        // control variables. Without parent class this is a invisible attack.
        player_control = transform.parent.GetComponent<SmashAttack>().is_player_controlled();
        push_strength = transform.parent.GetComponent<SmashAttack>().get_push_strength();
        collider_sizes = transform.parent.GetComponent<SmashAttack>().get_collider_sizes();
        // set collider radius to initial value
        sphere_collider = transform.GetComponent<SphereCollider>();
        sphere_collider.radius = collider_sizes[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // expand collider until maximum
        if(sphere_collider.radius < collider_sizes[1])
        {
            sphere_collider.radius *= expand_factor;
        }
        else{
            // destroy on max expand
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy") && player_control)
        {
            // player used smash attack and hits enemy
            Vector3 dir = (other.transform.position - transform.position).normalized;
            other.transform.GetComponent<Rigidbody>().AddForce(dir * push_strength, ForceMode.VelocityChange);
        }
        else if(other.gameObject.CompareTag("Player") && !player_control)
        {
            // enemy used smash attack and hits player
            Vector3 dir = (other.transform.position - transform.position).normalized;
            other.transform.GetComponent<Rigidbody>().AddForce(dir * push_strength, ForceMode.VelocityChange);
            other.gameObject.GetComponent<PlayerController>().loose_full_control();
        }
    }

    public bool is_player_controlled() {return player_control;}
}
