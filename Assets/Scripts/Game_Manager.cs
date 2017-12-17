using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game_Manager : MonoBehaviour {

	public int points, life;
	public Text currency, lives;
    public AudioClip[] audios;

	// Use this for initialization
	void Start () {
		life = 10;
		lives.text = life.ToString();
		points = 300;
		currency.text = points.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		
		currency.text = points.ToString ();
		lives.text = life.ToString();

		if (life <= 0) {
			GameOver ();
		}
	}

	public void LoseLife(){
		//Debug.Log ("Life Lost");
		life--;

	}


	 void GameOver(){
		//Debug.Log ("Game Over");

	}

	public void IncreasePoints(int addAmount){
		points += addAmount;
	}

	public void DecreasePoints(int loseAmount){
		points -= loseAmount;
	}

}


