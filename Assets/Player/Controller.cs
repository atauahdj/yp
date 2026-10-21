using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public Animator_player AP;
    public float speed = 5f;
    ParentNPC pnpc;
    CharacterController cc;
    public GameObject prefab_body;
    public GameObject Wanted_Image;
    void Start()
    {
        AP = GetComponent<Animator_player>();
        cc = GetComponent<CharacterController>();
        pnpc = FindObjectOfType<ParentNPC>();
        transform.position = new Vector3(transform.position.x,transform.position.y,-2);
        Wanted_Image.SetActive(false);
    }

    Vector2 movement;
    void Update()
    {
        Vector2 moveX = Gamepad.current.leftStick.ReadValue();
        Vector2 moveY = Gamepad.current.leftStick.ReadValue();
        movement = new Vector3(0,0, -2);
        movement += new Vector2(moveX.x * speed * Time.deltaTime, moveY.y * speed * Time.deltaTime);
        if (Keyboard.current.shiftKey.IsPressed())
        {
            speed = 7;
        }
        else
        {
            speed = 5;
        }
        if(moveX.x < 0 && moveX.x != 0)
        {
            AP.player_renderer.flipX = true;
        }
        else if (moveX.x != 0)
        {
            AP.player_renderer.flipX =false;
        }
        if (cc.velocity != Vector3.zero)
        {
            AP.walking = true;
        }
        else
        {
            AP.walking = false;
        }
        cc.Move(movement);
        if (Keyboard.current.eKey.IsPressed())
        {
            GameObject[] npc_c = GameObject.FindGameObjectsWithTag("NPC");
            foreach(GameObject npc in npc_c)
            {
                if(Vector2.Distance(npc.transform.position, transform.position) < 2f)
                {
                    pnpc.npc_counter++;
                    GameObject body_dead = Instantiate(prefab_body);
                    body_dead.transform.position = npc.transform.position;
                    Destroy(npc);
                }
            }
        }
    }
}
