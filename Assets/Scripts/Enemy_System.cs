
using UnityEngine;
using System.Collections;


public class Enemy_System : MonoBehaviour {

	//public SoundManager sm;

	public AudioSource ass;

	public GridManager grid;
	public Game_Manager gm;

	public float moveSpeed;
	public int maxHealth, currentHealth;
	public int damage;

	public bool attacked = false, reloading = false;

	public float reloadSpeed;
	public float reloadTemp;

	public Rigidbody2D rb2d;
	public Animator anim;
	public Player_System playerHealth;
	//---|Raycast|---\\
	public Transform startPoint, endPoint;
		
	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
		grid = FindObjectOfType<GridManager> ();
		gm = FindObjectOfType<Game_Manager> ();
		//sm = FindObjectOfType<SoundManager> ();
		ass = GetComponent<AudioSource> ();
		currentHealth = maxHealth;
		//Debug.Log (gameObject.name);
	}
	
	// Update is called once per frame
	void Update () {
		rb2d.velocity = new Vector2 (-moveSpeed * moveSpeed, 0);

		Debug.DrawLine(startPoint.position, endPoint.position, Color.yellow);
		RaycastHit2D hit = Physics2D.Linecast(startPoint.position, endPoint.position, 1 << LayerMask.NameToLayer ("Player") );

		if (hit.collider != null && !attacked) {
			playerHealth = FindObjectOfType<Player_System> ();
			Attack ();
		}

		if (attacked) {
			if (reloadTemp <= 0) {
				reloadTemp = reloadSpeed;
			} else {
				Reload ();
			}
		}

	}
		
	void Reload () {
		//Debug.Log ("Reload");
		reloadTemp -= Time.deltaTime;
		if (reloadTemp < 0) {
			anim.SetBool ("Attack", false);
			attacked = false;
		}
	}

	void Attack() {
		//Debug.Log ("Attack");
		playerHealth.TakeDamage (damage);
		anim.SetBool ("Attack", true);
        //ass.clip = sm.audios [0];

        ass.clip = FindObjectOfType<Game_Manager>().audios[0];
        ass.Play();
        attacked = true;
	}

	public void TakeDamage(int amount) {
		currentHealth -= amount;

		if (currentHealth <= 0) {
			Death ();
		}
	}

	void Death(){
		//If they are on row 1
		if (gameObject.transform.position.y > 1.5 ) {
			grid.row1EnemyCount--;
		} 
		//If they are on row 2
		else if (gameObject.transform.position.y < 1.5  && gameObject.transform.position.y > -1.5) {
			grid.row2EnemyCount--;
		} 
		//if they are on row 3
		else if (gameObject.transform.position.y < -1.5 ) {
			grid.row3EnemyCount--;
		}

		if (gameObject.name == "EnemyEasy(Clone)") {
			gm.IncreasePoints (50);
		}
		if (gameObject.name == "EnemyMedium(Clone)") {
			gm.IncreasePoints (100);
		}
		if (gameObject.name == "EnemyHard(Clone)") {
			gm.IncreasePoints (200);
		}
		anim.SetBool ("Death", true);
        ass.clip = FindObjectOfType<Game_Manager>().audios[6];
        ass.Play ();


        Invoke("Dead", 0.7f);

	}

//	void OnCollision2D(Collision2D other){
//		if (other.gameObject.tag == "Portal") {
//			gm.LoseLife (); 
//		}
//	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Portal") {
			gm.LoseLife ();
			ass.clip = FindObjectOfType<Game_Manager>().audios[7];
            ass.Play ();

            Invoke("Dead", 0.2f);
		}
	}

	void Dead(){
		
		Destroy (gameObject);
	}
}
