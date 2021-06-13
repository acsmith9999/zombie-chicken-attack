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
        if (PlayerPrefs.GetInt("currentlives") == 0)
        {
            PlayerPrefs.SetInt("currentlives", 3);

            PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold")/2);
        }
    }
}
