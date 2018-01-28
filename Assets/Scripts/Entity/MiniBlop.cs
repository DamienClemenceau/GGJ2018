using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBlop : MonoBehaviour {
    public GameObject follow;
    public string color;
    public float speed;
    
    void Awake () {
        SpriteRenderer[] renderer = GetComponentsInChildren<SpriteRenderer>();
        Object[] sprites = Resources.LoadAll("Sprites/mini" + color +  "_spritesheet"); 
        for (int i = 0; i < renderer.Length; i++)
        {
            renderer[i].sprite = (Sprite)sprites[i + 1];
        }
    }
	
	void Update () {
	    if(follow != null)
        {
            transform.position = Vector3.Lerp(transform.position, follow.transform.position, Time.deltaTime * speed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null && follow == null)
        {
            follow = player.miniBlopMarkers[player.blopCollected];
            player.blopCollected++;
        }
    }
}
