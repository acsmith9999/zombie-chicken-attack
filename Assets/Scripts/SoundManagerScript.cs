using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip music, chickenDeathSound, foxDeathSound;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        chickenDeathSound = Resources.Load<AudioClip>("ChickenDeath");
        foxDeathSound = Resources.Load<AudioClip>("FoxDeath");
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "ChickenDeath":
                audioSrc.PlayOneShot(chickenDeathSound);
                break;
            case "FoxDeath":
                audioSrc.PlayOneShot(foxDeathSound);
                break;
        }
        
    }
}
