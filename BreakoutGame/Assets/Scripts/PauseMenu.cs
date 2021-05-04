using Breakout.Scoreboards;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenu;
    public GameManager gm;

    [SerializeField] private Scoreboard scoreboard;
    [SerializeField] private Canvas leaderboard;

    private readonly string mainMenu = "MainMenu";
    private bool multiplayer = false;
    private readonly string multiPlayerLevel = "Multiplayer-Game";
    private readonly string singlePlayerLevel = "Singleplayer-Game";

    private void Start()
    {
        GetPlayingState();
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
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void Restart()
    {
        if (multiplayer) SceneManager.LoadScene(multiPlayerLevel);
        if (!multiplayer) SceneManager.LoadScene(singlePlayerLevel);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void LeaderBoard()
    {
        leaderboard.gameObject.SetActive(true);
    }

    public void LeaderBoardBackPressed()
    {
        leaderboard.gameObject.SetActive(false);
    }

    private void GetPlayingState()
    {
        if (PlayerPrefs.GetString("multiplayer") == "true") multiplayer = true;
        if (PlayerPrefs.GetString("multiplayer") == "false") multiplayer = false;
    }

}
