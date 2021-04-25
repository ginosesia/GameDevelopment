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
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private PowerUpManager powerUpManager;
    [SerializeField] private OptionsStates optionsStates;
    private SpriteRenderer ball;
    private int value;
    private readonly int lifeLost = -1;
    private readonly string jumpKey = "Jump";
    private readonly string outOfBounds = "OutOfBounds";
    private readonly string rbrick = "Red-Brick";
    private readonly string bbrick = "Blue-Brick";
    private readonly string gbrick = "Green-Brick";
    private readonly string pbrick = "Pink-Brick";
    private readonly float speed = 400f;


    // Start is called before the first frame update
    private void Start()
    {
        rigidBody = rigidBody.GetComponent<Rigidbody2D>();
        GetBallColor();
        
    }

    // Update is called once per frame
    private void Update()
    {
        
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

    private void GetBallColor()
    {
        ball = rigidBody.GetComponent<SpriteRenderer>();
        value = optionsStates.GetColor();
        switch (value)
        {
            case 0:
                ball.color = Color.red;
                break;
            case 1:
                ball.color = Color.green;
                break;
            case 2:
                ball.color = Color.blue;
                break;
            case 3:
                ball.color = Color.magenta;
                break;
            case 4:
                ball.color = Color.yellow;
                break;
            case 5:
                ball.color = Color.white;
                break;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(outOfBounds))
        {
            soundManager.PlaySound(1);
            rigidBody.velocity = Vector2.zero;
            gameManager.AdjustLives(lifeLost, false);
            ballInPlay = false;
        }
    }

    public void SetBallColor(Color color)
    {
        rigidBody.GetComponent<SpriteRenderer>().color = color;
    }

    public void SetBallPosition()
    {
        transform.position = paddle.position;
    }

    public void SetBallSpeed(float test)
    {
        if (test == 1)
        {
            ballSpeed = speed;
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
        //Play Sound for brick hit
        soundManager.PlaySound(0);

        //Play brick broken animation
        Transform animation = Instantiate(transform, collision.transform.position, collision.transform.rotation);
        Destroy(animation.gameObject, 1.5f);

        //Select power up when brick is hit
        powerUpManager.SelectPowerUp(collision);
    }
}