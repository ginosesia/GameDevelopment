using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int lives;
    public int score;
    public int numberOfBricks;
    public int numberOfBalls = 1;
    public int currentLevelNumber;
    public float timer = 2;
    public bool gameOver;
    public static bool gameIsPaused = false;
    public Text scoreLabel;
    public Text livesLabel;
    public Text doubleSpeed;
    public Text highScoreLabel;
    public Text levelCompleteLabel;
    public InputField nameInput;
    public GameObject gameOverPanel;
    public GameObject nextLevelPanel;
    public Ball ball; 
    public Transform[] levels;
    private LeaderBoard lb;
    private readonly string livesText = "Lives: ";
    private readonly string scoreText = "Score: ";
    private readonly string level = "Level1";
    private readonly string menu = "MainMenu";
    private readonly string gball = "Ball";
    private readonly string rBrick = "Red-Brick";
    private readonly string pBrick = "Pink-Brick";
    private readonly string gBrick = "Green-Brick";
    private readonly string bBrick = "Blue-Brick";



    // Start is called before the first frame update
    void Start()
    {
        lb = GameObject.FindObjectOfType(typeof(LeaderBoard)) as LeaderBoard;
        livesLabel.text = livesText + lives;
        scoreLabel.text = scoreText + score;
        numberOfBricks = GameObject.FindGameObjectsWithTag(rBrick).Length
            + GameObject.FindGameObjectsWithTag(pBrick).Length
            + GameObject.FindGameObjectsWithTag(gBrick).Length
            + GameObject.FindGameObjectsWithTag(bBrick).Length;
        gameIsPaused = false;
        Time.timeScale = 1f;
    }


    public void AdjustLives(int change)
    {
        if (lives >= 1)
        {
            lives += change;
            livesLabel.text = livesText + lives;
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
        //LeaderBoard lb = gameObject.GetComponent<LeaderBoard>();
        lb.AddNewEntry(score, name);
        nameInput.gameObject.SetActive(false);
        highScoreLabel.text = "Congratulations " + name + "\n Your New Score is: " + score;
    }


    public void AdjustScore(int change)
    {
        score += change;
        scoreLabel.text = scoreText + score;
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

    public void WatchReplay()
    {

    }
}
