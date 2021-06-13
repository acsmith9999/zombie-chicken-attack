using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCount : MonoBehaviour
{
    public Text lifeCounter;

    private void Start()
    {

        
    }
    private void Update()
    {
        lifeCounter.text = "Lives: " + PlayerPrefs.GetInt("currentlives").ToString();
    }
}
