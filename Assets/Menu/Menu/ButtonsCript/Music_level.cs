using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_level : MonoBehaviour
{
    public bool status_audio = true;
    public AudioSource audio_music;
    void Start()
    {
        audio_music.mute = status_audio;
        audio_music.mute = false;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void OnOffMusic()
    {
        if (!status_audio)
        {
            audio_music.mute = true;
            status_audio = true;
        }
        else if(status_audio)
        {
            audio_music.mute = false;
            status_audio = false;
        }
    }
}
