using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_body : MonoBehaviour
{
    public float timer_since_moment_murder = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer_since_moment_murder += Time.deltaTime;
    }
}
