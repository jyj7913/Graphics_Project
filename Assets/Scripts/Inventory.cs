using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    private Text keyNum;
    private int n = 0;
    public int goal;    // °¢ Scene¿¡¼­ È¹µæÇØ¾ßÇÏ´Â ¿­¼è ¼ö
    public SphereCollider baseCollider;     // ¹® ¾Õ¿¡ À§Ä¡ÇÑ sphere collider

    private void Start()
    {
        baseCollider.enabled = false;   // °ÔÀÓ ½ÃÀÛ½Ã¿¡´Â È¹µæÇÑ ¿­¼è°¡ 0°³ÀÌ¹Ç·Î ¹®À» ¿­ ¼ö ¾ø´Ù -> collider ºñÈ°¼ºÈ­
        keyNum = GetComponent<Text>();
        // Stage3ÀÇ °æ¿ì Stage3 Scene, StageMaze Scene, Stage2Final SceneÀ¸·Î ±¸¼ºµÇ¹Ç·Î ÃÑ È¹µæÇØ¾ßÇÒ ¿­¼è¼ö¿Í °¢ ¾À¿¡¼­ È¹µæÇØ¾ßÇÒ ¿­¼è¼ö(goal)°¡ ´Ù¸§
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

    public void ItemGet()   // ¿­¼è¸¦ È¹µæÇÒ °æ¿ì canvas¿¡ È¹µæÇÑ ¿­¼è ¼ö increase
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

        if (n == goal)  // goal¸¸Å­ÀÇ ¿­¼è¸¦ È¹µæÇßÀ» °æ¿ì sphere collider enable
        {
            baseCollider.enabled = true;
        }
    }
}