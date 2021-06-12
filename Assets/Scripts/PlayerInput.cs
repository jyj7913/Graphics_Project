using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private string verticalMove = "Vertical";
    private string horizontalMove = "Horizontal";
    private string changeDim = "ChangeView";

    public float move_vert { get; private set; }
    public float move_hori { get; private set; }
    public bool changeView { get; private set; }

    // Update is called once per frame
    void Update()                                               // Update Player inputs then Player can use them
    {
        move_vert = Input.GetAxis(verticalMove);
        move_hori = Input.GetAxis(horizontalMove);
        changeView = Input.GetButtonDown(changeDim);
    }
}
