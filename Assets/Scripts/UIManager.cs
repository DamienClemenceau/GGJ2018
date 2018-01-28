using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Player player;
    public Text curStamina, maxStamina;
    public Text deathCounter;
    public Text curCollect, maxCollect;
    public GameObject pause, message;

    private bool isPaused;
    private bool drawMessage = true;
    private float startTime;

    void Start () {
        startTime = Time.time;

        curStamina.text = player.stamina.ToString();
        maxStamina.text = player.maxStamina.ToString();
        
        curCollect.text = player.blopCollected.ToString();
        maxCollect.text = FindObjectsOfType<MiniBlop>().Length.ToString();

        if(GameManager.instance != null)
            deathCounter.text = GameManager.instance.deathCount.ToString();
    }

    private void LateUpdate()
    {
        curStamina.text = player.stamina.ToString();
        curCollect.text = player.blopCollected.ToString();

        if (GameManager.instance != null) { 
            deathCounter.text = GameManager.instance.deathCount.ToString();
        }

        if (isPaused)
        {
						if (Input.GetButtonDown("Back"))
						{
								BackToMenu();
						}
				}
        pause.SetActive(isPaused);
        message.SetActive(drawMessage);
    }

    private void Update()
    {
        if(Time.time - startTime > 3f)
        {
            drawMessage = false;
        }

        if (Input.GetButtonDown("Pause"))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

	public void BackToMenu()
	{
		isPaused = false;
		Time.timeScale = 1.0f;
		SceneManager.LoadScene(0);
	}

}
