using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using TMPro;

public class GameOverUIX : MonoBehaviour
{
    [SerializeField] private GameObject UI_handler;
    private float grow = 1;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.register("GameOver", game_over);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void game_over()
    {
        UI_handler.transform.Find("GameOver").GetComponent<TextMeshProUGUI>().text = "GAME\nOver";
        Invoke("larger_animation", 0.01f);
        InvokeRepeating("reset_font_size", 2.5f, 2.5f);
    }

    void larger_animation()
    {
        UI_handler.transform.Find("GameOver").GetComponent<TextMeshProUGUI>().fontSize *= 1 + 0.0008f * grow;
        Invoke("larger_animation", 0.01f);
    }

    void reset_font_size()
    {
        grow = -grow;
    }
}
