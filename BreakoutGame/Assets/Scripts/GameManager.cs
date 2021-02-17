using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int lives;
    public int score;
    public int numberOfBricks;
    public int levelNumber = 1;
    public int currentLevelNumber;
    public bool gameOver;
    public static bool gameIsPaused = false;
    public Text scoreLabel;
    public Text livesLabel;
    public Text highScoreLabel;
    public Text levelCompleteLabel;
    public InputField nameInput;
    public GameObject gameOverPanel;
    public GameObject nextLevelPanel;
    public Ball ball; 
    public Transform[] levels;
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

    public void AdjustLives(int change)
    {
        if (lives > 1)
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
                nextLevelPanel.SetActive(true);
                levelNumber++;
            }
        }   
    }

    public void NextLevel()
    {
        nextLevelPanel.SetActive(false);
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
        int highScore = PlayerPrefs.GetInt("HIGHSCORE");
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HIGHSCORE", score);
            highScoreLabel.text = "New High Score: \nEnter Your Name Below.";
            nameInput.gameObject.SetActive(true);
        } else
        {
            highScoreLabel.text = PlayerPrefs.GetString("HIGHSCORENAME") + "'s High Score was "  + highScore + " \n Can you Beat it?";
        }
        
    }

    public void EndNameEdit()
    {
        string name = nameInput.text;
        PlayerPrefs.SetString("HIGHSCORENAME", name);
        nameInput.gameObject.SetActive(false);
        highScoreLabel.text = "Congratulations " + name + "\n Your New High Score is: " + score;

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
