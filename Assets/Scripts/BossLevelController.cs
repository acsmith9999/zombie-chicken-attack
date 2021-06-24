using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossLevelController : MonoBehaviour
{
    public float respawnDelay;
    public FoxMove foxMove;
    public GameObject WinDoor;
    private GameObject[] enemies;

    public GameObject farmer;
    public GameObject[] powerUps, obstacles;

    private Vector2 spawnPosition, foxSpawnPoint, enemySpawnPos;

    public KeyCode _Key;
    public int levelNumber, minObstacles, maxObstacles;

    public int levelKillCount, totalKillCount;

    public bool startButtonActive, farmerDead;

    private float secondsBetweenSpawns;
    private float elapsedTime = 0.0f;

    public GameObject gameOver, gameWin, buttonRestart, buttonFinish, buttonStart, winText;
    // Start is called before the first frame update
    void Start()
    {
        buttonStart.SetActive(true);
        startButtonActive = true;
        Time.timeScale = 0f;

        foxMove = FindObjectOfType<FoxMove>();
        foxMove.bossLevel = true;
        foxSpawnPoint = new Vector2(0, 0);


        totalKillCount = PlayerPrefs.GetInt("totalkills");

        levelKillCount = 0;
        PlayerPrefs.SetInt("levelkills", levelKillCount);

        SpawnObstacles();
        Spawn();
        gameOver.SetActive(false);
        gameWin.SetActive(false);
        winText.SetActive(false);
        buttonRestart.SetActive(false);

        if (PlayerPrefs.GetInt("levelaccess") > levelNumber)
        {
            Instantiate(WinDoor, new Vector2(-10, -5), Quaternion.identity);
        }

        secondsBetweenSpawns = Random.Range(10, 15);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("currentlives") < 1)
        {
            foxMove.gameObject.SetActive(false);
            gameOver.SetActive(true);
            buttonRestart.SetActive(true);
            Time.timeScale = 0.1f;
        }

        if (startButtonActive == true)
        {
            if (Input.GetKeyDown(_Key))
            {
                StartButton();
            }
        }

        if (GameObject.FindGameObjectsWithTag("Farmer").Length == 0)
        {
            EnemyMove[] enemies = GameObject.FindObjectsOfType<EnemyMove>();
            foreach (EnemyMove enemy in enemies)
            {
                enemy.ChickenDeath();
            }

            buttonFinish.SetActive(true);

        }
        elapsedTime += Time.deltaTime;
        if (elapsedTime > secondsBetweenSpawns)
        {
            spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height)));
            elapsedTime = 0;
            Instantiate(powerUps[Random.Range(0, powerUps.Length)], spawnPosition, Quaternion.identity);
        }
    }
    public void StartButton()
    {
        buttonStart.SetActive(false);
        startButtonActive = false;
        Time.timeScale = 1f;
    }

    public void FinishButton()
    {
        if (PlayerPrefs.GetInt("levelaccess") == levelNumber)
        {
            PlayerPrefs.SetInt("levelaccess", levelNumber + 1);
        }
        buttonFinish.SetActive(false);
        winText.SetActive(true);

    }

    public void closeTextButton()
    {
        winText.SetActive(false);

        buttonRestart.SetActive(true);

    }

    public void Spawn()
    {
        enemySpawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), Camera.main.farClipPlane / 2));
        Instantiate(farmer, enemySpawnPos, Quaternion.identity);
    }

    void SpawnObstacles()
    {
        for (int i = 1; i < Random.Range(minObstacles, maxObstacles); i++)
        {
            float spawnY = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float spawnX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
            spawnPosition = new Vector3(spawnX, spawnY);
            if ((spawnPosition - foxSpawnPoint).sqrMagnitude > 10)
            {
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], spawnPosition, Quaternion.identity);
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
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foxMove.gameObject.SetActive(false);
        foreach (GameObject enemy in enemies)
        {
            enemySpawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height)));
            if ((enemySpawnPos - foxSpawnPoint).sqrMagnitude < 4)
            {
                continue;
            }
            else
            {
                enemy.transform.position = enemySpawnPos;
            }

        }
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(respawnDelay / 10);
        Time.timeScale = 1f;
        foxMove.transform.position = foxSpawnPoint;
        farmer.transform.position = new Vector3(-30, 30);

        foxMove.gameObject.SetActive(true);

    }
}
