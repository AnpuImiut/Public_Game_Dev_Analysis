using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float push_strength = 20f;
    // variable controlling which target can be pushed
    private string allowed_target;
    private Rigidbody bullet_rb;
    // Start is called before the first frame update
    void Start()
    {
        bullet_rb = transform.GetComponent<Rigidbody>();
        bullet_rb.velocity = transform.TransformVector(Vector3.up) * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Destroy(gameObject);
        }
    }

    public string get_target() {return allowed_target;}
}
