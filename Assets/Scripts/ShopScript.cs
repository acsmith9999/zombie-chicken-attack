using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public GameObject dialogueBox, shopMenu, warningMessage;
    public Text dialogueText, warningText;
    public string dialogue;


    public bool playerInRange;
    // Start is called before the first frame update
    void Start()
    {
        shopMenu.SetActive(false);
        warningMessage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            dialogueBox.SetActive(true);
            dialogueText.text = dialogue;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                shopMenu.SetActive(true);
            }
        }
    }

    public void BuyLife()
    {
        if (PlayerPrefs.GetInt("totalkills") < 50)
        {
            warningMessage.SetActive(true);
            warningText.text = "Kill more chickens first!";
        }
        else if (PlayerPrefs.GetInt("lives") == 3)
        {
            warningMessage.SetActive(true);
            warningText.text = "Already at maximum!";
        }
        else if (PlayerPrefs.GetInt("lives") < 3 && PlayerPrefs.GetInt("totalkills") >= 50)
        {
            PlayerPrefs.SetInt("lives", PlayerPrefs.GetInt("lives") + 1);
            PlayerPrefs.SetInt("totalkills", PlayerPrefs.GetInt("totalkills") - 50);
        }
    }

    public void OK()
    {
        warningMessage.SetActive(false);
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
            dialogueBox.SetActive(false);
            shopMenu.SetActive(false);
        }
    }
}
