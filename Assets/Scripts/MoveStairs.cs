using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MoveStairs : MonoBehaviour
{

    private bool is2D;
    public CinemachineVirtualCamera player2d_cam;
    private Vector3 initial;

    // Start is called before the first frame update
    void Start()
    {
        if (player2d_cam.Priority == 10)
            is2D = true;
        else is2D = false;
        initial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ViewChange();
        if (is2D) {
            transform.position = new Vector3(7, initial.y, initial.z);
        }
        else
        {
            transform.position = initial;
        }
    }

    public void ViewChange()
    {
        if (Input.GetButtonDown("ChangeView"))
        {
            is2D = !is2D;

        }
    }
}
