using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SignPost : MonoBehaviour
{
    public GameObject dialogueBox, switchButton;
    public Text dialogueText;
    public string dialogue, altDialogue;
    
    public bool playerInRange;
    public int level;
    public string sceneToLoad;

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            dialogueBox.SetActive(true);

            if (PlayerPrefs.GetInt("levelaccess") >= level)
            {
                dialogueText.text = dialogue;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene(sceneToLoad);
                }
            }

            else{ dialogueText.text = altDialogue; }


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
            dialogueBox.SetActive(false);
        }
    }

}
