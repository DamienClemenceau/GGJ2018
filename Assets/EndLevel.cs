using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour {
    public string nextSceneName;
    private float startEnding;
    private bool ended;
    public GameObject marker;
    public ParticleSystem particle;
    public AudioClip audioCatch;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            Player p = other.GetComponent<Player>();
            ended = p.end = p.Completed(); // Hummm .....
            startEnding = Time.time;

            if(ended)
            {
                GameManager.instance.updateTimer = false;

                AudioSource audio = GetComponent<AudioSource>();
                audio.clip = audioCatch;
                audio.volume = GameManager.instance.audioVolume;
                audio.Play();

                particle.Play();
            }
        }
    }

    private void Start()
    {
        particle.Pause();
    }

    private void Update()
    {
        if(GameManager.instance.updateTimer)
        {
            GameManager.instance.timer += Time.time;
        }

        if (Time.time - startEnding > 3f && ended) { 
            SceneManager.LoadScene(nextSceneName);
        }
    }

}
