using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    [System.Serializable]
    public struct Wave
    {
        public GameObject basicEnemy;
        public int enemyCount;
        public float timeBeforeSpawn;
        public int rowOverflowCount;
    }

    public float timeBeforeSpawnInitial;

    public Wave[] waves; //Designate the starting wave
    public int waveNumber;
    public bool waveNotActive;
    public Transform[] spawnPoints;

    public float waveDelay;
    public float timeLeft;
    public bool checkingTime;
    public bool timerDone;

    public bool gameFinished;

    public int randomNum;
    
    public GridManager grid;

    void Start()
    {
        grid = FindObjectOfType<GridManager>();
        //set off waves here
        waveNotActive = true;
        gameFinished = false;

        checkingTime = true;
        timerDone = false;

        timeBeforeSpawnInitial = waves[waveNumber].timeBeforeSpawn;

        Debug.Log(waves.Length);
        Debug.Log(waves[waveNumber].enemyCount);
        
    }

    void Update()
    {
        if (checkingTime && grid.overallEnemyCount == 0 && waveNotActive && gameFinished == false)
        {
            timeLeft += Time.deltaTime;

            if (timeLeft >= waveDelay)
            {
                timerDone = true;
                checkingTime = false;
                timeLeft = 0;
            }
        }

        if (waveNumber >= waves.Length)
        {
            gameFinished = true; //Stops the spawning system.
        }

        if (timerDone && grid.overallEnemyCount == 0 && waveNotActive && gameFinished == false)
        {
            waveNotActive = false;
            SpawnWave();

        }

    }
    //THIS IS FINE DONT CHANGE IT
    void SpawnWave()
    {
        Debug.Log("Wave:" + waveNumber);

        StartCoroutine(SpawnEnemy(waves[waveNumber].enemyCount));

        waveNotActive = true;
        checkingTime = true;
        timerDone = false;

        waveNumber++;
    }

    IEnumerator SpawnEnemy(int enemyCount) //Take the amount of enemies needed and spawn that amount, make sure to wait the right amount inbetween waves
    {
        for (int i = 0; i < enemyCount; i++)
        {
            RandomNumber(); //Pick the Lane number

            waves[waveNumber].timeBeforeSpawn = timeBeforeSpawnInitial; //This is needed as when WaitForSeconds is run once, it leaves the variable at 1 instead of what it started at.

            Instantiate(waves[waveNumber].basicEnemy, spawnPoints[randomNum].position, spawnPoints[randomNum].rotation);

            yield return new WaitForSeconds(waves[waveNumber].timeBeforeSpawn);            //So .1f would be a tenth of a second, and 1f would be one second
        }
    }

    //Handles the validation to check if certain row's are too overpopulated for example.
    void RandomNumber()
    {
        randomNum = Random.Range(0, spawnPoints.Length); //spawnPoints.Length atm is 3

        if (randomNum == 2 && grid.row3EnemyCount >= waves[waveNumber].rowOverflowCount)
        {
            randomNum = 0;
            Debug.Log(randomNum + " " + grid.row3EnemyCount + " " + waves[waveNumber].rowOverflowCount);
            return;
        }

        if (randomNum == 1 && grid.row2EnemyCount >= waves[waveNumber].rowOverflowCount)
        {
            randomNum = 2;
            Debug.Log(randomNum + " " + grid.row2EnemyCount + " " + waves[waveNumber].rowOverflowCount);
            return;
        }

        if (randomNum == 0 && (grid.row1EnemyCount >= waves[waveNumber].rowOverflowCount))
        {
            Debug.Log(randomNum + " " + grid.row1EnemyCount + " " + waves[waveNumber].rowOverflowCount);
            randomNum = 1;
            return;
        }

    }



 

}
