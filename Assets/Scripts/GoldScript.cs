using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldScript : MonoBehaviour
{
    float magnetSpeed = 4f;
    Rigidbody2D rb;
    GameObject fox;
    Vector2 foxDirection;
    public bool magnetic;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fox = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (magnetic)
        {
            foxDirection = -(transform.position - fox.transform.position).normalized;
            rb.velocity = new Vector2(foxDirection.x, foxDirection.y) * magnetSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") + 1);
            SoundManagerScript.PlaySound("GoldSound");
        }
    }

}
