using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class MainController : MonoBehaviour
{
    private bool in_play;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.register("Play", reset);
        EventManager.trigger_event("Play");
    }

    void Update()
    {
        // only possible for WEBGL, Standalone ends game
        if(!in_play && Input.GetKeyDown(KeyCode.R))
        {
            EventManager.trigger_event("Play");
        }
    }

    void register_events()
    {
        EventManager.register("PlayerDestroyed", game_over);
        EventManager.register("WaveClear", next_wave);
    }

    void unregister_events()
    {
        EventManager.unregister("PlayerDestroyed", game_over);
        EventManager.unregister("WaveClear", next_wave);
    }

    void reset()
    {
        register_events();
        in_play = true;
        Invoke("next_wave", 1f);
    }

    void game_over()
    {
        EventManager.trigger_event("GameOver");
        unregister_events();
        #if UNITY_STANDALONE_WIN && !UNITY_EDITOR
        Invoke("quit_game", 5f);
        #endif
        #if UNITY_WEBGL
        in_play = false;
        #endif
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
