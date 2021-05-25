using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject WallFor2;
    private bool open2 = false;
    private bool open3 = false;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        PlayerPrefs.SetInt("Stage", 0);
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
                    open2 = true;
                    WallFor2.SetActive(false);
                }
            }

            if (!open3)
            {
                if (PlayerPrefs.GetInt("Stage") >= 2)
                {
                    open3 = true;

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
