using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    // resizes the text font
    private float grow = 1;
    [SerializeField] AudioClip audio_clip;
    private AudioSource audio_source;
    // Start is called before the first frame update
    void Start()
    {
        audio_source = transform.GetComponent<AudioSource>();
        EventManager.register("GameOver", game_over);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void game_over()
    {
        transform.GetComponent<TextMeshProUGUI>().text = "GAME\nOver";
        audio_source.PlayOneShot(audio_clip, 0.5f);
        Invoke("font_resize_animation", 0.01f);
        InvokeRepeating("reverse_font_size_growth", 2.5f, 2.5f);
    }

    void font_resize_animation()
    {
        transform.GetComponent<TextMeshProUGUI>().fontSize *= 1 + 0.0008f * grow;
        Invoke("font_resize_animation", 0.01f);
    }

    void reverse_font_size_growth()
    {
        grow = -grow;
    }
}
