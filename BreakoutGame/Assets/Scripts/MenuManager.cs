using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public string tutorial = "Tutorial";
    public string gamemode = "GameMode";
    public string options = "Options";
    public string key = "Key";


    public void Tutorial()
    {
        SceneManager.LoadScene(tutorial);
    }

    public void Options()
    {
        SceneManager.LoadScene(options);
    }

    public void Exit()
    {
       Application.Quit();
    }

    public void ShowKey()
    {
        SceneManager.LoadScene(key);

    }

    public void GameMode()
    {
        SceneManager.LoadScene(gamemode);
    }
}
