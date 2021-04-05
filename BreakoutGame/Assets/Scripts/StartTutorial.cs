using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartTutorial : MonoBehaviour
{

    public Text tutorialText;
    public Text tutorialStep;
    public Button nextButton;
    private int step = 0;
    private readonly string level = "Level1";

    // Start is called before the first frame update
    void Start()
    {
        tutorialStep.text = "1";
        tutorialText.text = "Use 'A' & 'D' or left & right arrows to move\n the paddle";
    }

    public void NextPressed()
    { 
        step += 1;
        if (step == 1)
        {
            tutorialStep.text = "2";
            tutorialText.text = "Press the space bar to launch the ball";
        }
        if (step == 2)
        {
            tutorialStep.text = "3";
            tutorialText.text = "Your score is represented on the top left of your screen";
        }
        if (step == 3)
        {
            tutorialStep.text = "4";
            tutorialText.text = "You have 3 lives to start with which is\n displayed at the top right of the screen";
        }
        if (step == 4)
        {
            tutorialStep.text = "5";
            tutorialText.text = "Catch the falling objects to trigger a \npower up";
        }
        if (step == 5)
        {
            tutorialStep.text = "6";
            tutorialText.text = "Press 'Esc' to pause the game and to \ndisplay more options";
            nextButton.GetComponentInChildren<Text>().text = "Play Game";
        }
        if (step == 6)
        {
            SceneManager.LoadScene(level);
        }
    }
}