using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    public void OnClickButton()                             // Clicking HOME button, then return to 'Stage Select Scene'
    {
        SceneManager.LoadScene("StageSelection");
    }

    public void OnClickRestart()                            // Clicking RESTART button, then reset all variables in Scene Manager and load 'Stage Select Scene' again.
    {
        PlayerPrefs.SetInt("LastPosition", 0);
        PlayerPrefs.SetInt("Stage", 0);
        SceneManager.LoadScene("StageSelection");
    }
}
