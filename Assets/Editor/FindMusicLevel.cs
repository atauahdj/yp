using System;
using Unity.VisualScripting;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FindMusicLevel : MonoBehaviour
{
    float timer = 0.001f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        Debug.Log(timer);
        
    }

    // Update is called once per frame
    GameObject ml;
    void Update()
    {
        if(timer > 0)
        {
            timer += 0.0025f;
        }
        
        if(timer > 0.3f)
        {
            ml = GameObject.FindGameObjectWithTag("Music");
            Music_level m_level = ml.GetComponent<Music_level>();
            UnityEventTools.AddPersistentListener(gameObject.GetComponent<Button>().onClick, m_level.OnOffMusic);
            timer = 0;
        }
    }
}
