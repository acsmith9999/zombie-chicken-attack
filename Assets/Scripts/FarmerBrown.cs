using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerBrown : MonoBehaviour
{
    public float moveSpeed;
    Transform target;
    public GameObject[] chickens;
    public GameObject stain, gold;
    public int goldValue, hitPoints, scoreValue;

    float fireRate, nextFire;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, (moveSpeed * Time.deltaTime));
        fireRate = Random.Range(2f, 3f);
        if (Time.time > nextFire)
        {
            Instantiate(chickens[Random.Range(0, chickens.Length)], transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            hitPoints -= 10;
            Destroy(other.gameObject);
            SoundManagerScript.PlaySound("Ow");
            GameObject.FindObjectOfType<ScoreScript>().scoreValue += 10;
            if (hitPoints < 1)
            {

                Destroy(this.gameObject);
                SoundManagerScript.PlaySound("ChickenDeath");
                SoundManagerScript.PlaySound("Explosion");
                Instantiate(stain, this.transform.position, Quaternion.identity);
                GameObject.FindObjectOfType<ScoreScript>().scoreValue += scoreValue;
                GameObject.FindObjectOfType<Controller>().totalKillCount++;
                PlayerPrefs.SetInt("totalkills", GameObject.FindObjectOfType<Controller>().totalKillCount);
                GameObject.FindObjectOfType<Controller>().levelKillCount++;
                PlayerPrefs.SetInt("levelkills", GameObject.FindObjectOfType<Controller>().levelKillCount);
                //for (int i = 0; i < goldValue; i++)
                //    {
                //        Instantiate(gold, new Vector2(this.transform.position.x + Random.Range(-2, 2), this.transform.position.y + Random.Range(-2, 2)), Quaternion.identity);
                //    }

            }
        }
    }
}
