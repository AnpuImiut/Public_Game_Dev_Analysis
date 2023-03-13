using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateTransition : MonoBehaviour
{
    [SerializeField] private bool destroy;
    [SerializeField] private bool inactivate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EventManager.register("GameOver", state_game_over);
        EventManager.register("Play", state_play);
    }

    void state_game_over()
    {
        Debug.Log(gameObject.name);
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
