using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private Transform entryContainer;
    [SerializeField] private Transform entryTemplate;
    private List<Transform> listOfScoresTransform;
    private readonly string menu = "MainMenu";

    private void Awake()
    {
        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);

        for (int i = 0; i < highscores.highScoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highScoreEntryList.Count; j++)
            {
                if (highscores.highScoreEntryList[j].score > highscores.highScoreEntryList[i].score)
                {
                    HighScore tmp = highscores.highScoreEntryList[i];
                    highscores.highScoreEntryList[i] = highscores.highScoreEntryList[j];
                    highscores.highScoreEntryList[j] = tmp;
                }
            }
        }

        listOfScoresTransform = new List<Transform>();
        foreach (HighScore score in highscores.highScoreEntryList)
        {
            CreateEntry(score, entryContainer, listOfScoresTransform);
        }
    }

    private void CreateEntry(HighScore highScore, Transform container, List<Transform> transformList)
    {
        float height = 40f;
        Transform entryObject = Instantiate(entryTemplate, container);
        RectTransform rectTransformObject = entryObject.GetComponent<RectTransform>();
        rectTransformObject.anchoredPosition = new Vector2(0, -height * transformList.Count);

        entryObject.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        if (rank <= 10)
        {
            rankString = rank switch
            {
                1 => "1st",
                2 => "2nd",
                3 => "3rd",
                _ => rank + "th",
            };
            int score = highScore.score;
            string name = highScore.name;
            entryObject.Find("PositionEntry").GetComponent<Text>().text = rankString;
            entryObject.Find("ScoreEntry").GetComponent<Text>().text = score.ToString();
            entryObject.Find("NameEntry").GetComponent<Text>().text = name;
            transformList.Add(entryObject);
        }
    }

    private class HighScores
    {
        public List<HighScore> highScoreEntryList;
    }

    [System.Serializable]
    private class HighScore
    {
        public int score;
        public string name;
    }

    public void AddNewEntry(int score, string name)
    {
        HighScore entry = new HighScore
        {
            score = score,
            name = name
        };

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);

        highscores.highScoreEntryList.Add(entry);
        string json = JsonUtility.ToJson(highscores);

        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    public void Back()
    {
        SceneManager.LoadScene(menu);
    }
}