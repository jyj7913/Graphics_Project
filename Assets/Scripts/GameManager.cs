using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject WallFor2;
    private bool open2 = false;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Stage", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!open2)
        {
            if(PlayerPrefs.GetInt("Stage") >= 1)
            {
                open2 = true;
                WallFor2.SetActive(false);
            }
        }
    }

    public void Stage1Clear()
    {
        PlayerPrefs.SetInt("Stage", 1);
    }
}
