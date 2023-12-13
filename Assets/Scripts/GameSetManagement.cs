using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetManagement : MonoBehaviour
{
    public GameObject stopperTree;
    private GameObject[] enemies;
    protected Rigidbody2D rb;
    private bool isMoving = true; //flag

    public GameObject firstBg;
    public GameObject secondBg;

    bool isfirstBgRendered = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Find all game objects with the "Enemy" tag at the start of the game
        enemies = GameObject.FindGameObjectsWithTag("Enemies");

        firstBg.SetActive(true);
        secondBg.SetActive(false);
    }

    void Update()
    {
        // Check if all enemies are defeated
        if (AreAllEnemiesDefeated() && stopperTree != null)
        {
            Debug.Log("all enemy down");
            // Call a method to handle the disappearance of the StopperTree
        
            if (isfirstBgRendered)
            {
                firstBg.SetActive(false);
                secondBg.SetActive(true);
            }
/*            else
            {
                firstBg.SetActive(true);
                secondBg.SetActive(false);
            }*/

            isfirstBgRendered = !isfirstBgRendered;

            Destroy(this.gameObject);
        }
    }

    bool AreAllEnemiesDefeated()
    {
        foreach (GameObject enemy in enemies)
        {
            // Check if any enemy is still active (not defeated)
            if (enemy != null && enemy.activeSelf)
            {
                return false; // At least one enemy is still active
            }
        }

        return true; // All enemies are defeated
    }
}