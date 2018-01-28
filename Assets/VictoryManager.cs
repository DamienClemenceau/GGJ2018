using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour {
    
	void Update () {
		if(Input.GetButton("Interact") && Input.GetButton("Pause"))
        {
            SceneManager.LoadScene(0);
        }
	}
}
