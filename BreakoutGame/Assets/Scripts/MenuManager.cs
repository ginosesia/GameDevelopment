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
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(tutorial);
    }

    public void Options()
    {
        SceneManager.LoadScene(options);
    }

    public void LeaderBoard()
    {
        SceneManager.LoadScene(leaderBoard);
    }

    public void Exit()
    {
       Application.Quit();
    }
}
