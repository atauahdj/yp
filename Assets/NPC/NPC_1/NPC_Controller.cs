using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

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
    void Start()
    {
        npc_anim = GetComponent<Animator_script_NPC>();
        point_temp = Instantiate(point_prefab);
        float x = Random.Range(-27.6f,27.6f);
        float y = Random.Range(-10.7f, 10.7f);
        point_temp.transform.position = new Vector3(x, y, -2);
        agent = GetComponent<NavMeshAgent>();
        agent.destination = point_temp.transform.position;
        agent.updateUpAxis = false;
        telephone = GameObject.FindGameObjectWithTag("telephone");
    }

    float stopTime;
    //int trup = 0;
    bool panic;
    void Update()
    {
        //anim
        if (!agent.isStopped)
        {
            npc_anim.walking = true;
        }
        else
        {
            npc_anim.walking = false;
        }
        //point
        if(Vector3.Distance(transform.position, point_temp.transform.position) <= 2f)
        {
            agent.isStopped = true;
            int how_long_stay = Random.Range(3, 10);
            stopTime += 0.0025f;
            if(stopTime >= how_long_stay && !panic)
            {
                float x = Random.Range(-27.6f, 27.6f);
                float y = Random.Range(-10.7f, 10.7f);
                point_temp.transform.position = new Vector3(x, y, -2);
                stopTime = 0;
                agent.isStopped = false;
            }
        }
        //walking
        if (panic && police_Script != null)
        {
            if (agent.isStopped)
            {
                agent.isStopped = false;
            }
            agent.destination = telephone.transform.position;
        }
        else
        {
            agent.destination = point_temp.transform.position;
        }
        //rotate to point
        if (point_temp.transform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        //body/panic mechanic
        if (Keyboard.current.eKey.isPressed)
        {
            bodyes = GameObject.FindGameObjectsWithTag("Dead_body");
        }
        if(bodyes.Length > 0)
        {
            if (Vector3.Distance(transform.position, bodyes[Random.Range(0, bodyes.Length)].transform.position) <= 8f)
            {
                panic = true;
                agent.isStopped = false;
                agent.speed = 5;
                police_Script = FindObjectOfType<Police_script>();
            }
        }
        if(panic && Vector3.Distance(transform.position, telephone.transform.position) <= 2.5f)
        {
            panic = false;
        }
    }
}
