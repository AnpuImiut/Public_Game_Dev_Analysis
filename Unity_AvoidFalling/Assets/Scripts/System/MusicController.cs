using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] AudioClip[] background_music;
    private AudioSource audio_source;
    // Start is called before the first frame update
    void Start()
    {
        audio_source = transform.GetComponent<AudioSource>();
        EventManager.register("MusicNormal", play_normal_music);
        EventManager.register("MusicBoss", play_boss_music);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void play_normal_music()
    {
        audio_source.Pause();
        audio_source.clip = background_music[0];
        audio_source.volume = 0.65f;
        audio_source.pitch = 1.0f;
        audio_source.Play();
    }

    void play_boss_music()
    {
        audio_source.Pause();
        audio_source.clip = background_music[1];
        audio_source.volume = 0.35f;
        audio_source.pitch = 0.92f;
        audio_source.Play();
    }
}
