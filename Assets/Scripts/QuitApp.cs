using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApp : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Quit"))
        {
            Debug.Log("quit");
            Application.Quit();
        }
    }
}
