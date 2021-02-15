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

    private readonly int lifeLost = -1;

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
                    Transform animation = Instantiate(RedBrick, collision.transform.position, collision.transform.rotation);
                    Destroy(animation.gameObject, 1.5f);
                    DropPowerUp(collision);
                }
                else if (collision.gameObject.CompareTag(pbrick))
                {
                    Transform animation = Instantiate(PinkBrick, collision.transform.position, collision.transform.rotation);
                    Destroy(animation.gameObject, 1.5f);
                    DropPowerUp(collision);
                }
                else if (collision.gameObject.CompareTag(bbrick))
                {
                    Transform animation = Instantiate(BlueBrick, collision.transform.position, collision.transform.rotation);
                    DropPowerUp(collision);
                    Destroy(animation.gameObject, 1.5f);
                }
                else if (collision.gameObject.CompareTag(gbrick))
                {
                    Transform animation = Instantiate(GreenBrick, collision.transform.position, collision.transform.rotation);
                    Destroy(animation.gameObject, 1.5f);
                    DropPowerUp(collision);
                }
                gameManager.AdjustScore(brick.points);
                gameManager.UpdateNumberOfBricks();
                Destroy(collision.gameObject);

            }
        }
    }

    public void DropPowerUp(Collision2D collision)
    {
        int random = Random.Range(1,101);
        if (random < 15 )
        {
            Instantiate(powerUp, collision.transform.position, collision.transform.rotation);
        }
    }


}
