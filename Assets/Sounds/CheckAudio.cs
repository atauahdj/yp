using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAudio : MonoBehaviour
{
    public Music_level music_prefab;
    public GameObject music;
    void Start()
    {
        music = GameObject.FindGameObjectWithTag("Music");
        if (music == null)
        {
            Instantiate(music_prefab);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
