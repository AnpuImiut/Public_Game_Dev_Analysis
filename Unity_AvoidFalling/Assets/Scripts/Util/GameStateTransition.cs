using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateTransition : MonoBehaviour
{
    [SerializeField] private bool destroy;
    [SerializeField] private bool inactivate;

    void OnEnable()
    {
        EventManager.register("GameOver", state_game_over);
        EventManager.register("Play", state_play);
    }

    void OnDisable()
    {
        
    }

    void OnDestroy()
    {
        EventManager.unregister("GameOver", state_game_over);
        EventManager.unregister("Play", state_play);
    }

    void state_game_over()
    {
        if(destroy) {Destroy(gameObject);}
        else if(inactivate) 
        {
            gameObject.SetActive(false);
        }
    }

    void state_play()
    {
        if(inactivate)
        {
            gameObject.SetActive(true);
        }
    }
}
