using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using TMPro;

public class SpawnCDX : MonoBehaviour
{
    [SerializeField] private GameObject UI_handler;
    [SerializeField] private int max_cd;
    private int cd;
    // Start is called before the first frame update
    void Start()
    {
        // register wave spawning as an event
        EventManager.register("SpawnWave", start_cd);
        EventManager.register("GameOver", game_over);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(cd > 0)
        {
            UI_handler.transform.Find("SpawnCountdown").GetComponent<TextMeshProUGUI>().fontSize *= 1.02f; 
        }
    }

    void game_over()
    {
        EventManager.unregister("SpawnWave", start_cd);
    }

    void start_cd()
    {
        cd = max_cd;
        UI_handler.transform.Find("SpawnCountdown").GetComponent<TextMeshProUGUI>().text = Convert.ToString(cd);
        Invoke("run_cd", 1f);
    }

    void run_cd()
    {
        cd -= 1;
        UI_handler.transform.Find("SpawnCountdown").GetComponent<TextMeshProUGUI>().fontSize = 36f;
        if(cd > 0)
        {
            UI_handler.transform.Find("SpawnCountdown").GetComponent<TextMeshProUGUI>().text = Convert.ToString(cd);
            Invoke("run_cd", 1f);
        }
        else if(cd == 0)
        {
            UI_handler.transform.Find("SpawnCountdown").GetComponent<TextMeshProUGUI>().fontSize = 60f;
            UI_handler.transform.Find("SpawnCountdown").GetComponent<TextMeshProUGUI>().text = "START";
            Invoke("run_cd", 1f);
        }
        else
        {
            UI_handler.transform.Find("SpawnCountdown").GetComponent<TextMeshProUGUI>().text = "";
        }
    }
}
