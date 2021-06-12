using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
    Stage Selection Scene에서 이전 stage를 완수했을 경우에만 다음 stage로 갈 수 있게끔
    이전 stage 완수 여부에 따라 stage gate를 열고 닫는 script 
 */
public class StageLock : MonoBehaviour
{
    private bool open2 = false;
    private bool open3 = false;
    public SphereCollider baseCollider2;
    public SphereCollider baseCollider3;

    void Start()
    {
        baseCollider2.enabled = false;
        baseCollider3.enabled = false;
    }

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
                    baseCollider2.enabled = true;
                }
            }

            if (!open3)
            {
                if (PlayerPrefs.GetInt("Stage") >= 2)
                {
                    open3 = true;
                    baseCollider3.enabled = true;
                }
            }
        }
    }
}
