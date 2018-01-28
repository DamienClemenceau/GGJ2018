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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            Player p = other.GetComponent<Player>();
            ended = p.end = p.Completed(); // Hummm .....
            startEnding = Time.time;

            if(ended)
            {
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
        if (Time.time - startEnding > 3f && ended) { 
            SceneManager.LoadScene(nextSceneName);
        }
    }

}
