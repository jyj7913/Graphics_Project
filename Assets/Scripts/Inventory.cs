using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    private Text keyNum;
    private int n = 0;
    public int goal;
    public SphereCollider baseCollider;

    private void Start()
    {
        baseCollider.enabled = false;
        keyNum = GetComponent<Text>();
        if (SceneManager.GetActiveScene().name == "StageMaze") {
            keyNum.text = "Key: " + (n + PlayerPrefs.GetInt("keys")) + "/ 5";
        }
        else if (SceneManager.GetActiveScene().name == "Stage3")
        {
            keyNum.text = "Key: " + n + "/ " + (goal + 3);
        }
        else
            keyNum.text = "Key: " + n + "/ " + goal;
    }

    public void ItemGet()
    {
        n++;
        if (SceneManager.GetActiveScene().name == "StageMaze")
        {
            keyNum.text = "Key: " + (n + PlayerPrefs.GetInt("keys")) + "/ 5";
        }
        else if (SceneManager.GetActiveScene().name == "Stage3")
        {
            keyNum.text = "Key: " + n + "/ " + (goal + 3);
        }
        else
            keyNum.text = "Key: " + n + "/ " + goal;

        if (n == goal)
        {
            baseCollider.enabled = true;
        }
    }
}