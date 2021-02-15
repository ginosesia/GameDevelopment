using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public string level = "Level1";
    public string options = "Options";

    public void Play()
    {
        SceneManager.LoadScene(level);
    }

    public void Options()
    {
        SceneManager.LoadScene(options);
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
