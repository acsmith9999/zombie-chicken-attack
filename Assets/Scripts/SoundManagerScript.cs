using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip music, chickenDeathSound, foxDeathSound, goldSound, explosion, invisibility, life, rapidFire, runningShoes, shotgun, moneyMagnet, ow;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        chickenDeathSound = Resources.Load<AudioClip>("ChickenDeath");
        foxDeathSound = Resources.Load<AudioClip>("FoxDeath");
        goldSound = Resources.Load<AudioClip>("GoldSound");
        explosion = Resources.Load<AudioClip>("Explosion");
        invisibility = Resources.Load<AudioClip>("Invisibility");
        life = Resources.Load<AudioClip>("Life");
        rapidFire = Resources.Load<AudioClip>("RapidFire");
        runningShoes = Resources.Load<AudioClip>("RunningShoes");
        shotgun = Resources.Load<AudioClip>("Shotgun");
        moneyMagnet = Resources.Load<AudioClip>("MoneMagnet");
        ow = Resources.Load<AudioClip>("Ow");


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
            case "GoldSound":
                audioSrc.PlayOneShot(goldSound);
                break;
            case "Explosion":
                audioSrc.PlayOneShot(explosion);
                break;
            case "Invisibility":
                audioSrc.PlayOneShot(invisibility);
                break;
            case "Life":
                audioSrc.PlayOneShot(life);
                break;
            case "RapidFire":
                audioSrc.PlayOneShot(rapidFire);
                break;
            case "RunningShoes":
                audioSrc.PlayOneShot(runningShoes);
                break;
            case "Shotgun":
                audioSrc.PlayOneShot(shotgun);
                break;
            case "MoneyMagnet":
                audioSrc.PlayOneShot(moneyMagnet);
                break;
            case "Ow":
                audioSrc.PlayOneShot(ow);
                break;
        }
        
    }
}
