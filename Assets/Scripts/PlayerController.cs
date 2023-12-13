using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;

    
    private enum State { idle, running, jumping, falling, hurt}
    private State state = State.idle;
    private float initialYPosition;
    private bool isFalling;
    private float fallDuration = 0.3f;
    private float currentFallTime;

    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private int cherries = 0;
    [SerializeField] private TextMeshProUGUI cherryText;
    [SerializeField] private float hurtForce = 5f;
    [SerializeField] private AudioSource cherry;
    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource tookDamage;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        initialYPosition = transform.position.y;
    }

    private void Update()
    {
        if (state != State.hurt)
        {
            Movement();
            CheckFalling();
        }
        AnimationState();
        anim.SetInteger("state", (int)state); //set animation
    }

    private void CheckFalling()
    {
        if (state == State.running || state == State.idle)
        {
            if (!coll.IsTouchingLayers(ground))
            {
                currentFallTime += Time.deltaTime;

                if (currentFallTime >= fallDuration)
                {
                    state = State.falling;
                    currentFallTime = 0f; // Reset the timer for the next use
                }
            }
            else
            {
                currentFallTime = 0f; // Reset the timer when grounded
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectables")
        {
            cherry.Play();
            Destroy(collision.gameObject);
            cherries += 1;
            cherryText.text = cherries.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemies")
        {

            Enemies enemies = other.gameObject.GetComponent<Enemies>();
            if (state == State.falling)
            {
                enemies.JumpedOn();
                //Destroy(other.gameObject);
                Jump();
            }
            else
            {
                state = State.hurt;
                tookDamage.Play();
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    //Enemy is right = damaged and bounce left
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);

                }
                else
                {
                    //Enemy is left = damaged and bounce right
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
            
        }
    }

    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if(rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if(coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.running;
        }

        else
        {
            state = State.idle;
        }
    }

    private void Movement()
    {
        float movement = Input.GetAxis("Horizontal");
        float jump = Input.GetAxis("Vertical");

        //Move left
        if (movement < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }

        //Move right
        else if (movement > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }

        else
        {
            //rb.velocity = new Vector2(0, rb.velocity.y);
        }

        //JUMPING
        if (jump > 0 && coll.IsTouchingLayers(ground))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
    }

    private void Footstep()
    {
        footstep.Play();
    }

    //public float moveSpeed = 5f;
    //public float jumpForce = 10f;

    //private Rigidbody2D rb;
    //private bool isGrounded;
    //private Transform groundCheck;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    //groundCheck = transform.Find("GroundCheck");
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    // Check if the player is on the ground
    //    isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Foreground"));

    //    // Handle player input
    //    float horizontalInput = Input.GetAxis("Horizontal");
    //    Vector2 moveDirection = new Vector2(horizontalInput, 0);
    //    rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

    //    // Jumping  
    //    if (isGrounded && Input.GetButtonDown("Jump"))
    //    {
    //        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    //    }
    //}


}
