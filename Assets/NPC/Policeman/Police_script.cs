using Unity.AI.Navigation.Samples;
using UnityEngine;
using UnityEngine.AI;

public class Police_script : MonoBehaviour
{
    NavMeshAgent agent;
    public bool police_called;
    public Vector3 start_pos;
    GameObject body;
    public GameObject[] bodies;
    public Controller player;

    public int true_body;
    float startTime;

    public bool find_body;
    public bool chase_player;
    public SpriteRenderer sprite_renderer;
    float time_chasing;

    void Start()
    {
        police_called = true;
        agent = GetComponent<NavMeshAgent>();
        start_pos = transform.position;
        bodies = GameObject.FindGameObjectsWithTag("Dead_body");
        true_body = bodies.Length;
        player = FindObjectOfType<Controller>();
        Debug.Log(true_body);
    }

    void Update()
    {
        startTime += Time.deltaTime;
        agent.updateRotation = false;
        // ---- 1. Погоня за игроком ----
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Dead_body"))
        {
            if (b == null) continue;
            Script_body body_sb = b.GetComponent<Script_body>();
            if (body_sb == null) continue;

            if (body_sb.timer_since_moment_murder <= 4f
                && Vector3.Distance(player.transform.position, b.transform.position) <= 7f
                && Vector3.Distance(player.transform.position, transform.position) <= 7f)
            {
                chase_player = true;
                break;
            }
        }

        if (chase_player)
        {
            agent.speed = 5f;
            agent.destination = player.transform.position;
            player.Wanted_Image.SetActive(true);
            time_chasing += Time.deltaTime;
            if(time_chasing > 30f)
            {
                chase_player = false;
            }
            if (Vector3.Distance(transform.position, player.transform.position) <= 3f)
            {
                Destroy(player.gameObject);
                chase_player = false;
                find_body = true;
            }
            return; // пока гонимся — не занимаемся трупами
        }
        else
        {
            agent.speed = 3.5f;
            player.Wanted_Image.SetActive(false);
        }

        agent.speed = 3.5f;

        // ---- 2. Работа с трупами ----
        true_body = 0;
        foreach (var b in bodies) if (b != null) true_body++;

        if (true_body == 0)
        {
            find_body = true;
            body = null;
        }

        // Возврат домой и самоуничтожение
        if (find_body && Vector3.Distance(transform.position, start_pos) <= 3f)
        {
            Destroy(gameObject);
            return;
        }

        // Выбор ближайшего трупа
        if (body == null && true_body > 0)
        {
            float minDist = Mathf.Infinity;
            for (int i = 0; i < bodies.Length; i++)
            {
                if (bodies[i] == null) continue;
                float d = Vector3.Distance(transform.position, bodies[i].transform.position);
                if (d < minDist)
                {
                    minDist = d;
                    body = bodies[i];
                }
            }
        }
        // Поворот к цели
            if(transform.position.x < agent.destination.x)
            {
                sprite_renderer.transform.rotation = Quaternion.Euler(0f, 0,0f);
            }
            else
            {
                sprite_renderer.transform.rotation = Quaternion.Euler(0f, -180f, 0f);
            }
        // Движение и уничтожение трупа
        if (body != null)
        {
            agent.destination = body.transform.position;

            if (Vector3.Distance(transform.position, body.transform.position) <= 3f)
            {
                Destroy(body);
                body = null;
                true_body--;
//                Debug.Log(true_body);
            }
        }
        else if (find_body)
        {
            agent.destination = start_pos;
        }
    }
}