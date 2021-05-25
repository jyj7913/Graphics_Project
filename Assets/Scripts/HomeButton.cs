using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickButton()
    {
        SceneManager.LoadScene("StageSelection");
    }

    public void OnClickRestart()
    {
        PlayerPrefs.SetInt("LastPosition", 0);
        PlayerPrefs.SetInt("Stage", 0);
        SceneManager.LoadScene("StageSelection");
    }
}