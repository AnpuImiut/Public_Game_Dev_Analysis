using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using TMPro;

public class UI_Score : MonoBehaviour
{
    private int score;
    private int max_score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        max_score = 0;
        EventManager.register("EnemyDestroyed", update_score_plain);
        EventManager.register("BossDestroyed", update_score_boss);
        EventManager.register("GameOver", game_over);
    }

    void OnEnable()
    {
        score = 0;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    // only called if score increases (linked to event "EnemyDestroyed")
    void update_score_plain()
    {
        score += 1;
        transform.GetComponent<TextMeshProUGUI>().text = $"Score: {score}\nBest: {max_score}";
    }

    void update_score_boss()
    {
        score += 9;
        transform.GetComponent<TextMeshProUGUI>().text = $"Score: {score}\nBest: {max_score}";
    }

    void game_over()
    {
        max_score = score;
    }
}
