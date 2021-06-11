using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoxMove : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;

    private bool holdPosition;
    private Vector2 playerPos;
    private Vector2 fireDir;

    public GameObject Fox;
    public GameObject bulletDown;
    public GameObject bulletUp;
    public GameObject bulletLeft;
    public GameObject bulletRight;
    public GameObject stain;

    public Vector2 startPos;

    private Vector2 bulletPos;
    private float fireRate = 0.1f;
    private float nextFire = 0.2f;

    public float waitToReload;

    private int lifeCount;

    public int levelAccess;

    public Controller gameLevelManager;
    // Start is called before the first frame update
    void Start()
    {
        gameLevelManager = FindObjectOfType<Controller>();

        rb = GetComponent<Rigidbody2D>();
        if (PlayerPrefs.HasKey("lives"))
        {
            lifeCount = PlayerPrefs.GetInt("lives");
        }
        else { PlayerPrefs.SetInt("lives", 3); }

    }

    // Update is called once per frame
    void Update()
    {
        playerPos = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        moveCharacter(playerPos);
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            fire();
        }
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            lifeCount -= 1;
            PlayerPrefs.SetInt("lives", lifeCount);
            gameLevelManager.Respawn();
            SoundManagerScript.PlaySound("FoxDeath");
            Instantiate(stain, this.transform.position, Quaternion.identity);
        }
        if (other.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene("Farm");
            if(levelAccess < gameLevelManager.levelNumber)
            {
                levelAccess = gameLevelManager.levelNumber +1;
                PlayerPrefs.SetInt("levelaccess", levelAccess);
            }

        }
    }



    void fire()
    {
        bulletPos = transform.position;

        if (Input.GetKey(KeyCode.Space) && holdPosition == false)
        {
            holdPosition = true;
            fireDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            holdPosition = false;
        }

        if (holdPosition == true)
        {
            fireDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (fireDir.x < -0.5)
            {
                bulletPos += new Vector2(-1.5f, 0);
                Instantiate(bulletLeft, bulletPos, Quaternion.identity);
            }
            else if (fireDir.x > 0.5)
            {
                bulletPos += new Vector2(1.5f, 0);
                Instantiate(bulletRight, bulletPos, Quaternion.identity);
            }
            else if (fireDir.y < -0.5)
            {
                bulletPos += new Vector2(0, -1.5f);
                Instantiate(bulletDown, bulletPos, Quaternion.identity);
            }
            else if (fireDir.y > 0.5)
            {
                bulletPos += new Vector2(0, 1.5f);
                Instantiate(bulletUp, bulletPos, Quaternion.identity);
            }

        }

        else
        {
            if (Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                bulletPos += new Vector2(-1.5f, 0);
                Instantiate(bulletLeft, bulletPos, Quaternion.identity);
            }

            else if (Input.GetAxisRaw("Vertical") < -0.5f)
            {
                bulletPos += new Vector2(0, -1.5f);
                Instantiate(bulletDown, bulletPos, Quaternion.identity);
            }

            else if (Input.GetAxisRaw("Vertical") > 0.5f)
            {
                bulletPos += new Vector2(0, 1.5f);
                Instantiate(bulletUp, bulletPos, Quaternion.identity);
            }

            else if (Input.GetAxisRaw("Horizontal") > 0.5f)
            {
                bulletPos += new Vector2(1.5f, 0);
                Instantiate(bulletRight, bulletPos, Quaternion.identity);
            }

        }

    }

}
