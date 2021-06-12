using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * Stage 1에서 'G' key를 누를 경우 아이템이 등장하는 퍼즐을 위한 script
 */

public class ShowItem_onPressKey : MonoBehaviour
{
    private bool pressed = false;
    public Item Target;     // 특정 키를 눌렀을 때 활성화시키고자 하는 열쇠 object

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
