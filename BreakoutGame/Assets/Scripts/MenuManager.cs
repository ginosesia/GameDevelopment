using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public string tutorial = "TutorialOrGame";
    public string play = "Level1";
    public string options = "Options";
    public string leaderBoard = "LeaderBoard";

    public void Play()
    {
        SceneManager.LoadScene(play);
        Debug.Log("Play");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(tutorial);
        Debug.Log("Tutorial");
    }

    public void Options()
    {
        SceneManager.LoadScene(options);
        Debug.Log("Options");
    }

    public void LeaderBoard()
    {
        SceneManager.LoadScene(leaderBoard);
        Debug.Log("LeaderBoard");
    }

    public void Exit()
    {
       Application.Quit();
       Debug.Log("Exit");
    }
}
