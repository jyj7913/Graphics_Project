using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Stage1에서 특정 몬스터와 부딪혔을 때에만 열쇠를 등장시키는 퍼즐을 위한 script
 */


public class ShowItem_onTrigger : MonoBehaviour
{

    public Item Target; // 열쇠 object

    void Start()
    {
        Target.setActive(false);
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)   // Target에 설정된 collider에 플레이어가 부딪힐 경우 열쇠 object activate.
    {
        if (col.tag.Equals("Player"))
        {
            Target.setActive(true); 
        }
    }


}
