using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_script_NPC : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite[] Idle;
    int idl;
    public Sprite[] Move;
    int mv;
    public bool walking;

    float currentTime;

    bool kill;

    void Update()
    {
        if (walking)
        {
            if (mv >= Move.Length)
            {
                mv = 0;
            }
            sr.sprite = Move[mv];
            currentTime += Time.deltaTime;
            if (currentTime > 0.25f)
            {
                mv++;
                currentTime = 0;
                if (mv >= Move.Length)
                {
                    mv = 0;
                }
            }
        }
        else
        {
            if (idl >= Idle.Length)
            {
                idl = 0;
            }
            sr.sprite = Idle[idl];
            currentTime += Time.deltaTime;
            if (currentTime > 0.25f)
            {
                idl++;
                currentTime = 0;
                if (idl >= Idle.Length)
                {
                    idl = 0;
                }
            }
        }
    }
}
