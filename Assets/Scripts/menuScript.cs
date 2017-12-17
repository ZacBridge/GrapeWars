using UnityEngine;
using UnityEngine.UI;// we need this namespace in order to access UI elements within our script
using System.Collections;
using UnityEngine.SceneManagement; // neded in order to load scenes

public class menuScript : MonoBehaviour 
{

	//public SoundManager sm;

	public AudioSource ass;


	public Canvas StartMenu;
	public Canvas quitMenu;
    public Canvas instructionMenu;
	public Button startText;
	public Button exitText;
    public Button closeText;

	void Start ()

	{
		//sm = FindObjectOfType<SoundManager> ();
		ass = GetComponent<AudioSource> ();
		StartMenu = StartMenu.GetComponent<Canvas> ();
		quitMenu = quitMenu.GetComponent<Canvas>();
        instructionMenu = instructionMenu.GetComponent<Canvas>();
		startText = startText.GetComponent<Button>();
		exitText =  exitText.GetComponent<Button>();
		quitMenu.enabled = false;
        instructionMenu.enabled = false;

	}

	public void playAudio () {
        ass.clip = FindObjectOfType<Game_Manager>().audios[2];

        //      .clip = sm.audios [2];
        ass.Play();
    }

	public void ExitPress() //this function will be used on our Exit button

	{
		quitMenu.enabled = true; //enable the Quit menu when we click the Exit button
		startText.enabled = false; //then disable the Play and Exit buttons so they cannot be clicked
		exitText.enabled = false;

	}

	public void NoPress() //this function will be used for our "NO" button in our Quit Menu

	{
		quitMenu.enabled = false; //we'll disable the quit menu, meaning it won't be visible anymore
		startText.enabled = true; //enable the Play and Exit buttons again so they can be clicked
		exitText.enabled = true;

	}


    public void instructionClosePress()
    {
        instructionMenu.enabled = false; //we'll disable the instruction menu, meaning it won't be visible anymore
        startText.enabled = true; //enable the Play and Exit buttons again so they can be clicked
        exitText.enabled = true;
    }

    public void instructionPress()
    {
        quitMenu.enabled = false;
        startText.enabled = false;
        exitText.enabled = false;
        instructionMenu.enabled = true;
        closeText.enabled = true;

    }

	public void StartLevel () //this function will be used on our Play button

	{
		SceneManager.LoadScene("Main"); //this will load our first level from our build settings. "1" is the second scene in our game

	}

	public void ExitGame () //This function will be used on our "Yes" button in our Quit menu

	{
		Application.Quit(); //this will quit our game. Note this will only work after building the game

	}

}