using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowItem : MonoBehaviour
{

    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        Target.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.G)) {
            Target.SetActive(true);
        }

        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("Player"))
        {
            Target.SetActive(true);
        }
    }


}
