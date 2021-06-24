using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public float respawnDelay;

    private FoxMove movementScript;
    public GameObject WinDoor, farmer, player;
    private GameObject[] enemies;

    public GameObject[] listOfPrefabEnemies, powerUps, obstacles;
    public float numberOfEnemies;

    private Vector2 spawnPosition, foxSpawnPoint, enemySpawnPos;
    public float waveNumber;
    public KeyCode _Key;
    public int levelNumber, minObstacles, maxObstacles, maxEnemies;

    public int winCondition, levelKillCount, totalKillCount;

    private int hasGame;
    private bool hasDoor, startButtonActive;
    public bool bossLevel;

    private float secondsBetweenSpawns;
    private float elapsedTime = 0.0f;

    public GameObject gameOver, gameWin, buttonRestart, buttonNext, buttonStart, winText;

    // Start is called before the first frame update
    void Start()
    {
        foxSpawnPoint = new Vector2(0, 0);

        buttonStart.SetActive(true);
        startButtonActive = true;

        Time.timeScale = 0f;

        hasGame = 1;
        PlayerPrefs.SetInt("hasgame", hasGame);
        totalKillCount = PlayerPrefs.GetInt("totalkills");

        levelKillCount = 0;
        PlayerPrefs.SetInt("levelkills", levelKillCount);

        waveNumber = 1;

        if (PlayerPrefs.GetString("player") == "fox")
        {
            player = Instantiate(Resources.Load("Fox", typeof(GameObject)), foxSpawnPoint, Quaternion.identity) as GameObject;
        }
        else if (PlayerPrefs.GetString("player") == "cat")
        {
            player = Instantiate(Resources.Load("BootsyCat", typeof(GameObject)), foxSpawnPoint, Quaternion.identity) as GameObject;
        }
        
        movementScript = FoxMove.FindObjectOfType<FoxMove>();

        if (enemies == null)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }


        SpawnObstacles();
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
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !bossLevel)
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
        if (elapsedTime > secondsBetweenSpawns)
        {
            spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), Camera.main.farClipPlane / 2));
            elapsedTime = 0;
            Instantiate(powerUps[Random.Range(0, powerUps.Length)], spawnPosition, Quaternion.identity);
        }

        if (GameObject.FindGameObjectsWithTag("Farmer").Length == 0 && bossLevel)
        {
            EnemyMove[] enemies = GameObject.FindObjectsOfType<EnemyMove>();
            foreach (EnemyMove enemy in enemies)
            {
                enemy.ChickenDeath();
            }

            buttonNext.SetActive(true);
            buttonRestart.SetActive(true);

        }
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

        movePlayer();
        Spawn();
        gameWin.SetActive(false);
        buttonRestart.SetActive(false);
        buttonNext.SetActive(false);

        if (PlayerPrefs.GetInt("levelkills") > winCondition && !hasDoor && bossLevel == false)
        {
            spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), Camera.main.farClipPlane / 2));
            Instantiate(WinDoor, spawnPosition, Quaternion.identity);
            hasDoor = true;
        }


        //spawn random items

        if (elapsedTime > secondsBetweenSpawns)
        {
            spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), Camera.main.farClipPlane / 2));
            elapsedTime = 0;
            Instantiate(powerUps[Random.Range(0, powerUps.Length)], spawnPosition, Quaternion.identity);
        }

        EnemyBullet[] enemyBullets = GameObject.FindObjectsOfType<EnemyBullet>();
        foreach (EnemyBullet bullet in enemyBullets)
        {
            Destroy(bullet.gameObject);
        }
    }

    public void FinishButton()
    {
        if (bossLevel)
        {
            if (PlayerPrefs.GetInt("levelaccess") == levelNumber)
            {
                PlayerPrefs.SetInt("levelaccess", levelNumber + 1);
            }
            buttonRestart.SetActive(false);
            winText.SetActive(true);
        }
    }

    public void closeTextButton()
    {
        winText.SetActive(false);

        buttonRestart.SetActive(true);

    }
    void Spawn()
    {
        if (!bossLevel)
        {
            numberOfEnemies = Random.Range(waveNumber * 2, waveNumber * 4);
            if (numberOfEnemies <= maxEnemies)
            {
                for (int i = 0; i < numberOfEnemies; i++)
                {
                    enemySpawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height)));

                    if (Vector3.Distance(enemySpawnPos, foxSpawnPoint) < 4)
                    {
                        continue;
                    }
                    else
                    {
                        Instantiate(listOfPrefabEnemies[Random.Range(0, listOfPrefabEnemies.Length)], enemySpawnPos, Quaternion.identity);
                    }

                }
            }
            else if (numberOfEnemies > maxEnemies)
            {
                for (int i = 0; i < maxEnemies; i++)
                {
                    enemySpawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height)));

                    if (Vector3.Distance(enemySpawnPos, foxSpawnPoint) < 4)
                    {
                        continue;
                    }
                    else
                    {
                        Instantiate(listOfPrefabEnemies[Random.Range(0, listOfPrefabEnemies.Length)], enemySpawnPos, Quaternion.identity);
                    }
                }
            }
        }
        else if (bossLevel)
        {
            enemySpawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height)));
            bool farmerSpawned = false;
            while (!farmerSpawned)
            {
                if (Vector2.Distance(enemySpawnPos, foxSpawnPoint) < 10)
                {
                    continue;
                }
                else
                {
                    Instantiate(farmer, enemySpawnPos, Quaternion.identity);
                    farmerSpawned = true;
                }
                break;
            }
        }
    }
    void SpawnObstacles()
    {
        for (int i = 1; i < Random.Range(minObstacles, maxObstacles); i++)
        {
            float spawnY = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float spawnX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
            spawnPosition = new Vector2(spawnX, spawnY);
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
        movementScript.gameObject.SetActive(false);
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
            if (bossLevel)
            {

                farmer.transform.position = enemySpawnPos;
            }
        }
        
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(respawnDelay/10);
        Time.timeScale = 1f;
        movePlayer();


        movementScript.gameObject.SetActive(true);

    }

    public void movePlayer()
    {
        movementScript.transform.position = foxSpawnPoint;
    }
}
