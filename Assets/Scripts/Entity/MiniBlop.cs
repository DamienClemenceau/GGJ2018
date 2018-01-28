using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBlop : MonoBehaviour {
    public GameObject follow;
    public string color;

	void Awake () {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        switch(color)
        {
            case "Yellow":
                renderer.sprite = Resources.Load("Sprites/mini_blop_jaune", typeof(Sprite)) as Sprite;
                break;
            case "Green":
                renderer.sprite = Resources.Load("Sprites/mini_blop_vert", typeof(Sprite)) as Sprite;
                break;
            case "Purple":
                renderer.sprite = Resources.Load("Sprites/mini_blop_violet", typeof(Sprite)) as Sprite;
                break;
            default:
                renderer.sprite = Resources.Load("Sprites/mini_blop_bleu", typeof(Sprite)) as Sprite;
                break;
        }
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
            player.blopCollected++;
            follow = player.miniBlopMarker;
        }
    }
}
