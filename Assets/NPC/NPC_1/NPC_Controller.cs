using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class NPC_Controller : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject point_prefab;
    Transform point;
    GameObject point_temp;
    Animator_script_NPC npc_anim;
    public GameObject[] bodyes;
    public GameObject telephone;
    Police_script police_Script;
    public GameObject police_prefab;

    // флаг, что мент уже был вызван (чтобы не звонить повторно)
    bool policeCalled = false;

    void Start()
    {
        npc_anim = GetComponent<Animator_script_NPC>();
        point_temp = Instantiate(point_prefab);
        float x = Random.Range(-27.6f, 27.6f);
        float y = Random.Range(-10.7f, 10.7f);
        point_temp.transform.position = new Vector3(x, y, -2);
        agent = GetComponent<NavMeshAgent>();
        agent.destination = point_temp.transform.position;
        agent.updateUpAxis = false;
        telephone = GameObject.FindGameObjectWithTag("telephone");
    }

    float stopTime;
    public GameObject currentBody;
    bool panic;

    void Update()
    {
        // panic-режим: бежим к телефону, только если мент ещё не вызван
        if (panic && police_Script == null && !policeCalled)
        {
            if (agent.isStopped)
            {
                agent.isStopped = false;
            }
            agent.destination = telephone.transform.position;
        }
        else
        {
            // после вызова мента — возвращаемся к обычному патрулированию
            if (point_temp != null)
            {
                agent.destination = point_temp.transform.position;
            }
        }

        if (police_Script == null)
        {
            police_Script = FindObjectOfType<Police_script>();
        }

        // anim
        if (agent.velocity.magnitude > 0.1f)
        {
            npc_anim.walking = true;
        }
        else
        {
            npc_anim.walking = false;
        }

        // point
        if (point_temp != null && Vector3.Distance(transform.position, point_temp.transform.position) <= 2f)
        {
            agent.isStopped = true;
            int how_long_stay = Random.Range(3, 10);
            stopTime += 0.0025f;
            if (stopTime >= how_long_stay && !panic)
            {
                float x = Random.Range(-27.6f, 27.6f);
                float y = Random.Range(-10.7f, 10.7f);
                point_temp.transform.position = new Vector3(x, y, -2);
                stopTime = 0;
                agent.isStopped = false;
            }
        }

        // rotate to point
        if (point_temp != null)
        {
            if (agent.destination.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        // body/panic mechanic
        if (Keyboard.current != null && Keyboard.current.eKey.isPressed)
        {
            bodyes = GameObject.FindGameObjectsWithTag("Dead_body");
        }

        // ищем ближайший труп
        currentBody = null;
        if (bodyes != null && bodyes.Length > 0)
        {
            float minDist = Mathf.Infinity;
            for (int i = 0; i < bodyes.Length; i++)
            {
                if (bodyes[i] == null) continue; // труп мог быть уничтожен ментом
                float d = Vector3.Distance(transform.position, bodyes[i].transform.position);
                if (d < minDist)
                {
                    minDist = d;
                    currentBody = bodyes[i];
                }
            }

            if (currentBody != null)
            {
                if (Vector3.Distance(transform.position, currentBody.transform.position) <= 6f && police_Script == null && !policeCalled)
                {
                    panic = true;
                    agent.isStopped = false;
                    agent.speed = 5;
                    police_Script = FindObjectOfType<Police_script>();
                }
                else if (police_Script != null)
                {
                    // мент уже есть — успокаиваемся, ждём
                    panic = false;
                    agent.isStopped = false;
                    agent.speed = 3.5f;
                }
            }
        }
        else
        {
            // трупов нет — можно снова паниковать при следующем трупе
            if (police_Script == null)
            {
                panic = false;
                policeCalled = false;
            }
        }

        // у телефона: звоним, только если ещё не звонили
        if (telephone != null && Vector3.Distance(transform.position, telephone.transform.position) <= 2.5f)
        {
            if (police_Script == null && !policeCalled)
            {
                panic = false;
                policeCalled = true;
                Instantiate(police_prefab);
            }
        }

        // лог с защитой от null
        // string bodyName = (currentBody != null) ? currentBody.name : "null";
        // Debug.Log("currentBody: " + bodyName + " panic:" + panic + " policeCalled:" + policeCalled);
    }
}