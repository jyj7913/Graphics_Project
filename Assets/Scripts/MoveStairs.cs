using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MoveStairs : MonoBehaviour
{

    private bool is2D;
    public CinemachineVirtualCamera player2d_cam;
    private Vector3 initial;

    void Start()
    {
        if (player2d_cam.Priority == 10)    // 2D camera�� priority�� ���� 2D ������� �ƴ��� �Ǵ�
            is2D = true;
        else is2D = false;
        initial = transform.position;   // �ʱ� ����� ��ġ�� ����
    }

    void Update()
    {
        ViewChange();
        if (is2D) {
            transform.position = new Vector3(7, initial.y, initial.z);  // 2D ����� ��� ����� x ��ġ�� �÷��̾��� x ��ġ�� �����ϰ� ���� 
        }
        else
        {
            transform.position = initial;   // 3D ����� ��� �ʱ� ��ġ������ �ǵ���
        }
    }

    public void ViewChange()
    {
        if (Input.GetButtonDown("ChangeView"))  // 'Q' Ű�� ���� ���
        {
            is2D = !is2D;

        }
    }
}
