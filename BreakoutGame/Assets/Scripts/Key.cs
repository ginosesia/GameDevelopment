using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Key : MonoBehaviour
{
    public string back = "MainMenu";

    public void Back()
    {
        SceneManager.LoadScene(back);
    }
}