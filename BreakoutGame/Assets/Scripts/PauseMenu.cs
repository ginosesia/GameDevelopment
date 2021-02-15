using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenu;
    public GameManager gm;
    private readonly string menu = "MainMenu";
    private readonly string level = "Level1";

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gamePaused)
            {
                Resume();
            } else
            {
                if (gm.gameOver == false) Pause();
            }
        }
    }


    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;

    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(menu);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(level);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;

    }
}
