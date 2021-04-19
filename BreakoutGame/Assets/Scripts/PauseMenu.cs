using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenu;
    public GameObject leaderBoard;
    public GameManager gm;
    [SerializeField] private GameObject mainMenu;
    private readonly string level = "Level1";

    private void Start()
    {
        
    }

    public void Update()
    {

    }

    public void ShowPauseMenu()
    {
        if (gamePaused)
        {
            Resume();
        }
        else
        {
            if (gm.gameOver == false) Pause();
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
        mainMenu.SetActive(true);
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

    public void LeaderBoard()
    {
        leaderBoard.SetActive(true);
    }

    public void leaderBoardBackPressed()
    {
        leaderBoard.SetActive(false);
    }
}
