using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBubble : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if(player != null)
        {
            player.stamina = player.maxStamina;
            Destroy(gameObject);
        }
    }
}
