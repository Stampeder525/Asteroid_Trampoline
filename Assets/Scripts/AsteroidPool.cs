using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class AsteroidPool : MonoBehaviour {

    public GameObject asteroidPrefab;                               //The asteroid game object.
    public GameObject healthPackPrefab;
    public int asteroidPoolSize = 20;                                //How many asteroids to keep on standby.
    public float spawnRate = 3f;                                    //How quickly asteroids spawn.

    private GameObject[] asteroids;                                 //Collection of pooled asteroids.
    private int currentAsteroid = 0;                                //Index of the current asteroid in the collection.

    public Vector2 objectPoolPosition = new Vector2(-15, -25);     //A holding position for our unused asteroids offscreen.

    private float timeSinceLastSpawned;


    void Start()
    {
        timeSinceLastSpawned = 0f;

        //Initialize the asteroids collection.
        asteroids = new GameObject[asteroidPoolSize];
        //Loop through the collection... 
        for (int i = 0; i < asteroidPoolSize; i++)
        {
            //...and create the individual asteroids
            if(i%5 == 0 && i != 0)
            {
                //Make 1 health pack for every 9 asteroids
                asteroids[i] = (GameObject)Instantiate(healthPackPrefab, objectPoolPosition, Quaternion.identity);
            }
            else
            {
                //make asteroid
                asteroids[i] = (GameObject)Instantiate(asteroidPrefab, objectPoolPosition, Quaternion.identity);
            }
        }
    }


    //This spawns columns as long as the game is not over.
    void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;
        
        if (!GameManager.instance.gameOver && timeSinceLastSpawned >= spawnRate)
        {
            timeSinceLastSpawned = 0f;

            int spawnSide = Random.Range(0, 4);
            float spawnYPosition = 0;
            float spawnXPosition = 0;
            if(spawnSide == 0)
            {
                spawnYPosition = 11f;
                spawnXPosition = Random.Range(-20, 20);
            }
            else if (spawnSide == 1)
            {
                spawnXPosition = 21f;
                spawnYPosition = Random.Range(-20, 20);
            }
            else if(spawnSide == 2)
            {
                spawnYPosition = -11f;
                spawnXPosition = Random.Range(-20, 20);
            }
            else if (spawnSide == 3)
            {
                spawnXPosition = -21f;
                spawnYPosition = Random.Range(-20, 20);
            }

            //...then set the current column to that position.
            //Check to make sure asteroid isn't currently on screen
            if(asteroids[currentAsteroid].GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Kinematic)
            {
                asteroids[currentAsteroid].transform.position = new Vector2(spawnXPosition, spawnYPosition);
                asteroids[currentAsteroid].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
            //Increase the value of currentColumn. If the new size is too big, set it back to zero
            currentAsteroid++;

            if (currentAsteroid >= asteroidPoolSize)
            {
                currentAsteroid = 0;
            }

            if (GameManager.instance.GetScore() > 25)
            {
                spawnRate = 2f;
            }
            if (GameManager.instance.GetScore() > 50)
            {
                spawnRate = 1f;
            }

        }
    }
}
