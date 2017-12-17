using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour {
	//public SoundManager sm;
	public AudioSource ass;

	public GameObject test;
	public RaycastHit2D hit;
	public RaycastHit2D hitForTower;

	//So variables arrays for each tile almost
	public GameObject[] row1;
	public GameObject[] row2;
	public GameObject[] row3;

	public GameObject[] towers;
	public GameObject towerSelected;
	public Game_Manager gm;


	public int row1EnemyCount, row2EnemyCount, row3EnemyCount, overallEnemyCount;

	//assign the collision detection for them
	void Start()
	{
		ass = GetComponent<AudioSource> ();
		//sm = FindObjectOfType<SoundManager> ();
		row1EnemyCount = 0;
		row2EnemyCount = 0;
		row3EnemyCount = 0;
		overallEnemyCount = 0;
		gm = FindObjectOfType<Game_Manager> ();

	}

	//depending which one you click saves the active click?

	void Update()
	{
		if (Input.GetMouseButtonDown (0)) { //Has the mouse clicked?
			ass.clip = FindObjectOfType<Game_Manager>().audios[2];
            ass.Play ();
            

            hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			hitForTower = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero, 1 << LayerMask.NameToLayer ("Grid"));
			//Find out what the click was
			//Debug.Log (hit.collider.gameObject.name);

			//So from here its what do you want to do with when you click and stuff.
			//I click tile, if I've select tower 1, then I want to instantiate tower 1 on that tile, so instantiate that tower position = tile position.
			if (hitForTower.collider.gameObject.name == "TowerEasyBuy" && gm.points >= 100) {
				towerSelected = towers [0];
				gm.DecreasePoints (100);
			}
			else if (hitForTower.collider.gameObject.name == "TowerMediumBuy" && gm.points >= 200) {
				towerSelected = towers [1];
				gm.DecreasePoints (200);
			}
			else if (hitForTower.collider.gameObject.name == "TowerHardBuy" && gm.points >= 300) {
				towerSelected = towers [2];
				gm.DecreasePoints (300);
			}

			//If I have a selected tile and I have a tower saved, im sorry for this
			if (towerSelected && hitForTower.collider.gameObject.tag == "FloorSquare" && hitForTower.collider.gameObject.GetComponent<Tile>().towerPlaced == false) 
			{
				hitForTower.collider.gameObject.GetComponent<Tile> ().towerPlaced = true;
                ass.clip = FindObjectOfType<Game_Manager>().audios[3];

                ass.Play ();

                GameObject newTower = Instantiate (towerSelected);
				hitForTower.collider.gameObject.GetComponent<Tile> ().tower = newTower;
				newTower.GetComponent<Player_System> ().gridObject = hitForTower.collider.gameObject;
				newTower.transform.position = hit.collider.gameObject.transform.position;

				//SAve the tower, and save the tile where a tower is. 
				// Find the tower with 0 health, and the tile set to true, then set that to false

				towerSelected = null;
			}


		}
		//Calculates overall enemy count
		overallEnemyCount = row1EnemyCount + row2EnemyCount + row3EnemyCount;
	}
}