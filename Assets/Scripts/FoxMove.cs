using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoxMove : MonoBehaviour
{
    public float moveSpeed = 7;

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

    public KeyCode bombKey;
    private bool invisible;

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
        if (PlayerPrefs.HasKey("currentlives"))
        {
            lifeCount = PlayerPrefs.GetInt("currentlives");
        }
        else { PlayerPrefs.SetInt("currentlives", PlayerPrefs.GetInt("maxlives")); }
        moveSpeed = 7;
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
        if (Input.GetKeyDown(bombKey))
        {
            UseBomb();
        }
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    public void UseBomb()
    {
        if (PlayerPrefs.GetInt("bombcount") > 0)
            {
            SoundManagerScript.PlaySound("Explosion");
            PlayerPrefs.SetInt("bombcount", PlayerPrefs.GetInt("bombcount") - 1);
            EnemyMove[] enemies = GameObject.FindObjectsOfType<EnemyMove>();
            foreach (EnemyMove enemy in enemies)
            {
                enemy.ChickenDeath();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && invisible == false)
        {
            lifeCount -= 1;
            PlayerPrefs.SetInt("currentlives", lifeCount);
            gameLevelManager.Respawn();
            SoundManagerScript.PlaySound("FoxDeath");
            Instantiate(stain, this.transform.position, Quaternion.identity);
            fireRate = 0.1f;
        }
        if (other.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene("Farm");
            if(PlayerPrefs.GetInt("levelaccess") == gameLevelManager.levelNumber)
            {
                PlayerPrefs.SetInt("levelaccess", gameLevelManager.levelNumber +1);
            }
        }
        if (other.gameObject.tag == "Bomb" && PlayerPrefs.GetInt("bombcount") < 3)
        {
            PlayerPrefs.SetInt("bombcount", PlayerPrefs.GetInt("bombcount") + 1);
            SoundManagerScript.PlaySound("Shotgun");
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Life" && PlayerPrefs.GetInt("currentlives") < PlayerPrefs.GetInt("maxlives"))
        {
            PlayerPrefs.SetInt("currentlives", PlayerPrefs.GetInt("currentlives") + 1);
            SoundManagerScript.PlaySound("Life");
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Shoes")
        {
            StartCoroutine(RunningShoes());
            SoundManagerScript.PlaySound("RunningShoes");
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Invisibility")
        {
            StartCoroutine(InvisibilityPotion());
            SoundManagerScript.PlaySound("Invisibility");
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Gun")
        {
            StartCoroutine(RapidFire());
            SoundManagerScript.PlaySound("RapidFire");
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "EnemyBullet" && invisible == false)
        {
            lifeCount -= 1;
            PlayerPrefs.SetInt("currentlives", lifeCount);
            gameLevelManager.Respawn();
            SoundManagerScript.PlaySound("FoxDeath");
            Instantiate(stain, this.transform.position, Quaternion.identity);
            fireRate = 0.1f;
        }
        if (other.gameObject.tag == "Money")
        {
            GoldScript[] gold = GameObject.FindObjectsOfType<GoldScript>();
            foreach (GoldScript coin in gold)
            {
                coin.magnetic = true;
            }
            SoundManagerScript.PlaySound("MoneyMagnet");
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Egg" && invisible == false)
        {
            lifeCount -= 1;
            PlayerPrefs.SetInt("currentlives", lifeCount);
            gameLevelManager.Respawn();
            SoundManagerScript.PlaySound("FoxDeath");
            SoundManagerScript.PlaySound("Explosion");
            Instantiate(stain, this.transform.position, Quaternion.identity);
            fireRate = 0.1f;
            EnemyMove[] enemies = GameObject.FindObjectsOfType<EnemyMove>();
            foreach (EnemyMove enemy in enemies)
            {
                enemy.ChickenDeath();
            }
            Destroy(this.gameObject);
        }
    }

    public IEnumerator RunningShoes()
    {
        moveSpeed += 3;
        yield return new WaitForSeconds(3);
        moveSpeed -= 3;
    }

    public IEnumerator InvisibilityPotion()
    {
        invisible = true;
        yield return new WaitForSeconds(5);
        invisible = false;
    }

    public IEnumerator RapidFire()
    {
        fireRate = fireRate / 2;
        yield return new WaitForSeconds(5);
        fireRate = fireRate * 2;
    }

    void fire()
    {
        bulletPos = transform.position;

        //if (Input.GetKey(KeyCode.Space) && holdPosition == false)
        //{
        //    holdPosition = true;
        //    fireDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //}

        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    holdPosition = false;
        //}

        //if (holdPosition == true)
        //{
        //    fireDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //    if (fireDir.x < -0.5)
        //    {
        //        bulletPos += new Vector2(-1.5f, 0);
        //        Instantiate(bulletLeft, bulletPos, Quaternion.identity);
        //    }
        //    else if (fireDir.x > 0.5)
        //    {
        //        bulletPos += new Vector2(1.5f, 0);
        //        Instantiate(bulletRight, bulletPos, Quaternion.identity);
        //    }
        //    else if (fireDir.y < -0.5)
        //    {
        //        bulletPos += new Vector2(0, -1.5f);
        //        Instantiate(bulletDown, bulletPos, Quaternion.identity);
        //    }
        //    else if (fireDir.y > 0.5)
        //    {
        //        bulletPos += new Vector2(0, 1.5f);
        //        Instantiate(bulletUp, bulletPos, Quaternion.identity);
        //    }

        //}

        //else
        //{
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

        //}

    }

}
