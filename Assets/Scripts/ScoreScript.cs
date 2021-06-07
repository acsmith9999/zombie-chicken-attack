using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public static int scoreValue;
    Text score;

    public static int highScoreValue;
    //Text highScore;

    // Start is called before the first frame update
    void Start()
    {
        scoreValue = 0;
        score = GameObject.Find("Score").GetComponent<Text>();

       // highScore = GameObject.Find("HighScore").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if (scoreValue > highScoreValue)
        {
            highScoreValue = scoreValue;
        }
        score.text = "Score: " + scoreValue;
        //highScore.text = "HIGH SCORE: " + highScoreValue;

    }
}
