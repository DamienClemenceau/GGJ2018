using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Vector2 velocity;
    private Vector2 directionnalInput;
    private float velocitySmoothing;
    private float gravity = -5;

    public float speed;

    void Start () {
	}
	
	void Update () {
        float targetVelocityX = Input.GetAxisRaw("Horizontal") * speed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocitySmoothing, 0.01f);
     //   velocity.y += gravity * Time.deltaTime;

        transform.position += (Vector3)(velocity * Time.deltaTime);
    }
}
