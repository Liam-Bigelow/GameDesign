using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitFeedback : MonoBehaviour
{
    private string message = "";
    
    // this will simply show to the user that it was a hit
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("HIT");
        message = "HIT!";
        
        // Remove bullet from scene
        Destroy( other );
    }

    private void OnGUI()
    {
        GUI.Box( new Rect(0, 0, 50, 40), message );
    }
}
