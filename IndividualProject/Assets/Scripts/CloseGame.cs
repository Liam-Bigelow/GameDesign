using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGame : MonoBehaviour
{
    public void exitGame()
    {
        Debug.Log("EXIT");
        Application.Quit();
    }
}
