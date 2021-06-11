using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCounter : MonoBehaviour
{
    public GameObject level;
    public Controller controller;
    Text levelText;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindObjectOfType<Controller>();
        levelText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "Wave: " + controller.waveNumber;
    }
}
