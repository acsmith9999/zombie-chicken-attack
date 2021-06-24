using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed;
    Transform target;
    public GameObject stain, gold, bullet, egg;
    public int scoreValue = 100;
    public int goldValue, hitPoints;
    

    public bool shooting;
    public bool laysEgg, explodeProof;

    public float fireRate, layRate;
    public float nextFire = 0f;
    public float nextLay = 0f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, (moveSpeed * Time.deltaTime));

        if (shooting)
        {
            fireRate = Random.Range(1f, 4f);
            if (Time.time > nextFire)
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
                nextFire = Time.time + fireRate;
            }
        }

        if (laysEgg)
        {
            if (GameObject.FindGameObjectsWithTag("Egg").Length < 3)
            {
                layRate = Random.Range(5f, 8f);
                if (Time.time > nextLay)
                {
                    Instantiate(egg, transform.position, Quaternion.identity);
                    nextLay = Time.time + layRate;
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            hitPoints -= 10;
            Destroy(other.gameObject);
            if (hitPoints < 1)
            {
                ChickenDeath();
                SoundManagerScript.PlaySound("ChickenDeath");

            }
        }

        if (other.gameObject.tag == "Egg" && explodeProof == false)
        {
            SoundManagerScript.PlaySound("Explosion");
            ChickenDeath();
            Destroy(other.gameObject);
        }
    }

    public void ChickenDeath()
    {
        GameObject.FindObjectOfType<ScoreScript>().scoreValue += scoreValue;
        GameObject.FindObjectOfType<Controller>().totalKillCount++;
        PlayerPrefs.SetInt("totalkills", GameObject.FindObjectOfType<Controller>().totalKillCount);
        GameObject.FindObjectOfType<Controller>().levelKillCount++;
        PlayerPrefs.SetInt("levelkills", GameObject.FindObjectOfType<Controller>().levelKillCount);
        Destroy(this.gameObject);


        Instantiate(stain, this.transform.position, Quaternion.identity);

        if (Random.Range(0, 100) > 30)
        {
            for (int i = 0; i < goldValue; i++)
            {
                Instantiate(gold, new Vector2(this.transform.position.x + Random.Range(-2, 2), this.transform.position.y + Random.Range(-2, 2)), Quaternion.identity);
            }
        }
    }
}
