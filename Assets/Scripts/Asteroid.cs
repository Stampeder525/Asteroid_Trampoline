using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
    private Rigidbody2D rb2d;
    private int hitPoints;
    private Animator anim;

    public int maxHitPoints = 5;
	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        hitPoints = maxHitPoints;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if(rb2d.velocity.y < 2)
        {
            Debug.Log("Stopped!");
        }
        if (GameManager.instance.gameOver)
        {
            Vector2 dir = (Vector2)transform.position - Vector2.zero;
            rb2d.AddForce(dir * 100);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid") && gameObject.CompareTag("Asteroid"))
        {
            hitPoints = 0;
            GetComponent<AudioSource>().Play();
            GameManager.instance.IncrementCombo();
            GameManager.instance.IncrementScore();
        }
        else if (collision.gameObject.CompareTag("Earth"))
        {
            hitPoints = 0;
            if (gameObject.CompareTag("Asteroid"))
            {
                GameManager.instance.ResetCombo();
            }
            //rb2d.velocity = Vector2.zero;
        }
        else if (collision.gameObject.CompareTag("Trampoline") && gameObject.CompareTag("Asteroid"))
        {
            hitPoints--;
            if(hitPoints == 0)
            {
                GetComponent<AudioSource>().Play();
                //GameManager.instance.ResetCombo();
                GameManager.instance.IncrementScore();
            }
        }
        else if (collision.gameObject.CompareTag("Shockwave") && gameObject.CompareTag("Asteroid"))
        {
            hitPoints = 0;
            GetComponent<AudioSource>().Play();
            GameManager.instance.ResetCombo();
            GameManager.instance.IncrementScore();
        }

        if (hitPoints == 3 && gameObject.CompareTag("Asteroid"))
        {
            anim.SetTrigger("AsteroidMedHealth");
        }
        else if(hitPoints == 1 && gameObject.CompareTag("Asteroid"))
        {
            anim.SetTrigger("AsteroidLowHealth");
        }
        else if(hitPoints <= 0)
        {
            if (gameObject.CompareTag("Asteroid"))
            {
                gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                anim.SetTrigger("AsteroidDestroyed");
            }
            else
            {
                DestroyAsteroid();
            }
        }
    }

    public void DestroyAsteroid()
    {
        if (hitPoints <= 0)
        {
            transform.position = GameManager.instance.GetComponent<AsteroidPool>().objectPoolPosition;
            rb2d.velocity = Vector2.zero;
            rb2d.bodyType = RigidbodyType2D.Kinematic;
            hitPoints = maxHitPoints;
            if (gameObject.CompareTag("Asteroid"))
            {
                gameObject.GetComponent<PolygonCollider2D>().enabled = true;
                anim.SetTrigger("AsteroidFullHealth");
            }
            
        }
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Trampoline"))
    //    {
    //        rb2d.AddForce(transform.up * 100);
    //    }
    //}
}
