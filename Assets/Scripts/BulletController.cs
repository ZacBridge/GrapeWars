using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	//public Game_Manager sm;

	public AudioSource ass;

	public float moveSpeed;
	public int damage;

	void Start() {
		ass = GetComponent<AudioSource> ();

	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector2 (transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);

        if (transform.position.x >= 16)
            Dead();
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Enemy") {
            ass.clip = FindObjectOfType<Game_Manager>().audios[4];
			//ass.clip = sm.audios [4];
			ass.Play ();
			other.gameObject.GetComponent<Enemy_System> ().TakeDamage (damage);
			GetComponent<BoxCollider2D> ().isTrigger = true;
			Invoke ("Dead", 0.2f);
		}

    }

	void Dead(){

		Destroy (gameObject);
	}
}
