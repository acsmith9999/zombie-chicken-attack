using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public GameObject dialogueBox, shopMenu, warningMessage;
    public Text dialogueText, warningText, lifePrice;
    public string dialogue;
    public int price;

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
                price = PlayerPrefs.GetInt("maxlives") * 1000 - 2000;
                lifePrice.text = "INCREASE MAX LIFE = " + price + " GOLD";
            }
        }
    }

    public void BuyLife()
    {
        if (PlayerPrefs.GetInt("currentlives") == PlayerPrefs.GetInt("maxlives"))
        {
            warningMessage.SetActive(true);
            warningText.text = "Already at maximum!";
        }

        else if (PlayerPrefs.GetInt("gold") < 50)
        {
            warningMessage.SetActive(true);
            warningText.text = "Kill more chickens first!";
        }

        else if (PlayerPrefs.GetInt("currentlives") < PlayerPrefs.GetInt("maxlives") && PlayerPrefs.GetInt("gold") >= 50)
        {
            PlayerPrefs.SetInt("currentlives", PlayerPrefs.GetInt("currentlives") + 1);
            PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") - 50);
        }
    }

    public void IncreaseMaxLife()
    {
        if (PlayerPrefs.GetInt("maxlives") == 10)
        {
            warningMessage.SetActive(true);
            warningText.text = "Already at maximum!";
        }
        else if (PlayerPrefs.GetInt("gold") < price)
        {
            warningMessage.SetActive(true);
            warningText.text = "Kill more chickens first!";
        }
        else if (PlayerPrefs.GetInt("maxlives") < 10 && PlayerPrefs.GetInt("gold") >= price)
        {
            PlayerPrefs.SetInt("maxlives", PlayerPrefs.GetInt("maxlives") + 1);
            PlayerPrefs.SetInt("currentlives", PlayerPrefs.GetInt("currentlives") + 1);
            PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") - price);
            lifePrice.text = "INCREASE MAX LIFE = " + price + " GOLD";
        }
    }

    public void BuyBomb()
    {
        if (PlayerPrefs.GetInt("bombcount") == 3)
        {
            warningMessage.SetActive(true);
            warningText.text = "Already at maximum!";
        }
        else if (PlayerPrefs.GetInt("gold") < 200)
        {
            warningMessage.SetActive(true);
            warningText.text = "Kill more chickens first!";
        }
        else if (PlayerPrefs.GetInt("bombcount") < 3 && PlayerPrefs.GetInt("gold") >= 200)
        {
            PlayerPrefs.SetInt("bombcount", PlayerPrefs.GetInt("bombcount") + 1);
            PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") - 200);
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
