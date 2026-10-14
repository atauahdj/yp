using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police_anim : MonoBehaviour
{
    public SpriteRenderer spr;
    public Sprite[] walk_anim;
    float change_frame;
    int current_frame;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        change_frame += Time.deltaTime;
        if(change_frame >= 0.125f)
        {
            current_frame++;
            if(current_frame >= walk_anim.Length)
            {
                current_frame = 0;
            }
            spr.sprite = walk_anim[current_frame];
            change_frame = 0;
        }
    }
}
