using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCount : MonoBehaviour
{
    public GameObject lives;
    public static int lifeCount;
    Text lifeCounter;

    // Start is called before the first frame update
    void Start()
    {
        lifeCount = 3;
        lifeCounter = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeCounter.text = "Lives: " + lifeCount;
    }
}
