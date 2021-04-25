using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMode : MonoBehaviour
{

    private readonly string level = "Game";

    public void Singleplayer()
    {
        SceneManager.LoadScene(level);

    }

    public void Multiplayer()
    {
        SceneManager.LoadScene(level);
    }

}
