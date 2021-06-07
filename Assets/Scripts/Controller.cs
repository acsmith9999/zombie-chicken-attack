using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public float respawnDelay;
    public FoxMove foxMove;
    public GameObject enemyPrefab;
    private GameObject[] enemies;
    public float numberOfEnemies;
    private Vector2 enemyStartPos;
    public float levelNumber;

    public GameObject gameOver, gameWin, buttonRestart, buttonNext;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        levelNumber = 1;

        foxMove = FindObjectOfType<FoxMove>();

        if (enemies == null)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
        numberOfEnemies = Random.Range(levelNumber * 2, levelNumber * 4);

        for (int i = 0; i < numberOfEnemies; i++)
        {
            enemyStartPos.x = Random.Range(-10, 10);
            enemyStartPos.y = Random.Range(-5, 5);
            if ((enemyStartPos - foxMove.startPos).sqrMagnitude > 2)
            {
                Instantiate(enemyPrefab, enemyStartPos, Quaternion.identity);
            }

        }

        gameOver.SetActive(false);
        gameWin.SetActive(false);
        

    }

    // Update is called once per frame
    void Update()
    {

        if (LifeCount.lifeCount < 1)
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
        }
        
    }

    public void NextLevelButton()
    {
        levelNumber += 1;

        foxMove.transform.position = foxMove.startPos;

        numberOfEnemies = Random.Range(levelNumber * 2, levelNumber * 4);
        for (int i = 0; i < numberOfEnemies; i++)
        {
            enemyStartPos.x = Random.Range(-10, 10);
            enemyStartPos.y = Random.Range(-5, 5);
            if ((enemyStartPos - foxMove.startPos).sqrMagnitude > 2)
            {
                Instantiate(enemyPrefab, enemyStartPos, Quaternion.identity);
            }

        }
        gameWin.SetActive(false);
        buttonRestart.SetActive(false);
        buttonNext.SetActive(false);
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
            if ((enemyStartPos - foxMove.startPos).sqrMagnitude > 2)
            {
                enemy.transform.position = enemyStartPos;
            }
        }

        yield return new WaitForSeconds(respawnDelay);

        foxMove.transform.position = foxMove.startPos;


        foxMove.gameObject.SetActive(true);

    }
}
