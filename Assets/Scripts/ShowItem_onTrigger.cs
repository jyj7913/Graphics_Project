using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Stage1���� Ư�� ���Ϳ� �ε����� ������ ���踦 �����Ű�� ������ ���� script
 */


public class ShowItem_onTrigger : MonoBehaviour
{

    public Item Target; // ���� object

    void Start()
    {
        Target.setActive(false);
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)   // Target�� ������ collider�� �÷��̾ �ε��� ��� ���� object activate.
    {
        if (col.tag.Equals("Player"))
        {
            Target.setActive(true); 
        }
    }


}
