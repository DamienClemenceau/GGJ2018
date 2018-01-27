using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    public GameObject follow;

	void Start () {
        if(follow == null)
        {
            follow = GameObject.FindGameObjectWithTag("Player");
        }
	}
	
	void Update () {
		if(follow != null)
        {
            transform.position = new Vector3(follow.transform.position.x, follow.transform.position.y, -10);
        }
	}
}
