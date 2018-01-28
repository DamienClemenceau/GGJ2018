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
    public float maxStamina;
    public float staminaUseByBlop;
    public LayerMask groundLayer;
    public GameObject deathObject;

    [HideInInspector]
    public GameObject[] miniBlopMarkers;
    [HideInInspector]
    public float stamina = 0;
    [HideInInspector]
    public int blopCollected = 0;

    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;
    private SpriteRenderer spriteRenderer;
    private Animator _animator;

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
    private GameObject triggerObject;

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
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponentInChildren<Animator>();

        stamina = maxStamina;
        
        miniBlopMarkers = new GameObject[FindObjectsOfType<MiniBlop>().Length];

        for (int i = 0; i < miniBlopMarkers.Length; i++)
        {
            miniBlopMarkers[i] = new GameObject("MiniBlopMarker_" + i);
            miniBlopMarkers[i].transform.parent = transform;
            miniBlopMarkers[i].transform.localPosition = new Vector3(-2.5f + (-2 * i), -1.25f, 0);
        }
        
    }

    private void FixedUpdate()
    {
        float targetVelocityX = Input.GetAxisRaw("Horizontal") * speed;

        if (isRunning)
        {
            targetVelocityX *= runSpeed;
        }

        _animator.SetBool("isJumping", isJumping);

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocitySmoothing, 0.01f);

        transform.Translate(velocity * Time.deltaTime);
    }

    void Update()
    {
        if(Input.GetButton("Interact"))
        {
            Interact();
        }

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
            isJumping = true;

            lastBlopTime = Time.time;
            if(!isStaminaInfinite)
                stamina -= staminaUseByBlop;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.velocity += Vector2.up * (jumpForce * 0.75f);
        }

        if(Time.time - lastTimeStartInfiniteStamina > 10.0f)
        {
            isStaminaInfinite = false;
        }
    }

    private void Interact()
    {
        if(triggerObject != null)
        {
            JamInteraction jam = triggerObject.GetComponent<JamInteraction>();
            if(jam != null)
            {
                jam.BeginInteraction();
            }
        }
    }

    private bool IsGrounded()
    {
        Bounds bounds = _collider.bounds;

        Vector2 bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        Vector2 bottomRight = new Vector2(bounds.max.x, bounds.min.y);

        bool groundedLeft = Physics2D.Raycast(bottomLeft, Vector3.down, 0.15f, groundLayer);
        bool groundedRight = Physics2D.Raycast(bottomRight, Vector3.down, 0.15f, groundLayer);
        
        return groundedLeft || groundedRight;
    }

    public void TemporaryInfiniteStamina()
    {
        isStaminaInfinite = true;
        lastTimeStartInfiniteStamina = Time.time;
    }

    public void Death()
    {
        if(GameManager.instance != null)
            GameManager.instance.deathCount++;
        Instantiate(deathObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Trigger")
        {
            triggerObject = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == triggerObject)
        {
            triggerObject = null;
        }
    }
}
