using Unity.AI.Navigation.Samples;
using UnityEngine;
using UnityEngine.AI;

public class Police_script : MonoBehaviour
{
    NavMeshAgent agent;
    public bool police_called;
    public Vector3 start_pos;
    GameObject body;
    GameObject[] bodies;
    float startTime;
    void Start()
    {
        police_called = true;
        agent = GetComponent<NavMeshAgent>();
        start_pos = transform.position;
        bodies = GameObject.FindGameObjectsWithTag("Dead_body");
    }

    public bool find_body;
    void Update()
    {
        if(bodies.Length >= 0)
        {
            float minDist = Mathf.Infinity;
            for (int i = 0; i < bodies.Length; i++)
            {
                if (bodies[i] == null) continue; // труп мог быть уничтожен ментом
                float d = Vector3.Distance(transform.position, bodies[i].transform.position);
                if (d < minDist)
                {
                    minDist = d;
                    body = bodies[i];
                }
            }
            if(Vector3.Distance(transform.position, start_pos) <= 3f && find_body)
            {
                Destroy(gameObject);
            }
            agent.updateRotation = false;
            if(body != null)
            {
                if(Vector3.Distance(transform.position, bodies[Random.Range(0, bodies.Length)].transform.position) <= 3f)
                {
                    if(body != null)
                    {
                        find_body = true;
                    }
                    Destroy(body);
                }   
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
        if(startTime <= 1f)
        {
            startTime += Time.deltaTime;
        }
        if(Vector3.Distance(transform.position, start_pos) < 2f && startTime > 0.9f)
        {
            Destroy(gameObject);
        }
        else
        {
            body = GameObject.FindGameObjectWithTag("Dead_body");
        }
        // /Debug.Log(Vector3.Distance(transform.position, start_pos) + "  ||||  " + startTime);
    }
}
