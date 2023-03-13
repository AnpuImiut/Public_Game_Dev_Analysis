using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        EventManager.register("EnemyDestroyed", update_score_plain);
        EventManager.register("BossDestroyed", update_score_boss);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // only called if score increases (linked to event "EnemyDestroyed")
    void update_score_plain()
    {
        score += 1;
        transform.GetComponent<TextMeshProUGUI>().text = $"Score\n{score}";
    }

    void update_score_boss()
    {
        score += 9;
        transform.GetComponent<TextMeshProUGUI>().text = $"Score\n{score}";
    }
}
