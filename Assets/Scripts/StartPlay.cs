using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPlay : MonoBehaviour
{
    public GameObject resumeButton;
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
        SceneManager.LoadScene("Farm");
        PlayerPrefs.DeleteKey("lives");
        PlayerPrefs.SetInt("levelaccess", 1);
        PlayerPrefs.DeleteKey("hasgame");
        PlayerPrefs.DeleteKey("totalkills");
        PlayerPrefs.DeleteKey("gold");
        //delete all?
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
