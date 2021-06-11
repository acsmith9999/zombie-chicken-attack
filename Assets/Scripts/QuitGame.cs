using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public GameObject quit, sure;
    public bool playerInRange;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            quit.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            quit.SetActive(false);
            sure.SetActive(false);
        }
    }

    public void QuitButton()
    {
        quit.SetActive(false);
        sure.SetActive(true);
    }

    public void Sure()
    {
        Application.Quit();
    }


}
