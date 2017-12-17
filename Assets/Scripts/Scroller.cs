using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Scroller : MonoBehaviour
{
	public GameObject splash;

	public float scrollSpeed = 250;

	void Awake() {
		Invoke ("disableSplash", 2.0f);
	}

	IEnumerator Start ()
	{
		
		yield return new WaitForSeconds(60f);
		SceneManager.LoadScene("Start_Menu");


	}

	void Update ()
	{

		Vector3 pos = transform.position;
		
		Vector3 localVectorUp = transform.TransformDirection(0,1,0);
		
		
		pos += localVectorUp * scrollSpeed * Time.deltaTime;
		transform.position = pos;

		if (Input.anyKeyDown)
		{
			SceneManager.LoadScene("Start_Menu");
		}


	}

	public void disableSplash() {
		splash.gameObject.SetActive(false);
	}
}