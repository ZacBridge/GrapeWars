using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerWave : MonoBehaviour {
    /*
     *  THIS IS OLD AND DEPRECATED AND SHOULD TECHNICALLY WORK BUT DOESNT SO WE WILL LEAVE IT HERE AND PUT IT DOWN TO THE MYSTERY OF THE WORLD
     * */

	public enum SpawnState{SPAWNING, WAITING, COUNTING }; // Multiple states for spawning enemies

	[System.Serializable]
	public class Wave
	{
		public string name;	//Wave name
		public Transform enemy;	//Melee Class
		public int count;	//Count of melee enemies that will be spawned

//		public Transform enemyRanged;	//Ranged class
//		public int countRange;	//Count of ranged class which will be spawned

		public float rate;	//Rate of spawning
		
	}

	public Wave[] waves;	//How many waves there will be
	public int nextWave = 0;	//setting current wave to 0

	public Transform[] spawnPoints; // Adding spawnPoints for each row

	public float timeBetweenWaves; // delay for the waves
	public float waveCountDown;	// wave count down using real time clock

	public float searchCountdown; //search for enemies on scene.. if null it will start the next wave

	public SpawnState state = SpawnState.COUNTING; // Setting the state to counting the units

	public GridManager grid;

	void Start(){
		if (spawnPoints.Length == 0) {
			Debug.LogError ("No spawn points referenced lad");
		}
		grid = FindObjectOfType<GridManager> ();
		waveCountDown = timeBetweenWaves;

	}

	void Update(){
		if (state == SpawnState.WAITING) {
			// Check if enemies are still alive


			if (grid.overallEnemyCount == 0) {
				WaveCompleted();
			}else {
				return;
			}
		}

		if (waveCountDown <= 0) {
			if (state != SpawnState.SPAWNING) {
				//Start Spawning wave
				StartCoroutine (SpawnWave(waves[nextWave]));
			}
		} else {
			waveCountDown -= Time.deltaTime;
		}
	}

	void WaveCompleted(){
		Debug.Log ("Wave Completed!");

		state = SpawnState.COUNTING;
		waveCountDown = timeBetweenWaves;

		if (nextWave + 1 > waves.Length - 1) {
			nextWave = 0;
			Debug.Log ("All waves were complete restarting...");
			//End Scene heard if the game is completed
		} else {
			nextWave++;
		}
	}

	IEnumerator SpawnWave (Wave _wave){
		Debug.Log ("Spawning Wave: " + _wave.name);

		state = SpawnState.SPAWNING;

		for (int i = 0; i < _wave.count; i++){
			SpawnEnemy(_wave.enemy);
			yield return new WaitForSeconds (1f / _wave.rate);
		}

//		for (int r = 0; r < _wave.countRange; r++) {
//			SpawnRangedEnemy (_wave.enemyRanged);
//			yield return new WaitForSeconds (1f / _wave.rate);
//		}
//
		state = SpawnState.WAITING;

		yield break;
	}

	public int rand;

	void SpawnEnemy(Transform _enemy){
		Transform _sp;

		//Spawn Enemy
		Debug.Log("Spawning Enemy: " + _enemy.name);
//		int rand = Random.Range (0, spawnPoints.Length);

		rand = Random.Range (0, spawnPoints.Length);

		if (rand == 0 && grid.row1EnemyCount < 2) {
			_sp = spawnPoints [rand];
			Instantiate (_enemy, _sp.position, _sp.rotation);
		} 
		if (rand == 1 && grid.row2EnemyCount < 2) {
			_sp = spawnPoints [rand];
			Instantiate (_enemy, _sp.position, _sp.rotation);
		}
				
		if (rand == 2 && grid.row3EnemyCount < 2) {
			_sp = spawnPoints [rand];
			Instantiate (_enemy, _sp.position, _sp.rotation);
		}

		Debug.Log ("Spawned");
	}

//	void SpawnRangedEnemy(Transform _rangedEnemy){
//		//Spawn Enemy
//		Debug.Log("Spawning Enemy: " + _rangedEnemy.name);
//
//		Transform _sp = spawnPoints [Random.Range (0, spawnPoints.Length)];
//		Instantiate(_rangedEnemy, _sp.position, _sp.rotation);
//	}
}
