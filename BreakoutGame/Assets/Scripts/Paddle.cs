using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paddle : MonoBehaviour
{

    public float speed;
    public float rightBoundary;
    public float leftBoundary;
    public Ball ball;
    public GameManager gameManager;
    public Rigidbody2D rigidBody;
    public Rigidbody2D rigidBodySecondPlayer;

    [HideInInspector] public bool inReplayMode = false;

    [SerializeField] private SoundManager soundManager;
    [SerializeField] private OptionsStates optionsStates;
    [SerializeField] private PowerUpCollected powerUpCollected;
    private readonly string singleplayerPaddle = "singleplayer-paddle";
    private readonly string multiplayerPaddle = "multiplayer-paddle";
    private readonly string horizontal = "Horizontal";
    private Image paddle;
    private Image paddleSecond;
    private int value;
    private float horizontalValue;
    private bool multiplayer = false;

    // Start is called before the first frame update
    private void Start()
    {
        GetPlayingState();

        if (multiplayer)
        {
            rigidBodySecondPlayer = GameObject.Find(multiplayerPaddle).GetComponent<Rigidbody2D>();
            rigidBody = GameObject.Find(singleplayerPaddle).GetComponent<Rigidbody2D>();
        }

        if (!multiplayer) rigidBody = GameObject.Find(singleplayerPaddle).GetComponent<Rigidbody2D>();

        SetPaddleColor();
    }

    private void GetPlayingState()
    {
        if (PlayerPrefs.GetString("multiplayer") == "true") multiplayer = true;
        if (PlayerPrefs.GetString("multiplayer") == "false") multiplayer = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!inReplayMode)
        {

            MovePaddle(paddle, singleplayerPaddle, KeyCode.LeftArrow, KeyCode.RightArrow,rigidBody);

            if (multiplayer)
            {
                MovePaddle(paddleSecond, multiplayerPaddle, KeyCode.A, KeyCode.D, rigidBodySecondPlayer);
            }

            //Check if paddle reached right or left boundary
            if (transform.position.x < leftBoundary)
            {
                transform.position = new Vector2(leftBoundary, transform.position.y);
            }

            if (transform.position.x > rightBoundary)
            {
                transform.position = new Vector2(rightBoundary, transform.position.y);
            }
        }
    }

    private void MovePaddle(Image paddle, string tag, KeyCode leftCode,KeyCode rightCode, Rigidbody2D rigidBody)
    {
        horizontalValue = Input.GetAxis(horizontal);

        if (paddle.CompareTag(tag))
        { 
            if (Input.GetKey(leftCode))
            {
                rigidBody.transform.Translate(Vector2.right * horizontalValue * Time.deltaTime * speed);
            }
            if (Input.GetKey(rightCode))
            {
                rigidBody.transform.Translate(Vector2.right * horizontalValue * Time.deltaTime * speed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        powerUpCollected.PowerUpCollection(collision);
    }


    public void SetPaddleColor()
    {
        paddle = rigidBody.GetComponent<Image>();
        if (multiplayer) paddleSecond = rigidBodySecondPlayer.GetComponent<Image>();
        value = optionsStates.GetColor();
        switch (value)
        {
            case 0:
                paddle.color = Color.red;
                if (multiplayer) paddleSecond.color = Color.red;
                break;
            case 1:
                paddle.color = Color.green;
                if (multiplayer) paddleSecond.color = Color.green;
                break;
            case 2:
                paddle.color = Color.blue;
                if (multiplayer) paddleSecond.color = Color.blue;
                break;
            case 3:
                paddle.color = Color.magenta;
                if (multiplayer) paddleSecond.color = Color.magenta;
                break;
            case 4:
                paddle.color = Color.yellow;
                if (multiplayer) paddleSecond.color = Color.yellow;
                break;
            case 5:
                paddle.color = Color.white;
                if (multiplayer) paddleSecond.color = Color.white;
                break;
        }
    }
}
