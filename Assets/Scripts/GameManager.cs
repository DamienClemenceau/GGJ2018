using UnityEngine;

public class GameManager : MonoBehaviour {

	/**
	* Attributes
	*/
	[HideInInspector]
	public static GameManager instance = null;

	[HideInInspector]
	public LogsManager logsManager;
	[HideInInspector]
	public AudioSource audioSource;

	// We define the currentLanguage variable
	private string currentLangage = "en";
    public int deathCount = 0;
    /**
	* Monobehavior methods
	*/
    // Use this for initialization
    void Awake()
	{

		// Check if there is another gamemanager if it is, I self-destruct
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}

		instance = this;

		DontDestroyOnLoad(this.gameObject);

		//Set Logs Manager
		logsManager = new LogsManager();

		// We get the language of the game in the playerpref if it exists and we load the language of the game 
		//if (PlayerPrefs.GetString("language") != null && PlayerPrefs.GetString("language") != "")
		//{
		//	currentLangage = PlayerPrefs.GetString("language");
		//}

		// Get component AudioSource
		audioSource = GetComponent<AudioSource>();

		// We load the prefs Array
		//this.GetComponent<DataManager>().GetParameters();

		// We load the language
		//GameManager.ChangeLanguage(currentLangage);

		// Loading next scene
		//SceneManager.LoadScene(1, LoadSceneMode.Single);
	}
}
