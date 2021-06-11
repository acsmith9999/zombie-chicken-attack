using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public int scoreValue;
    Text score;

    public static int highScoreValue;

    // Start is called before the first frame update
    void Start()
    {
        scoreValue = 0;
        score = GameObject.Find("Score").GetComponent<Text>();
        if (PlayerPrefs.HasKey("highscore"))
        {
            highScoreValue = PlayerPrefs.GetInt("highscore", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreValue > highScoreValue)
        {
            highScoreValue = scoreValue;
        }
        score.text = "Score: " + scoreValue;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("highscore", highScoreValue);
    }
}
