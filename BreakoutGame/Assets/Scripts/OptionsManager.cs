using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsManager : MonoBehaviour
{
    private readonly string menu = "MainMenu";

    public void Back()
    {
        SceneManager.LoadScene(menu);
    }

}
