using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineController : MonoBehaviour {

    public float radius;
    public float bounce = 5f;
    public Vector3 mousePos;

    private Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        if (GameManager.instance.gameOver)
        {
            Vector2 dir = (Vector2)transform.position - Vector2.zero;
            rb2d.bodyType = RigidbodyType2D.Dynamic;
            rb2d.AddForce(dir * 100);
        }
        else {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //angle += mousePos.x;
            Vector2 offset = (Vector2)mousePos - Vector2.zero;
            offset.Normalize();
            offset = offset * radius;
            transform.position = offset;
            Vector2 dir = mousePos - this.transform.position;
            //Check if dir is negative, then reverse
            if (Mathf.Abs(mousePos.x) <= 2f && Mathf.Abs(mousePos.y) <= 2f)
            {
                dir = -1 * dir;
            }
            transform.up = dir;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Vector2 asteroidVelocity = collision.collider.GetComponent<Rigidbody2D>().velocity; //Not sure this will work
        if(collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "HealthPack")
        {
            GetComponent<AudioSource>().Play();
            Vector2 dir = collision.gameObject.transform.position - this.transform.position;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * bounce);
        }
    }
}
