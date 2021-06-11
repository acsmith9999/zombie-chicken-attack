using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMove : MonoBehaviour
{
    public double moveSpeed;
    private Transform target;
    public GameObject stain, gold;
    public int scoreValue = 100;
    public int goldValue;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, (float)(moveSpeed * Time.deltaTime));

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            GameObject.FindObjectOfType<ScoreScript>().scoreValue += scoreValue;
            GameObject.FindObjectOfType<Controller>().totalKillCount++;
            PlayerPrefs.SetInt("totalkills", GameObject.FindObjectOfType<Controller>().totalKillCount);
            GameObject.FindObjectOfType<Controller>().levelKillCount++;
            PlayerPrefs.SetInt("levelkills", GameObject.FindObjectOfType<Controller>().levelKillCount);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            SoundManagerScript.PlaySound("ChickenDeath");
            Instantiate(stain, this.transform.position, Quaternion.identity);

            if (Random.Range(0, 100) > 30)
            {
                for(int i = 0; i < goldValue; i++)
                {
                    Instantiate(gold, new Vector2(this.transform.position.x+ Random.Range(-1, 1), this.transform.position.y + Random.Range(-1, 1)), Quaternion.identity);
                }
            }
        }
    }
}
