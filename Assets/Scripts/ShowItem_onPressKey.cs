using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowItem_onPressKey : MonoBehaviour
{

    public Item Target;

    // Start is called before the first frame update
    void Start()
    {
        Target.setActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.G)) {
            Target.setActive(true);
        }
    }

   


}
