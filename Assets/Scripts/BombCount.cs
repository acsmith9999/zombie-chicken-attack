using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombCount : MonoBehaviour
{
    public Text bombCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bombCounter.text = "Bombs: " + PlayerPrefs.GetInt("bombcount").ToString();
    }
}
