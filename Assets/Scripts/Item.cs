using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{

    bool isPickUp;  // pick up ������ ��������
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

    void OnTriggerEnter(Collider col)   // ���迡 Capsule collider �����ϰ� ���⿡ �÷��̾ �ε��� ��� halo enable, pick up ������ ���·� ����
    {
        if (col.tag.Equals("Player"))
        {
            halo.enabled = true;
            isPickUp = true;
        }
    }

    void OnTriggerExit(Collider col)    // collider���� ��� ��� halo disable, pick up �Ұ����� ���·� ����
    {
        if (col.tag.Equals("Player"))
        {
            halo.enabled = false;
            isPickUp = false;
        }
    }


    void PickUp()
    {
        Destroy(gameObject);    // ���踦 pick up �� ��� ���踦 ȭ�鿡�� destroy

        inven.ItemGet();
    }

    public void setActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
