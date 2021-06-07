using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoxMove : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D myRigidBody;

    private bool holdPosition;
    private Vector2 playerPos;
    private Vector2 fireDir;

    private bool playerDead;

    public GameObject Fox;
    public GameObject bulletDown;
    public GameObject bulletUp;
    public GameObject bulletLeft;
    public GameObject bulletRight;
    public GameObject stain;

    public Vector2 startPos = new Vector2(0f, 0f);

    private Vector2 bulletPos;
    private float fireRate = 0.1f;
    private float nextFire = 0.2f;

    private bool reloading;
    public float waitToReload;

    public Controller gameLevelManager;
    // Start is called before the first frame update
    void Start()
    {
        gameLevelManager = FindObjectOfType<Controller>();

        myRigidBody = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        playerPos = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (playerPos.x < -0.5f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                fire();
            }
        }

        if (playerPos.x > 0.5f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                fire();
            }
        }

        if (playerPos.y > 0.5f)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                fire();
            }
        }

        if (playerPos.y < -0.5f)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                fire();
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            LifeCount.lifeCount -= 1;
            gameLevelManager.Respawn();
            SoundManagerScript.PlaySound("FoxDeath");
            Instantiate(stain, this.transform.position, Quaternion.identity);
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
