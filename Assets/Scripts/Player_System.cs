using UnityEngine;
using System.Collections;

public class Player_System : MonoBehaviour {

	public GameObject bullet;
	public GameObject gunPosition;

	public float moveSpeed;
	public int maxHealth, currentHealth;
	public int damage;

	public bool attacked = false, reloading = false , dying = false;

	public float reloadSpeed;
	public float reloadTemp;

	public Rigidbody2D rb2d;
	public Animator anim;

	Enemy_System enemyHealth;

	public GameObject gridObject;


	//---|Raycast|---\\
	public Transform startPoint, endPoint;
	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		rb2d.velocity = new Vector2 (moveSpeed * moveSpeed, 0);

		Debug.DrawLine(startPoint.position, endPoint.position, Color.yellow);
		RaycastHit2D hit = Physics2D.Linecast(startPoint.position, endPoint.position, 1 << LayerMask.NameToLayer ("Enemy"));

		if (hit.collider != null && !attacked) {
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
		anim.SetBool ("Attack", false);
		if (reloadTemp < 0) {
			
			attacked = false;
		}
	}

	void Attack() {
		//Debug.Log ("Attack");
		if (!dying) {
//		enemyHealth.TakeDamage (damage);
		anim.SetBool ("Attack", true);
		Invoke ("NewBullet", 0.55f);
//		GameObject bulletShot = Instantiate(bullet);
//		bulletShot.transform.position = gunPosition.transform.position;
//		bulletShot.GetComponent<BulletController> ().damage = damage;

//		Debug.Log (bulletShot.GetComponent<BulletController> ().damage);


			attacked = true;
		}
	}

	void NewBullet(){
		GameObject bulletShot = Instantiate(bullet);
		bulletShot.transform.position = gunPosition.transform.position;
		bulletShot.GetComponent<BulletController> ().damage = damage;

		//Debug.Log (bulletShot.GetComponent<BulletController> ().damage);
	}

	public void TakeDamage(int amount) {
		currentHealth -= amount;

		if (currentHealth <= 0) {
			Death ();
		}
	}

	void Death(){

		gridObject.GetComponent<Tile> ().tower = null;
		gridObject.GetComponent<Tile> ().towerPlaced = false;

		anim.SetBool ("Death", true);
		dying = true;
		Invoke ("Dead", 2.14f);
	}

	void Dead(){

		Destroy (gameObject);
	}
}
