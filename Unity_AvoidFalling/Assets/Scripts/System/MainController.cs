using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

enum GameState
{
    Play,
    GameOver
}

public class MainController : MonoBehaviour
{
    private GameState game_state;
    // Start is called before the first frame update
    void Start()
    {
        game_state = GameState.Play;
        register_events();
        Invoke("next_wave", 1f);
    }

    void Update()
    {
        
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

    void game_over()
    {
        unregister_events();
        game_state = GameState.GameOver;
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
}
