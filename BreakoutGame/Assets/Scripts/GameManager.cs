using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int lives;
    public Text doubleSpeed;
    public bool gameIsPaused = false;

    [HideInInspector] public int score;
    [HideInInspector] public int numberOfBricks;
    [HideInInspector] public int numberOfBalls = 1;
    [HideInInspector] public int currentLevelNumber;
    [HideInInspector] public float timer = 2;
    [HideInInspector] public bool gameOver;
    [SerializeField] private LeaderBoard lb;
    [SerializeField] private Text scoreLabel;
    [SerializeField] private Text livesLabel;
    [SerializeField] private Text highScoreLabel;
    [SerializeField] private Text levelCompleteLabel;
    [SerializeField] private InputField nameInput;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject nextLevelPanel;
    [SerializeField] private Ball ball;
    [SerializeField] private Transform[] levels;
    [SerializeField] private PauseMenu pm;
    private readonly GameObject leaderBoardObject;
    private readonly string livesText = "Lives: ";
    private readonly string scoreText = "Score: ";
    private readonly string level = "Level1";
    private readonly string menu = "MainMenu";
    private readonly string rBrick = "Red-Brick";
    private readonly string pBrick = "Pink-Brick";
    private readonly string gBrick = "Green-Brick";
    private readonly string bBrick = "Blue-Brick";

    // Start is called before the first frame update
    void Start()
    {
        livesLabel.text = livesText + lives;
        scoreLabel.text = scoreText + score;
        numberOfBricks = GameObject.FindGameObjectsWithTag(rBrick).Length
            + GameObject.FindGameObjectsWithTag(pBrick).Length
            + GameObject.FindGameObjectsWithTag(gBrick).Length
            + GameObject.FindGameObjectsWithTag(bBrick).Length;
        gameIsPaused = false;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pm.ShowPauseMenu();
        }
    }

    public void AdjustLives(int change, bool extraLife)
    {
        if (lives >= 1)
        {
            if (lives == 1 && extraLife == false)
            {
                EndGame();
            } else
            {
                lives += change;
                livesLabel.text = livesText + lives;
            }
        } else
        {
            EndGame();
            livesLabel.text = livesText + 0;
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
        gameIsPaused = false;
        ShowScore();
    }

    public void ShowScore()
    {
        highScoreLabel.text = "Your score is: " + score + "\nEnter Your Name Below";
        nameInput.gameObject.SetActive(true);
    }

    public void SaveScore()
    {
        string name = nameInput.text;
        lb.AddNewEntry(score, name);
        nameInput.gameObject.SetActive(false);
        highScoreLabel.text = "Congratulations " + name + "\n Your New Score is: " + score;
    }


    public void AdjustScore(int change, bool doublePoints)
    {
        if (!doublePoints)
        {
            score += change;
            scoreLabel.text = scoreText + score;
        } else
        {
            score *= 2;
            scoreLabel.text = scoreText + score;
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(level);
        Time.timeScale = 1f;
        gameOver = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(menu);
    }
}
