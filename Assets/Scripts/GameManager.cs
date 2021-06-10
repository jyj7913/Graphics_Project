using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void Stage1Clear()
    {
        PlayerPrefs.SetInt("Stage", 1);
    }
    public void Stage2Clear()
    {
        PlayerPrefs.SetInt("Stage", 2);
    }
    public void Stage3Clear()
    {
        PlayerPrefs.SetInt("Stage", 3);
    }
}
