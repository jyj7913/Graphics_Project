using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * Stage 1���� 'G' key�� ���� ��� �������� �����ϴ� ������ ���� script
 */

public class ShowItem_onPressKey : MonoBehaviour
{
    private bool pressed = false;
    public Item Target;     // Ư�� Ű�� ������ �� Ȱ��ȭ��Ű���� �ϴ� ���� object

    void Start()
    {
        Target.setActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.G) && !pressed) {
            Target.setActive(true);
            pressed = true;
        }
    }

   


}
