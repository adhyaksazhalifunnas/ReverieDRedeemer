using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchObjects : MonoBehaviour
{
    public GameObject firstBg;
    public GameObject secondBg;

    bool isfirstBgRendered = true;

    void Start()
    {
        // Ensure both objects are initially active
        firstBg.SetActive(true);
        secondBg.SetActive(false);
    }

    void Update()
    {
        // Check for key press
        if (Input.GetKeyDown(KeyCode.L))
        {
            // Toggle rendering of objects based on current state
            if (isfirstBgRendered)
            {
                firstBg.SetActive(false);
                secondBg.SetActive(true);
            }
            else
            {
                firstBg.SetActive(true);
                secondBg.SetActive(false);
            }

            isfirstBgRendered = !isfirstBgRendered;
        }
    }
}