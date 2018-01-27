using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    /**
	* Attributes
	*/
    [HideInInspector]
    public static Player instance = null;

    public float speed, runSpeed;
    public float jumpForce;
    public LayerMask groundLayer;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;

    private Vector2 velocity;
    private Vector2 directionnalInput;
    private float velocitySmoothing;

    private bool wasGrounded = false;
    private bool isJumping = false;
    private bool canJump = true;
    private float lastJumpTime, lastGroundedTime;

    private bool facingRight;
    private bool isRunning;

    /**
    * Accessors
    */


    /**
	* Monobehavior methods
	*/
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        float targetVelocityX = Input.GetAxisRaw("Horizontal") * speed;

        if(isRunning)
        {
            targetVelocityX *= runSpeed;
        }

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocitySmoothing, 0.01f);

        transform.Translate(velocity * Time.deltaTime);

        Flip();

        if (IsGrounded())
        {
            isJumping = false;
            wasGrounded = true;
            lastGroundedTime = Time.time;

            if(Input.GetButtonDown("Run"))
            {
                isRunning = true;
            } else
            {
                isRunning = false;
            }
        }

        if (Input.GetButtonDown("Jump") && wasGrounded && !isJumping && canJump)
        {
            isJumping = true;
            lastJumpTime = Time.time;

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(Vector2.up * jumpForce);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector3.down, 0.15f, groundLayer);
    }

    private void Flip()
    {
        if ((facingRight && velocity.x > 0) || (!facingRight && velocity.x < 0))
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            facingRight = !facingRight;
        }
    }
}
