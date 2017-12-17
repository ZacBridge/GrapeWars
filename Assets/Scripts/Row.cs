using UnityEngine;
using System.Collections;

public class Row : MonoBehaviour {

	public GridManager grid;

	void Start() {
		grid = FindObjectOfType<GridManager> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Enemy"){
			if (gameObject.name == "Row1") {
				grid.row1EnemyCount++;
			} else if (gameObject.name == "Row2") {
				grid.row2EnemyCount++;
			} else if (gameObject.name == "Row3") {
				grid.row3EnemyCount++;
			}
		}
	}

}
