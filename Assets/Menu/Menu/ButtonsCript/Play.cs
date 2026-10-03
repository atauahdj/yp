using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public void LOAD_LEVEL(int level_index)
    {
        Time.timeScale = 1f;    
        SceneManager.LoadScene(level_index);
    }
}
