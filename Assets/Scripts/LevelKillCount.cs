using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelKillCount : MonoBehaviour
{
    public Text levelKillCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        levelKillCounter.text = "Pops: " + PlayerPrefs.GetInt("levelkills").ToString();
    }
}
