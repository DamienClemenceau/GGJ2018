﻿using System;
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
    public float maxStamina;
    public float staminaUseByBlop;
    public LayerMask groundLayer;
    public GameObject miniBlopMarker;

    [HideInInspector]
    public float stamina;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;

    private Vector2 velocity;
    private Vector2 directionnalInput;
    private float velocitySmoothing;

    private bool wasGrounded = false;
    private bool isJumping = false;
    private float lastJumpTime, lastGroundedTime;

    private bool facingRight;
    private bool isRunning;
    private float lastBlopTime;
    private float lastStaminaIncrement;
    private bool isStaminaInfinite;

    public float lastTimeStartInfiniteStamina { get; private set; }

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

        stamina = maxStamina;
    }

    private void FixedUpdate()
    {
        float targetVelocityX = Input.GetAxisRaw("Horizontal") * speed;

        if (isRunning)
        {
            targetVelocityX *= runSpeed;
        }

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocitySmoothing, 0.01f);

        transform.Translate(velocity * Time.deltaTime);
    }

    void Update()
    {
        Flip();

        if (IsGrounded())
        {
            bool canRegenStamina = Time.time - lastStaminaIncrement > 0.25f;
            if (stamina < maxStamina && canRegenStamina)
            {
                lastStaminaIncrement = Time.time;
                stamina++;
            }

            isJumping = false;
            wasGrounded = true;
            lastGroundedTime = Time.time;

            if(Input.GetButton("Run"))
            {
                isRunning = true;
            } else
            {
                isRunning = false;
            }
        }
        else if (Time.time - lastGroundedTime > 0.25f)
        {
            wasGrounded = false;
            isJumping = false;
        }

        bool canJump = Time.time - lastJumpTime > 0.25f;
        bool canBlob = Time.time - lastBlopTime > 0.6f;

        if (Input.GetButtonDown("Jump") && wasGrounded && !isJumping && canJump)
        {
            isJumping = true;
            lastJumpTime = Time.time;

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.velocity += Vector2.up * jumpForce;
        }
        else if (Input.GetButtonDown("Jump") && !wasGrounded && (stamina >= staminaUseByBlop || isStaminaInfinite) && canBlob)
        {
            lastBlopTime = Time.time;
            if(!isStaminaInfinite)
                stamina -= staminaUseByBlop;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.velocity += Vector2.up * (jumpForce * 0.6f);
        }

        if(Time.time - lastTimeStartInfiniteStamina > 10.0f)
        {
            isStaminaInfinite = false;
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

    public void TemporaryInfiniteStamina()
    {
        isStaminaInfinite = true;
        lastTimeStartInfiniteStamina = Time.time;
    }
}
