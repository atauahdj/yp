using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pause;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            pause.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void OffPause()
    {
        pause.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ExitToMenu(int number_menu)
    {
        SceneManager.LoadScene(number_menu);
    }
}
