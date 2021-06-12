using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{

    bool isPickUp;  // pick up 가능한 상태인지
    private Behaviour halo;
    private Inventory inven;

    void Start()
    {
        halo = (Behaviour)GetComponent("Halo");
        halo.enabled = false;
        inven = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if (isPickUp && Input.GetKeyDown(KeyCode.E)) PickUp();
    }

    void OnTriggerEnter(Collider col)   // 열쇠에 Capsule collider 설정하고 여기에 플레이어가 부딪힐 경우 halo enable, pick up 가능한 상태로 설정
    {
        if (col.tag.Equals("Player"))
        {
            halo.enabled = true;
            isPickUp = true;
        }
    }

    void OnTriggerExit(Collider col)    // collider에서 벗어날 경우 halo disable, pick up 불가능한 상태로 설정
    {
        if (col.tag.Equals("Player"))
        {
            halo.enabled = false;
            isPickUp = false;
        }
    }


    void PickUp()
    {
        Destroy(gameObject);    // 열쇠를 pick up 할 경우 열쇠를 화면에서 destroy

        inven.ItemGet();
    }

    public void setActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
