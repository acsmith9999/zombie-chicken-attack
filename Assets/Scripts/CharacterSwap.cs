using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSwap : MonoBehaviour
{
    public bool playerInRange;
    public int level;
    private GameObject player;
    public SpriteRenderer rend;
    public Sprite fox, cat;
    //public Animator anim;

    public GameObject switchButton;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("player", "cat");
        SwitchCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && PlayerPrefs.GetInt("levelaccess") >= level)
        {
            switchButton.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            switchButton.SetActive(false);
        }
    }

    public void SwitchCharacter()
    {
        if (PlayerPrefs.GetString("player") == "fox")
        {
            PlayerPrefs.SetString("player", "cat");
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            player = Instantiate (Resources.Load("BootsyCatNoWeapons", typeof(GameObject)), new Vector2(1.8418f,-0.0514f), Quaternion.identity) as GameObject;
            rend.sprite = fox;

        }
        else if (PlayerPrefs.GetString("player") == "cat")
        {
            PlayerPrefs.SetString("player", "fox");
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            player = Instantiate(Resources.Load("FoxNoWeapons", typeof(GameObject)), new Vector2(1.8418f, -0.0514f), Quaternion.identity) as GameObject;
            rend.sprite = cat;
        }
    }
}
