using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMode : MonoBehaviour
{
    private Paddle paddle;

    private readonly string multiplayer = "true";
    private readonly string singleplayer = "false";
    private readonly string multiPlayerLevel = "Multiplayer-Game";
    private readonly string singlePlayerLevel = "Singleplayer-Game";

    public void Singleplayer()
    {
        SetMultiplayer(singleplayer);
        SceneManager.LoadScene(singlePlayerLevel);
    }

    public void Multiplayer()
    {
        SetMultiplayer(multiplayer);
        SceneManager.LoadScene(multiPlayerLevel);
    }

    private void SetMultiplayer(string state)
    {
        PlayerPrefs.SetString("multiplayer", state);
    }

}
