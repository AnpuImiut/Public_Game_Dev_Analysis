using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class BossController : MonoBehaviour
{
    private bool can_spawn = false;
    private bool spawned = false;
    private bool can_smash = false;
    private bool can_shoot_single = false;
    private bool can_shoot_ring = false;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bullet_strength;
    private GameObject player;
    private int enemy_alive;
    private int counter;
    void Start()
    {
        enemy_alive = 1;
        transform.GetComponent<SmashAttack>().set_availability(can_smash);
        player = GameObject.Find("Player");
        EventManager.register("EnemyDestroyed", enemy_destroyed);
        counter = 1;
    }

    void Update()
    {
        if(can_spawn)
        {
            spawn_logic();
        }
    }

    void init_abilities()
    {
        // each boss comes with a wave of minions
        spawn_logic();
        // single bullets functionality
        if(can_shoot_single)
        {
            Invoke("single_shot", 2f);
        }
        // bullet ring functionality
        if(can_shoot_ring)
        {
            Invoke("shoot_ring", 4f);
        }
    }

    void spawn_logic()
    {
        counter += 1;
        if(enemy_alive == 1 && !spawned)
        {
            spawned = true;
            Invoke("spawn_wave", 4f);
        }
    }

    void spawn_wave()
    {
        int num_spawns = 2 + Convert.ToInt32(can_spawn) + Convert.ToInt32(can_smash)
                           + Convert.ToInt32(can_shoot_single) + Convert.ToInt32(can_shoot_ring);
        SpawnManager obj_ref = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        for(int i = 0; i < num_spawns; i++)
        {
            Invoke("spawn_minion", 1f);
        }
        enemy_alive += num_spawns;
        obj_ref.add_to_enemy_count(num_spawns);
    }

    void spawn_minion()
    {
        SpawnManager obj_ref = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        obj_ref.spawn_enemy(transform.position, 8f);
    }

    void single_shot()
    {
        GameObject tmp = Instantiate(bullet, transform.position - new Vector3(0, 1.75f, 0), bullet.transform.rotation);
        // calculate the angle for the future position of the player
        Vector3 dir_current_player = (player.transform.position - transform.position).normalized;
        Vector3 dir_future_player = (player.GetComponent<Rigidbody>().velocity * 0.2f + player.transform.position - transform.position).normalized;
        float predict_angle = Vector3.SignedAngle(dir_current_player, dir_future_player, Vector3.up);
        float angle = Vector3.SignedAngle(bullet.transform.TransformVector(Vector3.up), dir_current_player, Vector3.up);
        // translate bullet by off-set and rotate accordingly
        tmp.transform.Rotate(new Vector3(angle+predict_angle, 0, 0));
        tmp.transform.Translate(Vector3.up * 1.5f);
        tmp.GetComponent<BulletController>().set_pushStrength_and_target("Player", bullet_strength);
        Invoke("single_shot", 2f);
    }

    void shoot_ring()
    {
        transform.GetComponent<BulletRing>().trigger_bullet_ring("Player", transform.position - new Vector3(0, 1.75f, 0));
        Invoke("shoot_ring", 4f);
    }

    public void enemy_destroyed()
    {
        enemy_alive -= 1;
        if(enemy_alive == 1)
        {
            spawned = false;
        }
    }

    public void set_boss_difficulty(int diff)
    {
        if(diff % 2 == 0)
        {
            can_spawn = true;
        }
        if(diff % 3 == 0)
        {
            can_smash = true;
            transform.GetComponent<SmashAttack>().set_availability(can_smash);
        }
        if(diff % 5 == 0)
        {
            can_shoot_single = true;
        }
        if(diff % 7 == 0)
        {
            can_shoot_ring = true;
        }
        init_abilities();
    }

}
