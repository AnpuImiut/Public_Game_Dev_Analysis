using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float push_strength = 20f;
    // valid target
    private string allowed_target;
    private Rigidbody bullet_rb;
    
    void Start()
    {
        bullet_rb = transform.GetComponent<Rigidbody>();
        bullet_rb.velocity = transform.TransformVector(Vector3.up) * speed;
    }

    public void set_pushStrength_and_target(string target, float push_s)
    {
        push_strength = push_s;
        allowed_target = target;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == allowed_target)
        {
            Rigidbody object_rb = other.GetComponent<Rigidbody>();
            object_rb.AddForce(bullet_rb.velocity.normalized * push_strength, ForceMode.VelocityChange);
            /* 
                The player has functionality to make steering smoother by reducing its 
                velocity slowly. That counteracts being pushed by the bullet. Therefore
                the player needs to loose control when hit by outer-force.
             */
            if(other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerController>().loose_full_control();
            }
            Destroy(gameObject);
        }
    }

    public string get_target() {return allowed_target;}
}
