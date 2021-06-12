using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    private Text keyNum;
    private int n = 0;
    public int goal;    // �� Scene���� ȹ���ؾ��ϴ� ���� ��
    public SphereCollider baseCollider;     // �� �տ� ��ġ�� sphere collider

    private void Start()
    {
        baseCollider.enabled = false;   // ���� ���۽ÿ��� ȹ���� ���谡 0���̹Ƿ� ���� �� �� ���� -> collider ��Ȱ��ȭ
        keyNum = GetComponent<Text>();
        // Stage3�� ��� Stage3 Scene, StageMaze Scene, Stage2Final Scene���� �����ǹǷ� �� ȹ���ؾ��� ������� �� ������ ȹ���ؾ��� �����(goal)�� �ٸ�
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

    public void ItemGet()   // ���踦 ȹ���� ��� canvas�� ȹ���� ���� �� increase
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

        if (n == goal)  // goal��ŭ�� ���踦 ȹ������ ��� sphere collider enable
        {
            baseCollider.enabled = true;
        }
    }
}