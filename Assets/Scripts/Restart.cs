using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    public void RestartButton()
    {
        SceneManager.LoadScene("Farm");
        Time.timeScale = 1f;
        if (PlayerPrefs.GetInt("lives") == 0)
        {
            PlayerPrefs.SetInt("lives", 3);

            //penalty for game over?
            //GameObject.FindObjectOfType<ScoreScript>().scoreValue = GameObject.FindObjectOfType<ScoreScript>().scoreValue/2;
        }
    }
}
