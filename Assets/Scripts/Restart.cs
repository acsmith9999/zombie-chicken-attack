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
        if (PlayerPrefs.GetInt("currentlives") < 1)
        {
            PlayerPrefs.SetInt("currentlives", PlayerPrefs.GetInt("maxlives"));

            PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold")/2);
        }
    }
}
