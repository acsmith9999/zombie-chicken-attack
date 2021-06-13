using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPlay : MonoBehaviour
{
    public GameObject resumeButton, areYouSure, newButton;
    private void Start()
    {
        if (PlayerPrefs.HasKey("hasgame"))
        {
            resumeButton.SetActive(true);
        }
        else { resumeButton.SetActive(false); }
    }

    public void StartButton()
    {
        if (PlayerPrefs.GetInt("hasgame") != 1)
        {
            NewGame();
        }
        else
        {
            areYouSure.SetActive(true);
            newButton.SetActive(false);
        }
        //delete all?
    }

    public void AreYouSure()
    {
        NewGame();
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Farm");
        PlayerPrefs.DeleteKey("currentlives");
        PlayerPrefs.SetInt("maxlives", 3);
        PlayerPrefs.SetInt("levelaccess", 1);
        PlayerPrefs.DeleteKey("hasgame");
        PlayerPrefs.DeleteKey("totalkills");
        PlayerPrefs.DeleteKey("gold");
        PlayerPrefs.DeleteKey("bombcount");
    }
    public void QuitButton()
    {
        Application.Quit();
    }

    public void ResumeButton()
    {
        SceneManager.LoadScene("Farm");

    }

}
