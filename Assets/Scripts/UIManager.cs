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
    public GameObject pause;

    private bool isPaused;

    void Start () {
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
            pause.SetActive(true);

            if (Input.GetButtonDown("Back"))
            {
                isPaused = false;
                Time.timeScale = 1.0f;
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            pause.SetActive(false);
        }
    }

    private void Update()
    {
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

}
