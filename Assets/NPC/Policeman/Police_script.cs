using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation.Samples;
using UnityEngine;
using UnityEngine.AI;

public class Police_script : MonoBehaviour
{
    NavMeshAgent agent;
    public bool police_called;
    public Vector3 start_pos;
    GameObject body;
    void Start()
    {
        police_called = true;
        agent = GetComponent<NavMeshAgent>();
        start_pos = transform.position;
    }

    bool find_body;
    void Update()
    {
        if(Vector3.Distance(transform.position, start_pos) <= 3f && find_body)
        {
            Destroy(gameObject);
        }
        agent.updateRotation = false;
        body = GameObject.FindGameObjectWithTag("Dead_body");
        if(Vector3.Distance(transform.position, body.transform.position) <= 3f)
        {
            find_body = true;
            Destroy(body);
        }
        if(!find_body)
        {
            agent.destination = body.transform.position;
        }
        else
        {
            agent.destination = start_pos;
        }
    }
}
