using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class EnemyRandomMovementWithCenter : MonoBehaviour
{
    private GameObject follow_target;
    [SerializeField] private float speed;
    [SerializeField] private int max_random_targets;
    [SerializeField] private float radius;
    private Vector3[] targets;
    private int target_count = -1;
    private float old_dist;
    private bool in_control = true;
    private Rigidbody object_rb;
    // Start is called before the first frame update
    void Start()
    {
        object_rb = transform.GetComponent<Rigidbody>();
        if(follow_target == null)
        {
            follow_target = GameObject.Find("Player");
        }
        EventManager.register("GameOver", game_over);
        targets = new Vector3[max_random_targets];
    }

    // Update is called once per frame
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
        target_count = 0;
        old_dist = Vector3.Distance(targets[target_count], transform.position) + 1f;
    }

    void move_to_target()
    {
        if(old_dist < Vector3.Distance(targets[target_count], transform.position))
        {
            object_rb.velocity = Vector3.zero;
            target_count += 1;
            if(target_count == targets.Length)
            {
                target_count = -1;
            }
        }
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

    void game_over()
    {
        Destroy(transform.gameObject);
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
                Invoke("regain_control", 4f);
            }
        }
        // contact with bullet: loose control and get pushed
        else if(other.transform.CompareTag("Bullet"))
        {
            if(other.transform.GetComponent<BulletController>().get_target() == "Enemy")
            {
                loose_control(0.1f);
                Invoke("regain_control", 4f);
            }
        }
    }

    void loose_control(float reduce)
    {
        in_control = false;
        object_rb.velocity = object_rb.velocity * reduce;
    }
    void regain_control() {in_control = true;}
}
