using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;


public class EarthController : MonoBehaviour { //SOMETHING IN HERE BREAKING BUILD
    public int maxHitPoints = 3;
    private int hitPoints;
    private Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        hitPoints = maxHitPoints;
	}
	
	//// Update is called once per frame
	//void Update () {
        
	//}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            CameraShaker.Instance.ShakeOnce(3f, 3f, 0.1f,0.1f);
            GetComponent<AudioSource>().Play();
            hitPoints--;
            if (hitPoints == 2)
            {
                anim.SetTrigger("FulltoMed");
            }
            else if (hitPoints == 1)
            {
                anim.SetTrigger("MedtoLow");
            }
            else if (hitPoints == 0)
            {
                GetComponent<AudioSource>().Play();
                anim.SetTrigger("Exploding");
                CameraShaker.Instance.ShakeOnce(10f, 10f, 0.1f, 2f);
                GameManager.instance.gameOver = true;
            }
        }
        else if (collision.gameObject.CompareTag("HealthPack"))
        {
            collision.gameObject.GetComponent<AudioSource>().Play();
            if (hitPoints < maxHitPoints)
            {
                hitPoints++;
                if (hitPoints == 3)
                {
                    anim.SetTrigger("MedtoFull");
                }
                else if (hitPoints == 2)
                {
                    anim.SetTrigger("LowtoMed");
                }
            }
        }
    }
}
