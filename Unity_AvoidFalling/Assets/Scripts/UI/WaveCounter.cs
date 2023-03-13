using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using TMPro;

public class WaveCounter : MonoBehaviour
{
    private int wave_counter;
    // Start is called before the first frame update
    void Start()
    {
        wave_counter = 0;
        EventManager.register("SpawnWave", update_wave_counter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // only called if score increases (linked to event "EnemyDestroyed")
    void update_wave_counter()
    {
        wave_counter += 1;
        transform.GetComponent<TextMeshProUGUI>().text = $"Wave: {wave_counter}";
    }
}
