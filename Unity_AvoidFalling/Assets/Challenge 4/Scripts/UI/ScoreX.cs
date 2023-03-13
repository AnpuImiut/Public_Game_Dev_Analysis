using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using TMPro;

public class ScoreX : MonoBehaviour
{
    [SerializeField] private GameObject UI_handler;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        EventManager.register("Scored", update_score);
        EventManager.register("GameOver", game_over);
    }

    void game_over()
    {
        EventManager.unregister("Scored", update_score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void update_score()
    {
        score += 1;
        UI_handler.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = $"Score\n{score}";
    }
}
