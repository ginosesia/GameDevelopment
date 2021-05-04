using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartTutorial : MonoBehaviour
{
    public Text tutorialText;

    [SerializeField] private GameManager gameManager;
    private string message;
    private bool multiplayer;
    private readonly int[] step = {1,2,3,4,5,6,7};
    int i = 0;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("multiplayer") == "true") multiplayer = true;
    }

    public void PlayTutorial()
    {
        tutorialText.text = "Tutorial";
        Invoke(nameof(SetText), 3);
    }


    private void SetText()
    {
        if (i >= 9)
        {
            gameManager.tutorialCanvas.gameObject.SetActive(false);
            PlayerPrefs.SetString("Tutorial", "false");
        }
        else
        {
            tutorialText.text = Next(i);
            i++;
            int timeLength = 4;
            if (i == 3) timeLength = 5;
            Invoke(nameof(SetText), timeLength);
        }
    }

    public string Next(int i)
    {
        string singleplayerText = "Use the 'Left' and 'Right' arrows to move the paddle";
        string multiplayerText = "Player one: \nUse the 'Left' and 'Right' arrows to move the paddle\n\n" +
            "Player two:\nUse the 'A' and 'D' keys to move the paddle";

        switch (i)
        {
            case 0:
                if (!multiplayer)
                {
                    message = singleplayerText;
                } else
                {
                    message = multiplayerText;
                }
                break;
            case 1:
                message = "To launch the ball press the 'Space' key on your keyboard";
                break;
            case 2:
                message = "Your score is represented on the top left of your screen";
                break;
            case 3:
                message = "You have 3 lives to start the game. \n Indicated at the top right of your screen";
                break;
            case 4:
                message = "Catch the falling objects to trigger power ups";
                break;
            case 5:
                message = "Press 'Esc' to pause the game to display additional options";
                break;
            case 6:
                message = "To watch a replay press the 'R' key on your keyboard";
                break;
            case 7:
                message = "To return to gameplay from a replay press the 'R' key again";
                break;
            case 8:
                message = "Enjoy the Game\nGood luck";
                break;
        }
        return message;
    }
}