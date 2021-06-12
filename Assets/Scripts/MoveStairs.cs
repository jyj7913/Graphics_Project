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
        if (player2d_cam.Priority == 10)    // 2D camera의 priority를 통해 2D 모드인지 아닌지 판단
            is2D = true;
        else is2D = false;
        initial = transform.position;   // 초기 계단의 위치값 저장
    }

    void Update()
    {
        ViewChange();
        if (is2D) {
            transform.position = new Vector3(7, initial.y, initial.z);  // 2D 모드일 경우 계단의 x 위치를 플레이어의 x 위치와 동일하게 설정 
        }
        else
        {
            transform.position = initial;   // 3D 모드일 경우 초기 위치값으로 되돌림
        }
    }

    public void ViewChange()
    {
        if (Input.GetButtonDown("ChangeView"))  // 'Q' 키를 누를 경우
        {
            is2D = !is2D;

        }
    }
}
