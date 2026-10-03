using UnityEngine;
using UnityEngine.InputSystem;

public class Animator_player : MonoBehaviour
{
    public SpriteRenderer player_renderer;
    public Sprite[] Idle;
    int idl;
    public Sprite[] Move;
    int mv;
    public Sprite[] Kill;
    int kl;
    public bool walking;
    ParentNPC pnpc;
    float currentTime;

    bool kill;
    Controller c;
    private void Start()
    {
        c = GetComponent<Controller>();
    }
    void Update()
    {
        if(c.speed == 5)
        {
            if (walking)
            {
                if (mv >= Move.Length)
                {
                    mv = 0;
                }
                player_renderer.sprite = Move[mv];
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
                player_renderer.sprite = Idle[idl];
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
        else
        {
            if (walking)
            {
                if (mv >= Move.Length)
                {
                    mv = 0;
                }
                player_renderer.sprite = Move[mv];
                currentTime += Time.deltaTime;
                if (currentTime > 0.125f)
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
                player_renderer.sprite = Idle[idl];
                currentTime += Time.deltaTime;
                if (currentTime > 0.125f)
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

        if (Keyboard.current.eKey.IsPressed())
        {
            kill = true;
        }
        if (kill)
        {
            if (kl >= Kill.Length)
            {
                kl = 0;
            }
            player_renderer.sprite = Kill[kl];
            currentTime += Time.deltaTime;
            if (currentTime > 0.125f)
            {
                kl++;
                currentTime = 0;
                if (kl >= Kill.Length)
                {
                    kill = false;
                }
            }
        }
    }
}