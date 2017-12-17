using UnityEngine;
using System.Collections;
using UnityEngine.UI;//Added to use UI Functions
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //public SoundManager sm;

    public AudioSource ass;

    public Canvas pauseMenu;
	public Canvas quitMenu;
	public Canvas mainMenu;
	public Button continueText;
	public Button exitText;
	public Button menuText;

	public static bool activePause = false;

	// Use this for initialization
	void Awake()
	{
		pauseMenu = GameObject.Find("Pause Menu").GetComponent<Canvas>();//Finds the Game object in the Scene
		pauseMenu.gameObject.SetActive(false);//Set the Game object to not be active
	}

	private void Start()
	{
		//sm = FindObjectOfType<SoundManager> ();
		ass = GetComponent<AudioSource> ();
		quitMenu = quitMenu.GetComponent<Canvas>();
		quitMenu.gameObject.SetActive (false);
		mainMenu.gameObject.SetActive (false);
		mainMenu = mainMenu.GetComponent<Canvas>();
		continueText = continueText.GetComponent<Button>();
		exitText = exitText.GetComponent<Button>();
	}

	// Update is called once per frame
	void Update()
	{
		Pause();
	}

	public void playAudio () {
        ass.clip = FindObjectOfType<Game_Manager>().audios[2];

        //      ass.clip = sm.audios [2];
        ass.Play();
    }

	public void Pause()
	{

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (activePause == false) //(pauseMenu.gameObject.activeInHierarchy == false)
			{
				pauseMenu.gameObject.SetActive(true);//Set the Game object to be active
				Time.timeScale = 0;
				activePause = true;
				quitMenu.gameObject.SetActive(false);//Set the Game object to be active
				mainMenu.gameObject.SetActive(false);//Set the Game object to be active
			}
			else if (activePause == true)
			{
				pauseMenu.gameObject.SetActive(false);//Set the Game object to not be active
				activePause = false;
				Time.timeScale = 1;
			}
		}
	}



	public void ExitPress() //this function will be used on our Exit button

	{
		quitMenu.gameObject.SetActive(true);
		continueText.gameObject.SetActive(true);
		exitText.gameObject.SetActive(true);

	}

	public void NoPress() //this function will be used for our "NO" button in our Quit Menu

	{
		quitMenu.gameObject.SetActive(false);


	}

	public void YesPress() //This function will be used on our "Yes" button in our Quit menu

	{
		Application.Quit(); //this will quit our game. Note this will only work after building the game

	}


	public void menuNoPress()
	{
		mainMenu.gameObject.SetActive(false);
	}

	public void menuPress()
	{
		mainMenu.gameObject.SetActive(true);
	}

	public void continuePress()
	{
		quitMenu.enabled = false;
		pauseMenu.gameObject.SetActive(false);//Set the Game object to not be active
		activePause = false;
		Time.timeScale = 1;
	}


	public void menuYesPress()
	{
		SceneManager.LoadScene("StartMenu");
		Time.timeScale = 1;
	}
}
