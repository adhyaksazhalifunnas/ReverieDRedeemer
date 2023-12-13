using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private enum State { idle, running, jumping }
    private State state = State.idle;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    private void Update()
    {
        float hDirection = Input.GetAxis("Horizontal");

        if (hDirection < 0 )
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
        }
    }
}
