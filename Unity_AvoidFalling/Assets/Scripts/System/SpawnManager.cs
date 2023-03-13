using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spawn_on_start;
    [SerializeField] private GameObject[] enemy_type;
    [SerializeField] private GameObject[] power_up_type;
    [SerializeField] float[] enemy_spawn_prob;
    private int wave_counter;
    [SerializeField] float spawn_radius;
    [SerializeField] Vector3 spawn_radius_center;
    private int enemy_alive;
    [SerializeField] private AudioClip[] spawn_sound;
    private AudioSource audio_source;
    // Start is called before the first frame update
    void Start()
    {
        audio_source = transform.GetComponent<AudioSource>();
        // initialize variables
        wave_counter = 0;
        // register events
        EventManager.register("Play", reset);
        EventManager.register("GameOver", game_over);
        EventManager.register("EnemyDestroyed", enemy_destroyed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void game_over()
    {
        CancelInvoke();
        EventManager.unregister("SpawnWave", next_wave);
    }

    public void enemy_destroyed()
    {
        enemy_alive -= 1;
        if(!Convert.ToBoolean(enemy_alive))
        {
            EventManager.trigger_event("WaveClear");
            EventManager.trigger_event("normal_music"); 
        }
    }

    public void add_to_enemy_count(int num)
    {
        enemy_alive += num;
    }

    void next_wave()
    {
        if((wave_counter + 1)  % 5 == 0 && wave_counter > 0)
        {
            Invoke("boss_wave", 7f);
        }
        else
        {
            Invoke("spawn_wave", 7f);
        }
        
    }
    void spawn_wave()
    {
        System.Random rnd_gen = RandomGenerator.get_instance();
        wave_counter += 1;
        for(int i = 0; i < wave_counter; i++)
        {
            Invoke("spawn_enemy", 1f * (i + 1));
            // for each enemy spawning there is a chance for a power-up to spawn
            if(rnd_gen.NextDouble() < 0.2)
            {
                Invoke("spawn_power_up", 1.2f * (i + 1));
            }
        }
        enemy_alive = wave_counter;   
    }

    void boss_wave()
    {
        (float x, float z) = RandomGenerator.generate_2D_point_circle(spawn_radius);
        GameObject boss = Instantiate(
            enemy_type[enemy_type.Length - 1], spawn_radius_center + new Vector3(x, 0, z), 
            enemy_type[enemy_type.Length - 1].transform.rotation
        );
        boss.GetComponent<BossController>().set_boss_difficulty(select_boss_difficulty());
        wave_counter += 1;
        enemy_alive = 1;
        EventManager.trigger_event("boss_music");
    }

    int select_boss_difficulty()
    {
        int[] difficulties = {  2, 3, 5, 7, 
                                6, 10, 14, 15, 21, 35,
                                30, 42, 105, 210};
        int max_index = Math.Max(difficulties.Length - 1, (int) Math.Ceiling(wave_counter / 2.0));
        return difficulties[RandomGenerator.get_instance().Next(0, max_index)];
    }

    // instantiate random enemy w.r.t. to difficulty
    void spawn_enemy()
    {
        (float x, float z) = RandomGenerator.generate_2D_point_circle(spawn_radius);
        int rnd_index = select_enemy();
        Instantiate(    enemy_type[rnd_index],
                        spawn_radius_center + new Vector3(x, 0, z),
                        enemy_type[rnd_index].transform.rotation);
        audio_source.PlayOneShot(spawn_sound[0], 0.6f);
    }

    public void spawn_enemy(Vector3 spawn_center, float radius)
    {
        (float x, float z) = RandomGenerator.generate_2D_point_circle(radius);
        int rnd_index = select_enemy();
        Instantiate(    enemy_type[rnd_index],
                        spawn_center + new Vector3(x, 0, z),
                        enemy_type[rnd_index].transform.rotation);
        audio_source.PlayOneShot(spawn_sound[0], 0.5f);
    }

    int select_enemy()
    {
        System.Random rnd_gen = RandomGenerator.get_instance();
        float tmp_prob = (float) rnd_gen.NextDouble();
        int selected = 0;
        float sum = 0;
        /* alters the initial probability distribution to a uniform distribution 
        after 10 waves */
        for(int i = 0; i < enemy_spawn_prob.Length; i++)
        {
            sum += enemy_spawn_prob[i] + (1f/enemy_spawn_prob.Length - enemy_spawn_prob[i]) * Math.Min(1f, (wave_counter - 1)/10f);
            if(tmp_prob <= sum)
            {
                selected = i;
                break;
            }
        }
        return selected;
    }
    // randomly spawns a power-up
    void spawn_power_up()
    {
        System.Random rnd_gen = RandomGenerator.get_instance();
        (float x, float z) = RandomGenerator.generate_2D_point_circle(spawn_radius);
        int last_index = rnd_gen.Next(0, power_up_type.Length);
        Instantiate(    power_up_type[last_index],
                        spawn_radius_center + new Vector3(x, 0, z),
                        power_up_type[last_index].transform.rotation);
        audio_source.PlayOneShot(spawn_sound[1], 0.7f);
    }

    void reset()
    { 
        EventManager.register("SpawnWave", next_wave);
        wave_counter = 0;
        GameObject tmp = null;
        foreach(GameObject x in spawn_on_start)
        {
            tmp = Instantiate(x, Vector3.zero, x.transform.rotation);
        }
        tmp.name = "PowerUpIndicator";
    }
}
