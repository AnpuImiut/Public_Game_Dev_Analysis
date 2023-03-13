using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    [SerializeField] private GameObject[] object_type;
    public int enemyCount;
    private int waveCount;
    [SerializeField] private GameObject player; 

    void Start()
    {
        waveCount = 1;
        // register events
        EventManager.register("SpawnWave", next_wave);
        EventManager.register("GameOver", game_over);
        EventManager.register("EnemyDestroyed", enemy_destroyed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void next_wave()
    {
        Invoke("spawn_wave", 7f);
    }

    void spawn_wave()
    {
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15); // make powerups spawn at player end
        System.Random rnd_gen = RandomGenerator.get_instance();
        // If no powerups remain, spawn a powerup
        if ((float) rnd_gen.NextDouble() < waveCount/5f) // check that there are zero powerups
        {
            Instantiate(object_type[1], GenerateSpawnPosition() + powerupSpawnOffset, object_type[1].transform.rotation);
        }

        // Spawn number of enemy balls based on wave number
        for (int i = 0; i < waveCount; i++)
        {
            Instantiate(object_type[0], GenerateSpawnPosition(), object_type[0].transform.rotation);
        }
        enemyCount = waveCount;
        waveCount += 1;
        ResetPlayerPosition(); // put player back at start

    }

    void game_over()
    {
        EventManager.unregister("SpawnWave", next_wave);
    }

    void enemy_destroyed()
    {
        enemyCount -= 1;
        if(!Convert.ToBoolean(enemyCount))
        {
            EventManager.trigger_event("WaveClear");
        }
    }

    // Move player back to position in front of own goal
    void ResetPlayerPosition ()
    {
        player.transform.position = new Vector3(0, 0, -7);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

    }

    public int get_wave_count()
    {
        return waveCount;
    }

    Vector3 GenerateSpawnPosition()
    {
        (float x, float z) = RandomGenerator.generate_2D_point_rect(
            new float[2] {-17f, 17f}, new float[2] {13f, 24f}
        );
        return new Vector3(x, 0f, z);
    }

}
