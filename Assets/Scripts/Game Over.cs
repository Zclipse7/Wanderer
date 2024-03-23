using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Forest Level");
    }

    public void Quit()
    {
        Application.Quit();
    }
}