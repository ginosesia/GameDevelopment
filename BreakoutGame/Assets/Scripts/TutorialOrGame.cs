using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialOrGame : MonoBehaviour
{

    private readonly string menu = "MainMenu";
    private readonly string tutorial = "Tutorial";
    private readonly string level1 = "Level1";

    public void Back()
    {
        SceneManager.LoadScene(menu);
    }

    public void Level1()
    {
        SceneManager.LoadScene(level1);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(tutorial);
    }

}
