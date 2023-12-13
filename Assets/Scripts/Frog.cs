using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemies
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumpLength = 3f;
    [SerializeField] private float jumpHeight = 6f;
    [SerializeField] private LayerMask ground;
    private Collider2D coll;

    private bool facingLeft = true;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Transition Jump to Fall
        if (anim.GetBool("Jumping"))
        {
            if (rb.velocity.y < .1)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);

            }
        }

        //Transition Fall to Jump

        if (coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);
        }
    }

    private void Move()
    {
        if (facingLeft)
        {
            //Test if beyond leftCap
            if (transform.position.x > leftCap)
            {
                //Sprite facing correct
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }

                //Check if ground
                if (coll.IsTouchingLayers())
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = false;
            }
        }
        else
        {
            //Test if beyond leftCap
            if (transform.position.x < rightCap)
            {
                //Sprite facing correct
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }

                //Check if ground
                if (coll.IsTouchingLayers())
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }




}