using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameObject WallFor2;
    private GameObject WallFor3;
    private bool open2 = false;
    private bool open3 = false;
    // Start is called before the first frame update
    private void Awake()
    {
        WallFor2 = GameObject.Find("WallFor2");
        WallFor3 = GameObject.Find("WallFor3");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(WallFor2);
            DontDestroyOnLoad(WallFor3);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "StageSelection")
        {
            if (!open2)
            {
                if (PlayerPrefs.GetInt("Stage") >= 1)
                {
                    Debug.Log("Open!");
                    open2 = true;
                    WallFor2.SetActive(false);
                }
            }

            if (!open3)
            {
                if (PlayerPrefs.GetInt("Stage") >= 2)
                {
                    open3 = true;
                    WallFor3.SetActive(false);
                }
            }
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
}
