using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowItem_onTrigger : MonoBehaviour
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
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("Player"))
        {
            Target.setActive(true);
        }
    }


}
