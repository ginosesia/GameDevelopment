using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public bool ballInPlay;
    public Transform paddle;
    public Transform RedBrick;
    public Transform PinkBrick;
    public Transform BlueBrick;
    public Transform GreenBrick;
    public Transform powerUp;
    public Transform doubleSpeed;
    private readonly int lifeLost = -1;
    private bool doubleSpeedDropped = false;
    public float ballSpeed;
    private readonly string jumpKey = "Jump";
    private readonly string outOfBounds = "OutOfBounds";
    private readonly string rbrick = "Red-Brick";
    private readonly string bbrick = "Blue-Brick";
    private readonly string gbrick = "Green-Brick";
    private readonly string pbrick = "Pink-Brick";
    public GameManager gameManager;

    // Start is called before the first frame update
    private void Start()
    {
        rigidBody.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameManager.numberOfBricks == 0)
        {
            SetBallPosition();
        }
        else
        {
            if (gameManager.gameOver)
            {
                return;
            }

            if (!ballInPlay)
            {
                SetBallPosition();
            }

            if (Input.GetButtonDown(jumpKey) && !ballInPlay)
            {
                ballInPlay = true;
                rigidBody.AddForce(Vector2.up * ballSpeed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(outOfBounds))
        {
            rigidBody.velocity = Vector2.zero;
            gameManager.AdjustLives(lifeLost);
            ballInPlay = false;
        }
    }

    public void SetBallPosition()
    {
        transform.position = paddle.position;
    }

    public void SetBallSpeed(float test)
    {
        if (test == 1) ballSpeed = 400f;
        else ballSpeed *= test;
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
                gameManager.AdjustScore(brick.points);
            }
            else
            {
                if (collision.gameObject.CompareTag(rbrick))
                {
                    PlayAnimation(RedBrick, collision);
                }
                else if (collision.gameObject.CompareTag(pbrick))
                {
                    PlayAnimation(PinkBrick, collision);
                }
                else if (collision.gameObject.CompareTag(bbrick))
                {
                    PlayAnimation(BlueBrick, collision);
                }
                else if (collision.gameObject.CompareTag(gbrick))
                {
                    PlayAnimation(GreenBrick, collision);
                }
                gameManager.AdjustScore(brick.points);
                gameManager.UpdateNumberOfBricks();
                Destroy(collision.gameObject);
            }
        }
    }

    public void PlayAnimation(Transform transform, Collision2D collision)
    {
        Transform animation = Instantiate(transform, collision.transform.position, collision.transform.rotation);
        Destroy(animation.gameObject, 1.5f);
        DropPowerUp(collision);
    }

    public void DropPowerUp(Collision2D collision)
    {
        if (gameManager.lives < 5)
        {
            int random = Random.Range(1, 101);
            if (random < 15)
            {
                if (random <= 5)
                {
                    if (!doubleSpeedDropped) DropDoubleSpeed(collision);
                }
                else
                {
                    Instantiate(powerUp, collision.transform.position, collision.transform.rotation);
                }
            }
        }
    }

    public void DropDoubleSpeed(Collision2D collision)
    {
        if (!doubleSpeedDropped)
        {
            Instantiate(doubleSpeed, collision.transform.position, collision.transform.rotation);
            doubleSpeedDropped = true;
        }
    }
}