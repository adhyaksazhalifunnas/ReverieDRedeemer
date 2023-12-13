using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ChangeCrateProperties(1f); // Set crate mass to 1 when 'J' key is pressed and enable movement
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            ChangeCrateProperties(100f); // Revert crate mass to 100 when 'J' key is released and disable movement
        }
    }

    private void ChangeCrateProperties(float mass)
    {
        GameObject[] crates = GameObject.FindGameObjectsWithTag("Crate");

        foreach (GameObject crate in crates)
        {
            Rigidbody2D crateRB = crate.GetComponent<Rigidbody2D>();

            if (crateRB != null)
            {
                crateRB.mass = mass;

                if (mass == 100f)
                {
                    crateRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                }
                else
                {
                    crateRB.constraints = RigidbodyConstraints2D.None;
                }
            }
        }
    }
}