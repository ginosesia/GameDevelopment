using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text doubleSpeed;
    public bool gameIsPaused = false;
    public Canvas tutorialCanvas;

    [HideInInspector] public int numberOfBricks;
    [HideInInspector] public int numberOfBalls = 1;
    [HideInInspector] public int currentLevelNumber;
    [HideInInspector] public float timer = 2;
    [HideInInspector] public bool gameOver;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Ball ball;
    [SerializeField] private Transform[] levels;
    [SerializeField] private PauseMenu pm;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private StartTutorial tutorial;

    private bool multiplayer = false;
    private readonly GameObject leaderBoardObject;
    private readonly string menu = "MainMenu";
    private readonly string rBrick = "Red-Brick";
    private readonly string pBrick = "Pink-Brick";
    private readonly string gBrick = "Green-Brick";
    private readonly string bBrick = "Blue-Brick";
    private readonly string multiPlayerLevel = "Multiplayer-Game";
    private readonly string singlePlayerLevel = "Singleplayer-Game";

    // Start is called before the first frame update
    private void Start()
    {
        GetPlayingState();
        PlayTutorial();
        numberOfBricks = GameObject.FindGameObjectsWithTag(rBrick).Length
            + GameObject.FindGameObjectsWithTag(pBrick).Length
            + GameObject.FindGameObjectsWithTag(gBrick).Length
            + GameObject.FindGameObjectsWithTag(bBrick).Length;
        gameIsPaused = false;
        pm.gameObject.SetActive(gameIsPaused);
        Time.timeScale = 1f;
    }

    private void PlayTutorial()
    {
        if (PlayerPrefs.GetString("Tutorial") == "True")
        {
            tutorialCanvas.gameObject.SetActive(true);
            tutorial.PlayTutorial();
        } else
        {
            tutorialCanvas.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pm.ShowPauseMenu();
        }
    }


    public void UpdateNumberOfBricks()
    {
        numberOfBricks--;
        if (numberOfBricks <= 0)
        {
            if (currentLevelNumber >= levels.Length - 1)
            {
                EndGame();
            } else
            {
                Invoke(nameof(DelayNextLevel), 1);
            }
        }   
    }

    public void DelayNextLevel()
    {
        ball.rigidBody.velocity = Vector2.zero;
        ball.ballInPlay = false;
        NextLevel();
    }

    public void NextLevel()
    {
        currentLevelNumber++;
        Instantiate(levels[currentLevelNumber], Vector2.zero, Quaternion.identity);
        numberOfBricks = GameObject.FindGameObjectsWithTag(rBrick).Length
            + GameObject.FindGameObjectsWithTag(pBrick).Length
            + GameObject.FindGameObjectsWithTag(gBrick).Length
            + GameObject.FindGameObjectsWithTag(bBrick).Length;
    }

    public void EndGame()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        uIManager.ShowScore();
    }

    public void PlayAgain()
    {
        if (multiplayer) SceneManager.LoadScene(multiPlayerLevel);
        if (!multiplayer) SceneManager.LoadScene(singlePlayerLevel);
        Time.timeScale = 1f;
        gameOver = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(menu);
    }

    private void GetPlayingState()
    {
        if (PlayerPrefs.GetString("multiplayer") == "true") multiplayer = true;
        if (PlayerPrefs.GetString("multiplayer") == "false") multiplayer = false;
    }
}
