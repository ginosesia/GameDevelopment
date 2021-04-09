using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public Rigidbody2D rigidBody;
    public bool ballInPlay;
    public bool multipleBalls = false;
    public float ballSpeed;

    [SerializeField] private Transform paddle;
    [SerializeField] private Transform RedBrick;
    [SerializeField] private Transform PinkBrick;
    [SerializeField] private Transform BlueBrick;
    [SerializeField] private Transform GreenBrick;
    [SerializeField] private Transform powerUp;
    [SerializeField] private Transform doubleSpeed;
    [SerializeField] private Transform doublePointsBall;
    [SerializeField] private Transform spawnBalls;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioClip PaddleHit;
    [SerializeField] private AudioClip WallHit;
    [SerializeField] private AudioClip OutOfBoundsHit;
    [SerializeField] private AudioClip BrickHit;

    private OptionsManager optionsManager;
    private Collision2D spawnBallsCollision;
    private readonly int lifeLost = -1;
    private readonly bool powerUpSetting = false;
    private bool doubleSpeedDropped = false;
    private bool doublePoints = false;
    private readonly string jumpKey = "Jump";
    private readonly string outOfBounds = "OutOfBounds";
    private readonly string rbrick = "Red-Brick";
    private readonly string bbrick = "Blue-Brick";
    private readonly string gbrick = "Green-Brick";
    private readonly string pbrick = "Pink-Brick";
    private static readonly string Sound = "Sound";
    private static readonly string Music = "Music";
    private static readonly string PowerUps = "PowerUps";
    private readonly string[] switches = { Sound, Music, PowerUps };
    private string state;
    private bool SoundState; 
    //private bool MusicState; 
    private bool PowerUpsState; 


    // Start is called before the first frame update
    private void Start()
    {
        rigidBody.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckGameOptionsStates();
        if (gameManager.numberOfBricks == 0) SetBallPosition();
        else
        {
            if (gameManager.gameOver) return;
            if (!ballInPlay) SetBallPosition();
            if (Input.GetButtonDown(jumpKey) && !ballInPlay)
            {
                ballInPlay = true;
                rigidBody.AddForce(Vector2.up * ballSpeed);
            }
        }
    }

    private void CheckGameOptionsStates()
    {
        foreach (string togleSwitch in switches)
        {
            state = PlayerPrefs.GetString(togleSwitch);
            switch (state)
            {
                case "True":
                    if (togleSwitch == Sound) SoundState = true;
                    //if (togleSwitch == Music) MusicState = true;
                    if (togleSwitch == Music) PowerUpsState = true;
                    break;
                case "False":
                    if (togleSwitch == Sound) SoundState = false;
                    //if (togleSwitch == Music) MusicState = false;
                    if (togleSwitch == Music) PowerUpsState = false;
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(outOfBounds))
        {
            if (SoundState) PlayClip(OutOfBoundsHit);
            rigidBody.velocity = Vector2.zero;
            gameManager.AdjustLives(lifeLost, false);
            ballInPlay = false;
        }
    }

    public void SetBallPosition()
    {
        transform.position = paddle.position;
    }

    public void SetBallSpeed(float test)
    {
        if (test == 1)
        {
            ballSpeed = 400f;
        }
        else
        {
            ballSpeed *= test;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.CompareTag(rbrick) || collision.transform.CompareTag(gbrick)
            || collision.transform.CompareTag(pbrick) || collision.transform.CompareTag(bbrick))
        {
            Brick brick = collision.gameObject.GetComponent<Brick>();
            if (brick.hitsNeeded > 1)
            {
                brick.HitBrick();
                gameManager.AdjustScore(brick.points, false);
            }
            else
            {
                if (collision.gameObject.CompareTag(rbrick)) PlayAnimation(RedBrick, collision);
                if (collision.gameObject.CompareTag(pbrick)) PlayAnimation(PinkBrick, collision);
                if (collision.gameObject.CompareTag(bbrick)) PlayAnimation(BlueBrick, collision);
                if (collision.gameObject.CompareTag(gbrick)) PlayAnimation(GreenBrick, collision);

                gameManager.AdjustScore(brick.points, false);
                gameManager.UpdateNumberOfBricks();
                Destroy(collision.gameObject);
            }
        }
    }

    private void PlayAnimation(Transform transform, Collision2D collision)
    {
        PlayClip(PaddleHit);
        Transform animation = Instantiate(transform, collision.transform.position, collision.transform.rotation);
        Destroy(animation.gameObject, 1.5f);
        SelectPowerUp(collision);
    }

    private void SelectPowerUp(Collision2D collision)
    {
        if (PowerUpsState)
        {
            int random = Random.Range(1, 100);
            if (random <= 15 && gameManager.currentLevelNumber > 2) DropDoubleSpeed(collision);
            if (random >= 20 && random <= 35 && gameManager.score <= 150) DropDoublePoints(collision);
            if (random >= 35 && random <= 65 && gameManager.lives < 4) DropExtraLife(collision);
            if (random >= 92 && gameManager.numberOfBalls == 1)
            {
                Instantiate(spawnBalls, collision.transform.position, collision.transform.rotation);
                multipleBalls = true;
                spawnBallsCollision = collision;
            }
        }
    }

    private void DropDoublePoints(Collision2D collision)
    {
        if (!doublePoints) Instantiate(doublePointsBall, collision.transform.position, collision.transform.rotation);
        doublePoints = true; 
    }

    public void DropMultipleBalls()
    {
        if (!multipleBalls) return;
        for (int i = 1; i <=3; i++)
        {
            Ball ball2 = gameObject.AddComponent<Ball>();
            Instantiate(ball2, spawnBallsCollision.transform.position, spawnBallsCollision.transform.rotation);
        }
    }

    private void DropExtraLife(Collision2D collision)
    {
        if (gameManager.lives < 5)
        {
            int random = Random.Range(1, 101);
            if (random < 15) Instantiate(powerUp, collision.transform.position, collision.transform.rotation);
        }
    }

    private void DropDoubleSpeed(Collision2D collision)
    {
        int random = Random.Range(1, 101);
        if (random < 15) if (!doubleSpeedDropped)
            {
                Instantiate(doubleSpeed, collision.transform.position, collision.transform.rotation);
                doubleSpeedDropped = true;
            }
    }

    private void PlayClip(AudioClip audio)
    {
        GetComponent<AudioSource>().PlayOneShot(audio);
    }
}