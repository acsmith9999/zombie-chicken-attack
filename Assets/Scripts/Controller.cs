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

    public GameObject[] listOfPrefabEnemies, powerUps, obstacles;
    public float numberOfEnemies;
    private Vector2 enemyStartPos, foxStartPos;
    public float waveNumber;
    public KeyCode _Key;
    public int levelNumber, minObstacles, maxObstacles, maxEnemies;

    public int winCondition, levelKillCount, totalKillCount;

    private int hasGame;
    private bool hasDoor, startButtonActive;

    private float secondsBetweenSpawns;
    private float elapsedTime = 0.0f;

    public GameObject gameOver, gameWin, buttonRestart, buttonNext, buttonStart;

    // Start is called before the first frame update
    void Start()
    {
        buttonStart.SetActive(true);
        startButtonActive = true;
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

        for (int i = 1; i < Random.Range(minObstacles, maxObstacles); i++)
        {
            float spawnY = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float spawnX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

            Vector2 spawnPosition = new Vector2(spawnX, spawnY);
            if ((spawnPosition - foxMove.startPos).sqrMagnitude > 10)
            {
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], spawnPosition, Quaternion.identity);
            }
        }

        Spawn();
        gameOver.SetActive(false);
        gameWin.SetActive(false);

        if (PlayerPrefs.GetInt("levelaccess") > levelNumber)
        {
            hasDoor = true;
            Instantiate(WinDoor, new Vector2 (-10, -5), Quaternion.identity);
        }

        secondsBetweenSpawns = Random.Range(10, 15);
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
        if (startButtonActive == true)
        {
            if (Input.GetKeyDown(_Key))
            {
                StartButton();
            }
        }

        elapsedTime += Time.deltaTime;
    }

    public void StartButton()
    {
        buttonStart.SetActive(false);
        startButtonActive = false;
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


        //spawn random items

        if (elapsedTime > secondsBetweenSpawns)
        {
            elapsedTime = 0;
            Instantiate(powerUps[Random.Range(0, powerUps.Length)], enemyStartPos, Quaternion.identity);
        }
    }
    void Spawn()
    {
        numberOfEnemies = Random.Range(waveNumber * 2, waveNumber * 4);
        if (numberOfEnemies <= maxEnemies) {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                enemyStartPos.x = Random.Range(-10, 10);
                enemyStartPos.y = Random.Range(-5, 5);
                if ((enemyStartPos - foxMove.startPos).sqrMagnitude > 15)
                {
                    Instantiate(listOfPrefabEnemies[Random.Range(0, listOfPrefabEnemies.Length)], enemyStartPos, Quaternion.identity);
                }
            }
        }
        else if (numberOfEnemies > maxEnemies)
        {
            for (int i = 0; i < maxEnemies; i++)
            {
                enemyStartPos.x = Random.Range(-10, 10);
                enemyStartPos.y = Random.Range(-5, 5);
                if ((enemyStartPos - foxMove.startPos).sqrMagnitude > 15)
                {
                    Instantiate(listOfPrefabEnemies[Random.Range(0, listOfPrefabEnemies.Length)], enemyStartPos, Quaternion.identity);
                }
            }
        }
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }
    public IEnumerator RespawnCoroutine()
    {
        foxStartPos = new Vector2(0, 0);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foxMove.gameObject.SetActive(false);
        foreach (GameObject enemy in enemies)
        {
            enemyStartPos.x = Random.Range(-10, 10);
            enemyStartPos.y = Random.Range(-5, 5);
            if ((enemyStartPos - foxStartPos).sqrMagnitude > 15)
            {
                enemy.transform.position = enemyStartPos;
            }
        }
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(respawnDelay/10);
        Time.timeScale = 1f;
        foxMove.transform.position = foxMove.startPos;


        foxMove.gameObject.SetActive(true);

    }
}
