using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;


/* 
    Controller of enemy type that randomly moves around the player, 
    making it hard to predict the movement.
 */
public class EnemyRandomMovementWithCenter : MonoBehaviour
{
    // Random movement variables
    [SerializeField] private int max_random_targets;
    private Vector3[] targets;
    [SerializeField] private float radius;


    // Control variables
    [SerializeField] private float speed;
    private int target_count = -1;
    private bool in_control = true;


    // Other variables
    private float old_dist;
    private GameObject follow_target;
    private Rigidbody object_rb;
    
    void Start()
    {
        object_rb = transform.GetComponent<Rigidbody>();
        if(follow_target == null)
        {
            follow_target = GameObject.FindWithTag("Player");
        }
        targets = new Vector3[max_random_targets];
    }

    void Update()
    {
        is_falling();
        if(target_count == -1)
        {
            init_random_targets();
        }
        else if(in_control)
        {
            move_to_target();
        }
    }

    void init_random_targets()
    {
        // initiate random target points around follow object
        Vector3 target_pos = follow_target.transform.position;
        for(int i = 0; i < targets.Length; i++)
        {
            (float x, float z) = RandomGenerator.generate_2D_point_rect(
                new float[] {target_pos.x - radius, target_pos.x + radius}, 
                new float[] {target_pos.z - radius, target_pos.z + radius}
            );
            x += Math.Sign(x);
            z += Math.Sign(z);
            targets[i] = new Vector3(x, 0.04f, z);
        }
        // lazy solution to make enemy stop on target points
        target_count = 0;
        old_dist = Vector3.Distance(targets[target_count], transform.position) + 1f;
    }

    void move_to_target()
    {
        // if NPC overshoots target point it new distance will be greater
        // than the distance measured last frame
        if(old_dist < Vector3.Distance(targets[target_count], transform.position))
        {
            object_rb.velocity = Vector3.zero;
            target_count += 1;
            if(target_count == targets.Length)
            {
                target_count = -1;
            }
        }
        // as long NPC moves to designated target point, force is applied
        else
        {
            Vector3 dir = (targets[target_count] - transform.position).normalized;
            object_rb.AddForce(dir * speed);
            old_dist = Vector3.Distance(targets[target_count], transform.position);
        }
    }

    void is_falling()
    {
        // when NPC starts falling, it looses its control forever (special case)
        if(transform.position.y < -0.01 && in_control)
        {
            loose_control(0.5f);
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        // contact with powered up player: loose control and get pushed
        if(other.transform.CompareTag("Player"))
        {
            if(other.gameObject.GetComponent<PlayerController>().is_powered_up() > 0)
            {
                loose_control(0.1f);
                Invoke("regain_control", 4f);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // contact with force-field: loose control and get pushed
        if(other.transform.CompareTag("ForceField"))
        {
            if(other.transform.GetComponent<ForceField>().is_player_controlled())
            {
                loose_control(0.1f);
            }
        }
        // contact with bullet: loose control and get pushed
        else if(other.transform.CompareTag("Bullet"))
        {
            if(other.transform.GetComponent<BulletController>().get_target() == "Enemy")
            {
                loose_control(0.1f);
            }
        }
    }

    // make object unable to move on its own until after 4 seconds
    void loose_control(float reduce)
    {
        in_control = false;
        object_rb.velocity = object_rb.velocity * reduce;
        Invoke("regain_control", 4f);
    }
    void regain_control() {in_control = true;}
}
