using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class MainControllerX : MonoBehaviour
{
    [SerializeField] private float init_difficulty;
    // Start is called before the first frame update
    void Start()
    {
        register_events();
        Invoke("next_wave", 1f);
    }

    void register_events()
    {
        EventManager.register("GameOver", game_over);
        EventManager.register("WaveClear", next_wave);
    }

    void unregister_events()
    {
        EventManager.unregister("GameOver", game_over);
        EventManager.unregister("WaveClear", next_wave);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void game_over()
    {
        unregister_events();
        Invoke("quit_game", 5f);
    }

    void quit_game()
    {
        Application.Quit();
    }

    void next_wave()
    {
        Invoke("event_next_wave", 1f);
    }

    void event_next_wave()
    {
        EventManager.trigger_event("SpawnWave");
    }

    public float get_difficulty()
    {
        return init_difficulty;
    }
}
