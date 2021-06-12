using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
    Stage Selection Scene���� ���� stage�� �ϼ����� ��쿡�� ���� stage�� �� �� �ְԲ�
    ���� stage �ϼ� ���ο� ���� stage gate�� ���� �ݴ� script 
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
