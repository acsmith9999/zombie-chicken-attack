using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMove : MonoBehaviour
{
    public double moveSpeed;
    private Transform target;
    public GameObject stain;
    public int scoreValue = 100;

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
            ScoreScript.scoreValue += scoreValue;
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            SoundManagerScript.PlaySound("ChickenDeath");
            Instantiate(stain, this.transform.position, Quaternion.identity);
        }
    }
}
