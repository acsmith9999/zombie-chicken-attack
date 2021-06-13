using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public float respawnDelay;
    public FoxMove foxMove;
    public GameObject WinDoor;
    private GameObject[] enemies;
    public GameObject[] listOfPrefabEnemies;
    public float numberOfEnemies;
    private Vector2 enemyStartPos;
    public float waveNumber;
    public KeyCode _Key;
    public int levelNumber;

    public int winCondition;
    public int levelKillCount;
    public int totalKillCount;

    private int hasGame;
    private bool hasDoor;

    public GameObject gameOver, gameWin, buttonRestart, buttonNext, buttonStart;

    // Start is called before the first frame update
    void Start()
    {
        buttonStart.SetActive(true);
        Time.timeScale = 0f;

        hasGame = 1;
        PlayerPrefs.SetInt("hasgame", hasGame);
        totalKillCount = PlayerPrefs.GetInt("totalkills");

        levelKillCount = 0;
        PlayerPrefs.SetInt("levelkills", levelKillCount);

        waveNumber = 1;

        foxMove = FindObjectOfType<FoxMove>();
        foxMove.startPos = new Vector2(0,0);

        if (enemies == null)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }

        Spawn();
        gameOver.SetActive(false);
        gameWin.SetActive(false);

        if (PlayerPrefs.GetInt("levelaccess") > levelNumber)
        {
            hasDoor = true;
            Instantiate(WinDoor, new Vector2 (-10, -5), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerPrefs.GetInt("currentlives") < 1)
        {
            GameObject.FindGameObjectWithTag("Player").SetActive(false);
            gameOver.SetActive(true);
            buttonRestart.SetActive(true);
            Time.timeScale = 0.1f;
        }
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            gameWin.SetActive(true);
            buttonRestart.SetActive(true);
            buttonNext.SetActive(true);
            if (Input.GetKeyDown(_Key))
            {
                NextLevelButton();
            }
        }
    }

    public void StartButton()
    {
        buttonStart.SetActive(false);
        Time.timeScale = 1f;
    }

    public void NextLevelButton()
    {
        waveNumber += 1;

        foxMove.transform.position = foxMove.startPos;

        Spawn();
        gameWin.SetActive(false);
        buttonRestart.SetActive(false);
        buttonNext.SetActive(false);

        if (PlayerPrefs.GetInt("levelkills") > winCondition && !hasDoor)
        {
            Instantiate(WinDoor, enemyStartPos, Quaternion.identity);
            hasDoor = true;
        }
    }
    void Spawn()
    {
        numberOfEnemies = Random.Range(waveNumber * 2, waveNumber * 4);
        for (int i = 0; i < numberOfEnemies; i++)
        {
            enemyStartPos.x = Random.Range(-10, 10);
            enemyStartPos.y = Random.Range(-5, 5);
            if ((enemyStartPos - foxMove.startPos).sqrMagnitude > 10)
            {
                Instantiate(listOfPrefabEnemies[Random.Range(0, listOfPrefabEnemies.Length)], enemyStartPos, Quaternion.identity);
            }

        }
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }
    public IEnumerator RespawnCoroutine()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foxMove.gameObject.SetActive(false);
        foreach (GameObject enemy in enemies)
        {
            enemyStartPos.x = Random.Range(-10, 10);
            enemyStartPos.y = Random.Range(-5, 5);
            if ((enemyStartPos - foxMove.startPos).sqrMagnitude > 10)
            {
                enemy.transform.position = enemyStartPos;
            }
        }
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(respawnDelay/10);
        Time.timeScale = 1f;
        foxMove.transform.position = foxMove.startPos;


        foxMove.gameObject.SetActive(true);

    }
}
