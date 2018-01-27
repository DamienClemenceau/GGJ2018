using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBlop : MonoBehaviour {
    public GameObject follow;

	void Start () {
       		
	}
	
	void Update () {
	    if(follow != null)
        {
            transform.position = Vector3.Lerp(transform.position, follow.transform.position, Time.deltaTime * 2);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            follow = player.miniBlopMarker;
        }
    }
}
