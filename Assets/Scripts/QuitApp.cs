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
            Application.Quit();
            PlayerPrefs.SetFloat("LastPosition", 0f);               // If press 0, set player's position at 'Stage Select Scene' to original position
        }
    }
}
