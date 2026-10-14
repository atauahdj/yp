using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    void Start()
    {
        // for(int i = 0; i < 5; i++)
        // {
        //     Debug.Log(i);
        // }
    }

    // Update is called once per frame
    bool _timeScale;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_timeScale)
            {
                Time.timeScale = 0.25f;
                _timeScale = false;
            }
            else
            {
                Time.timeScale = 1f;
                _timeScale = true;
            }
        }
    }
}
