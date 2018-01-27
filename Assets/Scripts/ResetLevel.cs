using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour {
    public float lifeTime;

    void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnDestroy()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
