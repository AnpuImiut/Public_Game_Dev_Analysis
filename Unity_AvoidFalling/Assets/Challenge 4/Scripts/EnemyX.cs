using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class EnemyX : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody enemyRb;
    private GameObject playerGoal;
    private bool self_controlled = true;
    [SerializeField] private float control_cd;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerGoal = GameObject.Find("Goals/Player Goal");
        int wave_count = GameObject.Find("SpawnManager").GetComponent<SpawnManagerX>().get_wave_count();
        speed *= (1 + 0.5f * (float) (wave_count - 1));
        Invoke("move", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void move()
    {
        System.Random rnd_gen = RandomGenerator.get_instance();
        if(self_controlled)
        {
            // reset velocity
            enemyRb.velocity = Vector3.zero;
            // get initial difficulty and wave count
            float init_diff = GameObject.Find("MainController").GetComponent<MainControllerX>().get_difficulty();
            int wave_count = GameObject.Find("SpawnManager").GetComponent<SpawnManagerX>().get_wave_count();
            float tmp_diff = Math.Min(0.85f, init_diff + (0.85f - init_diff) * (1f/wave_count));
            // either random direction or to goal
            Vector3 lookDirection;
            if((float) rnd_gen.NextDouble() < tmp_diff)
            {
                lookDirection = (playerGoal.transform.position - transform.position).normalized;
            }
            else
            {
                (float x, float z) = RandomGenerator.generate_2D_point_rect(
                    new float[2] {-17f, 17f}, new float[2] {-7f, 21f} 
                );
                lookDirection = (new Vector3(x, 0.5f, z) - transform.position).normalized;
            }
            enemyRb.AddForce(lookDirection * speed);
        }
        Invoke("move", (float) (rnd_gen.NextDouble() + 2) * 2);
    }    

    void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if(other.gameObject.name == "Enemy Goal")
        {
            EventManager.trigger_event("EnemyDestroyed");
            EventManager.trigger_event("Scored");
            Destroy(gameObject);
        } 
        else if(other.gameObject.name == "Player Goal")
        {
            EventManager.trigger_event("EnemyDestroyed");
            EventManager.trigger_event("WrongScored");
            Destroy(gameObject);
        }
        else if(other.gameObject.name == "Player")
        {
            self_controlled = false;
            Invoke("regain_control", control_cd);
        }
    }

    void regain_control()
    {
        self_controlled = true;
    }

}
