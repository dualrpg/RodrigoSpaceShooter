using UnityEngine;
using TMPro;


public class GameScore : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    int score;

    public int Score
    {
        get
        {
            return this.score;
        }
        set
        {
            this.score = value;
            UpdateGameScoreTextUI();
        }
    }

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI> ();
    }

    void UpdateGameScoreTextUI()
    {
        string scoreStr = string.Format ("{0:0000000}", score);
        scoreText.text = scoreStr;
    }
}
