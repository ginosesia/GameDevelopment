using System.Collections;
using System.Collections.Generic;
using Breakout.Scoreboards;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public int lives;

    private readonly string livesText = "Lives: ";
    private readonly string scoreText = "Score: ";
    private readonly string gameOverScoreText = "Your score is: ";
    private readonly string nameText = "\nEnter Your Name Below";
    private readonly string congrats = "Congratulations ";
    private readonly string newScore = "\n Your New Score is: ";

    [HideInInspector] public int score;
    [SerializeField] private Scoreboard scoreboard;
    [SerializeField] private Canvas leaderboard;
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private Text scoreLabel;
    [SerializeField] private Text livesLabel;
    [SerializeField] private Text highScoreLabel;
    [SerializeField] private Text powerUp;
    [SerializeField] private InputField nameInput;
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        lives = 3;
        powerUp.gameObject.SetActive(false);
        livesLabel.text = livesText + lives;
        scoreLabel.text = scoreText + score;

        leaderboard.gameObject.SetActive(false);
    }

    public void AdjustLives(int change, bool extraLife)
    {
        if (lives >= 1)
        {
            if (lives == 1 && extraLife == false)
            {
                gameManager.EndGame();
            }
            else
            {
                lives += change;
                livesLabel.text = livesText + lives;
            }
        }
        else
        {
            gameManager.EndGame();
            livesLabel.text = livesText + 0;
        }
    }

    public void ShowScore()
    {
        highScoreLabel.text = gameOverScoreText + score + nameText;
        nameInput.gameObject.SetActive(true);
    }

    public void SaveScore()
    {
        string name = nameInput.text;

        ScoreboardEntryData entryData = new ScoreboardEntryData
        {
            entryName = name,
            entryScore = score
        };

        scoreboard.AddEntry(entryData);

        nameInput.gameObject.SetActive(false);
        highScoreLabel.text = congrats + name + newScore + score;
    }

    public void AdjustScore(int change, bool doublePoints)
    {
        if (!doublePoints)
        {
            score += change;
            scoreLabel.text = scoreText + score;
        }
        else
        {
            score *= 2;
            scoreLabel.text = scoreText + score;
        }
    }
}